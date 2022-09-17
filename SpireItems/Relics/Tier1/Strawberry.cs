using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Strawberry : Relic
    {
        public static ItemDef item;
        public int strawberries = 0;
        public override void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSSTRAWBERRY_NAME";
            item.nameToken = "STSSTRAWBERRY_NAME";
            item.pickupToken = "STSSTRAWBERRY_PICKUP";
            item.descriptionToken = "STSSTRAWBERRY_DESC";
            item.loreToken = "STSSTRAWBERRY_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/Strawberry.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            ItemTag[] tags = new ItemTag[] { ItemTag.Healing };
            item.tags = tags;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // raise Max HP by 5%, poor mans pearl probably
            On.RoR2.Run.Start += Run_Start;
            On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged;
            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;

            Log.LogInfo("Strawberry done");
        }

        private void Run_Start(On.RoR2.Run.orig_Start orig, Run self)
        {
            strawberries = 0;
            orig(self);
        }

        private void CharacterBody_OnInventoryChanged(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
        {
            if (self.inventory)
            {
                var invBerries = self.inventory.GetItemCount(item.itemIndex);
                if (invBerries >= 1 && invBerries > strawberries)
                {
                    self.maxHealth += 35 * invBerries;
                    self.healthComponent.Heal(35, new ProcChainMask());
                    strawberries = invBerries;
                }
                else if (invBerries < strawberries)
                {
                    self.maxHealth -= 35 * strawberries;
                    strawberries = invBerries;
                }
            }
            orig(self);
        }


        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            if (self.inventory)
            {
                var invBerries = self.inventory.GetItemCount(item.itemIndex);
                if (invBerries >= 1)
                {
                    var toAdd = 35 * invBerries;
                    self.baseMaxHealth += toAdd;
                    orig(self);
                    self.baseMaxHealth -= toAdd;
                    return;
                }
            }
            orig(self);
        }

        private void AddTokens()
        {
            LanguageAPI.Add("STSSTRAWBERRY_NAME", "Strawberry");
			LanguageAPI.Add("STSSTRAWBERRY_PICKUP", "Increase your maximum health.");
			LanguageAPI.Add("STSSTRAWBERRY_DESC", "Increase <style=cIsHealing>maximum health</style> by <style=cIsHealing>5%</style> <style=cStack>(+5% per stack)</style>.");
			LanguageAPI.Add("STSSTRAWBERRY_LORE", "Delicious! Haven't seen any of these since the blight. - Ranwind");
        }
    }
}
