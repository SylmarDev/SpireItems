using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class CentennialPuzzle
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "CENTPUZZLE_NAME";
            item.nameToken = "CENTPUZZLE_NAME";
            item.pickupToken = "CENTPUZZLE_PICKUP";
            item.descriptionToken = "CENTPUZZLE_DESC";
            item.loreToken = "CENTPUZZLE_LORE";

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
			// no idea

            Log.LogInfo("CentennialPuzzle done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("CENTPUZZLE_NAME", "Centennial Puzzle");
			LanguageAPI.Add("CENTPUZZLE_PICKUP", "The first time you lose HP each combat, draw 3 cards.");
			LanguageAPI.Add("CENTPUZZLE_DESC", "");
			LanguageAPI.Add("CENTPUZZLE_LORE", "Upon solving the puzzle you feel a powerful warmth in your chest.");
        }
    }
}
