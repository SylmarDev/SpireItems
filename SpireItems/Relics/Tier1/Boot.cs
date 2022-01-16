using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Boot
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "SMBTHREEBOOT_NAME";
            item.nameToken = "SMBTHREEBOOT_NAME";
            item.pickupToken = "SMBTHREEBOOT_PICKUP";
            item.descriptionToken = "SMBTHREEBOOT_DESC";
            item.loreToken = "SMBTHREEBOOT_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/Boot.png");
            item.pickupModelPrefab = Resources.Load<GameObject>("Prefabs/PickupModels/PickupMystery");

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // do bonus damage to armored enemies
            //On.RoR2.HealthComponent.TakeDamage += On_HCTakeDamage;

            Log.LogInfo("Boot done");
        }

        // THE BOOT STILL DOESN'T WORK!

        private void On_HCTakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            if (di == null || di.rejected || !di.attacker || di.attacker == self.gameObject) return;
            // all temp, boot doesn't work rn
            // one of these doesn't reference anything
            var inv = false; // di.attacker.GetComponent<HealthComponent>().body.inventory;
            int bootCount = 0; // inv.GetItemCount(item.itemIndex);


            if (inv && self.body.armor > 0)
            {
                if (bootCount >= 1)
                {
                    Log.LogMessage("stripping armor");
                    
                    self.body.SetPropertyValue<float>("armor", self.body.GetPropertyValue<float>("armor") - (1000 * bootCount)); // 10k for demo, put back at 10
                }
            }
            orig(self, di);
        }

        private void AddTokens()
        {
            LanguageAPI.Add("SMBTHREEBOOT_NAME", "The Boot");
			LanguageAPI.Add("SMBTHREEBOOT_PICKUP", "If enemy has armor, deal additional damage");
			LanguageAPI.Add("SMBTHREEBOOT_DESC", "If enemy has armor, reduce it by 10<style=cStack>(+10 per stack)</style> when calculating your damage.");
			LanguageAPI.Add("SMBTHREEBOOT_LORE", "When wound up, the boot grows larger in size.");
        }
    }
}
