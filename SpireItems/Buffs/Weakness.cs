using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Weakness
    {
        public static BuffDef buff;
        public CustomBuff completeBuff;
        public void Init()
        {
            buff = ScriptableObject.CreateInstance<BuffDef>();

            buff.buffColor = Color.green;
            buff.canStack = false;
            buff.isDebuff = true;
            buff.name = "STSWeakness";

            buff.iconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/buff/icon_weak.png");

            completeBuff = new CustomBuff(buff);
            BuffAPI.Add(completeBuff);

            // hook
            On.RoR2.HealthComponent.TakeDamage += On_HCTakeDamage;

            Log.LogInfo("Weak (Buff) done");
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
                var isWeak = cb.HasBuff(buff);
                if (isWeak)
                {
                    di.damage *= 0.75f;
                }
            }

            orig(self, di);
        }
    }
}
