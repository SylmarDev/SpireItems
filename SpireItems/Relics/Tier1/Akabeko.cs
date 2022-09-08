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
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;


            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/akabeko.png");
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
            On.RoR2.HealthComponent.TakeDamage += On_HCTakeDamage;

            Log.LogInfo("Akabeko done.");
        }

        private void On_HCTakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
        {
            if (di == null || di.rejected || !di.attacker || di.attacker == self.gameObject)
            {
                orig(self, di);
                return;
            }

            var cb = di.attacker.GetComponent<CharacterBody>();

            if (cb)
            {
                var inv = di.attacker.GetComponent<HealthComponent>().body.inventory;
                if (inv && (self.health > (self.fullCombinedHealth * 0.95)))
                {
                    int akabekoCount = inv.GetItemCount(item.itemIndex);
                    if (akabekoCount == 1)
                    {
                        //Log.LogMessage("Akabeko proc'd!");
                        di.damage *= 1.85f;
                    }
                    else if (akabekoCount >= 2)
                    {
                        //Log.LogMessage("Akabeko proc'd! (2 or more!)");
                        di.damage *= 1.85f + (0.65f * (akabekoCount - 1));
                    }
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
            LanguageAPI.Add("AKABEKO_PICKUP", "Deal bonus damage to enemies above 95% health.");

            //The Description is where you put the actual numbers and give an advanced description.
            // LanguageAPI.Add("AKABEKO_DESC", "Whenever you <style=cIsDamage>kill an enemy</style>, you have a <style=cIsUtility>5%</style> chance to cloak for <style=cIsUtility>4s</style> <style=cStack>(+1s per stack)</style>.");
            LanguageAPI.Add("AKABEKO_DESC", "Deal +85% <style=cStack>(+65% per stack)</style> damage to enemies above 95% health.");

            //The Lore is, well, flavor. You can write pretty much whatever you want here.
            LanguageAPI.Add("AKABEKO_LORE", "Muuu~");
        }
    }
}
