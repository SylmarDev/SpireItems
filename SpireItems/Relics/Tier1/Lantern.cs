using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Lantern
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "LANTERN_NAME";
            item.nameToken = "LANTERN_NAME";
            item.pickupToken = "LANTERN_PICKUP";
            item.descriptionToken = "LANTERN_DESC";
            item.loreToken = "LANTERN_LORE";

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
			// not sure yet

            Log.LogInfo("Lantern done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("LANTERN_NAME", "Lantern");
			LanguageAPI.Add("LANTERN_PICKUP", "");
			LanguageAPI.Add("LANTERN_DESC", "");
			LanguageAPI.Add("LANTERN_LORE", "An eerie lantern which illuminates only for the wielder.");
        }
    }
}
