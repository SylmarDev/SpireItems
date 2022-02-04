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
            On.RoR2.HealthComponent.TakeDamage += On_HCTakeDamage;

            Log.LogInfo("Pen Nib (Buff) done");
        }

        private void On_HCTakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            if (di == null || self.body == null || di.rejected || !di.attacker || di.inflictor == null || di.attacker == self.gameObject)
            {
                orig(self, di);
                return;
            }

            var cb = di.attacker.GetComponent<HealthComponent>().body;
            
            if (cb)
            {
                var isNib = cb.HasBuff(buff);
                if (isNib)
                {
                    //Log.LogMessage($"nib proced! damage prior to nib calc: {di.damage}.. damage after nib: {(di.damage * 2)}");
                    di.damage *= 2f; // temp 1k, move to 2f
                    di.attacker.GetComponent<HealthComponent>().body.RemoveBuff(buff);
                }
            }

            orig(self, di);
        }
    }
}
