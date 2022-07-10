using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class TinyChest
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSVERYSMALLBOX_NAME";
            item.nameToken = "STSVERYSMALLBOX_NAME";
            item.pickupToken = "STSVERYSMALLBOX_PICKUP";
            item.descriptionToken = "STSVERYSMALLBOX_DESC";
            item.loreToken = "STSVERYSMALLBOX_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/TinyChest.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // small increase to red chance


            Log.LogInfo("TinyChest done.");
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STSVERYSMALLBOX_NAME", "Tiny Chest");
			LanguageAPI.Add("STSVERYSMALLBOX_PICKUP", "small increase to red chance");
			LanguageAPI.Add("STSVERYSMALLBOX_DESC", "");
			LanguageAPI.Add("STSVERYSMALLBOX_LORE", "\"A fine prototype.\" - The Architect");
        }
    }
}
