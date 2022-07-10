using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class RedSkull
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSREDSKULL_NAME";
            item.nameToken = "STSREDSKULL_NAME";
            item.pickupToken = "STSREDSKULL_PICKUP";
            item.descriptionToken = "STSREDSKULL_DESC";
            item.loreToken = "STSREDSKULL_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/RedSkull.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // if you're below 50% hp deal more damage
            On.RoR2.HealthComponent.TakeDamage += On_HCTakeDamage;

            Log.LogInfo("RedSkull done.");
        }

        private void On_HCTakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            if (di == null || di.rejected || !di.attacker || di.attacker == self.gameObject || di.attacker.GetComponent<HealthComponent>() == null || di.attacker.GetComponent<HealthComponent>().body == null)
            {
                orig(self, di);
                return;
            }

            var hc = di.attacker.GetComponent<HealthComponent>();
            var lessThanHalf = hc.fullHealth / 2 >= hc.combinedHealth;
            var inv = hc.body.inventory;

            if (inv)
            {
                int rsCount = inv.GetItemCount(item.itemIndex);
                if (rsCount >= 1 && lessThanHalf)
                {
                    //Log.LogMessage("under 50%, dealing additional damage!");
                    di.damage *= 1f + (0.3f * rsCount);
                }
            }
            orig(self, di);
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STSREDSKULL_NAME", "Red Skull");
			LanguageAPI.Add("STSREDSKULL_PICKUP", "Deal more damage while low health");
			LanguageAPI.Add("STSREDSKULL_DESC", "Increase damage to enemies by 30% <style=cStack>(+30% per stack)</style> while your health is below 50%");
			LanguageAPI.Add("STSREDSKULL_LORE", "A small skull covered in ornamental paint.");
        }
    }
}
