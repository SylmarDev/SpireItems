using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class OddlySmoothStone
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "VERYSMOOTHSTONE_NAME";
            item.nameToken = "VERYSMOOTHSTONE_NAME";
            item.pickupToken = "VERYSMOOTHSTONE_PICKUP";
            item.descriptionToken = "VERYSMOOTHSTONE_DESC";
            item.loreToken = "VERYSMOOTHSTONE_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/OddlySmoothStone.png");
            item.pickupModelPrefab = Resources.Load<GameObject>("Prefabs/PickupModels/PickupMystery");

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
			// give permanent armor up 

            Log.LogInfo("OddlySmoothStone done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("VERYSMOOTHSTONE_NAME", "Oddly Smooth Stone");
			LanguageAPI.Add("VERYSMOOTHSTONE_PICKUP", "Gain armor");
			LanguageAPI.Add("VERYSMOOTHSTONE_DESC", "");
			LanguageAPI.Add("VERYSMOOTHSTONE_LORE", "You have never seen something so smooth and pristine. This must be the work of the Ancients.");
        }
    }
}
