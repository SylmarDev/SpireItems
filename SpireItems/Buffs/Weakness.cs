using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;
using SpireItems.Buffs;
using BepInEx.Configuration;

namespace SylmarDev.SpireItems
{
    public class Weakness : BuffBase<Weakness>
    {
        public override string BuffName => "Weakness";
        public override Color Color => Color.green;
        public override bool CanStack => false;
        public override bool IsDebuff => true;
        public override Sprite BuffIcon => SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/buff/icon_weak.png");
        public override void Init()
        {
            CreateBuff();
            Hooks();
        }
        public override void Hooks()
        {
            On.RoR2.HealthComponent.TakeDamage += On_HCTakeDamage;
        }
        private void On_HCTakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            if (di == null || self.body == null || di.rejected || !di.attacker || di.inflictor == null || di.attacker == self.gameObject)
            {
                orig(self, di);
                return;
            }

            var cb = di.attacker.GetComponent<CharacterBody>();

            if (cb)
            {
                var isWeak = cb.HasBuff(BuffDef);
                if (isWeak)
                {
                    di.damage *= 0.75f;
                }
            }

            orig(self, di);
        }
    }
}