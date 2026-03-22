using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

internal class UserConfig
{
    [MaintainOrder]
    [Tooltip("Clear the list to patch all mods.")]
    public List<ModKey> ModsToPatch { get; set; } = [];

    [MaintainOrder]
    [Tooltip("If you find an unwanted staff in your patch, add it here to ignore it.")]
    public List<IFormLinkGetter<IWeaponGetter>> ExcludedStaves { get; set; } = [];

    [MaintainOrder]
    [Tooltip("If you find an unwanted scroll in your patch, add it here to ignore it.")]
    public List<IFormLinkGetter<IScrollGetter>> ExcludedScrolls { get; set; } = [];
}
