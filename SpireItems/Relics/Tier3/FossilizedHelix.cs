using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;
using System;

namespace SylmarDev.SpireItems
{
    public class FossilizedHelix : Relic
    {
        public static ItemDef item;
        public override void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STS_PRAISEHELIX_NAME";
            item.nameToken = "STS_PRAISEHELIX_NAME";
            item.pickupToken = "STS_PRAISEHELIX_PICKUP";
            item.descriptionToken = "STS_PRAISEHELIX_DESC";
            item.loreToken = "STS_PRAISEHELIX_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier3;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/FossilizedHelix.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            ItemTag[] tags = new ItemTag[] { ItemTag.Utility };
            item.tags = tags;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // Chance to gain a damage-null buff on kill
            On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;

            Log.LogInfo("FossilizedHelix done.");
        }

        private void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
        {
            orig(self, damageReport);
            if (damageReport.attackerBody == null)
            {
                return;
            }

            var cb = damageReport.attackerBody;
            var inv = cb.inventory;

            if (inv)
            {
                var ic = inv.GetItemCount(item.itemIndex);
                if (ic >= 1)
                {
                    var procChance = ic * 15f;
                    var proc = cb.master ? Util.CheckRoll(procChance, cb.master) : Util.CheckRoll(procChance);
                    if (proc)
                    {
                        cb.AddBuff(Buffer.instance.BuffDef);
                    }
                }
            }
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STS_PRAISEHELIX_NAME", "Fossilized Helix");
			LanguageAPI.Add("STS_PRAISEHELIX_PICKUP", "Chance to gain Buffer on kill");
			LanguageAPI.Add("STS_PRAISEHELIX_DESC", "15% chance <style=cStack>(+15% per stack)</style> to gain Buffer on kill. Buffer nullfies next source of damage.");
			LanguageAPI.Add("STS_PRAISEHELIX_LORE", "Seemingly indestructible, you wonder what kind of creature this belonged to.");
        }
    }
}
