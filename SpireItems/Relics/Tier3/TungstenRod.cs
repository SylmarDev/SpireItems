using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class TungstenRod
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STS_TUNGSTEN_NAME";
            item.nameToken = "STS_TUNGSTEN_NAME";
            item.pickupToken = "STS_TUNGSTEN_PICKUP";
            item.descriptionToken = "STS_TUNGSTEN_DESC";
            item.loreToken = "STS_TUNGSTEN_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier3;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/TungstenRod.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // 5-7.5 repulsions, can't decide
            On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;

            Log.LogInfo("TungstenRod done.");
        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            if (di == null || di.rejected || !di.attacker || di.attacker == self.gameObject)
            {
                orig(self, di);
                return;
            }

            var cb = self.body;
            if (cb)
            {
                var inv = cb.inventory;
                if (inv && inv.GetItemCount(item) >= 1)
                {
                    var ic = inv.GetItemCount(item);
                    di.damage = Mathf.Max(di.damage - (ic * 37.5f), 1f);
                }
            }
            orig(self, di);
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STS_TUNGSTEN_NAME", "Tungsten Rod");
			LanguageAPI.Add("STS_TUNGSTEN_PICKUP", "Receive a large flat damage reduction from all attacks.");
			LanguageAPI.Add("STS_TUNGSTEN_DESC", "Reduce all incoming damage by 38 <style=cStack>(+38 per stack)</style>. Cannot be reduced below 1.");
			LanguageAPI.Add("STS_TUNGSTEN_LORE", "It's very very heavy.");
        }
    }
}
