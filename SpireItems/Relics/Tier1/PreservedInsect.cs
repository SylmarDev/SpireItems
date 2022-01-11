using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class PreservedInsect
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "PERSERVEDINSECT_NAME";
            item.nameToken = "PERSERVEDINSECT_NAME";
            item.pickupToken = "PERSERVEDINSECT_PICKUP";
            item.descriptionToken = "PERSERVEDINSECT_DESC";
            item.loreToken = "PERSERVEDINSECT_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
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
			// tele bosses spawn at only 75 HP, or elite monsters only spawn at 75% HP. Not sure, but either way it'll have crazy negative synergy with crowbar and akabeko, other "first hit" items

            Log.LogInfo("PreservedInsect done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("PERSERVEDINSECT_NAME", "Preserved Insect");
			LanguageAPI.Add("PERSERVEDINSECT_PICKUP", "Boss enemies spawn at only 75% of their HP");
			LanguageAPI.Add("PERSERVEDINSECT_DESC", "");
			LanguageAPI.Add("PERSERVEDINSECT_LORE", "The insect seems to create a shrinking aura that targets particularly large enemies.");
        }
    }
}
