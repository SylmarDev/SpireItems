using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class SingingBowl
    {
        public static ItemDef item;
        public int healthAdded = 0;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "SINGINGBOWL_NAME";
            item.nameToken = "SINGINGBOWL_NAME";
            item.pickupToken = "SINGINGBOWL_PICKUP";
            item.descriptionToken = "SINGINGBOWL_DESC";
            item.loreToken = "SINGINGBOWL_LORE";

            // tier
            item.tier = ItemTier.Tier2;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/SingingBowl.png");
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
            // Whenever you pickup an item add 2 max HP
            On.RoR2.GenericPickupController.GrantItem += GenericPickupController_GrantItem;
            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;

            Log.LogInfo("SingingBowl done.");
        }

        private void GenericPickupController_GrantItem(On.RoR2.GenericPickupController.orig_GrantItem orig, GenericPickupController self, CharacterBody body, Inventory inventory)
        {
            orig(self, body, inventory);
            if (inventory.GetItemCount(item.itemIndex) >= 1)
            {
                healthAdded += 2 * inventory.GetItemCount(item.itemIndex);
                body.RecalculateStats();
            }
        }

        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            if (self.inventory)
            {
                var invBowls = self.inventory.GetItemCount(item.itemIndex);
                if (invBowls >= 1)
                {
                    var toAdd = healthAdded;
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
			LanguageAPI.Add("SINGINGBOWL_NAME", "Singing Bowl");
			LanguageAPI.Add("SINGINGBOWL_PICKUP", "Whenever you pick up an item add 2 max HP.");
			LanguageAPI.Add("SINGINGBOWL_DESC", "Whenever you pickup an item, permanently gain 2 max HP <style=cStack>(+2 per stack)</style>.");
			LanguageAPI.Add("SINGINGBOWL_LORE", "This well-used artifact rings out with a beautiful melody when struck.");
        }
    }
}
