using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Whetstone
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSWHETSTONE_NAME";
            item.nameToken = "STSWHETSTONE_NAME";
            item.pickupToken = "STSWHETSTONE_PICKUP";
            item.descriptionToken = "STSWHETSTONE_DESC";
            item.loreToken = "STSWHETSTONE_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/Whetstone.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // upgrade 2 stacks of damage items into their next rarity

            Log.LogInfo("Whetstone done.");
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STSWHETSTONE_NAME", "Whetstone");
			LanguageAPI.Add("STSWHETSTONE_PICKUP", "upgrade 2 stacks of damage items into their next rarity");
			LanguageAPI.Add("STSWHETSTONE_DESC", "");
			LanguageAPI.Add("STSWHETSTONE_LORE", "\"Flesh never beats steel.\" - Kublai the Great");
        }
    }
}
