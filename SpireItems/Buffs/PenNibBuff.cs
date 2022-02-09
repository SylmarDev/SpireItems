﻿using System;
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
            On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;

            Log.LogInfo("Pen Nib (Buff) done");
        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            if (di == null || self.body == null || di.rejected || !di.attacker || di.attacker == self.gameObject)
            {
                orig(self, di);
                return;
            } // no inflictor check so this is probably doomed to a blood shrine bug

            var cb = di.attacker.GetComponent<CharacterBody>();
            if (cb)
            {
                var isNib = cb.HasBuff(buff);
                if (isNib)
                { 
                    di.damage *= 2f; // temp 1k, move to 2f
                    di.attacker.GetComponent<HealthComponent>().body.RemoveBuff(buff);
                }
            }
            orig(self, di);
        }
    }
}
