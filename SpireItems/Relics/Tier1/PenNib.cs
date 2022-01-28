using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;
using System;

namespace SylmarDev.SpireItems
{
    public class PenNib
    {
        public static ItemDef item;
        public int hits = 0;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "VIOLENTPEN_NAME";
            item.nameToken = "VIOLENTPEN_NAME";
            item.pickupToken = "VIOLENTPEN_PICKUP";
            item.descriptionToken = "VIOLENTPEN_DESC";
            item.loreToken = "VIOLENTPEN_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/PenNib.png");
            item.pickupModelPrefab = Resources.Load<GameObject>("Prefabs/PickupModels/PickupMystery");

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // every 10th attack deals double damage
            On.RoR2.GlobalEventManager.OnHitEnemy += GlobalEventManager_OnHitEnemy;

            Log.LogInfo("PenNib done");
        }

        private void GlobalEventManager_OnHitEnemy(On.RoR2.GlobalEventManager.orig_OnHitEnemy orig, GlobalEventManager self, DamageInfo damageInfo, GameObject victim)
        {
            if (damageInfo == null || damageInfo.rejected || !damageInfo.attacker || damageInfo.attacker == victim.gameObject || damageInfo.attacker.GetComponent<CharacterBody>().inventory == null)
            {
                orig(self, damageInfo, victim);
                return;
            }

            var cb = damageInfo.attacker.GetComponent<HealthComponent>().body;
            
            if (cb)
            {
                if (cb.inventory.GetItemCount(item.itemIndex) >= 1)
                {
                    hits += 1;
                    if (hits >= 10)
                    {
                        var nine = Math.Min(9, cb.inventory.GetItemCount(item.itemIndex));
                        for (var i = 0; i < nine; i++)
                        {
                            cb.AddBuff(PenNibBuff.buff);
                        }
                        hits = 0;
                    }
                }
            }

            orig(self, damageInfo, victim);
        }

        private void AddTokens()
        {
            LanguageAPI.Add("VIOLENTPEN_NAME", "Pen Nib");
			LanguageAPI.Add("VIOLENTPEN_PICKUP", "Every 10th attack deals double damage");
			LanguageAPI.Add("VIOLENTPEN_DESC", "After landing 10 hits, Deal an additional 100% damage for 1 <style=cStack>(+1 per stack)</style> attack(s). Maximum cap at 10 attacks.");
			LanguageAPI.Add("VIOLENTPEN_LORE", "Holding the nib, you can see everyone ever slain by a previous owner of the pen. A violent history.");
        }
    }
}
