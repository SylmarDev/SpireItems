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

            enableAkabeko = config.Bind("Toggles", "enableAkabeko", true, "Set to true to enable Akabeko.");
            enableBagOfMarbles = config.Bind("Toggles", "enableBagOfMarbles", true, "Set to true to enable BagOfMarbles.");
            enableBloodVial = config.Bind("Toggles", "enableBloodVial", true, "Set to true to enable BloodVial.");
            enableBoot = config.Bind("Toggles", "enableBoot", true, "Set to true to enable Boot.");
            enableBronzeScales = config.Bind("Toggles", "enableBronzeScales", true, "Set to true to enable BronzeScales.");
            enableCeramicFish = config.Bind("Toggles", "enableCeramicFish", true, "Set to true to enable CeramicFish.");
            enableJuzuBracelet = config.Bind("Toggles", "enableJuzuBracelet", true, "Set to true to enable JuzuBracelet.");
            enableMawBank = config.Bind("Toggles", "enableMawBank", true, "Set to true to enable MawBank.");
            enableMealTicket = config.Bind("Toggles", "enableMealTicket", true, "Set to true to enable MealTicket.");
            enableOddlySmoothStone = config.Bind("Toggles", "enableOddlySmoothStone", true, "Set to true to enable OddlySmoothStone.");
            enableOrichalcum = config.Bind("Toggles", "enableOrichalcum", true, "Set to true to enable Orichalcum.");
            enablePenNib = config.Bind("Toggles", "enablePenNib", true, "Set to true to enable PenNib.");
            enableRedMask = config.Bind("Toggles", "enableRedMask", true, "Set to true to enable RedMask.");
            enableStrawberry = config.Bind("Toggles", "enableStrawberry", true, "Set to true to enable Strawberry.");
            enableToyOrnithopter = config.Bind("Toggles", "enableToyOrnithopter", true, "Set to true to enable ToyOrnithopter.");
            enableVajra = config.Bind("Toggles", "enableVajra", true, "Set to true to enable Vajra.");
            enableDamaru = config.Bind("Toggles", "enableDamaru", true, "Set to true to enable Damaru.");
            enableDreamCatcher = config.Bind("Toggles", "enableDreamCatcher", true, "Set to true to enable DreamCatcher.");
            enableHappyFlower = config.Bind("Toggles", "enableHappyFlower", true, "Set to true to enable HappyFlower.");
            enablePerservedInsect = config.Bind("Toggles", "enablePerservedInsect", true, "Set to true to enable PerservedInsect.");
            enableRedSkull = config.Bind("Toggles", "enableRedSkull", true, "Set to true to enable RedSkull.");
            enableSmilingMask = config.Bind("Toggles", "enableSmilingMask", true, "Set to true to enable SmilingMask.");
            enableWarPaint = config.Bind("Toggles", "enableWarPaint", true, "Set to true to enable WarPaint.");
            enableWhetstone = config.Bind("Toggles", "enableWhetstone", true, "Set to true to enable Whetstone.");

            enableGoldenIdol = config.Bind("Toggles", "enableGoldenIdol", true, "Set to true to enable GoldenIdol.");
            enableBloodIdol = config.Bind("Toggles", "enableBloodIdol", true, "Set to true to enable BloodIdol.");
            enablePear = config.Bind("Toggles", "enablePear", true, "Set to true to enable Pear.");
            enableClockworkSouvenir = config.Bind("Toggles", "enableClockworkSouvenir", true, "Set to true to enable ClockworkSouvenir.");
            enableOrangePellets = config.Bind("Toggles", "enableOrangePellets", true, "Set to true to enable OrangePellets.");
            enableSlingOfCourage = config.Bind("Toggles", "enableSlingOfCourage", true, "Set to true to enable SlingOfCourage.");
            enableNeowsLament = config.Bind("Toggles", "enableNeowsLament", true, "Set to true to enable NeowsLament.");
            enableDarkstonePeriapt = config.Bind("Toggles", "enableDarkstonePeriapt", true, "Set to true to enable DarkstonePeriapt.");
            enableGremlinHorn = config.Bind("Toggles", "enableGremlinHorn", true, "Set to true to enable GremlinHorn.");
            enableMutagenicStrength = config.Bind("Toggles", "enableMutagenicStrength", true, "Set to true to enable MutagenicStrength.");

            enableStrangeSpoon = config.Bind("Toggles", "enableStrangeSpoon", true, "Set to true to enable StrangeSpoon.");
            enableNecronomicon = config.Bind("Toggles", "enableNecronomicon", true, "Set to true to enable Necronomicon.");
            enableFaceOfCleric = config.Bind("Toggles", "enableFaceOfCleric", true, "Set to true to enable FaceOfCleric.");
            enableDuVuDoll = config.Bind("Toggles", "enableDuVuDoll", true, "Set to true to enable DuVuDoll.");
            enableTungstenRod = config.Bind("Toggles", "enableTungstenRod", true, "Set to true to enable TungstenRod.");
            //enableCalipers = config.Bind("Toggles", "enableCalipers", true, "Set to true to enable Calipers.");
            enableFossilizedHelix = config.Bind("Toggles", "enableFossilizedHelix", true, "Set to true to enable FossilizedHelix.");

            enableCoffeeDripper = config.Bind("Toggles", "enableCoffeeDripper", true, "Set to true to enable CoffeeDripper.");
        }
    }
}
