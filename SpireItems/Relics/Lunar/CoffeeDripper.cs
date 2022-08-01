using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class CoffeeDripper
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STS_COFFEEDRIPPER_NAME";
            item.nameToken = "STS_COFFEEDRIPPER_NAME";
            item.pickupToken = "STS_COFFEEDRIPPER_PICKUP";
            item.descriptionToken = "STS_COFFEEDRIPPER_DESC";
            item.loreToken = "STS_COFFEEDRIPPER_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Lunar;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/CoffeeDripper.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // reduce healing by 25%, increase damage by 50%
            On.RoR2.HealthComponent.Heal += HealthComponent_Heal;
            On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;

            Log.LogInfo("CoffeeDripper done.");
        }

        private float HealthComponent_Heal(On.RoR2.HealthComponent.orig_Heal orig, HealthComponent self, float amount, ProcChainMask procChainMask, bool nonRegen)
        {
            if (self && self.body && self.body.inventory && self.body.inventory.GetItemCount(item) > 0)
            {
                for (var i = self.body.inventory.GetItemCount(item); i > 0; i--)
                {
                    amount *= 0.75f;
                }
            }
            return orig(self, amount, procChainMask, nonRegen);
        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            var flag = false;
            if (damageInfo.attacker && damageInfo.attacker.GetComponent<CharacterBody>() && 
                damageInfo.attacker.GetComponent<CharacterBody>().inventory && damageInfo.attacker.GetComponent<CharacterBody>().inventory.GetItemCount(item) > 0)
            {
                flag = true;
            } else if (damageInfo.inflictor && damageInfo.inflictor.GetComponent<CharacterBody>() &&
                damageInfo.inflictor.GetComponent<CharacterBody>().inventory && damageInfo.inflictor.GetComponent<CharacterBody>().inventory.GetItemCount(item) > 0)
            {
                flag = true;
            }

            if (flag)
            {
                for (var i = 0; i < (damageInfo.attacker != null ? damageInfo.attacker.GetComponent<CharacterBody>().inventory.GetItemCount(item) : damageInfo.inflictor.GetComponent<CharacterBody>().inventory.GetItemCount(item)); i++)
                {
                    damageInfo.damage *= 1.5f;
                }
            }
            orig(self, damageInfo);
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STS_COFFEEDRIPPER_NAME", "Coffee Dripper");
			LanguageAPI.Add("STS_COFFEEDRIPPER_PICKUP", "reduce healing by 25%, increase damage by 50%");
			LanguageAPI.Add("STS_COFFEEDRIPPER_DESC", "");
			LanguageAPI.Add("STS_COFFEEDRIPPER_LORE", "\"Yes, another cup please. Back to work. Back to work!\" -The Architect");
        }
    }
}
