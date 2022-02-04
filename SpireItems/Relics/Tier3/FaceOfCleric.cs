using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class FaceOfCleric
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "FACEOFCLERIC_NAME";
            item.nameToken = "FACEOFCLERIC_NAME";
            item.pickupToken = "FACEOFCLERIC_PICKUP";
            item.descriptionToken = "FACEOFCLERIC_DESC";
            item.loreToken = "FACEOFCLERIC_LORE";

            // tier
            item.tier = ItemTier.Tier3;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/FaceOfCleric.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // better infusion (1k cap)

            Log.LogInfo("FaceOfCleric done.");
        }

        private void AddTokens()
        {
			LanguageAPI.Add("FACEOFCLERIC_NAME", "Face of Cleric");
			LanguageAPI.Add("FACEOFCLERIC_PICKUP", "better infusion (1k cap)");
			LanguageAPI.Add("FACEOFCLERIC_DESC", "");
			LanguageAPI.Add("FACEOFCLERIC_LORE", "Everyone loves Cleric.");
        }
    }
}
