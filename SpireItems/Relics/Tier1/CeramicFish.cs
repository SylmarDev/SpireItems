using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class CeramicFish
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "CERAMICFISH_NAME";
            item.nameToken = "CERAMICFISH_NAME";
            item.pickupToken = "CERAMICFISH_PICKUP";
            item.descriptionToken = "CERAMICFISH_DESC";
            item.loreToken = "CERAMICFISH_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/CeramicFish.png");
            item.pickupModelPrefab = Resources.Load<GameObject>("Prefabs/PickupModels/PickupMystery");

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
			// give gold when you use an ability

            Log.LogInfo("CeramicFish done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("CERAMICFISH_NAME", "Ceramic Fish");
			LanguageAPI.Add("CERAMICFISH_PICKUP", "Whenever you use an ability, gain a small amount of gold");
			LanguageAPI.Add("CERAMICFISH_DESC", "");
			LanguageAPI.Add("CERAMICFISH_LORE", "Meticulousy painted, these fish were revered to bring great fortune.");
        }
    }
}
