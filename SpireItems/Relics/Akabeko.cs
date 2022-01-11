using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class Akabeko
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "AKABEKO_NAME";
            item.nameToken = "AKABEKO_NAME";
            item.pickupToken = "AKABEKO_PICKUP";
            item.descriptionToken = "AKABEKO_DESC";
            item.loreToken = "AKABEKO_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture
            item.pickupIconSprite = Resources.Load<Sprite>("Textures/MiscIcons/texMysteryIcon");
            item.pickupModelPrefab = Resources.Load<GameObject>("Prefabs/PickupModels/PickupMystery");

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            On.RoR2.HealthComponent.TakeDamage += On_HCTakeDamage;
         

            Log.LogInfo("Akabeko done.");
        }

        private void GlobalEventManager_onServerDamageDealt(DamageReport report)
        {
            if (!report.attacker || !report.attackerBody)
            {
                return;
            }

            Log.LogMessage("in Akabeko specific onhit");

            var inv = report.attackerBody.inventory;
            var victimMaxHP = report.victimBody.levelMaxHealth;
            var victimHP = report.victim.health;

            if (inv)
            {
                int akabekoCount = inv.GetItemCount(item.itemIndex);
                if (akabekoCount > 0 && victimHP > (victimMaxHP * 0.95f))
                {
                    Log.LogMessage("Akabeko proc'd!");
                    report.damageInfo.damage *= 1000.16f; // please work

                }
            }
        }

       
        private void On_HCTakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            Log.LogMessage("in Akabeko specific onhit");

            if (di == null || di.rejected || !di.attacker || di.attacker == self.gameObject) return;

            var inv = di.attacker.GetComponent<HealthComponent>().body.inventory;
            
            Log.LogMessage(inv.ToString());
            Log.LogMessage(self.health.ToString());
            Log.LogMessage(self.fullHealth.ToString());
            if (inv && (self.health > (self.fullHealth * 0.95)))
            {
                int akabekoCount = inv.GetItemCount(item.itemIndex);
                if (akabekoCount > 0)
                {
                    Log.LogMessage("Akabeko proc'd!");
                    di.damage *= 1.16f; // temp 100
                }
            }
            orig(self, di);
        }

        //This function adds the tokens from the item using LanguageAPI, the comments in here are a style guide, but is very opiniated. Make your own judgements!
        private void AddTokens()
        {
            //The Name should be self explanatory
            LanguageAPI.Add("AKABEKO_NAME", "Akabeko");

            //The Pickup is the short text that appears when you first pick this up. This text should be short and to the point, numbers are generally ommited.
            LanguageAPI.Add("AKABEKO_PICKUP", "Your first Attack on an Enemy deals additional damage");

            //The Description is where you put the actual numbers and give an advanced description.
            // LanguageAPI.Add("AKABEKO_DESC", "Whenever you <style=cIsDamage>kill an enemy</style>, you have a <style=cIsUtility>5%</style> chance to cloak for <style=cIsUtility>4s</style> <style=cStack>(+1s per stack)</style>.");
            LanguageAPI.Add("AKABEKO_DESC", "Whenever you <style=cIsDamage>kill an enemy</style>, you have a <style=cIsUtility>5%</style> chance to cloak for <style=cIsUtility>4s</style> <style=cStack>(+1s per stack)</style>.");

            //The Lore is, well, flavor. You can write pretty much whatever you want here.
            LanguageAPI.Add("AKABEKO_LORE", "Muuu~");
        }
    }
}
