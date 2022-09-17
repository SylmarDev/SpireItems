using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class NeowsLament : Relic
    {
        public static ItemDef item;
        public override void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "NEOWSLAMENT_NAME";
            item.nameToken = "NEOWSLAMENT_NAME";
            item.pickupToken = "NEOWSLAMENT_PICKUP";
            item.descriptionToken = "NEOWSLAMENT_DESC";
            item.loreToken = "NEOWSLAMENT_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier2;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/NeowsLament.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            ItemTag[] tags = new ItemTag[] { ItemTag.Damage, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist };
            item.tags = tags;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // small chance to instantly kill enemy on hit
            On.RoR2.GlobalEventManager.OnHitEnemy += GlobalEventManager_OnHitEnemy;

            Log.LogInfo("NeowsLament done.");
        }

        private void GlobalEventManager_OnHitEnemy(On.RoR2.GlobalEventManager.orig_OnHitEnemy orig, GlobalEventManager self, DamageInfo damageInfo, GameObject victim)
        {
            if (damageInfo.attacker == null || damageInfo.attacker.GetComponent<CharacterBody>().inventory == null || damageInfo.rejected || victim.GetComponent<HealthComponent>() == null)
            {
                orig(self, damageInfo, victim);
                return;
            }

            var cb = damageInfo.attacker.GetComponent<HealthComponent>().body;
            var inv = cb.inventory;

            if (inv)
            {
                if (inv.GetItemCount(item.itemIndex) >= 1)
                {
                    var procChance = Mathf.Min(inv.GetItemCount(item.itemIndex) * 0.5f, 1.5f);
                    var proc = cb.master ? Util.CheckRoll(procChance, cb.master) : Util.CheckRoll(procChance);
                    if (proc)
                    {
                        // Log.LogMessage("neow's lament proced, starting death protocol");
                        DamageInfo instaKill = damageInfo;
                        instaKill.damageColorIndex = DamageColorIndex.DeathMark;
                        instaKill.damage = victim.GetComponent<HealthComponent>().fullCombinedHealth * 5; // just to be sure
                        victim.GetComponent<HealthComponent>().TakeDamage(instaKill);
                    }
                }
            }
            orig(self, damageInfo, victim);
        }

        private void AddTokens()
        {
			LanguageAPI.Add("NEOWSLAMENT_NAME", "Neow's Lament");
			LanguageAPI.Add("NEOWSLAMENT_PICKUP", "Small chance to instantly kill enemy on hit. Stacks up to 3 times.");
			LanguageAPI.Add("NEOWSLAMENT_DESC", "0.5% chance to instantly kill enemy on hit. Stacks up to 1.5% chance.");
			LanguageAPI.Add("NEOWSLAMENT_LORE", "The blessing of lamentation bestowed by Neow.");
        }
    }
}
