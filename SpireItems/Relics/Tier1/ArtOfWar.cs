using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class ArtOfWar
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "ARTOFWAR_NAME";
            item.nameToken = "ARTOFWAR_NAME";
            item.pickupToken = "ARTOFWAR_PICKUP";
            item.descriptionToken = "ARTOFWAR_DESC";
            item.loreToken = "ARTOFWAR_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/ArtOfWar.png");
            item.pickupModelPrefab = Resources.Load<GameObject>("Prefabs/PickupModels/PickupMystery");

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
			// probably something like red whip, but where staying out of combat resets cooldowns or lowers them

            Log.LogInfo("ArtOfWar done");
        }

        private void AddTokens()
        {
            LanguageAPI.Add("ARTOFWAR_NAME", "Art of War");
			LanguageAPI.Add("ARTOFWAR_PICKUP", "Leaving Combat lowers ability cooldowns");
			LanguageAPI.Add("ARTOFWAR_DESC", "");
			LanguageAPI.Add("ARTOFWAR_LORE", "This ancient manuscript contains wisdom from a past age.");
        }
    }
}
