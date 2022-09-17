using BepInEx.Configuration;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace SylmarDev.SpireItems
{
    public class SpireConfig
    {
        public static ConfigEntry<bool> enableAkabeko;
        public static ConfigEntry<bool> enableBagOfMarbles;
        public static ConfigEntry<bool> enableBloodVial;
        public static ConfigEntry<bool> enableBoot;
        public static ConfigEntry<bool> enableBronzeScales;
        public static ConfigEntry<bool> enableCeramicFish;
        public static ConfigEntry<bool> enableJuzuBracelet;
        public static ConfigEntry<bool> enableMawBank;
        public static ConfigEntry<bool> enableMealTicket;
        public static ConfigEntry<bool> enableOddlySmoothStone;
        public static ConfigEntry<bool> enableOrichalcum;
        public static ConfigEntry<bool> enablePenNib;
        public static ConfigEntry<bool> enableRedMask;
        public static ConfigEntry<bool> enableStrawberry;
        public static ConfigEntry<bool> enableToyOrnithopter;
        public static ConfigEntry<bool> enableVajra;
        public static ConfigEntry<bool> enableDamaru;
        public static ConfigEntry<bool> enableDreamCatcher;
        public static ConfigEntry<bool> enableHappyFlower;
        public static ConfigEntry<bool> enablePerservedInsect;
        public static ConfigEntry<bool> enableRedSkull;
        public static ConfigEntry<bool> enableSmilingMask;
        public static ConfigEntry<bool> enableWarPaint;
        public static ConfigEntry<bool> enableWhetstone;

        public static ConfigEntry<bool> enableGoldenIdol;
        public static ConfigEntry<bool> enableBloodIdol;
        public static ConfigEntry<bool> enablePear;
        public static ConfigEntry<bool> enableClockworkSouvenir;
        public static ConfigEntry<bool> enableOrangePellets;
        public static ConfigEntry<bool> enableSlingOfCourage;
        public static ConfigEntry<bool> enableNeowsLament;
        public static ConfigEntry<bool> enableDarkstonePeriapt;
        public static ConfigEntry<bool> enableGremlinHorn;
        public static ConfigEntry<bool> enableMutagenicStrength;

        public static ConfigEntry<bool> enableStrangeSpoon;
        public static ConfigEntry<bool> enableNecronomicon;
        public static ConfigEntry<bool> enableFaceOfCleric;
        public static ConfigEntry<bool> enableDuVuDoll;
        public static ConfigEntry<bool> enableTungstenRod;
        public static ConfigEntry<bool> enableCalipers;
        public static ConfigEntry<bool> enableFossilizedHelix;

        public static ConfigEntry<bool> enableCoffeeDripper;

        public void Init(string configPath)
        {
            var config = new ConfigFile(Path.Combine(configPath, SpireItems.PluginGUID + ".cfg"), true);

            enableAkabeko = config.Bind("General.Toggles", "Enable Akabeko", true, "Set to true to enable Akabeko.");
            enableBagOfMarbles = config.Bind("General.Toggles", "Enable Bag Of Marbles", true, "Set to true to enable Bag Of Marbles.");
            enableBloodVial = config.Bind("General.Toggles", "Enable Blood Vial", true, "Set to true to enable Blood Vial.");
            enableBoot = config.Bind("General.Toggles", "Enable Boot", true, "Set to true to enable Boot.");
            enableBronzeScales = config.Bind("General.Toggles", "Enable Bronze Scales", true, "Set to true to enable Bronze Scales.");
            enableCeramicFish = config.Bind("General.Toggles", "Enable Ceramic Fish", true, "Set to true to enable Ceramic Fish.");
            enableJuzuBracelet = config.Bind("General.Toggles", "Enable Juzu Bracelet", true, "Set to true to enable Juzu Bracelet.");
            enableMawBank = config.Bind("General.Toggles", "Enable Maw Bank", true, "Set to true to enable Maw Bank.");
            enableMealTicket = config.Bind("General.Toggles", "Enable Meal Ticket", true, "Set to true to enable Meal Ticket.");
            enableOddlySmoothStone = config.Bind("General.Toggles", "Enable Oddly Smooth Stone", true, "Set to true to enable Oddly Smooth Stone.");
            enableOrichalcum = config.Bind("General.Toggles", "Enable Orichalcum", true, "Set to true to enable Orichalcum.");
            enablePenNib = config.Bind("General.Toggles", "Enable Pen Nib", true, "Set to true to enable Pen Nib.");
            enableRedMask = config.Bind("General.Toggles", "Enable Red Mask", true, "Set to true to enable Red Mask.");
            enableStrawberry = config.Bind("General.Toggles", "Enable Strawberry", true, "Set to true to enable Strawberry.");
            enableToyOrnithopter = config.Bind("General.Toggles", "Enable Toy Ornithopter", true, "Set to true to enable Toy Ornithopter.");
            enableVajra = config.Bind("General.Toggles", "Enable Vajra", true, "Set to true to enable Vajra.");
            enableDamaru = config.Bind("General.Toggles", "Enable Damaru", true, "Set to true to enable Damaru.");
            enableDreamCatcher = config.Bind("General.Toggles", "Enable Dream Catcher", true, "Set to true to enable Dream Catcher.");
            enableHappyFlower = config.Bind("General.Toggles", "Enable Happy Flower", true, "Set to true to enable Happy Flower.");
            enablePerservedInsect = config.Bind("General.Toggles", "Enable Perserved Insect", true, "Set to true to enable Perserved Insect.");
            enableRedSkull = config.Bind("General.Toggles", "Enable Red Skull", true, "Set to true to enable Red Skull.");
            enableSmilingMask = config.Bind("General.Toggles", "Enable Smiling Mask", true, "Set to true to enable Smiling Mask.");
            enableWarPaint = config.Bind("General.Toggles", "Enable War Paint", true, "Set to true to enable War Paint.");
            enableWhetstone = config.Bind("General.Toggles", "Enable Whetstone", true, "Set to true to enable Whetstone.");

            enableGoldenIdol = config.Bind("General.Toggles", "Enable Golden Idol", true, "Set to true to enable Golden Idol.");
            enableBloodIdol = config.Bind("General.Toggles", "Enable Blood Idol", true, "Set to true to enable Blood Idol.");
            enablePear = config.Bind("General.Toggles", "Enable Pear", true, "Set to true to enable Pear.");
            enableClockworkSouvenir = config.Bind("General.Toggles", "Enable Clockwork Souvenir", true, "Set to true to enable Clockwork Souvenir.");
            enableOrangePellets = config.Bind("General.Toggles", "Enable Orange Pellets", true, "Set to true to enable Orange Pellets.");
            enableSlingOfCourage = config.Bind("General.Toggles", "Enable Sling Of Courage", true, "Set to true to enable Sling Of Courage.");
            enableNeowsLament = config.Bind("General.Toggles", "Enable Neows Lament", true, "Set to true to enable Neows Lament.");
            enableDarkstonePeriapt = config.Bind("General.Toggles", "Enable Darkstone Periapt", true, "Set to true to enable Darkstone Periapt.");
            enableGremlinHorn = config.Bind("General.Toggles", "Enable Gremlin Horn", true, "Set to true to enable Gremlin Horn.");
            enableMutagenicStrength = config.Bind("General.Toggles", "Enable Mutagenic Strength", true, "Set to true to enable Mutagenic Strength.");

            enableStrangeSpoon = config.Bind("General.Toggles", "Enable Strange Spoon", true, "Set to true to enable Strange Spoon.");
            enableNecronomicon = config.Bind("General.Toggles", "Enable Necronomicon", true, "Set to true to enable Necronomicon.");
            enableFaceOfCleric = config.Bind("General.Toggles", "Enable Face Of Cleric", true, "Set to true to enable Face Of Cleric.");
            enableDuVuDoll = config.Bind("General.Toggles", "Enable Du-Vu Doll", true, "Set to true to enable Du-Vu Doll.");
            enableTungstenRod = config.Bind("General.Toggles", "Enable Tungsten Rod", true, "Set to true to enable Tungsten Rod.");
            enableCalipers = config.Bind("General.Toggles", "Enable Calipers", true, "Set to true to enable Calipers.");
            enableFossilizedHelix = config.Bind("General.Toggles", "Enable Fossilized Helix", true, "Set to true to enable Fossilized Helix.");

            enableCoffeeDripper = config.Bind("General.Toggles", "Enable Coffee Dripper", true, "Set to true to enable Coffee Dripper.");
        }

        public List<ConfigEntry<bool>> GetToggles() 
        {
            List<ConfigEntry<bool>> toggles = new List<ConfigEntry<bool>>();

            toggles.Add(enableAkabeko);
            toggles.Add(enableBagOfMarbles);
            toggles.Add(enableBloodVial);
            toggles.Add(enableBoot);
            toggles.Add(enableBronzeScales);
            toggles.Add(enableCeramicFish);
            toggles.Add(enableJuzuBracelet);
            toggles.Add(enableMawBank);
            toggles.Add(enableMealTicket);
            toggles.Add(enableOddlySmoothStone);
            toggles.Add(enableOrichalcum);
            toggles.Add(enablePenNib);
            toggles.Add(enableRedMask);
            toggles.Add(enableStrawberry);
            toggles.Add(enableToyOrnithopter);
            toggles.Add(enableVajra);
            toggles.Add(enableDamaru);
            toggles.Add(enableDreamCatcher);
            toggles.Add(enableHappyFlower);
            toggles.Add(enablePerservedInsect);
            toggles.Add(enableRedSkull);
            toggles.Add(enableSmilingMask);
            toggles.Add(enableWarPaint);
            toggles.Add(enableWhetstone);

            toggles.Add(enableGoldenIdol);
            toggles.Add(enableBloodIdol);
            toggles.Add(enablePear);
            toggles.Add(enableClockworkSouvenir);
            toggles.Add(enableOrangePellets);
            toggles.Add(enableSlingOfCourage);
            toggles.Add(enableNeowsLament);
            toggles.Add(enableDarkstonePeriapt);
            toggles.Add(enableGremlinHorn);
            toggles.Add(enableMutagenicStrength);

            toggles.Add(enableStrangeSpoon);
            toggles.Add(enableNecronomicon);
            toggles.Add(enableFaceOfCleric);
            toggles.Add(enableDuVuDoll);
            toggles.Add(enableTungstenRod);
            toggles.Add(enableCalipers);
            toggles.Add(enableFossilizedHelix);

            toggles.Add(enableCoffeeDripper);

            return toggles;
        }

    }
}
