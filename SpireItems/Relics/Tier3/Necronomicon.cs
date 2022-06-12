using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Necronomicon
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "NECRONOMICON_NAME";
            item.nameToken = "NECRONOMICON_NAME";
            item.pickupToken = "NECRONOMICON_PICKUP";
            item.descriptionToken = "NECRONOMICON_DESC";
            item.loreToken = "NECRONOMICON_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier3;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/Necronomicon.png");
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
            // chance for attacks over 300% to hit twice
            On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;

            Log.LogInfo("Necronomicon done.");
        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            orig(self, di);
            if (di == null || di.rejected || !di.attacker || di.attacker == self.gameObject || di.attacker.GetComponent<HealthComponent>() == null || di.attacker.GetComponent<HealthComponent>().body == null)
            {
                return;
            }

            var inv = di.attacker.GetComponent<HealthComponent>().body.inventory;

            if (inv && di.damage >= di.attacker.GetComponent<CharacterBody>().damage * 4)
            {
                var necroCount = inv.GetItemCount(item.itemIndex);
                if (necroCount >= 1)
                {
                    for (var i = 0; i < necroCount; i++) // hit for every necronomicon
                    {
                        orig(self, di);
                        GlobalEventManager.instance.OnHitEnemy(di, self.gameObject);
                    }
                }
            }
        }

        private void AddTokens()
        {
			LanguageAPI.Add("NECRONOMICON_NAME", "Necronomicon");
			LanguageAPI.Add("NECRONOMICON_PICKUP", "Strong attacks hit twice.");
			LanguageAPI.Add("NECRONOMICON_DESC", "Hits that deal more than 400% damage hit an extra time. <style=cStack>(+1 time per stack)</style>");
			LanguageAPI.Add("NECRONOMICON_LORE", "Only a fool would try to harness this evil power. At night your dreams are haunted by images of the book devouring your mind.");
        }
    }
}
