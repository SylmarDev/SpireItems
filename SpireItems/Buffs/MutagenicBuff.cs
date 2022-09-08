using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using R2API;
using UnityEngine;
using SpireItems.Buffs;
using BepInEx.Configuration;

namespace SylmarDev.SpireItems
{
    public class MutagenicBuff : BuffBase<MutagenicBuff>
    {
        public override string BuffName => "Mutagenic Strength Buff";
        public override Color Color => Color.red;
        public override bool CanStack => false;
        public override bool IsDebuff => false;
        public override Sprite BuffIcon => SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/buff/mutagenicBuff.png");
        public override void Init()
        {
            CreateBuff();
            Hooks();
        }
        public override void Hooks()
        {
            On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
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
                var isMuta = cb.HasBuff(BuffDef);
                if (isMuta)
                {
                    di.damage *= 1.3f; // temp 1k, move to 2f
                    //di.attacker.GetComponent<HealthComponent>().body.RemoveBuff(BuffDef);
                }
            }
            orig(self, di);
        }
    }
}