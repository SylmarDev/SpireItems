using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class MawBank
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "MAWBANK_NAME";
            item.nameToken = "MAWBANK_NAME";
            item.pickupToken = "MAWBANK_PICKUP";
            item.descriptionToken = "MAWBANK_DESC";
            item.loreToken = "MAWBANK_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/MawBank.png");
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
            // give additional gold (maybe a multiplier?) until visit baazar, then get replaced with greyed out item
            On.RoR2.CharacterMaster.GiveMoney += CharacterMaster_GiveMoney;
            On.RoR2.PurchaseInteraction.OnInteractionBegin += PurchaseInteraction_OnInteractionBegin;

            Log.LogInfo("MawBank done");
        }

        private void CharacterMaster_GiveMoney(On.RoR2.CharacterMaster.orig_GiveMoney orig, CharacterMaster self, uint amount)
        {
            if (self.GetBody())
            {
                if (self.GetBody().inventory.GetItemCount(item.itemIndex) >= 1)
                {
                    //Log.LogMessage("multing moners. . .");
                    //Log.LogMessage(amount);
                    var famount = (float) amount;
                    famount *= 1f + (self.GetBody().inventory.GetItemCount(item.itemIndex) * 0.12f);
                    amount = (uint) famount;
                    amount += 3;
                    //Log.LogMessage(amount);
                }
            }
            orig(self, amount);
        }

        private void PurchaseInteraction_OnInteractionBegin(On.RoR2.PurchaseInteraction.orig_OnInteractionBegin orig, PurchaseInteraction self, Interactor activator)
        {
            orig(self, activator);
            var cb = activator.GetComponent<CharacterBody>();
            if (cb && self.costType == CostTypeIndex.LunarCoin)
            {
                if(cb.inventory.GetItemCount(item.itemIndex) >= 1)
                {
                    cb.inventory.RemoveItem(item.itemIndex, cb.inventory.GetItemCount(item.itemIndex)); // remove all Maw Banks on spending Lunar coins
                }
            }
        }

        private void AddTokens()
        {
            LanguageAPI.Add("MAWBANK_NAME", "Maw Bank");
			LanguageAPI.Add("MAWBANK_PICKUP", "Gain additional gold, until you spend Lunar Coins.");
			LanguageAPI.Add("MAWBANK_DESC", "Every time you gain gold, gain an additional 12% <style=cStack>(+12% per stack)</style> + 3");
			LanguageAPI.Add("MAWBANK_LORE", "Surprisingly popular, despite maw attacks being a regular occurrence.");
        }
    }
}
