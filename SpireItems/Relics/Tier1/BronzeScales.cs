using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class BronzeScales
    {
        public static ItemDef item;
        public bool isThorning = true;
        DamageInfo damageInfo = new DamageInfo();
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "THORNSCALES_NAME";
            item.nameToken = "THORNSCALES_NAME";
            item.pickupToken = "THORNSCALES_PICKUP";
            item.descriptionToken = "THORNSCALES_DESC";
            item.loreToken = "THORNSCALES_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/BronzeScales.png");
            item.pickupModelPrefab = Resources.Load<GameObject>("Prefabs/PickupModels/PickupMystery");

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define damageInfo
            damageInfo.inflictor = null;
            damageInfo.damageType = (DamageType.BypassArmor | DamageType.Silent);
            damageInfo.damageColorIndex = DamageColorIndex.Default;
            damageInfo.procCoefficient = 0f; // no crazy procs sadge
            damageInfo.rejected = false;
            damageInfo.crit = false;


            // define what item does below
            // deal damage back to enemies hitting you
            On.RoR2.HealthComponent.TakeDamage += On_HCTakeDamage;
            Log.LogInfo("BronzeScales done");
        }

        private void On_HCTakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            orig(self, di);

            if (di == null || di.rejected || !di.attacker || di.attacker == self.gameObject || !isThorning)
            {
                isThorning = true;
                return;
            }

            var thornCount = self.GetComponent<CharacterBody>().inventory.GetItemCount(item.itemIndex); // checks victims inventory since, you know
            var attackerThorns = di.attacker.GetComponent<CharacterBody>().inventory.GetItemCount(item.itemIndex); // doing this to stop a theorhetical thorn loop crash
            if (thornCount >= 1 && attackerThorns == 0)
            {
                isThorning = false;
                damageInfo.attacker = self.body.gameObject;
                damageInfo.damage = self.body.damage * 0.5f * thornCount; // worth 50% attack I hope maybe
                Log.LogMessage("using thorns!");
                Log.LogMessage(damageInfo.damage);
                di.attacker.GetComponent<HealthComponent>().TakeDamage(damageInfo);
                // damage the attacker (di.attacker)
            }
        }

        private void AddTokens()
        {
            LanguageAPI.Add("THORNSCALES_NAME", "Bronze Scales");
			LanguageAPI.Add("THORNSCALES_PICKUP", "When hit by an enemy, deal damage back");
			LanguageAPI.Add("THORNSCALES_DESC", "When hit, deal back damage worth 50%<style=cStack>(+50% per stack)</style> of your damage.");
			LanguageAPI.Add("THORNSCALES_LORE", "The sharp scales of the Guardian. Rearranges itself to protect its user.");
        }
    }
}
