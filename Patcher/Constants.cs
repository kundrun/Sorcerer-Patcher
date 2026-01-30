using Mutagen.Bethesda.Plugins;

internal static class ModKeys
{
    internal const string DefaultOutput = "Sorcerer-Patch.esp";

    internal static readonly ModKey Mysticism = ModKey.FromNameAndExtension("MysticismMagic.esp");
    internal static readonly ModKey Sorcerer  = ModKey.FromNameAndExtension("Sorcerer.esp");
}
