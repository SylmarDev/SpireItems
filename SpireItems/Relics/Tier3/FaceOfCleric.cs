using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class FaceOfCleric
    {
        public static ItemDef item;
        public int maxHealthGained = 0;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "FACEOFCLERIC_NAME";
            item.nameToken = "FACEOFCLERIC_NAME";
            item.pickupToken = "FACEOFCLERIC_PICKUP";
            item.descriptionToken = "FACEOFCLERIC_DESC";
            item.loreToken = "FACEOFCLERIC_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier3;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/FaceOfCleric.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            ItemTag[] tags = new ItemTag[] { ItemTag.Utility, ItemTag.Healing, ItemTag.OnKillEffect };
            item.tags = tags;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // better infusion (1k cap)
            On.RoR2.Run.Start += Run_Start;
            On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;
            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;

            Log.LogInfo("FaceOfCleric done.");
        }

        private void Run_Start(On.RoR2.Run.orig_Start orig, Run self)
        {
            maxHealthGained = 0;
            orig(self);
        }

        private void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
        {
            orig(self, damageReport);
            if (damageReport.attackerBody == null)
            {
                return;
            }

            var inv = damageReport.attackerBody.inventory;
            if (inv)
            {
                var clericCount = inv.GetItemCount(item.itemIndex);
                if (clericCount >= 1)
                {
                    maxHealthGained += clericCount;
                    damageReport.attackerBody.RecalculateStats();
                }
            }
        }

        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            if (self.inventory)
            {
                var clerics = self.inventory.GetItemCount(item.itemIndex);
                if (clerics >= 1)
                {
                    self.baseMaxHealth += maxHealthGained;
                    orig(self);
                    self.baseMaxHealth -= maxHealthGained;
                    return;
                }
            }
            orig(self);
        }

        private void AddTokens()
        {
			LanguageAPI.Add("FACEOFCLERIC_NAME", "Face of Cleric");
			LanguageAPI.Add("FACEOFCLERIC_PICKUP", "Killing an enemy permanently increases your maximum health.");
			LanguageAPI.Add("FACEOFCLERIC_DESC", "Killing an enemy permanently increases your maximum health by 1. <style=cStack>(+1 per stack)</style>");
			LanguageAPI.Add("FACEOFCLERIC_LORE", "Everyone loves Cleric.");
        }
    }
}
