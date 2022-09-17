using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Damaru : Relic
    {
        public static ItemDef item;
        public override void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "DAMARU_NAME";
            item.nameToken = "DAMARU_NAME";
            item.pickupToken = "DAMARU_PICKUP";
            item.descriptionToken = "DAMARU_DESC";
            item.loreToken = "DAMARU_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/Damaru.png");
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
            // chance on kill to gain mantra
            On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;

            Log.LogInfo("Damaru done.");
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
                    var procChance = ic * 15f;
                    var proc = cb.master ? Util.CheckRoll(procChance, cb.master) : Util.CheckRoll(procChance);
                    if (proc)
                    {
                        cb.AddBuff(Mantra.instance.BuffDef);
                    }
                }
            }
        }

        private void AddTokens()
        {
			LanguageAPI.Add("DAMARU_NAME", "Damaru");
			LanguageAPI.Add("DAMARU_PICKUP", "Chance on kill to gain Mantra.");
			LanguageAPI.Add("DAMARU_DESC", "15% Chance <style=cStack>(+15% per stack)</style> on kill to gain Mantra. Once you get 10 Mantra, enter Divinity for 5 seconds. In Divinity, deal an additional 200% damage.");
			LanguageAPI.Add("DAMARU_LORE", "The sound of the small drum keeps your mind awake, revealing a path forward.");
        }
    }
}