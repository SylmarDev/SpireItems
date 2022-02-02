using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Orichalcum
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "ORICHALCUM_NAME";
            item.nameToken = "ORICHALCUM_NAME";
            item.pickupToken = "ORICHALCUM_PICKUP";
            item.descriptionToken = "ORICHALCUM_DESC";
            item.loreToken = "ORICHALCUM_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/Orichalcum.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // bungus for barrier
            On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged;

            Log.LogInfo("Orichalcum done");
        }

        private void CharacterBody_OnInventoryChanged(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
        {
            self.AddItemBehavior<OrichalcumItemBehavior>(self.inventory.GetItemCount(item.itemIndex));
            orig(self);
        }

        private void AddTokens()
        {
            LanguageAPI.Add("ORICHALCUM_NAME", "Orichalcum");
			LanguageAPI.Add("ORICHALCUM_PICKUP", "Gain barrier after standing still for 1 second");
			LanguageAPI.Add("ORICHALCUM_DESC", "After standing still for <style=clsHealing>1</style> second, gain temporary barrier worth <style=clsHealing>0.6%</style> <style=cStack>(+0.6% per stack)</style> of your max barrier per second");
			LanguageAPI.Add("ORICHALCUM_LORE", "A green tinted metal of an unknown origin. Seemingly indestructible.");
        }
    }
}
