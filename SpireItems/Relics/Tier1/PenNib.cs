using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class PenNib
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "VIOLENTPEN_NAME";
            item.nameToken = "VIOLENTPEN_NAME";
            item.pickupToken = "VIOLENTPEN_PICKUP";
            item.descriptionToken = "VIOLENTPEN_DESC";
            item.loreToken = "VIOLENTPEN_LORE";

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
			// every 10th attack deals double damage

            Log.LogInfo("PenNib done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("VIOLENTPEN_NAME", "Pen Nib");
			LanguageAPI.Add("VIOLENTPEN_PICKUP", "Every 10th attack deals double damage");
			LanguageAPI.Add("VIOLENTPEN_DESC", "");
			LanguageAPI.Add("VIOLENTPEN_LORE", "Holding the nib, you can see everyone ever slain by a previous owner of the pen. A violent history.");
        }
    }
}
