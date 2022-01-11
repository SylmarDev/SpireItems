using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class BronzeScales
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "THORNSCALES_NAME";
            item.nameToken = "THORNSCALES_NAME";
            item.pickupToken = "THORNSCALES_PICKUP";
            item.descriptionToken = "THORNSCALES_DESC";
            item.loreToken = "THORNSCALES_LORE";

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
			// deal damage back to enemies hitting you

            Log.LogInfo("BronzeScales done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("THORNSCALES_NAME", "Bronze Scales");
			LanguageAPI.Add("THORNSCALES_PICKUP", "When hit by an enemy, deal damage back");
			LanguageAPI.Add("THORNSCALES_DESC", "");
			LanguageAPI.Add("THORNSCALES_LORE", "The sharp scales of the Guardian. Rearranges itself to protect its user.");
        }
    }
}
