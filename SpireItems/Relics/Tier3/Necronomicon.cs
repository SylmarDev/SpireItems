using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Necronomicon
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "NECRONOMICON_NAME";
            item.nameToken = "NECRONOMICON_NAME";
            item.pickupToken = "NECRONOMICON_PICKUP";
            item.descriptionToken = "NECRONOMICON_DESC";
            item.loreToken = "NECRONOMICON_LORE";

            // tier
            item.tier = ItemTier.Tier3;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/Necronomicon.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            ItemTag[] tags = new ItemTag[] { ItemTag.Damage };
            item.tags = tags;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // chance for attacks over 300% to hit twice

            Log.LogInfo("Necronomicon done.");
        }

        private void AddTokens()
        {
			LanguageAPI.Add("NECRONOMICON_NAME", "Necronomicon");
			LanguageAPI.Add("NECRONOMICON_PICKUP", "chance for attacks over 300% to hit twice");
			LanguageAPI.Add("NECRONOMICON_DESC", "");
			LanguageAPI.Add("NECRONOMICON_LORE", "Only a fool would try to harness this evil power. At night your dreams are haunted by images of the book devouring your mind.");
        }
    }
}
