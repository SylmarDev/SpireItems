using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Omamori
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "OMAMORI_NAME";
            item.nameToken = "OMAMORI_NAME";
            item.pickupToken = "OMAMORI_PICKUP";
            item.descriptionToken = "OMAMORI_DESC";
            item.loreToken = "OMAMORI_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/Omamori.png");
            item.pickupModelPrefab = Resources.Load<GameObject>("Prefabs/PickupModels/PickupMystery");

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
			// negate the next two curses you obtain

            Log.LogInfo("Omamori done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("OMAMORI_NAME", "Omamori");
			LanguageAPI.Add("OMAMORI_PICKUP", "Negate the next 2 Curses you obtain");
			LanguageAPI.Add("OMAMORI_DESC", "");
			LanguageAPI.Add("OMAMORI_LORE", "A common charm for staving off vile spirits. This one seems to possess a spark of divine energy.");
        }
    }
}
