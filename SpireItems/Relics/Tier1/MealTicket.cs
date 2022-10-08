using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using System;
using System.ComponentModel;

namespace SylmarDev.SpireItems
{
    public class MealTicket : Relic
    {
        public static ItemDef item;
        public int activeTickets = 0;
        public override void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "MEALTICKET_NAME";
            item.nameToken = "MEALTICKET_NAME";
            item.pickupToken = "MEALTICKET_PICKUP";
            item.descriptionToken = "MEALTICKET_DESC";
            item.loreToken = "MEALTICKET_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/MealTicket.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            ItemTag[] tags = new ItemTag[] { ItemTag.Utility, ItemTag.Healing, ItemTag.InteractableRelated };
            item.tags = tags;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // not sure yet, maybe gain max HP every bazaar trip, maybe random chance to gain max HP on opening a chest
            // Meal Ticket Max HP buffs stay even after scrapping them, partly cause I'm too lazy to fix it, partly because taking them with you to the bazaar back then warants you the max health, and its only 15 per lol
            On.RoR2.OnPlayerEnterEvent.OnTriggerEnter += OnPlayerEnterEvent_OnTriggerEnter;
            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;
            Log.LogInfo("MealTicket done");
        }

        private void OnPlayerEnterEvent_OnTriggerEnter(On.RoR2.OnPlayerEnterEvent.orig_OnTriggerEnter orig, OnPlayerEnterEvent self, Collider other)
        {
            if ((self.serverOnly && !NetworkServer.active) || self.calledAction)
            {
                return;
            }
            // might only work for first player to enter, who's to say!
            CharacterBody cb = other.GetComponent<CharacterBody>();
            if (cb && cb.isPlayerControlled)
            {
                //Log.LogMessage(self.gameObject.name);
                if(self.gameObject.name == "SpawnShopkeeperTrigger")
                {
                    //Log.LogMessage("adding health. . .");
                    var tc = cb.inventory.GetItemCount(item.itemIndex);
                    if (tc >= 1)
                    {
                        activeTickets += tc;
                        cb.RecalculateStats();
                    }
                }
            }
            orig(self, other);
        }


        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            if (self.inventory)
            {
                if (self.inventory.GetItemCount(item.itemIndex) >= 1)
                {
                    var x = self.baseMaxHealth;
                    self.baseMaxHealth += (35 * activeTickets);
                    orig(self);
                    self.baseMaxHealth = x; // put this back to prevent horrible atrocitites
                    return;
                }
            }
            orig(self);
        }


        private void AddTokens()
        {
            LanguageAPI.Add("MEALTICKET_NAME", "Meal Ticket");
			LanguageAPI.Add("MEALTICKET_PICKUP", "Gain max health every time you enter the Bazaar Between Time.");
			LanguageAPI.Add("MEALTICKET_DESC", "Permanently increase <style=cIsHealing>maximum health</style> by <style=cIsHealing>35</style> <style=cStack>(+35 per stack)</style> every time you visit the Bazaar Between Time.");
			LanguageAPI.Add("MEALTICKET_LORE", "Complimentary meatballs with every visit!");
        }
    }
}
