using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class OrangePellets : Relic
    {
        public static ItemDef item;
        public float procChance = 10f;
        public override void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "ORANGEPELLETS_NAME";
            item.nameToken = "ORANGEPELLETS_NAME";
            item.pickupToken = "ORANGEPELLETS_PICKUP";
            item.descriptionToken = "ORANGEPELLETS_DESC";
            item.loreToken = "ORANGEPELLETS_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier2;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/OrangePellets.png");
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
            // small chance to remove all your debuffs on kill
            On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;

            Log.LogInfo("OrangePellets done.");
        }

        private void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
        {
            orig(self, damageReport);

            var cb = damageReport.attackerBody;
            if (cb && cb.inventory)
            {
                var pellets = cb.inventory.GetItemCount(item.itemIndex);
                if (pellets >= 1)
                {
                    var proc = cb.master ? Util.CheckRoll(procChance * pellets, cb.master) : Util.CheckRoll(procChance * pellets);
                    if (proc)
                    {
                        foreach(BuffIndex bi in cb.activeBuffsList)
                        {
                            if(BuffCatalog.GetBuffDef(bi).isDebuff)
                            {
                                cb.RemoveBuff(bi);
                            }
                        }
                    }
                }
            }
        }

        private void AddTokens()
        {
			LanguageAPI.Add("ORANGEPELLETS_NAME", "Orange Pellets");
			LanguageAPI.Add("ORANGEPELLETS_PICKUP", "Chance to remove all your debuffs on kill.");
			LanguageAPI.Add("ORANGEPELLETS_DESC", "10% chance on kill negate all debuffs.");
			LanguageAPI.Add("ORANGEPELLETS_LORE", "Made from various fungi found throughout the Spire, they will stave off any affliction.");
        }
    }
}
