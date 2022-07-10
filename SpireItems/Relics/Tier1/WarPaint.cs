using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class WarPaint
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSPAINTOFWAR_NAME";
            item.nameToken = "STSPAINTOFWAR_NAME";
            item.pickupToken = "STSPAINTOFWAR_PICKUP";
            item.descriptionToken = "STSPAINTOFWAR_DESC";
            item.loreToken = "STSPAINTOFWAR_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/WarPaint.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // upgrade 2 stacks of healing items into their next rarity

            Log.LogInfo("WarPaint done.");
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STSPAINTOFWAR_NAME", "War Paint");
			LanguageAPI.Add("STSPAINTOFWAR_PICKUP", "upgrade 2 stacks of healing items into their next rarity");
			LanguageAPI.Add("STSPAINTOFWAR_DESC", "");
			LanguageAPI.Add("STSPAINTOFWAR_LORE", "In the past, Ironclads would create wards using enchanted war paint before charging into battle.");
        }
    }
}
