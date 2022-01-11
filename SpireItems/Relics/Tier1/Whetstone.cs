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
			// upon pickup pgrade two random attack skills

            Log.LogInfo("Whetstone done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("STSWHETSTONE_NAME", "Whetstone");
			LanguageAPI.Add("STSWHETSTONE_PICKUP", "Upon pickup, Upgrade 2 random Attacks");
			LanguageAPI.Add("STSWHETSTONE_DESC", "");
			LanguageAPI.Add("STSWHETSTONE_LORE", ""Flesh never beats steel." - Kublai the Great");
        }
    }
}
