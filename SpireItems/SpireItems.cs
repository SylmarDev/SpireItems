using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;
using BepInEx.Configuration;
using System;
using System.Reflection;

using Path = System.IO.Path;
using System.Collections.Generic;

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
    [R2APISubmoduleDependency(nameof(ItemAPI), nameof(LanguageAPI))]
    //[R2APISubmoduleDependency(nameof(BuffAPI)]

    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]

    //This is the main declaration of our plugin class. BepInEx searches for all classes inheriting from BaseUnityPlugin to initialize on startup.
    //BaseUnityPlugin itself inherits from MonoBehaviour, so you can use this as a reference for what you can declare and use in your plugin class: https://docs.unity3d.com/ScriptReference/MonoBehaviour.html

    public class SpireItems : BaseUnityPlugin
	{
        //The Plugin GUID should be a unique ID for this plugin, which is human readable (as it is used in places like the config).
        //If we see this PluginGUID as it is on thunderstore, we will deprecate this mod. Change the PluginAuthor and the PluginName !
        
        public const string PluginAuthor = "SylmarDev";
        public const string PluginName = "SpireItems";
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginVersion = "0.5.1";

        // assets
        public static AssetBundle resources;

        public static GameObject cardPrefab;
        public static GameObject smallPrefab;

        public static Vector3 scaleTo;

        // config file
        private static ConfigFile cfgFile;

        // calipers needs a buff or rework

        // todo: visual effects for divinity and maybe other buffs
        // todo: next batch of items
        // implement:
        // tiny chest
        // calipers
        // fix:
        // anchor
        // singing bowl
        // bag of prep

        // declare items
        public List<RelicConfigPair> relicConfigPairs; // thank goodness
        
        public static DamageInfo thornDi = new DamageInfo();

        // set buffs
        public static Vulnerable vulnerableBuff = new Vulnerable();
        public static PenNibBuff nibBuff = new PenNibBuff();
        public static Weakness weak = new Weakness();
        public static ArtifactBuff ab = new ArtifactBuff();
        public static Mantra mantra = new Mantra();
        public static Divinity divinity = new Divinity();
        public static MutagenicBuff mutaBuff = new MutagenicBuff();
        public static Buffer buffer = new Buffer();

        // item behaviors I guess
        public OrichalcumItemBehavior oriItemBehavior = new OrichalcumItemBehavior();

        public void Awake()
        {
            Log.Init(Logger);

            new SpireConfig().Init(Paths.ConfigPath);

            // load assets
            Log.LogInfo("Loading Resources. . .");
            using(var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SpireItems.spireitems_assets"))
            {
                resources = AssetBundle.LoadFromStream(stream);
            }

            cardPrefab = resources.LoadAsset<GameObject>("assets/SpireRelics/models/prefabs/item/card.prefab");

            // scale cards to look right
            scaleTo = new Vector3(2f, 2f, 2f);

            Log.LogInfo("Loading Items. . .");

            relicConfigPairs = new List<RelicConfigPair>();

            // white
            relicConfigPairs.Add(new RelicConfigPair(new Akabeko(), SpireConfig.enableAkabeko.Value));
            relicConfigPairs.Add(new RelicConfigPair(new BagOfMarbles(), SpireConfig.enableBagOfMarbles.Value));
            relicConfigPairs.Add(new RelicConfigPair(new BloodVial(), SpireConfig.enableBloodVial.Value));
            relicConfigPairs.Add(new RelicConfigPair(new Boot(), SpireConfig.enableBoot.Value));
            relicConfigPairs.Add(new RelicConfigPair(new BronzeScales(), SpireConfig.enableBronzeScales.Value));
            relicConfigPairs.Add(new RelicConfigPair(new CeramicFish(), SpireConfig.enableCeramicFish.Value));
            relicConfigPairs.Add(new RelicConfigPair(new JuzuBracelet(), SpireConfig.enableJuzuBracelet.Value));
            relicConfigPairs.Add(new RelicConfigPair(new MawBank(), SpireConfig.enableMawBank.Value));
            relicConfigPairs.Add(new RelicConfigPair(new MealTicket(), SpireConfig.enableMealTicket.Value));
            relicConfigPairs.Add(new RelicConfigPair(new OddlySmoothStone(), SpireConfig.enableOddlySmoothStone.Value));
            relicConfigPairs.Add(new RelicConfigPair(new Orichalcum(), SpireConfig.enableOrichalcum.Value));
            relicConfigPairs.Add(new RelicConfigPair(new PenNib(), SpireConfig.enablePenNib.Value));
            relicConfigPairs.Add(new RelicConfigPair(new RedMask(), SpireConfig.enableRedMask.Value));
            relicConfigPairs.Add(new RelicConfigPair(new Strawberry(), SpireConfig.enableStrawberry.Value));
            relicConfigPairs.Add(new RelicConfigPair(new ToyOrnithopter(), SpireConfig.enableToyOrnithopter.Value));
            relicConfigPairs.Add(new RelicConfigPair(new Vajra(), SpireConfig.enableVajra.Value));
            relicConfigPairs.Add(new RelicConfigPair(new Damaru(), SpireConfig.enableDamaru.Value));
            relicConfigPairs.Add(new RelicConfigPair(new DreamCatcher(), SpireConfig.enableDreamCatcher.Value));
            relicConfigPairs.Add(new RelicConfigPair(new HappyFlower(), SpireConfig.enableHappyFlower.Value));
            relicConfigPairs.Add(new RelicConfigPair(new PerservedInsect(), SpireConfig.enablePerservedInsect.Value));
            relicConfigPairs.Add(new RelicConfigPair(new RedSkull(), SpireConfig.enableRedSkull.Value));
            relicConfigPairs.Add(new RelicConfigPair(new SmilingMask(), SpireConfig.enableSmilingMask.Value));
            relicConfigPairs.Add(new RelicConfigPair(new WarPaint(), SpireConfig.enableWarPaint.Value));
            relicConfigPairs.Add(new RelicConfigPair(new Whetstone(), SpireConfig.enableWhetstone.Value));


            // green
            relicConfigPairs.Add(new RelicConfigPair(new GoldenIdol(), SpireConfig.enableGoldenIdol.Value));
            relicConfigPairs.Add(new RelicConfigPair(new BloodIdol(), SpireConfig.enableBloodIdol.Value));
            relicConfigPairs.Add(new RelicConfigPair(new Pear(), SpireConfig.enablePear.Value));
            relicConfigPairs.Add(new RelicConfigPair(new ClockworkSouvenir(), SpireConfig.enableClockworkSouvenir.Value));
            relicConfigPairs.Add(new RelicConfigPair(new OrangePellets(), SpireConfig.enableOrangePellets.Value));
            relicConfigPairs.Add(new RelicConfigPair(new SlingOfCourage(), SpireConfig.enableSlingOfCourage.Value));
            relicConfigPairs.Add(new RelicConfigPair(new NeowsLament(), SpireConfig.enableNeowsLament.Value));
            relicConfigPairs.Add(new RelicConfigPair(new DarkstonePeriapt(), SpireConfig.enableDarkstonePeriapt.Value));
            relicConfigPairs.Add(new RelicConfigPair(new GremlinHorn(), SpireConfig.enableGremlinHorn.Value));
            relicConfigPairs.Add(new RelicConfigPair(new MutagenicStrength(), SpireConfig.enableMutagenicStrength.Value));
            relicConfigPairs.Add(new RelicConfigPair(new PaperPhrog(), SpireConfig.enablePaperPhrog.Value));


            // red
            relicConfigPairs.Add(new RelicConfigPair(new StrangeSpoon(), SpireConfig.enableStrangeSpoon.Value));
            relicConfigPairs.Add(new RelicConfigPair(new Necronomicon(), SpireConfig.enableNecronomicon.Value));
            relicConfigPairs.Add(new RelicConfigPair(new FaceOfCleric(), SpireConfig.enableFaceOfCleric.Value));
            relicConfigPairs.Add(new RelicConfigPair(new DuVuDoll(), SpireConfig.enableDuVuDoll.Value));
            relicConfigPairs.Add(new RelicConfigPair(new TungstenRod(), SpireConfig.enableTungstenRod.Value));
            //relicConfigPairs.Add(new RelicConfigPair(new Calipers(), SpireConfig.enableCalipers.Value));
            relicConfigPairs.Add(new RelicConfigPair(new FossilizedHelix(), SpireConfig.enableFossilizedHelix.Value));

            // lunar
            relicConfigPairs.Add(new RelicConfigPair(new CoffeeDripper(), SpireConfig.enableCoffeeDripper.Value));

            foreach (var item in relicConfigPairs)
            {
                if (item.configSwitch) item.relic.Init();
            }

            // no idea why this has to go after, but it just works
            var transform = cardPrefab.transform;
            transform.localScale = scaleTo;

            Logger.LogInfo("Registering shared buffs. . .");
            vulnerableBuff.Init();
            nibBuff.Init();
            weak.Init();
            ab.Init(); // artifact buff
            mantra.Init();
            divinity.Init(); // todo; 
            mutaBuff.Init();
            buffer.Init();

            Log.LogInfo("Loading Thorn di. . .");
            thornDi.inflictor = null;
            thornDi.damageType = (DamageType.BypassArmor | DamageType.Silent);
            thornDi.damageColorIndex = DamageColorIndex.Default;
            thornDi.procCoefficient = 0f; // no crazy procs sadge
            thornDi.rejected = false;
            thornDi.crit = false;

            
            Log.LogInfo(nameof(Awake) + " done.");
        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.F1))
            //{
            //    var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;
            //    Log.LogInfo($"Player pressed F2. Spawning our custom item at coordinates {transform.position}");
            //    PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(BagOfMarbles.item.itemIndex), transform.position, transform.forward * 20f);
            //}

            //if (Input.GetKeyDown(KeyCode.F2))
            //{
            //    var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;
            //    Log.LogInfo($"Player pressed F2. Spawning our custom item at coordinates {transform.position}");
            //    PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(DuVuDoll.item.itemIndex), transform.position, transform.forward * 20f);
            //}

            //if (Input.GetKeyDown(KeyCode.F3))
            //{
            //    //var cb = PlayerCharacterMasterController.instances[0].master.GetBodyObject().GetComponent<CharacterBody>();
            //    //cb.AddTimedBuff(vulnerableBuff.BuffDef, 5);
            //    var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;
            //    Log.LogInfo($"Player pressed F2. Spawning our custom item at coordinates {transform.position}");
            //    PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(MutagenicStrength.item.itemIndex), transform.position, transform.forward * 20f);
            //}
            ////if (Input.GetKeyDown(KeyCode.F4))
            ////{
            ////    var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;
            ////    Log.LogInfo($"Player pressed F2. Spawning our custom item at coordinates {transform.position}");
            ////    PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(Calipers.item.itemIndex), transform.position, transform.forward * 20f);
            ////}
            //if (Input.GetKeyDown(KeyCode.F5))
            //{
            //    var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;
            //    Log.LogInfo($"Player pressed F2. Spawning our custom item at coordinates {transform.position}");
            //    PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(TungstenRod.item.itemIndex), transform.position, transform.forward * 20f);
            //}
            //if (Input.GetKeyDown(KeyCode.F6))
            //{
            //    var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;
            //    Log.LogInfo($"Player pressed F2. Spawning our custom item at coordinates {transform.position}");
            //    PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(FossilizedHelix.item.itemIndex), transform.position, transform.forward * 20f);
            //}
        }
    }

    public abstract class Relic
    {
        public abstract void Init();
    }

    public class RelicConfigPair
    {
        public Relic relic;
        public bool configSwitch;

        public RelicConfigPair(Relic r, bool b)
        {
            relic = r;
            configSwitch = b;
        }
    }
}
