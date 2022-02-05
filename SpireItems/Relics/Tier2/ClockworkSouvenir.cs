using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class ClockworkSouvenir
    {
        public static ItemDef item;
        public float procChance = 10f;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "CLOCKWORKSOUVENIR_NAME";
            item.nameToken = "CLOCKWORKSOUVENIR_NAME";
            item.pickupToken = "CLOCKWORKSOUVENIR_PICKUP";
            item.descriptionToken = "CLOCKWORKSOUVENIR_DESC";
            item.loreToken = "CLOCKWORKSOUVENIR_LORE";

            // tier
            item.tier = ItemTier.Tier2;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/clockworkSouviner.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            ItemTag[] tags = new ItemTag[] { ItemTag.OnKillEffect, ItemTag.Utility };
            item.tags = tags;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // chance to gain artifact on kill
            On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;

            Log.LogInfo("ClockworkSouvenir done.");
        }

        private void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
        {
            orig(self, damageReport);

            var cb = damageReport.attackerBody;
            if (cb && cb.inventory)
            {
                var clockCount = cb.inventory.GetItemCount(item.itemIndex);
                if (clockCount >= 1)
                {
                    var proc = cb.master ? Util.CheckRoll(procChance * clockCount, cb.master) : Util.CheckRoll(procChance * clockCount);
                    if (proc)
                    {
                        cb.AddBuff(ArtifactBuff.buff);
                    }
                }
            }
        }

        private void AddTokens()
        {
			LanguageAPI.Add("CLOCKWORKSOUVENIR_NAME", "Clockwork Souvenir");
			LanguageAPI.Add("CLOCKWORKSOUVENIR_PICKUP", "Chance to gain artifact on kill.");
			LanguageAPI.Add("CLOCKWORKSOUVENIR_DESC", "10% chance on kill to gain Artifact buff. Artifact buff negates the next debuff.");
			LanguageAPI.Add("CLOCKWORKSOUVENIR_LORE", "So many intricate gears.");
        }
    }
}
