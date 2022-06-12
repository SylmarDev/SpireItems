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
            On.RoR2.CharacterBody.SetBuffCount += CharacterBody_SetBuffCount;
        }
        private void CharacterBody_SetBuffCount(On.RoR2.CharacterBody.orig_SetBuffCount orig, CharacterBody self, BuffIndex buffType, int newCount)
        {
            var bd = BuffCatalog.GetBuffDef(buffType);
            var oldCount = self.GetBuffCount(bd); // only removes additive buffs
            if (self.GetBuffCount(BuffDef) >= 1 && bd.isDebuff && newCount > oldCount)
            {
                self.RemoveBuff(bd);
                self.RemoveBuff(BuffDef);
                return;
            }
            else
            {
                orig(self, buffType, newCount);
            }

        }
    }
}
