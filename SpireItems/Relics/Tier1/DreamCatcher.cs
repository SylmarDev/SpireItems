using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using System.Collections.Generic;
using IL.RoR2.UI;

namespace SylmarDev.SpireItems
{
    public class DreamCatcher : Relic
    {
        public static ItemDef item;
        public static Xoroshiro128Plus rng = null;
        public static PickupDropTable dropTable = null;
        public static GameObject voidPotentialPrefab = null;
        public static int catchers;

        public override void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSDREAMCATCHER_NAME";
            item.nameToken = "STSDREAMCATCHER_NAME";
            item.pickupToken = "STSDREAMCATCHER_PICKUP";
            item.descriptionToken = "STSDREAMCATCHER_DESC";
            item.loreToken = "STSDREAMCATCHER_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier1;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/DreamCatcher.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // save prefab location
            voidPotentialPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/OptionPickup/OptionPickup.prefab").WaitForCompletion();

            // define what item does below
            // 10% for each item to be a void potential
            On.RoR2.Run.Start += Run_Start;
            On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged;
            On.RoR2.ChestBehavior.ItemDrop += ChestBehavior_ItemDrop;

            Log.LogInfo("DreamCatcher done.");
        }

        private void Run_Start(On.RoR2.Run.orig_Start orig, Run self)
        {
            catchers = 0;
            orig(self);
        }

        private void CharacterBody_OnInventoryChanged(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
        {
            orig(self);
            var num = 0;

            if (PlayerCharacterMasterController.instances.Count == 0)
            {
                return;
            }

            for (var i = 0; i < PlayerCharacterMasterController.instances.Count; i++)
            {
                var cb = CharacterMaster.readOnlyInstancesList[i].GetBody();
                if (cb == null)
                {
                    return;
                }
                var inv = cb.inventory;
                if (inv == null)
                {
                    return;
                }
                num += inv.GetItemCount(item.itemIndex);
            }
            catchers = num;
        }

        private void ChestBehavior_ItemDrop(On.RoR2.ChestBehavior.orig_ItemDrop orig, ChestBehavior self)
        {
            rng = self.GetFieldValue<Xoroshiro128Plus>("rng");
            dropTable = self.dropTable;
            On.RoR2.PickupDropletController.CreatePickupDroplet_PickupIndex_Vector3_Vector3 += CreatePickupDroplet_BasicPickupDropTable;
            orig(self);
            rng = null;
            dropTable = null;
            On.RoR2.PickupDropletController.CreatePickupDroplet_PickupIndex_Vector3_Vector3 -= CreatePickupDroplet_BasicPickupDropTable;
        }

        private void CreatePickupDroplet_BasicPickupDropTable(On.RoR2.PickupDropletController.orig_CreatePickupDroplet_PickupIndex_Vector3_Vector3 orig, PickupIndex pickupIndex, Vector3 position, Vector3 velocity)
        {
            // dream catcher will now stack with everyone in the lobby
            var playerCount = PlayerCharacterMasterController.instances.Count;

            if (dropTable == null || playerCount == 0)
            {
                if (playerCount == 0)
                {
                    Log.LogError("No valid players? Idk man");
                }
                else
                {
                    Log.LogError("Droptable is null, this is the wrong hook!");
                }

                rng = null;
                On.RoR2.PickupDropletController.CreatePickupDroplet_PickupIndex_Vector3_Vector3 -= CreatePickupDroplet_BasicPickupDropTable;
                orig(pickupIndex, position, velocity);
            }

            // check chance
            var chance = catchers * 10f;
            var proc = Util.CheckRoll(chance);

            if (proc)
            {
                var tier = pickupIndex.pickupDef.itemTier;
                PickupIndex[] choices = null;
                PickupIndex[] choices2 = null;
                var n = 2; // 3 choices

                choices = new PickupIndex[n + 1];
                choices2 = dropTable.GenerateUniqueDrops(n, rng);
                dropTable.canDropBeReplaced = true;
                dropTable.InvokeMethod("Regenerate", Run.instance);

                choices[0] = pickupIndex;
                for (int i = 0; i < n; i++)
                {
                    choices[i + 1] = choices2[i];
                }
                GenericPickupController.CreatePickupInfo pickupInfo = new GenericPickupController.CreatePickupInfo
                {
                    pickerOptions = PickupPickerController.GenerateOptionsFromArray(choices),
                    position = position,
                    rotation = Quaternion.identity,
                    prefabOverride = voidPotentialPrefab,
                    pickupIndex = pickupIndex
                };
                PickupDropletController.CreatePickupDroplet(pickupInfo, position, velocity);
            } else
            {
                //On.RoR2.PickupDropletController.CreatePickupDroplet_PickupIndex_Vector3_Vector3 -= CreatePickupDroplet_BasicPickupDropTable; // might not need you
                orig(pickupIndex, position, velocity);
            }
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STSDREAMCATCHER_NAME", "Dream Catcher");
			LanguageAPI.Add("STSDREAMCATCHER_PICKUP", "Chance items from chests will be Void Potentials");
			LanguageAPI.Add("STSDREAMCATCHER_DESC", "10% Chance <style=cStack>(+10% per stack)</style> when opening a chest, that it is a void potential.");
			LanguageAPI.Add("STSDREAMCATCHER_LORE", "The northern tribes would often use dream catchers at night, believing they led to self improvement.");
        }
    }
}
