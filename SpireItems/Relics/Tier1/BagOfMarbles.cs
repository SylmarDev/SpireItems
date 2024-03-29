﻿using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;
using System.ComponentModel;

namespace SylmarDev.SpireItems
{
    public class BagOfMarbles : Relic
    {
        public static ItemDef item;
        public float procChance = 10f;
        public override void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "BAGOFMARBLES_NAME";
            item.nameToken = "BAGOFMARBLES_NAME";
            item.pickupToken = "BAGOFMARBLES_PICKUP";
            item.descriptionToken = "BAGOFMARBLES_DESC";
            item.loreToken = "BAGOFMARBLES_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/BagOfMarbles.png");
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
            // if enemy above 90% health when hit, chance to apply vulnerable
            On.RoR2.GlobalEventManager.OnHitEnemy += GlobalEventManager_OnHitEnemy;

            Log.LogInfo("BagOfMarbles done");
        }

        private void GlobalEventManager_OnHitEnemy(On.RoR2.GlobalEventManager.orig_OnHitEnemy orig, GlobalEventManager self, DamageInfo damageInfo, GameObject victim)
        {
            if (damageInfo == null || damageInfo.rejected || !damageInfo.attacker || damageInfo.attacker == victim.gameObject || damageInfo.attacker.GetComponent<CharacterBody>().inventory == null)
            {
                orig(self, damageInfo, victim);
                return;
            }

            int? marblesCount = damageInfo.attacker.GetComponent<CharacterBody>().inventory.GetItemCount(item.itemIndex);

            if (marblesCount == null || marblesCount < 1)
            {
                orig(self, damageInfo, victim);
                return;
            }

            var cb = damageInfo.attacker.GetComponent<CharacterBody>();
            if (cb)
            {
                var proc = cb.master ? Util.CheckRoll(procChance, cb.master) : Util.CheckRoll(procChance);
                if (proc)
                {
                    victim.GetComponent<HealthComponent>().body.AddTimedBuff(Vulnerable.instance.BuffDef, (float) marblesCount * 5f);
                }

            }
            orig(self, damageInfo, victim);
        }

        private void AddTokens()
        {
            LanguageAPI.Add("BAGOFMARBLES_NAME", "Bag of Marbles");
			LanguageAPI.Add("BAGOFMARBLES_PICKUP", "Chance to make enemies vulnerable.");
			LanguageAPI.Add("BAGOFMARBLES_DESC", "10% to make enemies Vulnerable for 5 seconds<style=cStack>(+5 seconds per stack)</style> on hit. Vulnernbility deals +50% damage from all sources.");
			LanguageAPI.Add("BAGOFMARBLES_LORE", "A once popular toy in the City. Useful for throwing enemies off balance.");
        }
    }
}
