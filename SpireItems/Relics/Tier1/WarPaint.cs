using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class WarPaint
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSWARPAINT_NAME";
            item.nameToken = "STSWARPAINT_NAME";
            item.pickupToken = "STSWARPAINT_PICKUP";
            item.descriptionToken = "STSWARPAINT_DESC";
            item.loreToken = "STSWARPAINT_LORE";

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
			// upgrade a non-damaging skill with a cooldown reduction or longer duration or something

            Log.LogInfo("WarPaint done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("STSWARPAINT_NAME", "War Paint");
			LanguageAPI.Add("STSWARPAINT_PICKUP", "");
			LanguageAPI.Add("STSWARPAINT_DESC", "");
			LanguageAPI.Add("STSWARPAINT_LORE", "In the past, Ironclads would create wards using enchanted war paint before charing into battle.");
        }
    }
}
