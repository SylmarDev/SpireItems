using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class AncientTeaSet
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "ANCIENTTEASET_NAME";
            item.nameToken = "ANCIENTTEASET_NAME";
            item.pickupToken = "ANCIENTTEASET_PICKUP";
            item.descriptionToken = "ANCIENTTEASET_DESC";
            item.loreToken = "ANCIENTTEASET_LORE";

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

            Log.LogInfo("AncientTeaSet done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("ANCIENTTEASET_NAME", "Ancient Tea Set");
			LanguageAPI.Add("ANCIENTTEASET_PICKUP", "Whenever you enter a rest site, start the next combat with 2 extra energy");
			LanguageAPI.Add("ANCIENTTEASET_DESC", "");
			LanguageAPI.Add("ANCIENTTEASET_LORE", "The key to a refreshing night's rest.");
        }
    }
}
