using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class SlingOfCourage : Relic
    {
        public static ItemDef item;
        public override void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "SLINGCOURAGE_NAME";
            item.nameToken = "SLINGCOURAGE_NAME";
            item.pickupToken = "SLINGCOURAGE_PICKUP";
            item.descriptionToken = "SLINGCOURAGE_DESC";
            item.loreToken = "SLINGCOURAGE_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier2;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/SlingOfCourage.png");
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
            // 20% additional elite damage.
            On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;

            Log.LogInfo("SlingOfCourage done.");
        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            if (di == null || di.rejected || !di.attacker || di.attacker == self.gameObject || di.attacker.GetComponent<CharacterBody>() == null || !self.gameObject)
            {
                orig(self, di);
                return;
            }

            var inv = di.attacker.GetComponent<CharacterBody>().inventory;
            var victimCB = self.gameObject.GetComponent<CharacterBody>();

            if  (inv && victimCB)
            {
                var victimisElite = victimCB.isElite;
                var slingCount = inv.GetItemCount(item.itemIndex);
                if (victimisElite && slingCount >= 1)
                {
                    di.damage *= 1f + (slingCount * 0.2f);
                }
            }
            orig(self, di);
        }

        private void AddTokens()
        {
			LanguageAPI.Add("SLINGCOURAGE_NAME", "Sling of Courage");
			LanguageAPI.Add("SLINGCOURAGE_PICKUP", "Deal bonus damage to Elite enemies.");
			LanguageAPI.Add("SLINGCOURAGE_DESC", "Increase damage to Elite enemies by 20% <style=cStack>(+20% per stack)</style>");
			LanguageAPI.Add("SLINGCOURAGE_LORE", "A handy tool for dealing with particularly tough opponents.");
        }
    }
}
