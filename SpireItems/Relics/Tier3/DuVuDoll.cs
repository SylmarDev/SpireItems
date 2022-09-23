using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class DuVuDoll : Relic
    {
        public static ItemDef item;
        public override void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STS_VOIDDOLL_NAME";
            item.nameToken = "STS_VOIDDOLL_NAME";
            item.pickupToken = "STS_VOIDDOLL_PICKUP";
            item.descriptionToken = "STS_VOIDDOLL_DESC";
            item.loreToken = "STS_VOIDDOLL_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier3;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/duvu.png");
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
            // 10% damage buff for every void item
            On.RoR2.HealthComponent.TakeDamage += On_HCTakeDamage;

            Log.LogInfo("DuVuDoll done.");
        }

        private void On_HCTakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            if (di == null || di.rejected || !di.attacker || di.attacker == self.gameObject || di.attacker.GetComponent<HealthComponent>() == null || di.attacker.GetComponent<HealthComponent>().body == null)
            {
                orig(self, di);
                return;
            }

            var inv = di.attacker.GetComponent<HealthComponent>().body.inventory;

            if (inv)
            {
                int dollCount = inv.GetItemCount(item.itemIndex);
                
                if (dollCount >= 1)
                {
                    var voidItemCount = inv.GetTotalItemCountOfTier(ItemTier.VoidTier1) +
                       inv.GetTotalItemCountOfTier(ItemTier.VoidTier2) +
                       inv.GetTotalItemCountOfTier(ItemTier.VoidTier3) +
                       inv.GetTotalItemCountOfTier(ItemTier.VoidBoss);

                    //Log.LogMessage("duvu proc'd!");
                    di.damage *= 1f + (0.1f * dollCount * voidItemCount);
                }
            }
            orig(self, di);
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STS_VOIDDOLL_NAME", "Du-Vu Doll");
			LanguageAPI.Add("STS_VOIDDOLL_PICKUP", "Deal more damage for every void item you have");
			LanguageAPI.Add("STS_VOIDDOLL_DESC", "Increase damage to enemies by 10% <style=cStack>(+10% per void item)</style>. <style=cStack>Additional stacks multiply damage.</style>");
			LanguageAPI.Add("STS_VOIDDOLL_LORE", "A doll devised to gain strength from malicious energy.");
        }
    }
}
