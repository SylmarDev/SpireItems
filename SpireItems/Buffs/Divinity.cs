using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using R2API;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Divinity
    {
        public static BuffDef buff;
        public CustomBuff completeBuff;
        public void Init()
        {
            buff = ScriptableObject.CreateInstance<BuffDef>();

            buff.buffColor = Color.white;
            buff.canStack = false;
            buff.isDebuff = false;
            buff.name = "STSDivinity";

            buff.iconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/buff/divinity.png");

            completeBuff = new CustomBuff(buff);
            BuffAPI.Add(completeBuff);

            // hook
            On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;

            Log.LogInfo("Divinity (Buff) done");
        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            if (di == null || self.body == null || di.rejected || !di.attacker || di.attacker == self.gameObject)
            {
                orig(self, di);
                return;
            }

            var cb = di.attacker.GetComponent<CharacterBody>();
            if (cb)
            {
                var isDivine = cb.HasBuff(buff);
                if (isDivine)
                {
                    di.damage *= 3f;
                }
            }
            orig(self, di);
        }
    }
}
