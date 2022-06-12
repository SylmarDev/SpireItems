using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class BronzeScales
    {
        public static ItemDef item;
        public bool isThorning = true;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "THORNSCALES_NAME";
            item.nameToken = "THORNSCALES_NAME";
            item.pickupToken = "THORNSCALES_PICKUP";
            item.descriptionToken = "THORNSCALES_DESC";
            item.loreToken = "THORNSCALES_LORE";


            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/BronzeScales.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            
            item.hidden = false;

            ItemTag[] tags = new ItemTag[] { ItemTag.Damage, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist };
            item.tags = tags;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // deal damage back to enemies hitting you
            On.RoR2.GlobalEventManager.OnHitEnemy += GlobalEventManager_OnHitEnemy;
            Log.LogInfo("BronzeScales done");
        }

        private void GlobalEventManager_OnHitEnemy(On.RoR2.GlobalEventManager.orig_OnHitEnemy orig, GlobalEventManager self, DamageInfo damageInfo, GameObject victim)
        {
            orig(self, damageInfo, victim);

            if (damageInfo == null || damageInfo.rejected || !damageInfo.attacker || !damageInfo.inflictor || damageInfo.attacker == victim || victim.GetComponent<CharacterBody>().inventory == null || damageInfo.attacker.GetComponent<CharacterBody>() == null || damageInfo.attacker.GetComponent<CharacterBody>().inventory == null)
            {
                return;
            }

            var cb = damageInfo.attacker.GetComponent<CharacterBody>();

            var thornCount = victim.GetComponent<CharacterBody>().inventory.GetItemCount(item.itemIndex);
            var attackerThorns = cb.inventory.GetItemCount(item.itemIndex);

            if (thornCount >= 1 && thornCount > attackerThorns)
            {
                SpireItems.thornDi.attacker = victim;
                SpireItems.thornDi.damage = victim.GetComponent<CharacterBody>().damage * 0.75f * thornCount;
                // Log.LogMessage("using thorns!");
                damageInfo.attacker.GetComponent<HealthComponent>().TakeDamage(SpireItems.thornDi);
            }
        }

        private void AddTokens()
        {
            LanguageAPI.Add("THORNSCALES_NAME", "Bronze Scales");
			LanguageAPI.Add("THORNSCALES_PICKUP", "When hit by an enemy, deal damage back.");
			LanguageAPI.Add("THORNSCALES_DESC", "When hit, deal back damage worth 75%<style=cStack>(+75% per stack)</style> of your damage.");
			LanguageAPI.Add("THORNSCALES_LORE", "The sharp scales of the Guardian. Rearranges itself to protect its user.");
        }
    }
}
