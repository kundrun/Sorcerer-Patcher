using Mutagen.Bethesda.Plugins;

internal partial class Patcher
{
    private static readonly IReadOnlyDictionary<FormKey, uint> SoulGemValues = new Dictionary<FormKey, uint>
    {
        { FormKeys.SLGM.Petty, 10 },
        { FormKeys.SLGM.PettyFilled, 40 },
        { FormKeys.SLGM.Lesser, 25 },
        { FormKeys.SLGM.LesserFilledPetty, 55 },
        { FormKeys.SLGM.LesserFilled, 80 },
        { FormKeys.SLGM.Common, 45 },
        { FormKeys.SLGM.CommonFilledPetty, 75 },
        { FormKeys.SLGM.CommonFilledLesser, 100 },
        { FormKeys.SLGM.CommonFilled, 135 },
        { FormKeys.SLGM.Greater, 75 },
        { FormKeys.SLGM.GreaterFilledPetty, 105 },
        { FormKeys.SLGM.GreaterFilledLesser, 130 },
        { FormKeys.SLGM.GreaterFilledCommon, 165 },
        { FormKeys.SLGM.GreaterFilled, 265 },
        { FormKeys.SLGM.Grand, 160 },
        { FormKeys.SLGM.GrandFilledPetty, 190 },
        { FormKeys.SLGM.GrandFilledLesser, 215 },
        { FormKeys.SLGM.GrandFilledCommon, 250 },
        { FormKeys.SLGM.GrandFilledGreater, 350 },
        { FormKeys.SLGM.GrandFilled, 400 },
        { FormKeys.SLGM.Black, 240 },
        { FormKeys.SLGM.BlackFilledPetty, 270 },
        { FormKeys.SLGM.BlackFilledLesser, 295 },
        { FormKeys.SLGM.BlackFilledCommon, 330 },
        { FormKeys.SLGM.BlackFilledGreater, 430 },
        { FormKeys.SLGM.BlackFilledWhite, 480 },
        { FormKeys.SLGM.BlackFilled, 600 }
    };
}
