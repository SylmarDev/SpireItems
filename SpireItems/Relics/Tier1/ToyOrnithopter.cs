using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class ToyOrnithopter
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "TOYORNITHOPTER_NAME";
            item.nameToken = "TOYORNITHOPTER_NAME";
            item.pickupToken = "TOYORNITHOPTER_PICKUP";
            item.descriptionToken = "TOYORNITHOPTER_DESC";
            item.loreToken = "TOYORNITHOPTER_LORE";

            // tier
            item.tier = ItemTier.Tier1;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/toyornothopter.png");
            item.pickupModelPrefab = Resources.Load<GameObject>("Prefabs/PickupModels/PickupMystery");

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // heal % when using equipment
            On.RoR2.EquipmentSlot.PerformEquipmentAction += EquipmentSlot_PerformEquipmentAction;

            Log.LogInfo("ToyOrnithopter done");
        }


        // this doesn't work and blood shrines are still bugged, those are the next two things
        private bool EquipmentSlot_PerformEquipmentAction(On.RoR2.EquipmentSlot.orig_PerformEquipmentAction orig, EquipmentSlot self, EquipmentDef equipmentDef)
        {
            orig(self, equipmentDef);
            var cb = self.characterBody;
            if (cb.inventory)
            {
                var oc = cb.inventory.GetItemCount(item.itemIndex);
                cb.healthComponent.Heal(0.2f * oc, default(ProcChainMask));
            }
            return true;
        }

        private void AddTokens()
        {
            LanguageAPI.Add("TOYORNITHOPTER_NAME", "Toy Ornithopter");
			LanguageAPI.Add("TOYORNITHOPTER_PICKUP", "Heal after using your equipment");
			LanguageAPI.Add("TOYORNITHOPTER_DESC", "");
			LanguageAPI.Add("TOYORNITHOPTER_LORE", "This little toy is the perfect companion for the lone adventurer!");
        }
    }
}
