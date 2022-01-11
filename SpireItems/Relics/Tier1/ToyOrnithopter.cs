using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class ToyOrnithopter
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "TOYORNITHOPTER_NAME";
            item.nameToken = "TOYORNITHOPTER_NAME";
            item.pickupToken = "TOYORNITHOPTER_PICKUP";
            item.descriptionToken = "TOYORNITHOPTER_DESC";
            item.loreToken = "TOYORNITHOPTER_LORE";

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
			// heal % when using equipment

            Log.LogInfo("ToyOrnithopter done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("TOYORNITHOPTER_NAME", "Toy Ornithopter");
			LanguageAPI.Add("TOYORNITHOPTER_PICKUP", "Heal after using your equipment");
			LanguageAPI.Add("TOYORNITHOPTER_DESC", "");
			LanguageAPI.Add("TOYORNITHOPTER_LORE", ""This little toy is the perfect companion for the lone adventurer!"");
        }
    }
}
