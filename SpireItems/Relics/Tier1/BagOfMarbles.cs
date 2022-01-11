using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class BagOfMarbles
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "BAGOFMARBLES_NAME";
            item.nameToken = "BAGOFMARBLES_NAME";
            item.pickupToken = "BAGOFMARBLES_PICKUP";
            item.descriptionToken = "BAGOFMARBLES_DESC";
            item.loreToken = "BAGOFMARBLES_LORE";

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
			// if enemy above 90% health when hit, chance to apply vulnerable

            Log.LogInfo("BagOfMarbles done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("BAGOFMARBLES_NAME", "Bag of Marbles");
			LanguageAPI.Add("BAGOFMARBLES_PICKUP", "Chance to make enemies vulnerable at the start of combat");
			LanguageAPI.Add("BAGOFMARBLES_DESC", "");
			LanguageAPI.Add("BAGOFMARBLES_LORE", "A once popular toy in the City. Useful for throwing enemies off balance.");
        }
    }
}
