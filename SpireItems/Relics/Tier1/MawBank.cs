using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class MawBank
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "MAWBANK_NAME";
            item.nameToken = "MAWBANK_NAME";
            item.pickupToken = "MAWBANK_PICKUP";
            item.descriptionToken = "MAWBANK_DESC";
            item.loreToken = "MAWBANK_LORE";

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
			// give additional gold (maybe a multiplier?) until visit baazar, then get replaced with greyed out item

            Log.LogInfo("MawBank done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("MAWBANK_NAME", "Maw Bank");
			LanguageAPI.Add("MAWBANK_PICKUP", "Gain additional gold, until you visit the Bazaar Between Time");
			LanguageAPI.Add("MAWBANK_DESC", "");
			LanguageAPI.Add("MAWBANK_LORE", "Surprisingly popular, despite maw attacks being a regular occurrence.");
        }
    }
}
