﻿using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Boot : Relic
    {
        public static ItemDef item;
        public override void Init()
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
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/Boot.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            ItemTag[] tags = new ItemTag[] { ItemTag.Damage };
            item.tags = tags;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // do bonus damage to armored enemies
            On.RoR2.HealthComponent.TakeDamage += On_HCTakeDamage;

            Log.LogInfo("Boot done");
        }

        private void On_HCTakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            if (di == null || self == null || di.rejected || !di.attacker || di.attacker == self.gameObject || di.attacker.GetComponent<HealthComponent>() == null || self.body == null)
            {
                orig(self, di);
                return;
            }

            var hc = di.attacker.GetComponent<HealthComponent>();
            if (hc)
            {
                var cb = hc.body;
                if (cb)
                {
                    var inv = cb.inventory;
                    if (inv && self.body.armor > 0)
                    {
                        int bootCount = inv.GetItemCount(item.itemIndex);
                        if (bootCount >= 1)
                        {
                            //Log.LogMessage("stripping armor");
                            self.body.armor -= (15 * bootCount);
                        }
                    }
                }
            }
            orig(self, di);
        }

        private void AddTokens()
        {
            LanguageAPI.Add("SMBTHREEBOOT_NAME", "The Boot");
			LanguageAPI.Add("SMBTHREEBOOT_PICKUP", "If enemy has armor, deal additional damage.");
			LanguageAPI.Add("SMBTHREEBOOT_DESC", "If enemy has armor, reduce it by 15<style=cStack>(+15 per stack)</style> when calculating your damage.");
			LanguageAPI.Add("SMBTHREEBOOT_LORE", "When wound up, the boot grows larger in size.");
        }
    }
}
