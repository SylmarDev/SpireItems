using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class HappyFlower
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "HAPPYFLOWER_NAME";
            item.nameToken = "HAPPYFLOWER_NAME";
            item.pickupToken = "HAPPYFLOWER_PICKUP";
            item.descriptionToken = "HAPPYFLOWER_DESC";
            item.loreToken = "HAPPYFLOWER_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = Resources.Load<Sprite>("Textures/MiscIcons/texMysteryIcon");
            item.pickupModelPrefab = Resources.Load<GameObject>("Prefabs/PickupModels/PickupMystery");

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
			// every 3 abilities, chance to reset a random cooldown

            Log.LogInfo("HappyFlower done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("HAPPYFLOWER_NAME", "Happy Flower");
			LanguageAPI.Add("HAPPYFLOWER_PICKUP", "Every 3 turns, gain 1 energy");
			LanguageAPI.Add("HAPPYFLOWER_DESC", "");
			LanguageAPI.Add("HAPPYFLOWER_LORE", "This unceasingly joyous plant is a popular novelty item among nobles.");
        }
    }
}
