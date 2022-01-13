using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class BagOfMarbles
    {
        public static ItemDef item;
        public float procChance = 30f;
        public void Init()
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
            // if enemy above 90% health when hit, chance to apply vulnerable
            On.RoR2.GlobalEventManager.OnHitEnemy += GlobalEventManager_OnHitEnemy;

            Log.LogInfo("BagOfMarbles done");
        }

        private void GlobalEventManager_OnHitEnemy(On.RoR2.GlobalEventManager.orig_OnHitEnemy orig, GlobalEventManager self, DamageInfo damageInfo, GameObject victim)
        {
            int marblesCount = damageInfo.attacker.GetComponent<CharacterBody>().inventory.GetItemCount(item.itemIndex);

            if (marblesCount < 1)
            {
                //orig(self, damageInfo, victim);
                return;
            }

            var cb = damageInfo.attacker.GetComponent<CharacterBody>();
            if (cb)
            {
                var proc = cb.master ? Util.CheckRoll(procChance, cb.master) : Util.CheckRoll(procChance);
                if (proc)
                {
                    victim.GetComponent<HealthComponent>().body.AddTimedBuff(Vulnerable.buff, marblesCount * 5f);
                }

            }
            orig(self, damageInfo, victim);
        }

        private void AddTokens()
        {
            LanguageAPI.Add("BAGOFMARBLES_NAME", "Bag of Marbles");
			LanguageAPI.Add("BAGOFMARBLES_PICKUP", "Chance to make enemies vulnerable");
			LanguageAPI.Add("BAGOFMARBLES_DESC", "30% to make enemies Vulnerable for 5 seconds<style=cStack>(+5 seconds per stack)</style> on hit. Vulnernbility deals +50% damage from all sources.");
			LanguageAPI.Add("BAGOFMARBLES_LORE", "A once popular toy in the City. Useful for throwing enemies off balance.");
        }
    }
}
