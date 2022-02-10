using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using R2API;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Mantra
    {
        public static BuffDef buff;
        public CustomBuff completeBuff;
        public void Init()
        {
            buff = ScriptableObject.CreateInstance<BuffDef>();

            buff.buffColor = Color.white;
            buff.canStack = true;
            buff.isDebuff = false;
            buff.name = "STSMantra";

            buff.iconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/buff/mantra.png");

            completeBuff = new CustomBuff(buff);
            BuffAPI.Add(completeBuff);

            // hook
            On.RoR2.CharacterBody.AddBuff_BuffDef += CharacterBody_AddBuff_BuffDef;

            Log.LogInfo("Mantra (Buff) done");
        }

        private void CharacterBody_AddBuff_BuffDef(On.RoR2.CharacterBody.orig_AddBuff_BuffDef orig, CharacterBody self, BuffDef buffDef)
        {
            var mantraCount = self.GetBuffCount(buff);
            if (mantraCount >= 9)
            {
                self.SetBuffCount(buff.buffIndex, 0);
                self.AddTimedBuff(Divinity.buff, 5f);
                return;
            } else
            {
                orig(self, buffDef);
            }
        }
    }
}
