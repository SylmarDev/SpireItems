using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Vulnerable
    {
        public static BuffDef buff;
        public CustomBuff completeBuff;
        public void Init()
        {
            buff = ScriptableObject.CreateInstance<BuffDef>();

            buff.buffColor = Color.magenta;
            buff.canStack = false;
            buff.isDebuff = true;
            buff.name = "STSVulnerable";

            buff.iconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/buff/icon_vulnerable.png");

            completeBuff = new CustomBuff(buff);
            BuffAPI.Add(completeBuff);

            // hook
            On.RoR2.HealthComponent.TakeDamage += On_HCTakeDamage;

            Log.LogInfo("Vulnerable (Buff) done");
        }


        private void On_HCTakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            if (di == null || self.body == null || di.rejected || !di.attacker || di.inflictor == null || di.attacker == self.gameObject)
            {
                orig(self, di);
                return;
            }

            var isVulnerable = self.body.HasBuff(buff);

            if (isVulnerable)
            {
                di.damage *= 1.5f; // temp 1k, move to 1.5f
            }

            orig(self, di); 
        }
    }
}
