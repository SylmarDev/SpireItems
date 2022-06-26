using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class PaperPhrog
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSPAPERPHROG_NAME";
            item.nameToken = "STSPAPERPHROG_NAME";
            item.pickupToken = "STSPAPERPHROG_PICKUP";
            item.descriptionToken = "STSPAPERPHROG_DESC";
            item.loreToken = "STSPAPERPHROG_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier2;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/paperPhrog.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            ItemTag[] tags = new ItemTag[] { ItemTag.Utility };
            item.tags = tags;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // vuln deals extra damage

            // I just realized this thing might be able to kill you if you proc a blood shrine while vulnerable,
            // but you'd have to be a huge dope to do that and probably deserve it so I'm not going to do anything about it

            Log.LogInfo("PaperPhrog done.");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("STSPAPERPHROG_NAME", "Paper Phrog");
            LanguageAPI.Add("STSPAPERPHROG_PICKUP", "Vulnerable enemies take more damage");
            LanguageAPI.Add("STSPAPERPHROG_DESC", "Deal 25% more<style=cStack>(+25% Per Stack)</style> damage to vulnerable enemies (Additive).");
            LanguageAPI.Add("STSPAPERPHROG_LORE", "The paper continually folds and unfolds itself into the shape of a small creature.");
        }
    }
}
