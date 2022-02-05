using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class GremlinHorn
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSGREMLINHORN_NAME";
            item.nameToken = "STSGREMLINHORN_NAME";
            item.pickupToken = "STSGREMLINHORN_PICKUP";
            item.descriptionToken = "STSGREMLINHORN_DESC";
            item.loreToken = "STSGREMLINHORN_LORE";

            // tier
            item.tier = ItemTier.Tier2;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/GremlinHorn.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            ItemTag[] tags = new ItemTag[] { ItemTag.OnKillEffect }; // be sure to update tags once I know what this one does
            item.tags = tags;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // Do an on kill effect (unsure of what yet)

            Log.LogInfo("GremlinHorn done.");
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STSGREMLINHORN_NAME", "Gremlin Horn");
			LanguageAPI.Add("STSGREMLINHORN_PICKUP", "Do an on kill effect (unsure of what yet)");
			LanguageAPI.Add("STSGREMLINHORN_DESC", "");
			LanguageAPI.Add("STSGREMLINHORN_LORE", "Gremlin Nobs are capable of growing until the day they die. Remarkable. - Ranwid");
        }
    }
}
