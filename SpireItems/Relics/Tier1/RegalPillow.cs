using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class RegalPillow
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "REALLYNICEPILLOW_NAME";
            item.nameToken = "REALLYNICEPILLOW_NAME";
            item.pickupToken = "REALLYNICEPILLOW_PICKUP";
            item.descriptionToken = "REALLYNICEPILLOW_DESC";
            item.loreToken = "REALLYNICEPILLOW_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/RegalPillow.png");
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

            Log.LogInfo("RegalPillow done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("REALLYNICEPILLOW_NAME", "Regal Pillow");
			LanguageAPI.Add("REALLYNICEPILLOW_PICKUP", "");
			LanguageAPI.Add("REALLYNICEPILLOW_DESC", "");
			LanguageAPI.Add("REALLYNICEPILLOW_LORE", "Now you can get a good proper night's rest.");
        }
    }
}
