using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class ArtifactBuff
    {
        public static BuffDef buff;
        public CustomBuff completeBuff;
        public void Init()
        {
            buff = ScriptableObject.CreateInstance<BuffDef>();

            buff.buffColor = Color.yellow;
            buff.canStack = true;
            buff.isDebuff = false;
            buff.name = "STSArtifactBuff";

            buff.iconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/buff/icon_artifact.png");

            completeBuff = new CustomBuff(buff);
            BuffAPI.Add(completeBuff);

            // hook
            On.RoR2.CharacterBody.SetBuffCount += CharacterBody_SetBuffCount;

            Log.LogInfo("Artifact (Buff) done");
        }

        private void CharacterBody_SetBuffCount(On.RoR2.CharacterBody.orig_SetBuffCount orig, CharacterBody self, BuffIndex buffType, int newCount)
        {
            var bd = BuffCatalog.GetBuffDef(buffType);
            var oldCount = self.GetBuffCount(bd); // only removes additive buffs
            if (self.GetBuffCount(buff) >= 1 && bd.isDebuff && newCount > oldCount)
            {
                self.RemoveBuff(bd);
                self.RemoveBuff(buff);
                return;
            }
            else
            {
                orig(self, buffType, newCount);
            }
        }
    }
}
