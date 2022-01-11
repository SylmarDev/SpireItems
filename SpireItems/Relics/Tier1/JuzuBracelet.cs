using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class JuzuBracelet
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "JUZUBRACELET_NAME";
            item.nameToken = "JUZUBRACELET_NAME";
            item.pickupToken = "JUZUBRACELET_PICKUP";
            item.descriptionToken = "JUZUBRACELET_DESC";
            item.loreToken = "JUZUBRACELET_LORE";

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
			// make less enemies spawn during tele events maybe

            Log.LogInfo("JuzuBracelet done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("JUZUBRACELET_NAME", "Juzu Bracelet");
			LanguageAPI.Add("JUZUBRACELET_PICKUP", "");
			LanguageAPI.Add("JUZUBRACELET_DESC", "");
			LanguageAPI.Add("JUZUBRACELET_LORE", "A ward against the unknown.");
        }
    }
}
