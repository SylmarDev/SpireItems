using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;
using SpireItems.Buffs;
using BepInEx.Configuration;

namespace SylmarDev.SpireItems
{
    public class ArtifactBuff : BuffBase<ArtifactBuff>
    {
        public override string BuffName => "Artifact Buff";
        public override Color Color => Color.yellow;
        public override bool CanStack => true;
        public override bool IsDebuff => false;
        public override Sprite BuffIcon => SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/buff/icon_artifact.png");
        public override void Init()
        {
            CreateBuff();
            Hooks();
        }
        public override void Hooks()
        {
            On.RoR2.CharacterBody.AddTimedBuff_BuffDef_float += CharacterBody_AddTimedBuff_BuffDef_float;
            On.RoR2.CharacterBody.AddTimedBuff_BuffDef_float_int += CharacterBody_AddTimedBuff_BuffDef_float_int;
            On.RoR2.DotController.InflictDot_refInflictDotInfo += DotController_InflictDot_refInflictDotInfo;
        }

        private void CharacterBody_AddTimedBuff_BuffDef_float(On.RoR2.CharacterBody.orig_AddTimedBuff_BuffDef_float orig, CharacterBody self, BuffDef buffDef, float duration)
        {
            if (self.healthComponent && self.HasBuff(BuffDef) && buffDef.buffIndex != BuffIndex.None && buffDef.isDebuff)
            {
                self.RemoveBuff(BuffDef);
                return;
            }
            orig(self, buffDef, duration);
        }

        private void CharacterBody_AddTimedBuff_BuffDef_float_int(On.RoR2.CharacterBody.orig_AddTimedBuff_BuffDef_float_int orig, CharacterBody self, BuffDef buffDef, float duration, int maxStacks)
        {
            if (self.healthComponent && self.HasBuff(BuffDef) && buffDef.buffIndex != BuffIndex.None && buffDef.isDebuff)
            {
                self.RemoveBuff(BuffDef);
                return;
            }
            orig(self, buffDef, duration, maxStacks);
        }

        private void DotController_InflictDot_refInflictDotInfo(On.RoR2.DotController.orig_InflictDot_refInflictDotInfo orig, ref InflictDotInfo inflictDotInfo)
        {
            var victim = inflictDotInfo.victimObject;
            var cb = victim?.GetComponent<CharacterBody>();
            if (cb && cb.healthComponent && cb.HasBuff(BuffDef))
            {
                cb.RemoveBuff(BuffDef);
                return;
            }
            orig(ref inflictDotInfo);
        }
    }
}
