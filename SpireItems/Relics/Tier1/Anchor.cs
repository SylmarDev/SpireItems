using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Anchor
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "ANCHOR_NAME";
            item.nameToken = "ANCHOR_NAME";
            item.pickupToken = "ANCHOR_PICKUP";
            item.descriptionToken = "ANCHOR_DESC";
            item.loreToken = "ANCHOR_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/anchor.png");
            item.pickupModelPrefab = SpireItems.smallPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // give barrier if first to hit
            On.RoR2.HealthComponent.TakeDamage += On_HCTakeDamage;

            Log.LogInfo("Anchor done");
        }

        private void On_HCTakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            if (di == null || di.rejected || !di.attacker || di.attacker == self.gameObject) return;

            var attacker = di.attacker;
            var inv = attacker.GetComponent<HealthComponent>().body.inventory;

            if (inv && (self.health > (self.fullHealth * 0.95)))
            {
                int anchorCount = inv.GetItemCount(item.itemIndex);
                if (anchorCount >= 1)
                {
                    //Log.LogMessage("Anchor proc'd!");
                    attacker.GetComponent<HealthComponent>().barrier += (10 * anchorCount);
                }
            }
            orig(self, di);
        }

        private void AddTokens()
        {
            LanguageAPI.Add("ANCHOR_NAME", "Anchor");
			LanguageAPI.Add("ANCHOR_PICKUP", "Hitting enemies above 95% health gives you a temporary barrier");
			LanguageAPI.Add("ANCHOR_DESC", "Gain a temporary barrier on hitting enemies above 95% health for 10<style=cStack>(+10 per stack)</style> health.");
			LanguageAPI.Add("ANCHOR_LORE", "Holding this miniature trinket, you feel heavier and more stable.");
        }
    }
}
