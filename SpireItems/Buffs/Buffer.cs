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
    public class Buffer : BuffBase<Buffer>
    {
        public override string BuffName => "Buffer";
        public override Color Color => Color.white;
        public override bool CanStack => true;
        public override bool IsDebuff => false;
        public override Sprite BuffIcon => SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/buff/buffer.png");
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
            }

            var cb = self.body;

            if (cb)
            {
                var isBuffered = cb.HasBuff(BuffDef);
                if (isBuffered)
                {
                    cb.RemoveBuff(BuffDef);
                    return;
                }
            }

            orig(self, di);
        }
    }
}
