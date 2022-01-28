using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Vajra
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "VAJRA_NAME";
            item.nameToken = "VAJRA_NAME";
            item.pickupToken = "VAJRA_PICKUP";
            item.descriptionToken = "VAJRA_DESC";
            item.loreToken = "VAJRA_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/Vajra.png");
            item.pickupModelPrefab = Resources.Load<GameObject>("Prefabs/PickupModels/PickupMystery");

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // flat damage buff
            On.RoR2.HealthComponent.TakeDamage += On_HCTakeDamage;

            Log.LogInfo("Vajra done");
        }

        private void On_HCTakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            if (di == null || di.rejected || !di.attacker || di.attacker == self.gameObject) return;

            var inv = di.attacker.GetComponent<HealthComponent>().body.inventory;

            if (inv)
            {
                int vajraCount = inv.GetItemCount(item.itemIndex);
                if (vajraCount >= 1)
                {
                    Log.LogMessage("vajra proc'd!");
                    di.damage *= 1f + (0.1f * vajraCount);
                }
            }
            orig(self, di);
        }

        private void AddTokens()
        {
            LanguageAPI.Add("VAJRA_NAME", "Vajra");
			LanguageAPI.Add("VAJRA_PICKUP", "Deal more damage");
			LanguageAPI.Add("VAJRA_DESC", "Increase damage to enemies by 10% <style=cStack>(+10% per stack)</style>");
			LanguageAPI.Add("VAJRA_LORE", "An ornamental relic given to warriors displaying glory in battle.");
        }
    }
}
