using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class DarkstonePeriapt
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSDARKSTONEPERI_NAME";
            item.nameToken = "STSDARKSTONEPERI_NAME";
            item.pickupToken = "STSDARKSTONEPERI_PICKUP";
            item.descriptionToken = "STSDARKSTONEPERI_DESC";
            item.loreToken = "STSDARKSTONEPERI_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier2;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/darkstonePericept.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            ItemTag[] tags = new ItemTag[] { ItemTag.Healing };
            item.tags = tags;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // Whenever you pickup a lunar item, permanently increase your max HP
            On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged;
            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;

            Log.LogInfo("DarkstonePeriapt done.");
        }

        private void CharacterBody_OnInventoryChanged(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
        {
            if (self.inventory)
            {
                var invDSP = self.inventory.GetItemCount(item.itemIndex);
                if (invDSP >= 1)
                {
                    self.RecalculateStats();
                }
            }
            orig(self);
        }


        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            if (self.inventory)
            {
                var invDSP = self.inventory.GetItemCount(item.itemIndex);
                if (invDSP >= 1)
                {
                    var toAdd = self.inventory.GetTotalItemCountOfTier(ItemTier.Lunar) * invDSP * 60;
                    self.baseMaxHealth += toAdd;
                    orig(self);
                    self.baseMaxHealth -= toAdd;
                    return;
                }
            }
            orig(self);
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STSDARKSTONEPERI_NAME", "Darkstone Periapt");
			LanguageAPI.Add("STSDARKSTONEPERI_PICKUP", "Increase your maximum health for every Lunar item you have.");
			LanguageAPI.Add("STSDARKSTONEPERI_DESC", "Gain 60 Max HP <style=cStack>(+60 Per Stack)</style> for every Lunar Item you have");
			LanguageAPI.Add("STSDARKSTONEPERI_LORE", "The stone draws power from dark energy, converting it into vitality for the wearer.");
        }
    }
}
