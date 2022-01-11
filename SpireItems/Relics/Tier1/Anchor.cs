using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Anchor
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "ANCHOR_NAME";
            item.nameToken = "ANCHOR_NAME";
            item.pickupToken = "ANCHOR_PICKUP";
            item.descriptionToken = "ANCHOR_DESC";
            item.loreToken = "ANCHOR_LORE";

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
			// give barrier if first to hit

            Log.LogInfo("Anchor done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("ANCHOR_NAME", "Anchor");
			LanguageAPI.Add("ANCHOR_PICKUP", "Gain barrier if you're the first to hit an enemy");
			LanguageAPI.Add("ANCHOR_DESC", "");
			LanguageAPI.Add("ANCHOR_LORE", "Holding this miniature trinket, you feel heavier and more stable");
        }
    }
}
