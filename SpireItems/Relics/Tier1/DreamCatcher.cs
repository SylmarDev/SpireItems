using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class DreamCatcher
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "DREAMCATCHER_NAME";
            item.nameToken = "DREAMCATCHER_NAME";
            item.pickupToken = "DREAMCATCHER_PICKUP";
            item.descriptionToken = "DREAMCATCHER_DESC";
            item.loreToken = "DREAMCATCHER_LORE";

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
			// no idea

            Log.LogInfo("DreamCatcher done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("DREAMCATCHER_NAME", "Dream Catcher");
			LanguageAPI.Add("DREAMCATCHER_PICKUP", "Whenever you rest, you may add a card to your deck");
			LanguageAPI.Add("DREAMCATCHER_DESC", "");
			LanguageAPI.Add("DREAMCATCHER_LORE", "The northern tribes would often use dream catchers at night, believing they led to self improvement.");
        }
    }
}
