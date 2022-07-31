using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class SmilingMask
    {
        public static ItemDef item;
        public static int masks;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSHAPPYMASK_NAME";
            item.nameToken = "STSHAPPYMASK_NAME";
            item.pickupToken = "STSHAPPYMASK_PICKUP";
            item.descriptionToken = "STSHAPPYMASK_DESC";
            item.loreToken = "STSHAPPYMASK_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/SmilingMask.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // subsquent chance shrine rolls cost less
            On.RoR2.Run.Start += Run_Start;
            On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged;
            On.RoR2.ShrineChanceBehavior.FixedUpdate += ShrineChanceBehavior_FixedUpdate;

            Log.LogInfo("SmilingMask done.");
        }

        private void Run_Start(On.RoR2.Run.orig_Start orig, Run self)
        {
            masks = 0;
            orig(self);
        }

        private void CharacterBody_OnInventoryChanged(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
        {
            orig(self);
            var num = 0;

            if (PlayerCharacterMasterController.instances.Count == 0)
            {
                return;
            }

            for (var i = 0; i < PlayerCharacterMasterController.instances.Count; i++)
            {
                var cb = CharacterMaster.readOnlyInstancesList[i].GetBody();
                if (cb == null)
                {
                    return;
                }
                var inv = cb.inventory;
                if (inv == null)
                {
                    return;
                }
                num += inv.GetItemCount(item.itemIndex);
            }
            masks = num;
        }

        private void ShrineChanceBehavior_FixedUpdate(On.RoR2.ShrineChanceBehavior.orig_FixedUpdate orig, ShrineChanceBehavior self)
        {
            if (masks >= 1)
            {
                var newCostPerPurchase = .4f;
                for (var i = 0; i < masks; i++)
                {
                    newCostPerPurchase *= 0.5f;
                }
                self.costMultiplierPerPurchase = 1f + newCostPerPurchase;
            }
            orig(self);
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STSHAPPYMASK_NAME", "Smiling Mask");
			LanguageAPI.Add("STSHAPPYMASK_PICKUP", "Subsquent Chance Shrine rolls cost less");
			LanguageAPI.Add("STSHAPPYMASK_DESC", "The shrine of chance cost multiplier is 50% <style=cStack>(+50% per stack, multiplicitive)</style> less.");
			LanguageAPI.Add("STSHAPPYMASK_LORE", "Mask worn by the merchant. He must have spares...");
        }
    }
}
