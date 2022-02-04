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

    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]

    //This is the main declaration of our plugin class. BepInEx searches for all classes inheriting from BaseUnityPlugin to initialize on startup.
    //BaseUnityPlugin itself inherits from MonoBehaviour, so you can use this as a reference for what you can declare and use in your plugin class: https://docs.unity3d.com/ScriptReference/MonoBehaviour.html

    public class SpireItems : BaseUnityPlugin
	{
        //The Plugin GUID should be a unique ID for this plugin, which is human readable (as it is used in places like the config).
        //If we see this PluginGUID as it is on thunderstore, we will deprecate this mod. Change the PluginAuthor and the PluginName !
        public const string PluginGUID = "SylmarDev.SpireItems";
        public const string PluginAuthor = "SylmarDev";
        public const string PluginName = "Slay The Spire Relics";
        public const string PluginVersion = "0.2.0";

        // assets
        public static AssetBundle resources;

        public static GameObject cardPrefab;
        public static GameObject smallPrefab;

        public static Vector3 scaleTo;

        // config file
        private static ConfigFile cfgFile;

        // todo: apply item tags to all items

        // declare items
        // the java native has logged on
        private static Akabeko akabeko = new Akabeko();
        private static Anchor anchor = new Anchor();
        private static BagOfMarbles marbles = new BagOfMarbles();
        private static BloodVial vial = new BloodVial();
        private static Boot boot = new Boot();
        private static BronzeScales scales = new BronzeScales();
        private static CeramicFish fish = new CeramicFish();
        private static JuzuBracelet juzu = new JuzuBracelet();
        private static MawBank maw = new MawBank();
        private static MealTicket mealTicket = new MealTicket();
        private static OddlySmoothStone smoothStone = new OddlySmoothStone();
        private static Orichalcum ori = new Orichalcum();
        private static PenNib nib = new PenNib();
        private static RedMask rm = new RedMask();
        private static Strawberry strawberry = new Strawberry();
        private static ToyOrnithopter toy = new ToyOrnithopter();
        private static Vajra vajra = new Vajra();

        // green
        private static GoldenIdol gi = new GoldenIdol(); 
        private static BloodIdol bloodidol = new BloodIdol();
        private static Pear pear = new Pear();
        private static ClockworkSouvenir cs = new ClockworkSouvenir();
        private static OrangePellets op = new OrangePellets();
        private static SingingBowl sb = new SingingBowl();

        // red
        private static StrangeSpoon strangeSpoon = new StrangeSpoon();
        

        public static DamageInfo thornDi = new DamageInfo();

        // any empty methods for BuffDefs need to go here to be edited later
        /*public static BuffDef freezeBuff { get; private set; }
        public static BuffDef fearBuff { get; private set; }
        */
        public static Vulnerable vulnerableBuff = new Vulnerable();
        public static PenNibBuff nibBuff = new PenNibBuff();
        public static Weakness weak = new Weakness();
        public static ArtifactBuff ab = new ArtifactBuff();

        // item behaviors I guess
        public OrichalcumItemBehavior oriItemBehavior = new OrichalcumItemBehavior();

        // hellfire is broken, so are blood shrines

        //The Awake() method is run at the very start when the game is initialized.
        public void Awake()
        {
            //Init our logging class so that we can properly log for debugging
            Log.Init(Logger);

            // load assets
            Log.LogInfo("Loading Resources. . .");
            using(var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SpireItems.spireitems_assets"))
            {
                resources = AssetBundle.LoadFromStream(stream);
            }

            cardPrefab = resources.LoadAsset<GameObject>("assets/SpireRelics/models/prefabs/item/card.prefab");

            scaleTo = new Vector3(2f, 2f, 2f);

            Log.LogInfo("Loading Items. . .");
            akabeko.Init();
            anchor.Init();
            marbles.Init();
            vial.Init();
            boot.Init();
            scales.Init();
            fish.Init();
            juzu.Init();
            maw.Init();
            mealTicket.Init();
            smoothStone.Init();
            ori.Init();
            nib.Init();
            rm.Init();
            strawberry.Init();
            toy.Init();
            vajra.Init();

            // green
            gi.Init();
            bloodidol.Init();
            pear.Init();
            cs.Init();
            op.Init();
            sb.Init();

            // red
            strangeSpoon.Init();

            // no idea why this has to go after, but it just works
            var transform = cardPrefab.transform;
            transform.localScale = scaleTo;

            Log.LogInfo("Loading Thorn di. . .");
            thornDi.inflictor = null;
            thornDi.damageType = (DamageType.BypassArmor | DamageType.Silent);
            thornDi.damageColorIndex = DamageColorIndex.Default;
            thornDi.procCoefficient = 0f; // no crazy procs sadge
            thornDi.rejected = false;
            thornDi.crit = false;


            Logger.LogDebug("Registering shared buffs. . .");
            vulnerableBuff.Init();
            nibBuff.Init();
            weak.Init();
            ab.Init(); // artifact buff
            
            // This line of log will appear in the bepinex console when the Awake method is done.
            Log.LogInfo(nameof(Awake) + " done.");
        }

        /*
        private void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.F2))
            {	
                var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;

                Log.LogInfo($"Player pressed F2. Spawning our custom item at coordinates {transform.position}");
                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(StrangeSpoon.item.itemIndex), transform.position, transform.forward * 20f);
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {	
                var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;

                Log.LogInfo($"Player pressed F2. Spawning our custom item at coordinates {transform.position}");
                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(GoldenIdol.item.itemIndex), transform.position, transform.forward * 20f);
            }

            if (Input.GetKeyDown(KeyCode.F4))
            {
                var cb = PlayerCharacterMasterController.instances[0].master.GetBody();
                cb.AddBuff(Vulnerable.buff);
            }
        } */
    }
}
