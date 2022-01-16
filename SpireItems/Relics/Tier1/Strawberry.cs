using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Strawberry
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSSTRAWBERRY_NAME";
            item.nameToken = "STSSTRAWBERRY_NAME";
            item.pickupToken = "STSSTRAWBERRY_PICKUP";
            item.descriptionToken = "STSSTRAWBERRY_DESC";
            item.loreToken = "STSSTRAWBERRY_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/Strawberry.png");
            item.pickupModelPrefab = Resources.Load<GameObject>("Prefabs/PickupModels/PickupMystery");

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
			// raise Max HP by 5%, poor mans pearl probably

            Log.LogInfo("Strawberry done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("STSSTRAWBERRY_NAME", "Strawberry");
			LanguageAPI.Add("STSSTRAWBERRY_PICKUP", "Upon pickup, raise your Max HP by 5%");
			LanguageAPI.Add("STSSTRAWBERRY_DESC", "");
			LanguageAPI.Add("STSSTRAWBERRY_LORE", "Delicious! Haven't seen any of these since the blight. - Ranwind");
        }
    }
}
