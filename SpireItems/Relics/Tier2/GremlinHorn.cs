using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class GremlinHorn
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSGREMLINHORN_NAME";
            item.nameToken = "STSGREMLINHORN_NAME";
            item.pickupToken = "STSGREMLINHORN_PICKUP";
            item.descriptionToken = "STSGREMLINHORN_DESC";
            item.loreToken = "STSGREMLINHORN_LORE";

            // set tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier2;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/GremlinHorn.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            ItemTag[] tags = new ItemTag[] { ItemTag.Utility, ItemTag.OnKillEffect }; // be sure to update tags once I know what this one does
            item.tags = tags;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // Do an on kill effect (unsure of what yet)
            On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;

            Log.LogInfo("GremlinHorn done.");
        }

        private void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
        {
            orig(self, damageReport);
            if (damageReport.attackerBody == null)
            {
                return;
            }

            var cb = damageReport.attackerBody;
            var inv = cb.inventory;
            if (inv)
            {
                var ic = inv.GetItemCount(item.itemIndex);
                if (ic >= 1)
                {
                    var procChance = ic == 1 ? 20f : 20f + (10f * (ic-1));
                    var proc = cb.master ? Util.CheckRoll(procChance, cb.master) : Util.CheckRoll(procChance);
                    if (proc)
                    {
                        damageReport.attackerBody.skillLocator.DeductCooldownFromAllSkillsAuthority(2f + (ic-1));
                    }
                }
            }
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STSGREMLINHORN_NAME", "Gremlin Horn");
			LanguageAPI.Add("STSGREMLINHORN_PICKUP", "Chance to reduce all skill cooldowns on kill.");
			LanguageAPI.Add("STSGREMLINHORN_DESC", "20% <style=cStack>(+10% per stack)</style> chance to reduce all skill cooldowns by 2 <style=cStack>(+1 per stack)</style> seconds.");
			LanguageAPI.Add("STSGREMLINHORN_LORE", "Gremlin Nobs are capable of growing until the day they die. Remarkable. - Ranwid");
        }
    }
}
