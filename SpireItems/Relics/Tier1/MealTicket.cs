using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class MealTicket
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "MEALTICKET_NAME";
            item.nameToken = "MEALTICKET_NAME";
            item.pickupToken = "MEALTICKET_PICKUP";
            item.descriptionToken = "MEALTICKET_DESC";
            item.loreToken = "MEALTICKET_LORE";

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
			// not sure yet, maybe gain max HP every bazaar trip, maybe random chance to gain HP on opening a chest

            Log.LogInfo("MealTicket done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("MEALTICKET_NAME", "Meal Ticket");
			LanguageAPI.Add("MEALTICKET_PICKUP", "");
			LanguageAPI.Add("MEALTICKET_DESC", "");
			LanguageAPI.Add("MEALTICKET_LORE", "Complimentary meatballs with every visit!");
        }
    }
}
