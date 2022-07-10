using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Pear
    {
        public static ItemDef item;
        public int pears = 0;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "PEAR_NAME";
            item.nameToken = "PEAR_NAME";
            item.pickupToken = "PEAR_PICKUP";
            item.descriptionToken = "PEAR_DESC";
            item.loreToken = "PEAR_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier2;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/Pear.png");
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
            // increase max hp
            On.RoR2.Run.Start += Run_Start;
            On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged;
            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;

            Log.LogInfo("Pear done.");
        }

        private void Run_Start(On.RoR2.Run.orig_Start orig, Run self)
        {
            pears = 0;
            orig(self);
        }

        private void CharacterBody_OnInventoryChanged(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
        {
            if (self.inventory)
            {
                var invBerries = self.inventory.GetItemCount(item.itemIndex);
                if (invBerries >= 1 && invBerries > pears)
                {
                    self.maxHealth += 100 * invBerries;
                    self.healthComponent.Heal(100, new ProcChainMask());
                    pears = invBerries;
                }
                else if (invBerries < pears)
                {
                    self.maxHealth -= 100 * pears;
                    pears = invBerries;
                }
            }
            orig(self);
        }


        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            if (self.inventory)
            {
                var invPears = self.inventory.GetItemCount(item.itemIndex);
                if (invPears >= 1)
                {
                    var toAdd = 100 * invPears;
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
			LanguageAPI.Add("PEAR_NAME", "Pear");
			LanguageAPI.Add("PEAR_PICKUP", "Increase your maximum health.");
			LanguageAPI.Add("PEAR_DESC", "Increase <style=cIsHealing>maximum health</style> by <style=cIsHealing>8%</style> <style=cStack>(+8% per stack)</style>.");
            LanguageAPI.Add("PEAR_LORE", "A common fruit before the Spireblight.");
        }
    }
}
