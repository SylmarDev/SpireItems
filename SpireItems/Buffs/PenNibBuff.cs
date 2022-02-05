using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using R2API;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class PenNibBuff
    {
        public static BuffDef buff;
        public CustomBuff completeBuff;
        public void Init()
        {
            buff = ScriptableObject.CreateInstance<BuffDef>();

            buff.buffColor = Color.yellow;
            buff.canStack = true;
            buff.isDebuff = false;
            buff.name = "STSPenNib";

            buff.iconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/buff/penNibBuff.png");

            completeBuff = new CustomBuff(buff);
            BuffAPI.Add(completeBuff);

            // hook
            On.RoR2.GlobalEventManager.OnHitEnemy += GlobalEventManager_OnHitEnemy;

            Log.LogInfo("Pen Nib (Buff) done");
        }

        private void GlobalEventManager_OnHitEnemy(On.RoR2.GlobalEventManager.orig_OnHitEnemy orig, GlobalEventManager self, DamageInfo damageInfo, GameObject victim)
        {
            if (damageInfo == null || damageInfo.rejected || !damageInfo.attacker || damageInfo.attacker == victim.gameObject || damageInfo.attacker.GetComponent<CharacterBody>().inventory == null)
            {
                orig(self, damageInfo, victim);
                return;
            }

            var cb = damageInfo.attacker.GetComponent<CharacterBody>();
            if (cb)
            {
                var isNib = cb.HasBuff(buff);
                if (isNib)
                {
                    Log.LogMessage($"nib proced! damage prior to nib calc: {damageInfo.damage}.. damage after nib: {(damageInfo.damage * 2)}");
                    Log.LogMessage($"btw, character base damage is {damageInfo.attacker.stats}");
                    damageInfo.damage *= 2f; // temp 1k, move to 2f
                    damageInfo.attacker.GetComponent<HealthComponent>().body.RemoveBuff(buff);
                }
            }
            orig(self, damageInfo, victim);
        }
    }
}
