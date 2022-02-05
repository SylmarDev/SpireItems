using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class RedMask
    {
        public static ItemDef item;
        public float procChance = 10f;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "REDMASK_NAME";
            item.nameToken = "REDMASK_NAME";
            item.pickupToken = "REDMASK_PICKUP";
            item.descriptionToken = "REDMASK_DESC";
            item.loreToken = "REDMASK_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/RedMask.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // Chance to apply weak.
            On.RoR2.GlobalEventManager.OnHitEnemy += GlobalEventManager_OnHitEnemy;

            Log.LogInfo("RedMask done.");
        }

        private void GlobalEventManager_OnHitEnemy(On.RoR2.GlobalEventManager.orig_OnHitEnemy orig, GlobalEventManager self, DamageInfo damageInfo, GameObject victim)
        {
            if (damageInfo == null || damageInfo.rejected || !damageInfo.attacker || damageInfo.attacker == victim.gameObject || damageInfo.attacker.GetComponent<CharacterBody>().inventory == null)
            {
                orig(self, damageInfo, victim);
                return;
            }

            int? maskCount = damageInfo.attacker.GetComponent<CharacterBody>().inventory.GetItemCount(item.itemIndex);

            if (maskCount == null || maskCount < 1)
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
                    victim.GetComponent<HealthComponent>().body.AddTimedBuff(Weakness.buff, (float)maskCount * 5f);
                }

            }
            orig(self, damageInfo, victim);
        }

        private void AddTokens()
        {
			LanguageAPI.Add("REDMASK_NAME", "Red Mask");
			LanguageAPI.Add("REDMASK_PICKUP", "Chance to make enemies weak.");
			LanguageAPI.Add("REDMASK_DESC", "10% to make enemies Weak for 5 seconds<style=cStack>(+5 seconds per stack)</style> on hit. Weakness makes enemies deal 25% less damage while affected.");
			LanguageAPI.Add("REDMASK_LORE", "This very stylish looking mask belongs to the leader of the Red Mask Bandits. Technically that makes you the leader now?");
        }
    }
}
