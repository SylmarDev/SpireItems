using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Nunchaku
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "NUNCHAKU_NAME";
            item.nameToken = "NUNCHAKU_NAME";
            item.pickupToken = "NUNCHAKU_PICKUP";
            item.descriptionToken = "NUNCHAKU_DESC";
            item.loreToken = "NUNCHAKU_LORE";

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
			// every 10 attacks chance to reset a cooldown, or gain movement speed  maybe

            Log.LogInfo("Nunchaku done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("NUNCHAKU_NAME", "Nunchaku");
			LanguageAPI.Add("NUNCHAKU_PICKUP", "Every time you play 10 attacks, do something");
			LanguageAPI.Add("NUNCHAKU_DESC", "");
			LanguageAPI.Add("NUNCHAKU_LORE", "A good training tool. Improves the posture and agility of the wielder.");
        }
    }
}
