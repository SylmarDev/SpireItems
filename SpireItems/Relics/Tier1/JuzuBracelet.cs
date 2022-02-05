using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class JuzuBracelet
    {
        public static ItemDef item;
        public bool hasJuzu = false;
        public int juzuCount = 0;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "JUZUBRACELET_NAME";
            item.nameToken = "JUZUBRACELET_NAME";
            item.pickupToken = "JUZUBRACELET_PICKUP";
            item.descriptionToken = "JUZUBRACELET_DESC";
            item.loreToken = "JUZUBRACELET_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/JuzuBracelet.png");
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
            // make less enemies spawn during tele events
            On.RoR2.TeleporterInteraction.OnInteractionBegin += TeleporterInteraction_OnInteractionBegin;
            On.RoR2.CombatDirector.SetNextSpawnAsBoss += CombatDirector_SetNextSpawnAsBoss;

            Log.LogInfo("Juzu Bracelet done");
        }

        private void TeleporterInteraction_OnInteractionBegin(On.RoR2.TeleporterInteraction.orig_OnInteractionBegin orig, TeleporterInteraction self, Interactor activator)
        {
            var activatorCb = activator.gameObject.GetComponent<CharacterBody>();
            if (activatorCb.inventory.GetItemCount(item.itemIndex) >= 1)
            {
                hasJuzu = true;
                juzuCount = activatorCb.inventory.GetItemCount(item.itemIndex); 
            }
            orig(self, activator);
        }

        private void CombatDirector_SetNextSpawnAsBoss(On.RoR2.CombatDirector.orig_SetNextSpawnAsBoss orig, CombatDirector self)
        {
            if (hasJuzu)
            {
                // Log.LogMessage("hasJuzu, reducing monsterCredit");
                for (var i = 0; i < juzuCount; i++)
                {
                    self.monsterCredit *= 0.9f; // temp set to 90% of self for every juzu bracelet
                }
                hasJuzu = false;
            }
            orig(self);
        }


        private void AddTokens()
        {
            LanguageAPI.Add("JUZUBRACELET_NAME", "Juzu Bracelet");
			LanguageAPI.Add("JUZUBRACELET_PICKUP", "Reduce amount of enemies at the start of Teleporter events.");
			LanguageAPI.Add("JUZUBRACELET_DESC", "Teleporter events start with only 90%<style=cStack>(+90% per stack)</style> of boss credit.");
			LanguageAPI.Add("JUZUBRACELET_LORE", "A ward against the unknown.");
        }
    }
}
