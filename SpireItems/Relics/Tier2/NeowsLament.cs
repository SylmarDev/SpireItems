using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class NeowsLament
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "NEOWSLAMENT_NAME";
            item.nameToken = "NEOWSLAMENT_NAME";
            item.pickupToken = "NEOWSLAMENT_PICKUP";
            item.descriptionToken = "NEOWSLAMENT_DESC";
            item.loreToken = "NEOWSLAMENT_LORE";

            // tier
            item.tier = ItemTier.Tier2;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/NeowsLament.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // small chance to instantly kill enemy on hit

            Log.LogInfo("NeowsLament done.");
        }

        private void AddTokens()
        {
			LanguageAPI.Add("NEOWSLAMENT_NAME", "Neow's Lament");
			LanguageAPI.Add("NEOWSLAMENT_PICKUP", "small chance to instantly kill enemy on hit");
			LanguageAPI.Add("NEOWSLAMENT_DESC", "");
			LanguageAPI.Add("NEOWSLAMENT_LORE", "The blessing of lamentation bestowed by Neow.");
        }
    }
}
