using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class GoldenIdol
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSGOLDIDOL_NAME";
            item.nameToken = "STSGOLDIDOL_NAME";
            item.pickupToken = "STSGOLDIDOL_PICKUP";
            item.descriptionToken = "STSGOLDIDOL_DESC";
            item.loreToken = "STSGOLDIDOL_LORE";

            // tier
            item.tier = ItemTier.Tier2;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/GoldenIdol.png");
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
            // Gain 25% more gold whenever you gain gold
            On.RoR2.CharacterMaster.GiveMoney += CharacterMaster_GiveMoney;

            Log.LogInfo("GoldenIdol done.");
        }

        private void CharacterMaster_GiveMoney(On.RoR2.CharacterMaster.orig_GiveMoney orig, CharacterMaster self, uint amount)
        {
            if (self.GetBody())
            {
                if (self.GetBody().inventory.GetItemCount(item.itemIndex) >= 1)
                {
                    //Log.LogMessage("multing moners. . .");
                    //Log.LogMessage(amount);
                    var famount = (float)amount;
                    famount *= 1f + (self.GetBody().inventory.GetItemCount(item.itemIndex) * 0.25f);
                    amount = (uint)famount;
                    //Log.LogMessage(amount);
                }
            }
            orig(self, amount);
        }


        private void AddTokens()
        {
			LanguageAPI.Add("STSGOLDIDOL_NAME", "Golden Idol");
			LanguageAPI.Add("STSGOLDIDOL_PICKUP", "Gain 25% more gold whenever you gain gold.");
			LanguageAPI.Add("STSGOLDIDOL_DESC", "Every time you gain gold, gain an additional 25% <style=cStack>(+25% per stack)</style>");
            LanguageAPI.Add("STSGOLDIDOL_LORE", "Made of solid gold, you feel richer just holding it.");
        }
    }
}
