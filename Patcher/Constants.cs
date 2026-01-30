using Mutagen.Bethesda;
using Mutagen.Bethesda.FormKeys.SkyrimSE;
using Mutagen.Bethesda.Plugins;

internal static class ModKeys
{
    internal const string DefaultOutput = "Sorcerer-Patch.esp";

    internal static readonly ModKey Mysticism = ModKey.FromNameAndExtension("MysticismMagic.esp");
    internal static readonly ModKey Sorcerer  = ModKey.FromNameAndExtension("Sorcerer.esp");

    internal static readonly ModKey YASTM = ModKey.FromNameAndExtension("YASTM.esp");
}

internal static class FormKeys
{
    internal static class SLGM
    {
        internal static readonly FormKey Petty               = Skyrim.SoulGem.SoulGemPetty.FormKey;
        internal static readonly FormKey PettyFilled         = Skyrim.SoulGem.SoulGemPettyFilled.FormKey;
        internal static readonly FormKey Lesser              = Skyrim.SoulGem.SoulGemLesser.FormKey;
        internal static readonly FormKey LesserFilledPetty   = ModKeys.YASTM.MakeFormKey(0x000820);
        internal static readonly FormKey LesserFilled        = Skyrim.SoulGem.SoulGemLesserFilled.FormKey;
        internal static readonly FormKey Common              = Skyrim.SoulGem.SoulGemCommon.FormKey;
        internal static readonly FormKey CommonFilledPetty   = ModKeys.YASTM.MakeFormKey(0x000821);
        internal static readonly FormKey CommonFilledLesser  = ModKeys.YASTM.MakeFormKey(0x000822);
        internal static readonly FormKey CommonFilled        = Skyrim.SoulGem.SoulGemCommonFilled.FormKey;
        internal static readonly FormKey Greater             = Skyrim.SoulGem.SoulGemGreater.FormKey;
        internal static readonly FormKey GreaterFilledPetty  = ModKeys.YASTM.MakeFormKey(0x000823);
        internal static readonly FormKey GreaterFilledLesser = ModKeys.YASTM.MakeFormKey(0x000824);
        internal static readonly FormKey GreaterFilledCommon = ModKeys.YASTM.MakeFormKey(0x000825);
        internal static readonly FormKey GreaterFilled       = Skyrim.SoulGem.SoulGemGreaterFilled.FormKey;
        internal static readonly FormKey Grand               = Skyrim.SoulGem.SoulGemGrand.FormKey;
        internal static readonly FormKey GrandFilledPetty    = ModKeys.YASTM.MakeFormKey(0x000826);
        internal static readonly FormKey GrandFilledLesser   = ModKeys.YASTM.MakeFormKey(0x000827);
        internal static readonly FormKey GrandFilledCommon   = ModKeys.YASTM.MakeFormKey(0x000828);
        internal static readonly FormKey GrandFilledGreater  = ModKeys.YASTM.MakeFormKey(0x000829);
        internal static readonly FormKey GrandFilled         = Skyrim.SoulGem.SoulGemGrandFilled.FormKey;
        internal static readonly FormKey Black               = Skyrim.SoulGem.SoulGemBlack.FormKey;
        internal static readonly FormKey BlackFilledPetty    = ModKeys.YASTM.MakeFormKey(0x00082A);
        internal static readonly FormKey BlackFilledLesser   = ModKeys.YASTM.MakeFormKey(0x00082B);
        internal static readonly FormKey BlackFilledCommon   = ModKeys.YASTM.MakeFormKey(0x00082C);
        internal static readonly FormKey BlackFilledGreater  = ModKeys.YASTM.MakeFormKey(0x00082D);
        internal static readonly FormKey BlackFilledWhite    = ModKeys.YASTM.MakeFormKey(0x000838);
        internal static readonly FormKey BlackFilled         = Skyrim.SoulGem.SoulGemBlackFilled.FormKey;
    }
}
