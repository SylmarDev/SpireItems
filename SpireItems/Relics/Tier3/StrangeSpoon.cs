using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class StrangeSpoon : Relic
    {
        public static ItemDef item;
        public float procChance = 0f;
        public float[] procChances = new float[] { 50f, 55f, 57.5f, 58f, 58.5f, 59f, 59.5f, 60f, 60.5f, 61f };
        public override void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STRANGESPOON_NAME";
            item.nameToken = "STRANGESPOON_NAME";
            item.pickupToken = "STRANGESPOON_PICKUP";
            item.descriptionToken = "STRANGESPOON_DESC";
            item.loreToken = "STRANGESPOON_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier3;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/StrangeSpoon.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            ItemTag[] tags = new ItemTag[] { ItemTag.Utility };
            item.tags = tags;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // 50% to not send ability into cooldown
            On.RoR2.Run.Start += Run_Start;
            On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged;
            On.RoR2.Skills.SkillDef.OnExecute += SkillDef_OnExecute;

            Log.LogInfo("StrangeSpoon done.");
        }

        // resest procChance after a run
        private void Run_Start(On.RoR2.Run.orig_Start orig, Run self)
        {
            procChance = 0f;
            orig(self);
        }

        private void CharacterBody_OnInventoryChanged(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
        {
            orig(self);
            if (self.inventory.GetItemCount(item.itemIndex) >= 1)
            {
                var spoons = Mathf.Max(self.inventory.GetItemCount(item.itemIndex), 10) - 1;
                procChance = procChances[spoons];
            }
        }

        private void SkillDef_OnExecute(On.RoR2.Skills.SkillDef.orig_OnExecute orig, RoR2.Skills.SkillDef self, GenericSkill skillSlot)
        {
            orig(self, skillSlot);
            var proc = Util.CheckRoll(procChance);
            if (!skillSlot.beginSkillCooldownOnSkillEnd && proc)
            {
                skillSlot.RestockSteplike();
            }
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STRANGESPOON_NAME", "Strange Spoon");
			LanguageAPI.Add("STRANGESPOON_PICKUP", "50% chance to not send ability into cooldown.");
			LanguageAPI.Add("STRANGESPOON_DESC", "50% chance <style=cStack>(+10% per stack)</style> an ability doesn't need to cooldown. Unaffected by Luck. Caps out at 10 stacks.");
			LanguageAPI.Add("STRANGESPOON_LORE", "Staring at the spoon, it appears to bend and twist around before your eyes.");
        }
    }
}