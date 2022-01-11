using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Boot
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "SMBTHREEBOOT_NAME";
            item.nameToken = "SMBTHREEBOOT_NAME";
            item.pickupToken = "SMBTHREEBOOT_PICKUP";
            item.descriptionToken = "SMBTHREEBOOT_DESC";
            item.loreToken = "SMBTHREEBOOT_LORE";

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
			// do bonus damage to armored enemies

            Log.LogInfo("Boot done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("SMBTHREEBOOT_NAME", "The Boot");
			LanguageAPI.Add("SMBTHREEBOOT_PICKUP", "If enemy has armor, deal additional damage");
			LanguageAPI.Add("SMBTHREEBOOT_DESC", "");
			LanguageAPI.Add("SMBTHREEBOOT_LORE", "When wound up, the boot grows larger in size.");
        }
    }
}
