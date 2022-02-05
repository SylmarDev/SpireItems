using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class DarkstonePeriapt
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSDARKSTONEPERI_NAME";
            item.nameToken = "STSDARKSTONEPERI_NAME";
            item.pickupToken = "STSDARKSTONEPERI_PICKUP";
            item.descriptionToken = "STSDARKSTONEPERI_DESC";
            item.loreToken = "STSDARKSTONEPERI_LORE";

            // tier
            item.tier = ItemTier.Tier2;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/DarkstonePeriapt.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            ItemTag[] tags = new ItemTag[] { ItemTag.Healing };
            item.tags = tags;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // Whenever you pickup a lunar item, permanently increase your max HP

            Log.LogInfo("DarkstonePeriapt done.");
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STSDARKSTONEPERI_NAME", "Darkstone Periapt");
			LanguageAPI.Add("STSDARKSTONEPERI_PICKUP", "");
			LanguageAPI.Add("STSDARKSTONEPERI_DESC", "");
			LanguageAPI.Add("STSDARKSTONEPERI_LORE", "The stone draws power from dark energy, converting it into vitality for the wearer.");
        }
    }
}
