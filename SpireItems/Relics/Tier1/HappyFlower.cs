using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class HappyFlower : Relic
    {
        public static ItemDef item;
        public override void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSPLEASEDPLANT_NAME";
            item.nameToken = "STSPLEASEDPLANT_NAME";
            item.pickupToken = "STSPLEASEDPLANT_PICKUP";
            item.descriptionToken = "STSPLEASEDPLANT_DESC";
            item.loreToken = "STSPLEASEDPLANT_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/HappyFlower.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // 10% for happy mask ghost (no damage boost, die quicker
            On.RoR2.CharacterBody.HandleOnKillEffectsServer += CharacterBody_HandleOnKillEffectsServer;

            Log.LogInfo("HappyFlower done.");
        }

        private void CharacterBody_HandleOnKillEffectsServer(On.RoR2.CharacterBody.orig_HandleOnKillEffectsServer orig, CharacterBody self, DamageReport damageReport)
        {
            orig(self, damageReport);
            var hfs = self.inventory?.GetItemCount(item.itemIndex);
            if (hfs == null || hfs < 1)
            {
                return;
            }

            var chance = hfs * 10f;
            var proc = self.master ? Util.CheckRoll((int) chance, self.master) : Util.CheckRoll((int) chance);

            if (proc)
            {
                var vb = damageReport.victimBody;
                vb.baseDamage *= 0.1f; // weaken, otherwise this is just better Happiest Mask lol
                Util.TryToCreateGhost(vb, self, 15 * (int) hfs);
            }
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STSPLEASEDPLANT_NAME", "Happy Flower");
			LanguageAPI.Add("STSPLEASEDPLANT_PICKUP", "Chance on killing an enemy to summon a weak ghost");
			LanguageAPI.Add("STSPLEASEDPLANT_DESC", "Killing enemies has a 10% Chance to spawn a ghost of the killed enemy. Lasts 15s <style=cStack>(+15s per stack)</style>.");
			LanguageAPI.Add("STSPLEASEDPLANT_LORE", "This unceasingly joyous plant is a popular novelty item among nobles.");
        }
    }
}
