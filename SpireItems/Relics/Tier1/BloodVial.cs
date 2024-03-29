﻿using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;
using System;

namespace SylmarDev.SpireItems
{
    public class BloodVial : Relic
    {
        public static ItemDef item;
        public override void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "BLOODVIAL_NAME";
            item.nameToken = "BLOODVIAL_NAME";
            item.pickupToken = "BLOODVIAL_PICKUP";
            item.descriptionToken = "BLOODVIAL_DESC";
            item.loreToken = "BLOODVIAL_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/BloodVial.png");
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
            // first to hit an enemy heal
            On.RoR2.HealthComponent.TakeDamage += On_HCTakeDamage;
            On.RoR2.ShrineBloodBehavior.Start += ShrineBloodBehavior_Start;

            Log.LogInfo("BloodVial done");
        }

        private void ShrineBloodBehavior_Start(On.RoR2.ShrineBloodBehavior.orig_Start orig, ShrineBloodBehavior self)
        {
            Log.LogMessage("starting a blood shrine!!!");
            orig(self);
        }

        private void On_HCTakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            if (di == null || di.rejected || !di.attacker || di.attacker == self.gameObject)
            {
                orig(self, di);
                return;
            }

            // nested if disaster zone
            var hc = di.attacker.GetComponent<HealthComponent>();
            if (hc)
            {
                var cb = di.attacker.GetComponent<HealthComponent>().body;
                if (cb)
                {
                    var inv = cb.inventory;
                    if (inv && (self.health > (self.fullCombinedHealth * 0.95)))
                    {
                        int vialCount = inv.GetItemCount(item.itemIndex);
                        if (vialCount >= 1)
                        {
                            cb.healthComponent.HealFraction(0.02f * vialCount, default);
                        }
                    }
                }
            }
            orig(self, di);
        }

        private void AddTokens()
        {
            LanguageAPI.Add("BLOODVIAL_NAME", "Blood Vial");
			LanguageAPI.Add("BLOODVIAL_PICKUP", "Heal a little HP at the start of combat.");
			LanguageAPI.Add("BLOODVIAL_DESC", "<style=cIsHealing>Heal 2% </style> <style=cStack>(+2% per stack)</style> on hitting enemies with over 95% health.");
			LanguageAPI.Add("BLOODVIAL_LORE", "A vial containing the blood of a pure and elder vampire.");
        }
    }
}
