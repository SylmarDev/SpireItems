using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;
using System;

namespace SylmarDev.SpireItems
{
    public class CeramicFish : Relic
    {
        public static ItemDef item;
        public override void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "CERAMICFISH_NAME";
            item.nameToken = "CERAMICFISH_NAME";
            item.pickupToken = "CERAMICFISH_PICKUP";
            item.descriptionToken = "CERAMICFISH_DESC";
            item.loreToken = "CERAMICFISH_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/CeramicFish.png");
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
            // give gold when you interact with something
            On.RoR2.GlobalEventManager.OnInteractionBegin += GlobalEventManager_OnInteractionBegin;
            Log.LogInfo("CeramicFish done");
        }

        private void GlobalEventManager_OnInteractionBegin(On.RoR2.GlobalEventManager.orig_OnInteractionBegin orig, GlobalEventManager self, Interactor interactor, IInteractable interactable, GameObject interactableObject)
        {
            var cb = interactor.GetComponent<CharacterBody>();
            if (cb)
            {
                if (cb.inventory)
                {
                    int itemCount = cb.inventory.GetItemCount(item.itemIndex);
                    if (itemCount > 0)
                    {
                        var scaledMoners = Run.instance.GetDifficultyScaledCost(9) * itemCount;
                        cb.master.GiveMoney(Convert.ToUInt32(scaledMoners));
                    }
                }
            }
            orig(self, interactor, interactable, interactableObject);
        }

        private void AddTokens()
        {
            LanguageAPI.Add("CERAMICFISH_NAME", "Ceramic Fish");
			LanguageAPI.Add("CERAMICFISH_PICKUP", "Activating an interactable gives you a small amount of gold.");
			LanguageAPI.Add("CERAMICFISH_DESC", "Activating an interactable gives you 9 <style=cStack>(+9 per stack)</style> gold. Scales over time.");
			LanguageAPI.Add("CERAMICFISH_LORE", "Meticulousy painted, these fish were revered to bring great fortune.");
        }
    }
}
