using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class OddlySmoothStone : Relic
    {
        public static ItemDef item;
        public int activeSmoothStones = 0;
        public override void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "VERYSMOOTHSTONE_NAME";
            item.nameToken = "VERYSMOOTHSTONE_NAME";
            item.pickupToken = "VERYSMOOTHSTONE_PICKUP";
            item.descriptionToken = "VERYSMOOTHSTONE_DESC";
            item.loreToken = "VERYSMOOTHSTONE_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/OddlySmoothStone.png");
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
            // give permanent armor up
            On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged;
            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;

            Log.LogInfo("OddlySmoothStone done");
        }

        private void CharacterBody_OnInventoryChanged(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
        {
            if(self.inventory)
            {
                var smoothStones = self.inventory.GetItemCount(item.itemIndex);
                if (smoothStones >= 1 && smoothStones > activeSmoothStones)
                {
                    self.armor += 10 * (smoothStones - activeSmoothStones);
                    activeSmoothStones = smoothStones;
                } else if (smoothStones < activeSmoothStones)
                {
                    self.armor -= 10 * (activeSmoothStones - smoothStones);
                    activeSmoothStones = smoothStones;
                }
            }
            orig(self);
        }


        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            if (self.inventory)
            {
                if (self.inventory.GetItemCount(item.itemIndex) >= 1)
                {
                    var temp = self.baseArmor;
                    self.baseArmor += 10 * activeSmoothStones;
                    // Log.LogMessage($"temp: {temp} ... baseForCalcs: {self.baseArmor}");
                    orig(self);
                    self.baseArmor = temp;
                    return;
                }
            }
            orig(self);
        }


        private void AddTokens()
        {
            LanguageAPI.Add("VERYSMOOTHSTONE_NAME", "Oddly Smooth Stone");
			LanguageAPI.Add("VERYSMOOTHSTONE_PICKUP", "Reduce incoming damage.");
			LanguageAPI.Add("VERYSMOOTHSTONE_DESC", "<style=cIsHealing>Increase armor</style> by <style=cIsHealing>10</style> <style=cStack>(+10 per stack)</style>");
			LanguageAPI.Add("VERYSMOOTHSTONE_LORE", "You have never seen something so smooth and pristine. This must be the work of the Ancients.");
        }
    }
}
