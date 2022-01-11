using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class SmilingMask
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSHAPPYMASK_NAME";
            item.nameToken = "STSHAPPYMASK_NAME";
            item.pickupToken = "STSHAPPYMASK_PICKUP";
            item.descriptionToken = "STSHAPPYMASK_DESC";
            item.loreToken = "STSHAPPYMASK_LORE";

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
			// set rolling lunar items to 1 coin each roll

            Log.LogInfo("SmilingMask done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("STSHAPPYMASK_NAME", "Smiling Mask");
			LanguageAPI.Add("STSHAPPYMASK_PICKUP", "While holding this, rerolling Lunar Items costs only 1 Lunar Coin");
			LanguageAPI.Add("STSHAPPYMASK_DESC", "");
			LanguageAPI.Add("STSHAPPYMASK_LORE", "Mask worn by the merchant. He must have spares... <br><br>Maybe he's a friend of the newt, who knows?");
        }
    }
}
