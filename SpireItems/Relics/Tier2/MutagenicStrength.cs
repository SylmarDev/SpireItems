using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;
using System;

namespace SylmarDev.SpireItems
{
    public class MutagenicStrength
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STS_STRENGTHONSTART_NAME";
            item.nameToken = "STS_STRENGTHONSTART_NAME";
            item.pickupToken = "STS_STRENGTHONSTART_PICKUP";
            item.descriptionToken = "STS_STRENGTHONSTART_DESC";
            item.loreToken = "STS_STRENGTHONSTART_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier2;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/MutagenicStrength.png");
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
            // increase damage by 30% for first minute of the stage (+30 secs per stack)
            CharacterBody.onBodyStartGlobal += CharacterBody_onBodyStartGlobal;

            Log.LogInfo("MutagenicStrength done.");
        }

        private void CharacterBody_onBodyStartGlobal(CharacterBody cb)
        {
            var inv = cb.inventory;
            if (inv != null)
            {
                var ms = cb.inventory.GetItemCount(item.itemIndex);

                if (ms > 0)
                {
                    if (ms == 1)
                    {
                        cb.AddTimedBuff(MutagenicBuff.instance.BuffDef, 60f);
                    } else
                    {
                        cb.AddTimedBuff(MutagenicBuff.instance.BuffDef, 60f + ((ms - 1) * 30f));
                    }
                }
            }
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STS_STRENGTHONSTART_NAME", "Mutagenic Strength");
			LanguageAPI.Add("STS_STRENGTHONSTART_PICKUP", "Increase damage at start of stage");
			LanguageAPI.Add("STS_STRENGTHONSTART_DESC", "Deal 30% more damage for the first 60 seconds<style=cStack>(+30 seconds per stack)</style> of a stage.");
			LanguageAPI.Add("STS_STRENGTHONSTART_LORE", "\"The results seem fleeting, triggering when the subject is in danger.\" - Unknown");
        }
    }
}
