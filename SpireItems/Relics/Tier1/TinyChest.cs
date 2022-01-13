using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class TinyChest
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSTINYCHEST_NAME";
            item.nameToken = "STSTINYCHEST_NAME";
            item.pickupToken = "STSTINYCHEST_PICKUP";
            item.descriptionToken = "STSTINYCHEST_DESC";
            item.loreToken = "STSTINYCHEST_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/TinyChest.png");
            item.pickupModelPrefab = Resources.Load<GameObject>("Prefabs/PickupModels/PickupMystery");

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
			// increase rare item chance, or mark out the hidden chest, or small chance to get refunded when buying a chest

            Log.LogInfo("TinyChest done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("STSTINYCHEST_NAME", "Tiny Chest");
			LanguageAPI.Add("STSTINYCHEST_PICKUP", "not sure yet");
			LanguageAPI.Add("STSTINYCHEST_DESC", "");
			LanguageAPI.Add("STSTINYCHEST_LORE", "A fine prototype. - The Architect");
        }
    }
}
