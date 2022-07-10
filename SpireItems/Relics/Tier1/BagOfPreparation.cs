using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class BagOfPreparation
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSBAGOFPREP_NAME";
            item.nameToken = "STSBAGOFPREP_NAME";
            item.pickupToken = "STSBAGOFPREP_PICKUP";
            item.descriptionToken = "STSBAGOFPREP_DESC";
            item.loreToken = "STSBAGOFPREP_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/Boot.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            ItemTag[] tags = new ItemTag[] { ItemTag.Damage };
            item.tags = tags;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // 2 bonus charges at the start of a stage
            Stage.onStageStartGlobal += Stage_onStageStartGlobal;
            

            Log.LogInfo("Bag of Prep is done");
        }

        private void Stage_onStageStartGlobal(Stage obj)
        {
            Log.LogMessage("hi :)");
            Log.LogMessage(PlayerCharacterMasterController.instances.Count);
            // get an NRE here after stage 1 for somehow

            for (var i = 0; i < PlayerCharacterMasterController.instances.Count; i++)
            {
                CharacterBody cb = CharacterMaster.readOnlyInstancesList[i].GetBody() != null ? CharacterMaster.readOnlyInstancesList[i].GetBody() : null;

                var bops = cb.inventory.GetItemCount(item.itemIndex);


                Log.LogMessage($"bop count: {bops}");

                if (bops > 0)
                {
                    var toAdd = 2 * bops;
                    cb.equipmentSlot.stock += toAdd;
                }
            }
        }

        private void AddTokens()
        {
            LanguageAPI.Add("STSBAGOFPREP_NAME", "Bag of Preparation");
            LanguageAPI.Add("STSBAGOFPREP_PICKUP", "Get extra charges of your equipment at the start of each stage.");
            LanguageAPI.Add("STSBAGOFPREP_DESC", "Get 2<style=cStack>(+2 per stack)</style> additional equipment charges at the start of each stage.");
            LanguageAPI.Add("STSBAGOFPREP_LORE", "Oversized adventurer's pack. Has many pockets and straps.");
        }
    }
}
