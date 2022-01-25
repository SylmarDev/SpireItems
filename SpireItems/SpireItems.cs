using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;
using BepInEx.Configuration;
using System;
using System.Reflection;

using Path = System.IO.Path;

namespace SylmarDev.SpireItems
{
	//This is an example plugin that can be put in BepInEx/plugins/ExamplePlugin/ExamplePlugin.dll to test out.
    //It's a small plugin that adds a relatively simple item to the game, and gives you that item whenever you press F2.

    //This attribute specifies that we have a dependency on R2API, as we're using it to add our item to the game.
    //You don't need this if you're not using R2API in your plugin, it's just to tell BepInEx to initialize R2API before this plugin so it's safe to use R2API.
    [BepInDependency(R2API.R2API.PluginGUID)]
	
	//This attribute is required, and lists metadata for your plugin.
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	
	//We will be using 3 modules from R2API: ItemAPI to add our item, ItemDropAPI to have our item drop ingame, and LanguageAPI to add our language tokens.
    [R2APISubmoduleDependency(nameof(ItemAPI), nameof(ItemDropAPI), nameof(LanguageAPI), nameof(BuffAPI))]
    //[R2APISubmoduleDependency(nameof(BuffAPI)]

    //This is the main declaration of our plugin class. BepInEx searches for all classes inheriting from BaseUnityPlugin to initialize on startup.
    //BaseUnityPlugin itself inherits from MonoBehaviour, so you can use this as a reference for what you can declare and use in your plugin class: https://docs.unity3d.com/ScriptReference/MonoBehaviour.html

    public class SpireItems : BaseUnityPlugin
	{
        //The Plugin GUID should be a unique ID for this plugin, which is human readable (as it is used in places like the config).
        //If we see this PluginGUID as it is on thunderstore, we will deprecate this mod. Change the PluginAuthor and the PluginName !
        public const string PluginGUID = "SylmarDev.SpireItems";
        public const string PluginAuthor = "SylmarDev";
        public const string PluginName = "Slay The Spire Relics";
        public const string PluginVersion = "0.0.1";

        // assets
        public static AssetBundle resources;

        // config file
        private static ConfigFile cfgFile;

        // declare items
        private static Akabeko akabeko = new Akabeko();
        private static Anchor anchor = new Anchor();
        private static BagOfMarbles marbles = new BagOfMarbles();
        private static BloodVial vial = new BloodVial();
        private static Boot boot = new Boot();
        private static BronzeScales scales = new BronzeScales();
        private static CeramicFish fish = new CeramicFish();
        private static JuzuBracelet juzu = new JuzuBracelet();

        public static DamageInfo thornDi = new DamageInfo();

        // any empty methods for BuffDefs need to go here to be edited later
        /*public static BuffDef freezeBuff { get; private set; }
        public static BuffDef fearBuff { get; private set; }
        */
        public static Vulnerable vulnerableBuff = new Vulnerable();

        //The Awake() method is run at the very start when the game is initialized.
        public void Awake()
        {
            //Init our logging class so that we can properly log for debugging
            Log.Init(Logger);

            // load assets (fingers crossed)
            Log.LogInfo("Loading Resources. . .");
            using(var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SpireItems.spireitems_assets"))
            {
                resources = AssetBundle.LoadFromStream(stream);
            }

            Log.LogInfo("Loading Items. . .");
            akabeko.Init();
            anchor.Init();
            marbles.Init();
            vial.Init();
            boot.Init();
            scales.Init();
            fish.Init();
            juzu.Init();

            Log.LogInfo("Loading Thorn di. . .");
            thornDi.inflictor = null;
            thornDi.damageType = (DamageType.BypassArmor | DamageType.Silent);
            thornDi.damageColorIndex = DamageColorIndex.Default;
            thornDi.procCoefficient = 0f; // no crazy procs sadge
            thornDi.rejected = false;
            thornDi.crit = false;


            Logger.LogDebug("Registering shared buffs. . .");
            vulnerableBuff.Init();
            
            // This line of log will appear in the bepinex console when the Awake method is done.
            Log.LogInfo(nameof(Awake) + " done.");
        }

        //The Update() method is run on every frame of the game.
        private void Update()
        {
            //This if statement checks if the player has currently pressed F2.
            if (Input.GetKeyDown(KeyCode.F2))
            {
                //Get the player body to use a position:	
                var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;

                //And then drop our defined item in front of the player.

                Log.LogInfo($"Player pressed F2. Spawning our custom item at coordinates {transform.position}");
                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(JuzuBracelet.item.itemIndex), transform.position, transform.forward * 20f);
            }

            //This if statement checks if the player has currently pressed F2.
            if (Input.GetKeyDown(KeyCode.F3))
            {
                //Get the player body to use a position:	
                var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;

                //And then drop our defined item in front of the player.

                Log.LogInfo($"Player pressed F2. Spawning our custom item at coordinates {transform.position}");
                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(BagOfMarbles.item.itemIndex), transform.position, transform.forward * 20f);
            }
        }
    }
}
