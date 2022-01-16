using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class BagOfPrep
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "PREPBAG_NAME";
            item.nameToken = "PREPBAG_NAME";
            item.pickupToken = "PREPBAG_PICKUP";
            item.descriptionToken = "PREPBAG_DESC";
            item.loreToken = "PREPBAG_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/BagOfPrep.png");
            item.pickupModelPrefab = Resources.Load<GameObject>("Prefabs/PickupModels/PickupMystery");

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
			// no idea

            Log.LogInfo("BagOfPrep done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("PREPBAG_NAME", "Bag of Preparation");
			LanguageAPI.Add("PREPBAG_PICKUP", "still working on it");
			LanguageAPI.Add("PREPBAG_DESC", "");
			LanguageAPI.Add("PREPBAG_LORE", "Oversized adventurer's pack. Has many pockets and straps.");
        }
    }
}
