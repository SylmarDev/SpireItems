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
    public class Mantra : BuffBase<Mantra>
    {
        public override string BuffName => "Mantra";
        public override Color Color => Color.white;
        public override bool CanStack => true;
        public override bool IsDebuff => false;
        public override Sprite BuffIcon => SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/buff/mantra.png");
        public override void Init()
        {
            CreateBuff();
            Hooks();
            Log.LogInfo("Mantra Buff Initialized");
        }
        public override void Hooks()
        {
            On.RoR2.CharacterBody.AddBuff_BuffDef += CharacterBody_AddBuff_BuffDef;
        }
        private void CharacterBody_AddBuff_BuffDef(On.RoR2.CharacterBody.orig_AddBuff_BuffDef orig, CharacterBody self, BuffDef buffDef)
        {
            var mantraCount = self.GetBuffCount(BuffDef);
            if (mantraCount >= 9)
            {
                self.SetBuffCount(BuffDef.buffIndex, 0);
                self.AddTimedBuff(Divinity.instance.BuffDef, 5f);
                return;
            }
            else
            {
                orig(self, buffDef);
            }
        }
    }
}
