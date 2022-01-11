using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class PotionBelt
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "POTIONBELT_NAME";
            item.nameToken = "POTIONBELT_NAME";
            item.pickupToken = "POTIONBELT_PICKUP";
            item.descriptionToken = "POTIONBELT_DESC";
            item.loreToken = "POTIONBELT_LORE";

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
			// not sure yet. Maybe its a fuel cell? Reduces equipment cooldown?

            Log.LogInfo("PotionBelt done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("POTIONBELT_NAME", "Potion Belt");
			LanguageAPI.Add("POTIONBELT_PICKUP", "");
			LanguageAPI.Add("POTIONBELT_DESC", "");
			LanguageAPI.Add("POTIONBELT_LORE", "I can hold more Potions using this belt!");
        }
    }
}
