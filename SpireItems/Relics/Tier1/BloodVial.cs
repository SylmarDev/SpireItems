using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class BloodVial
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "BLOODVIAL_NAME";
            item.nameToken = "BLOODVIAL_NAME";
            item.pickupToken = "BLOODVIAL_PICKUP";
            item.descriptionToken = "BLOODVIAL_DESC";
            item.loreToken = "BLOODVIAL_LORE";

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
			// first to hit an enemy heal

            Log.LogInfo("BloodVial done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("BLOODVIAL_NAME", "Blood Vial");
			LanguageAPI.Add("BLOODVIAL_PICKUP", "Heal a little HP at the start of combat");
			LanguageAPI.Add("BLOODVIAL_DESC", "");
			LanguageAPI.Add("BLOODVIAL_LORE", "A vial containing the blood of a pure and elder vampire.");
        }
    }
}
