using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class SlingOfCourage
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "SLINGCOURAGE_NAME";
            item.nameToken = "SLINGCOURAGE_NAME";
            item.pickupToken = "SLINGCOURAGE_PICKUP";
            item.descriptionToken = "SLINGCOURAGE_DESC";
            item.loreToken = "SLINGCOURAGE_LORE";

            // tier
            item.tier = ItemTier.Tier2;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/SlingOfCourage.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // 20% additional elite damage.

            Log.LogInfo("SlingOfCourage done.");
        }

        private void AddTokens()
        {
			LanguageAPI.Add("SLINGCOURAGE_NAME", "Sling of Courage");
			LanguageAPI.Add("SLINGCOURAGE_PICKUP", "20% additional elite damage.");
			LanguageAPI.Add("SLINGCOURAGE_DESC", "");
			LanguageAPI.Add("SLINGCOURAGE_LORE", "A handy tool for dealing with particularly tough opponents.");
        }
    }
}
