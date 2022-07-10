using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class PerservedInsect
    {
        public static ItemDef item;
        public static int insects;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSPERSERVEDINSECT_NAME";
            item.nameToken = "STSPERSERVEDINSECT_NAME";
            item.pickupToken = "STSPERSERVEDINSECT_PICKUP";
            item.descriptionToken = "STSPERSERVEDINSECT_DESC";
            item.loreToken = "STSPERSERVEDINSECT_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/PerservedInsect.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // elites spawn in with 10% less health, hyperbolic stacking (teddy bear stacking)
            On.RoR2.Run.Start += Run_Start;
            CharacterBody.onBodyStartGlobal += CharacterBody_onBodyStartGlobal;
            On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged;

            Log.LogInfo("PerservedInsect done.");
        }

        private void Run_Start(On.RoR2.Run.orig_Start orig, Run self)
        {
            insects = 0;
            orig(self);
        }

        private void CharacterBody_onBodyStartGlobal(CharacterBody obj)
        {
            if (insects >= 1 && obj.isElite && !obj.isPlayerControlled)
            {
                var percent = 1 - (1 / ((0.10f * insects) + 1)); // tougher times stacking
                DamageInfo di = new DamageInfo();
                di.damage = obj.maxHealth * percent;
                obj.healthComponent.TakeDamage(di);
            }
        }

        private void CharacterBody_OnInventoryChanged(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
        {
            orig(self);
            var num = 0;
            for (var i = 0; i < PlayerCharacterMasterController.instances.Count; i++)
            {
                num += CharacterMaster.readOnlyInstancesList[i].GetBody().inventory.GetItemCount(item.itemIndex);
            }
            insects = num;
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STSPERSERVEDINSECT_NAME", "Perserved Insect");
			LanguageAPI.Add("STSPERSERVEDINSECT_PICKUP", "Elites spawn with less health");
			LanguageAPI.Add("STSPERSERVEDINSECT_DESC", "10% (+10% per stack, hyperbolic) damage dealt to elite enemies when they spawn ");
			LanguageAPI.Add("STSPERSERVEDINSECT_LORE", "The insect seems to create a shrinking aura that targets particularly large enemies.");
        }
    }
}
