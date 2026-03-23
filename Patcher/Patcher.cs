using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;

internal partial class Patcher(IPatcherState<ISkyrimMod, ISkyrimModGetter> _state, UserConfig _config)
{
    internal void Run()
    {
        PatchSoulGemRecords();

        PatchStaffRecords(out var staffInfoList, out var staffSkillLevels);
        PatchStaffEnchantmentRecords(staffInfoList);
        PatchStaffRecipeRecords(staffSkillLevels);

        PatchScrollRecords(out var scrollInfoLookup);
        PatchScrollRecipeRecords(scrollInfoLookup);
    }
}
