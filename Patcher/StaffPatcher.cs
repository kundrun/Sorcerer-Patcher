using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

internal partial class Patcher
{
    private void PatchStaffRecords(out IEnumerable<StaffInfo> staffInfoList)
    {
        Console.WriteLine("Processing staves.");

        var staffEnchantInfoLookup = new Dictionary<FormKey, StaffInfo>();

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
            if (!staffEnchantInfoLookup.TryGetValue(staff.ObjectEffect.FormKey, out var staffInfo))
            {
                staffInfo = GetStaffInfo(staff.ObjectEffect.FormKey);
                if (staffInfo == null)
                {
                    continue;
                }
                staffEnchantInfoLookup.Add(staff.ObjectEffect.FormKey, staffInfo);
            }

            Weapon? patchedStaff = null;

            var expectedEnchantAmount = StaffEnchantAmounts(staffInfo.SkillLevel);

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

        staffInfoList = staffEnchantInfoLookup.Values.ToArray();
    }

    private StaffInfo? GetStaffInfo(FormKey staffEnchantKey)
    {
        var staffEnchant = TryResolve<IObjectEffectGetter>(staffEnchantKey);

        var primaryEffect = staffEnchant?.Effects
            .Select(x => TryResolve(x.BaseEffect))
            .MaxBy(x => x?.BaseCost);

        return primaryEffect != null
            ? new StaffInfo(staffEnchant!, primaryEffect.MinimumSkillLevel)
            : null;
    }

    private record StaffInfo(IObjectEffectGetter Enchant, uint SkillLevel);
}
