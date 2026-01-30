using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

internal partial class Patcher
{
    private void PatchStaffRecords()
    {
        Console.WriteLine("Processing staves.");

        var staffEnchantPrimaryEffects = new Dictionary<FormKey, IMagicEffectGetter>();

        var staves = _state.LoadOrder.PriorityOrder.Weapon().WinningOverrides()
            .Where(x => !ExcludedStaffMods.Contains(x.FormKey.ModKey))
            .Where(x => !ExcludedStaffKeys.Contains(x.FormKey))
            .Where(x => x.EditorID is { } editorId &&
                        !editorId.StartsWith("MAG_") &&
                        !editorId.Contains("Template"))
            .Where(x => !x.MajorFlags.HasFlag(Weapon.MajorFlag.NonPlayable))
            .Where(x => x.HasKeyword(FormKeys.KYWD.WeapTypeStaff))
            .Where(x => !x.HasAnyKeyword([
                FormKeys.KYWD.MagicDisallowEnchanting,
                FormKeys.KYWD.DaedricArtifact
            ]));

        foreach (var staff in staves)
        {
            if (!staffEnchantPrimaryEffects.TryGetValue(staff.ObjectEffect.FormKey, out var primaryEffect))
            {
                primaryEffect = GetPrimaryEffect(staff.ObjectEffect.FormKey);
                if (primaryEffect == null)
                {
                    continue;
                }
                staffEnchantPrimaryEffects.Add(staff.ObjectEffect.FormKey, primaryEffect);
            }

            Weapon? patchedStaff = null;

            var expectedEnchantAmount = StaffEnchantAmounts(primaryEffect.MinimumSkillLevel);

            if (expectedEnchantAmount != staff.EnchantmentAmount)
            {
                patchedStaff ??= _state.PatchMod.Weapons.GetOrAddAsOverride(staff);
                patchedStaff.EnchantmentAmount = expectedEnchantAmount;
            }

            if (patchedStaff != null)
            {
                Console.WriteLine($">>> Patched staff {staff.EditorID}.");
            }
        }
    }

    private IMagicEffectGetter? GetPrimaryEffect(FormKey staffEnchantKey)
    {
        var staffEnchant = TryResolve<IObjectEffectGetter>(staffEnchantKey);

        return staffEnchant?.Effects
            .Select(x => TryResolve(x.BaseEffect))
            .MaxBy(x => x?.BaseCost);
    }
}
