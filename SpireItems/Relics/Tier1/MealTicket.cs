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
    public class MealTicket
    {
        public static ItemDef item;
        public void Init()
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
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/MealTicket.png");
            item.pickupModelPrefab = Resources.Load<GameObject>("Prefabs/PickupModels/PickupMystery");

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // not sure yet, maybe gain max HP every bazaar trip, maybe random chance to gain HP on opening a chest
            On.RoR2.OnPlayerEnterEvent.OnTriggerEnter += OnPlayerEnterEvent_OnTriggerEnter;
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
                Log.LogMessage(self.gameObject.name);
                if(self.gameObject.name == "SpawnShopkeeperTrigger")
                {
                    Log.LogMessage("adding health. . .");
                    // todo health adds, but only in the shop, leaves upon next stage
                    var tc = cb.inventory.GetItemCount(item.itemIndex);
                    if (tc >= 1)
                    {
                        cb.baseMaxHealth += (15 * tc);
                        cb.RecalculateStats();
                    }
                }
            }
            orig(self, other);
        }

        private void AddTokens()
        {
            LanguageAPI.Add("MEALTICKET_NAME", "Meal Ticket");
			LanguageAPI.Add("MEALTICKET_PICKUP", "");
			LanguageAPI.Add("MEALTICKET_DESC", "");
			LanguageAPI.Add("MEALTICKET_LORE", "Complimentary meatballs with every visit!");
        }
    }
}
