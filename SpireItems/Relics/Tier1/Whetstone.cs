using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SylmarDev.SpireItems
{
    public class Whetstone
    {
        public static ItemDef item;
        public bool inPickupAlready = false;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSWHETSTONE_NAME";
            item.nameToken = "STSWHETSTONE_NAME";
            item.pickupToken = "STSWHETSTONE_PICKUP";
            item.descriptionToken = "STSWHETSTONE_DESC";
            item.loreToken = "STSWHETSTONE_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/Whetstone.png");
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
            // upgrade 2 stacks of damage items into their next rarity
            On.RoR2.Inventory.GiveItem_ItemIndex_int += Inventory_GiveItem_ItemIndex_int;

            Log.LogInfo("Whetstone done.");
        }

        private void Inventory_GiveItem_ItemIndex_int(On.RoR2.Inventory.orig_GiveItem_ItemIndex_int orig, Inventory self, ItemIndex itemIndex, int count)
        {
            orig(self, itemIndex, count);

            // deal with recursion problem
            if (inPickupAlready)
            {
                return;
            }

            inPickupAlready = true;

            // start from here
            CharacterBody cb = CharacterBody.readOnlyInstancesList.ToList().Find((CharacterBody body2) => body2.inventory == self);
            if (cb && self.GetItemCount(item) >= 1)
            {
                var whetstoneCount = self.GetItemCount(item);

                var viableReplaces = new List<ItemDef>();
                var rnd = new System.Random();

                foreach (var ii in self.itemAcquisitionOrder)
                {
                    var itemDef = ItemCatalog.GetItemDef(ii);

                    if (self.GetItemCount(itemDef) >= 1 && // is in inventory 
                        (itemDef.tier == ItemTier.Tier1 || itemDef.tier == ItemTier.Tier2) && // is white or green
                        Array.Exists(itemDef.tags, element => element == ItemTag.Damage)) // is a healing item
                    {
                        viableReplaces.Add(itemDef);
                    }
                }

                for (var i = 0; i < whetstoneCount; i++)
                {
                    for (var q = 0; q < 2; q++) // upgrade two items
                    {
                        if (viableReplaces.Count == 0) continue;

                        var r = rnd.Next(viableReplaces.Count);
                        var toReplace = viableReplaces.ElementAt(i);
                        var listByTier = toReplace.tier == ItemTier.Tier1 ? ItemCatalog.tier2ItemList : ItemCatalog.tier3ItemList; // should be fine

                        // add new
                        ItemDef toAdd = null;

                        while (toAdd == null || !(Array.Exists(toAdd.tags, element => element == ItemTag.Damage)))
                        {
                            toAdd = ItemCatalog.GetItemDef(listByTier.ElementAt(rnd.Next(listByTier.Count)));
                        }

                        self.GiveItem(toAdd); // this will probably inifinite loop

                        // remove old
                        self.RemoveItem(toReplace);
                        if (self.GetItemCount(toReplace) < 1) viableReplaces.RemoveAt(i);
                    }
                    self.RemoveItem(item); // remove whetstone
                }
            }

            inPickupAlready = false;
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STSWHETSTONE_NAME", "Whetstone");
			LanguageAPI.Add("STSWHETSTONE_PICKUP", "upgrade 2 stacks of damage items into their next rarity");
			LanguageAPI.Add("STSWHETSTONE_DESC", "");
			LanguageAPI.Add("STSWHETSTONE_LORE", "\"Flesh never beats steel.\" - Kublai the Great");
        }
    }
}
