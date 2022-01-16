using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;
using BepInEx.Configuration;
using System;
using System.Reflection;

using Path = System.IO.Path;

namespace SylmarDev.AdrianInfo
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

    public class AdrianInfo : BaseUnityPlugin
	{
        //The Plugin GUID should be a unique ID for this plugin, which is human readable (as it is used in places like the config).
        //If we see this PluginGUID as it is on thunderstore, we will deprecate this mod. Change the PluginAuthor and the PluginName !
        public const string PluginGUID = "SylmarDev.AdrianInfo";
        public const string PluginAuthor = "SylmarDev";
        public const string PluginName = "AdrianInfo";
        public const string PluginVersion = "0.0.1";

        // assets
        public static AssetBundle resources;

        // config file
        //private static ConfigFile cfgFile;
        ItemDef daisy = (ItemDef)Resources.Load("itemdefs/tphealingnova");
        ItemDef[] itemLi = Resources.LoadAll<ItemDef>("itemdefs");
        GameObject shrine = (GameObject) Resources.Load("prefabs/networkedobjects/shrines/shrineboss");

        // any empty methods for BuffDefs need to go here to be edited later
        /*public static BuffDef freezeBuff { get; private set; }
        public static BuffDef fearBuff { get; private set; }
        */

        //The Awake() method is run at the very start when the game is initialized.
        public void Awake()
        {
            //Init our logging class so that we can properly log for debugging
            Log.Init(Logger);

            // load assets (fingers crossed)
            Log.LogInfo("Loading Resources. . .");
            /*using(var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SpireItems.spireitems_assets"))
            {
                resources = AssetBundle.LoadFromStream(stream);
            } */

            Log.LogInfo("Overwriting Items. . .");
            foreach(ItemDef item in itemLi)
            {
                String nameString;
                String pickupString;
                switch (item.name) // i know this is gross, forgive me
                {
                    case "AlienHead": // Alien Head
                        nameString = item.nameToken;
                        pickupString = "Outer space pussy was so anticlimactic, whole lotta neck, its chiropractic.";
                        break;
                    case "ArmorPlate": // Replusion Armor Plate
                        nameString = "Armor Plate";
                        pickupString = item.pickupToken;
                        break;
                    case "ArmorReductionOnHit": // Shattering Justice
                        nameString = "Mjolnir";
                        pickupString = item.pickupToken;
                        break;
                    case "ArtifactKey": // Artifact Key
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "AttackSpeedOnCrit": // Pred Instincts
                        nameString = "Cultural Appropriation";
                        pickupString = item.pickupToken;
                        break;
                    case "AutoCastEquipment": // Gesture of the Drowned
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "Bandolier": // Bandolier
                        nameString = "Scavenger";
                        pickupString = item.pickupToken;
                        break;
                    case "BarrierOnKill": // Topaz Brooch
                        nameString = "Yellow Turtle";
                        pickupString = item.pickupToken;
                        break;
                    case "BarrierOnOverHeal": // Aegis
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "Bear": // Tougher Times
                        nameString = "Ted from the hit movie, \"Ted\", starring Mark Wahlberg";
                        pickupString = "Ted from the hit movie, \"Ted\", starring Mark Wahlberg";
                        break;
                    case "BeetleGland": // Queens Gland
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "Behemoth": // Brilliant Behemoth
                        nameString = "Cigma";
                        pickupString = item.pickupToken;
                        break;
                    case "BleedOnHit": // Tri tip dagger
                        nameString = "Angel Blade";
                        pickupString = "from Supernatural";
                        break;
                    case "BleedOnHitAndExplode": // Shatterspleen
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "BonusGoldPackOnKill": // Ghor's Tome
                        nameString = "War Crimes";
                        pickupString = "Bush did 9/11";
                        break;
                    case "BoostAttackSpeed": // no idea
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "BoostEquipmentRecharge": // the one red that lowers cooldowns by 4 secounds (skull)
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "BossDamageBonus": // AP Rounds
                        nameString = "FMJ";
                        pickupString = item.pickupToken;
                        break;
                    case "BounceNearby": // Sentient Meat Hook
                        nameString = "Dead By Daylight";
                        pickupString = item.pickupToken;
                        break;
                    case "CaptainDefenseMatrix": // defensive microbots?
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "ChainLightning": // Ukulele
                        nameString = "Emperor Palpatine";
                        pickupString = "Do it. Kill him.";
                        break;
                    case "Clover": // 57 leaf clover
                        nameString = "Devil's Lettuce";
                        pickupString = "420";
                        break;
                    case "CritGlasses": // lens makers
                        nameString = "Choice Specs";
                        pickupString = item.pickupToken;
                        break;
                    case "Crowbar": // crowbar
                        nameString = "Jason Todd";
                        pickupString = item.pickupToken;
                        break;
                    case "CutHp": // no idea
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "Dagger": // ceremonial dagger
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "DeathMark": // Death Mark
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "EnergizedOnEquipmentUse": // war horn
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "EquipmentMagazine": // fuel cell
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "ExecuteLowHealthElite": // old guilotine
                        nameString = item.nameToken;
                        pickupString = "down with the bourgeoisie";
                        break;
                    case "ExplodeOnDeath": // Will-o'-the-wisp
                        nameString = "Will-o'-wisp";
                        pickupString = item.pickupToken;
                        break;
                    case "ExtraLife": // dios best friend
                        nameString = "Super Auto Pets Mushroom";
                        pickupString = item.pickupToken;
                        break;
                    case "ExtraLifeConsumed": // consumed dios
                        nameString = "Git gud 4head";
                        pickupString = item.pickupToken;
                        break;
                    case "FallBoots": // H3AD 5tompers
                        nameString = "Fuck 12";
                        pickupString = "ACAB";
                        break;
                    case "Feather": // Hopoo Feather
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "FireballsOnHit": // no idea
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "FireRing": // Kjaros
                        nameString = "Blast Furnace";
                        pickupString = item.pickupToken;
                        break;
                    case "Firework": // Bundle of Fireworks
                        nameString = "Scrap";
                        pickupString = item.pickupToken;
                        break;
                    case "FlatHealth": // Steak (process of elim)
                        nameString = "The Impossible Whopper";
                        pickupString = item.pickupToken;
                        break;
                    case "FocusConvergence": // Focused Convergence
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "GhostOnKill": // Happiest Mask
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "GoldOnHit": // Brittle Crown
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "HeadHunter": // Wake of Vultures
                        nameString = "idk but it looks cool";
                        pickupString = item.pickupToken;
                        break;
                    case "HealOnCrit": // Harvesters Scythe
                        nameString = "For Whom the Bell Tolls";
                        pickupString = item.pickupToken;
                        break;
                    case "HealWhileSafe": // Catious Slug
                        nameString = "Leftovers";
                        pickupString = item.pickupToken;
                        break;
                    case "Hoof": // Goat Hoof
                        nameString = "Choice Scarf";
                        pickupString = item.pickupToken;
                        break;
                    case "IceRing": // Runalds
                        nameString = "Cryofreeze";
                        pickupString = item.pickupToken;
                        break;
                    case "Icicle": // Frost Relic
                        nameString = "Cryogonal";
                        pickupString = item.pickupToken;
                        break;
                    case "IgniteOnKill": // Gasoline
                        nameString = "Petrol";
                        pickupString = item.pickupToken;
                        break;
                    case "IncreaseHealing": // Rejuvenation Rack
                        nameString = "Heal Slut";
                        pickupString = item.pickupToken;
                        break;
                    case "Incubator": // no idea
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "Infusion": // Infusion
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "InvadingDoppelganger": // probably umbra artifact (spite maybe)
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "JumpBoost": // Wax Quail
                        nameString = "Slide Cancel";
                        pickupString = item.pickupToken;
                        break;
                    case "KillEliteFrenzy": // Brainstalks
                        nameString = "What The Fuck";
                        pickupString = item.pickupToken;
                        break;
                    case "Knurl": // Titanic Knurl
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "LaserTurbine": // Resonance Disk
                        nameString = "\"Tron\" Disk";
                        pickupString = item.pickupToken;
                        break;
                    case "LightningStrikeOnHit": //  no idea
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "LunarBadLuck": // Purity?
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "LunarDagger": // Shaped Glass
                        nameString = "Do It Pussy No Balls";
                        pickupString = item.pickupToken;
                        break;
                    case "LunarPrimaryReplacement": // Visions of Heresy
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "LunarSecondaryReplacement": // Hooks of Heresy
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "LunarSpecialReplacement": // Ruin
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "LunarTrinket": // Beads of Fealty
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "LunarUtilityReplacement": // Strides of Heresy (Shadowfade)
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "Medkit": // MedKit
                        nameString = item.nameToken;
                        pickupString = "Pills here!";
                        break;
                    case "MinionLeash": // no idea
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "Missile": // ATG
                        nameString = "The Iron Man Missile";
                        pickupString = item.pickupToken;
                        break;
                    case "MonstersOnShrineUse": // that one fuckin Lunar Item
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "Mushroom": // Bustling Fungus
                        nameString = "Lemon Custard";
                        pickupString = item.pickupToken;
                        break;
                    case "NearbyDamageBonus": // Focus Crystal
                        nameString = "Choice Band";
                        pickupString = item.pickupToken;
                        break;
                    case "NovaOnHeal": // N'kuhana's Opinion
                        nameString = "Political Debate";
                        pickupString = item.pickupToken;
                        break;
                    case "NovaOnLowHealth": // Genesis Loop
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "ParentEgg": // Planua?
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "Pearl":  // pearl
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "PersonalShield": // Personal Shield Generator
                        nameString = "Shit";
                        pickupString = "It's just shit";
                        break;
                    case "Phasing": // Old War Stealthkit
                        nameString = "Stealth Boy";
                        pickupString = "New Vegas > 3.";
                        break;
                    case "Plant": // Interstellar Desk Plant?
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "RandomDamageZone": // no idea
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "RepeatHeal": // corpsebloom
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "RoboBallBuddy": // no idea
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "ScrapGreen": // Green Scrap
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "ScrapRed": // Red Scrap
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "ScrapWhite": // White Scrap
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "ScrapYellow": // Yellow Scrap
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "SecondarySkillMagazine": // Backup Mag
                        nameString = "Fast Mags";
                        pickupString = item.pickupToken;
                        break;
                    case "Seed": // Leeching Seed
                        nameString = "Shell Bell";
                        pickupString = item.pickupToken;
                        break;
                    case "ShieldOnly": // Transendence
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "ShinyPearl": // Irradiant Pearl
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "ShockNearby": // Unstable Tesla Coil
                        nameString = item.nameToken;
                        pickupString = "Fuck Thomas Edison. All my homies hate Thomas Edison";
                        break;
                    case "SiphonOnLowHealth": // Mired Urn?
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "SkullCounter": // not death mark? something else
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "SlowOnHit": // Chronobabuble
                        nameString = "Chrono Trigger";
                        pickupString = item.pickupToken;
                        break;
                    case "SprintArmor": // Rose Buckler
                        nameString = "The Shield From \"Troy\"";
                        pickupString = item.pickupToken;
                        break;
                    case "SprintBonus": // Energy Drink
                        nameString = "Red Bull";
                        pickupString = "Red Bull gives you wings.";
                        break;
                    case "SprintOutOfCombat": // Red Whip
                        nameString = "Whip";
                        pickupString = "Nae nae.";
                        break;
                    case "SprintWisp": // Little Disciple
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "Squid": // Squid Polyp
                        nameString = "Squid Friend";
                        pickupString = item.pickupToken;
                        break;
                    case "StickyBomb": // Sticky Bomb
                        nameString = "Semtex";
                        pickupString = item.pickupToken;
                        break;
                    case "StunChanceOnHit": // Stun Gernade
                        nameString = "Concussion";
                        pickupString = item.pickupToken;
                        break;
                    case "SummonedEcho": // no idea
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "Syringe": // Soldiers Syringe
                        nameString = "Steroids";
                        pickupString = "'That's literally what it is.' - you";
                        break;
                    case "Talisman": // Soulbound Catalyst
                        nameString = "Beetlejuice";
                        pickupString = item.pickupToken;
                        break;
                    case "Thorns": // Razorwire
                        nameString = item.nameToken;
                        pickupString = "That one death from \"Wrong Turn\".";
                        break;
                    case "TitanGoldDuringTP": // Halcyon Seed
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "TonicAffliction": // Tonic Affliction
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "Tooth": // Monster Tooth
                        nameString = "Shark Tooth Necklace";
                        pickupString = item.pickupToken;
                        break;
                    case "TPHealingNova": // Lepton Daisy
                        nameString = "Lotus Fairy";
                        pickupString = "Lotus Fairy";
                        break;
                    case "TreasureCache": // Rusted Key
                        nameString = "Tetanus";
                        pickupString = item.pickupToken;
                        break;
                    case "UtilitySkillMagazine": // Hardlight afterburner
                        nameString = item.nameToken;
                        pickupString = item.pickupToken;
                        break;
                    case "WarCryOnMultiKill": // Berzerker's Pauldron
                        nameString = "Mayan Death Whistle";
                        pickupString = item.pickupToken;
                        break;
                    case "WardOnLevel": // Warbanner
                        nameString = "Scrap";
                        pickupString = item.pickupToken;
                        break;
                    default:
                        continue;
                }
                item.nameToken = nameString;
                item.pickupToken = pickupString;
            }

            // This line of log will appear in the bepinex console when the Awake method is done.
            Log.LogInfo(nameof(Awake) + " done.");
        }
    }
}
