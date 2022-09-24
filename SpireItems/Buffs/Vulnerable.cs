using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;
using SpireItems.Buffs;
using BepInEx.Configuration;

namespace SylmarDev.SpireItems
{
    public class Vulnerable : BuffBase<Vulnerable>
    {
        public override string BuffName => "Vulnerable";
        public override Color Color => Color.magenta;
        public override bool CanStack => false;
        public override bool IsDebuff => true;
        public override Sprite BuffIcon => SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/buff/icon_vulnerable.png");

        public override void Init()
        {
            CreateBuff();
            Hooks();
            Log.LogInfo("Vulnerable Buff Initialized");
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

            var isVulnerable = self.body.HasBuff(BuffDef);
            var cb = di.attacker.GetComponent<CharacterBody>();
            var inv = cb?.inventory;

            if (isVulnerable)
            {
                if (inv != null && SpireConfig.enablePaperPhrog.Value) {
                    var phrogCount = inv.GetItemCount(PaperPhrog.item);
                    di.damage = phrogCount >= 1 ? di.damage * (1.5f + (phrogCount * 0.25f)) : di.damage * 1.5f;
                } else
                {
                    di.damage *= 1.5f;
                }
                //di.damage *= 1.5f; // temp 1k, move to 1.5f
            }

            orig(self, di);
        }
    }
}
