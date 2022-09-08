using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Calipers
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STS_CALIPERS_NAME";
            item.nameToken = "STS_CALIPERS_NAME";
            item.pickupToken = "STS_CALIPERS_PICKUP";
            item.descriptionToken = "STS_CALIPERS_DESC";
            item.loreToken = "STS_CALIPERS_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier3;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/Calipers.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            ItemTag[] tags = new ItemTag[] { ItemTag.Utility };
            item.tags = tags;

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // make barrier drain slower
            On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged;

            Log.LogInfo("Calipers done.");
        }

        private void CharacterBody_OnInventoryChanged(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
        {
            orig(self);

            var inv = self.inventory;
            if (inv)
            {
                var calipCount = inv.GetItemCount(item);
                
                if (calipCount >= 1)
                {
                    for (var i = 0; i < calipCount; i++)
                    {
                        self.barrierDecayRate *= 0.6f;
                    }
                }
            }
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STS_CALIPERS_NAME", "Calipers");
			LanguageAPI.Add("STS_CALIPERS_PICKUP", "Barrier drains slower");
			LanguageAPI.Add("STS_CALIPERS_DESC", "Barrier drains 60% <style=cStack>(+60% per stack, multiplicitave)</style> slower.");
			LanguageAPI.Add("STS_CALIPERS_LORE", "\"Mechanical precision leads to greatness\" - The Architect");
        }
    }
}
