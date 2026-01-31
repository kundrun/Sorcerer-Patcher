using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;

internal partial class Patcher
{
    private void PatchStaffEnchantmentRecords(IEnumerable<StaffInfo> staffInfoList)
    {
        Console.WriteLine("Processing staff enchantments.");

        foreach (var (staffEnchant, staffSkillLevel) in staffInfoList)
        {
            ObjectEffect? patchedStaffEnchant = null;

            var expectedEnchantCost = StaffEnchantCosts(staffSkillLevel);

            if (expectedEnchantCost != staffEnchant.EnchantmentAmount ||
                expectedEnchantCost != staffEnchant.EnchantmentCost)
            {
                patchedStaffEnchant ??= _state.PatchMod.ObjectEffects.GetOrAddAsOverride(staffEnchant);
                patchedStaffEnchant.EnchantmentAmount = expectedEnchantCost;
                patchedStaffEnchant.EnchantmentCost = expectedEnchantCost;
            }

            var markerEffectKey = StaffEnchantMarkerEffects(staffEnchant);

            if (!markerEffectKey.IsNull &&
                staffEnchant.Effects.All(x => x.BaseEffect.FormKey != markerEffectKey))
            {
                patchedStaffEnchant ??= _state.PatchMod.ObjectEffects.GetOrAddAsOverride(staffEnchant);
                patchedStaffEnchant.Effects.Add(new Effect
                    {
                        BaseEffect = markerEffectKey.ToNullableLink<IMagicEffectGetter>(),
                        Data = new EffectData
                        {
                            Magnitude = 0.0f,
                            Duration = 0,
                            Area = 0
                        }
                    }
                );
            }

            if (patchedStaffEnchant != null)
            {
                Console.WriteLine($">>> Patched staff enchantment {staffEnchant.EditorID}.");
            }
        }
    }
}
