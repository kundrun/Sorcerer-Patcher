using Mutagen.Bethesda.FormKeys.SkyrimSE;
using Mutagen.Bethesda.Plugins;

using static Utilities;

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

    private static readonly IReadOnlySet<ModKey> ExcludedStaffMods = new HashSet<ModKey>
    {
        Dragonborn.ModKey
    };

    private static readonly IReadOnlySet<FormKey> ExcludedStaffKeys = new HashSet<FormKey>
    {
        Skyrim.Weapon.MQ303DragonPriestStaff.FormKey,                  // Nahkriin's Dragon Priest Staff
        Skyrim.Weapon.dunForelhostDragonPriestStaff.FormKey,           // Rahgot's Dragon Priest Staff
        Skyrim.Weapon.dunBlindCliffStaffReward.FormKey,                // Eye of Melka
        Skyrim.Weapon.dunCrystalDriftCaveStaff.FormKey,                // Gadnor's Staff of Charming
        Skyrim.Weapon.dunHalldirsCairnHalldirsStaff.FormKey,           // Halldir's Staff
        Skyrim.Weapon.dunValthumeDragonPriestStaff.FormKey,            // Hevnoraak's Staff
        Skyrim.Weapon.DA14SanguineRose.FormKey,                        // Sanguine Rose
        Skyrim.Weapon.DA16SkullofCorruption.FormKey,                   // Skull of Corruption
        Skyrim.Weapon.dunMarkarthWizardSpiderControlStaff.FormKey,     // Spider Control Rod
        Skyrim.Weapon.dunMarkarthWizardSpiderControlStaffFake.FormKey, // Spider Control Rod
        Skyrim.Weapon.FavorNelacarStaffFear.FormKey,                   // Staff of Arcane Authority
        Skyrim.Weapon.dunDarklightSilviaStaff.FormKey,                 // Staff of Hag's Wrath
        Skyrim.Weapon.dunSaarthalStaffJyrikStaff.FormKey,              // Staff of Jyrik Gauldurson
        Skyrim.Weapon.MG07StaffofMagnus.FormKey,                       // Staff of Magnus
        Skyrim.Weapon.MGRArniel02Staff.FormKey,                        // Staff of Tandil
        Skyrim.Weapon.DA15Wabbajack.FormKey,                           // Wabbajack
        Skyrim.Weapon.dunBluePalaceWabbajack.FormKey,                  // Wabbajack
        Skyrim.Weapon.dunRannveigSildsStaff.FormKey,                   // Sild's Staff
        Dawnguard.Weapon.DLC1LD_AetherialStaff.FormKey,                // Aetherial Staff
        Dawnguard.Weapon.DLC1RuunvaldStaff.FormKey,                    // Staff of Ruunvald
        Dragonborn.Weapon.DLC2MiraakStaff.FormKey,                     // Miraak's Staff
        Dragonborn.Weapon.DLC2MKMiraakStaff1.FormKey,                  // Miraak's Staff
        Dragonborn.Weapon.DLC2MKMiraakStaff2.FormKey,                  // Miraak's Staff
        Dragonborn.Weapon.DLC2MKMiraakStaff3.FormKey,                  // Miraak's Staff
        Dragonborn.Weapon.DLC2MKMiraakStaffTentacles1.FormKey,         // Miraak's Staff
        MakeFormKey("ccbgssse040-advobgobs.esl",            0x000805), // Goblin Totem Staff
        MakeFormKey("ccbgssse067-daedinv.esm",              0x147D9F), // Staff of Ehlno Ede
        MakeFormKey("ccbgssse019-staffofsheogorath.esl",    0x000D62), // Staff of Sheogorath
        MakeFormKey("ECSS - Staff of Sheogorath Patch.esp", 0x000828), // Staff of Sheogorath
        MakeFormKey("BSHeartland.esm",                      0x086813), // Flamelight Spire
        MakeFormKey("BSHeartland.esm",                      0x070144), // Staff of Awesome Conflagration
        MakeFormKey("BSHeartland.esm",                      0x070558), // Rod of Potency
        MakeFormKey("BSHeartland.esm",                      0x0705F1), // Staff of Titan Summoning
        MakeFormKey("BSHeartland.esm",                      0x07062B), // Sceptre of Frosty Entombment
        MakeFormKey("BSHeartland.esm",                      0x0BF7E9), // Nelan Heroloth's Sheepstaff
        MakeFormKey("Wyrmstooth.esp",                       0x1CF784), // Dwarven Paralysis Rod
        MakeFormKey("Wyrmstooth.esp",                       0x3060D5), // Vulom's Staff
        MakeFormKey("Wyrmstooth.esp",                       0x78F3A2), // Staff of Malentis
        MakeFormKey("Wyrmstooth.esp",                       0x8F793C)  // Alka's Staff
    };

    private static ushort StaffEnchantAmounts(uint skillLevel) =>
        skillLevel switch
        {
            < 25  => 500,
            < 50  => 750,
            < 75  => 1500,
            < 100 => 3000,
            _     => 5000
        };
}
