using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;

namespace SylmarDev.SpireItems
{
    public class BloodIdol
    {
        public static ItemDef item;
        public void Init()
        {
            // init
            item = ScriptableObject.CreateInstance<ItemDef>();

            // Tokens
            item.name = "STSBLOODIDOL_NAME";
            item.nameToken = "STSBLOODIDOL_NAME";
            item.pickupToken = "STSBLOODIDOL_PICKUP";
            item.descriptionToken = "STSBLOODIDOL_DESC";
            item.loreToken = "STSBLOODIDOL_LORE";

            // tier
            ItemTierDef itd = new ItemTierDef();
            itd.tier = ItemTier.Tier2;
            item._itemTierDef = itd;

            // display info (need assetbundle to create unique texture)
            item.pickupIconSprite = SpireItems.resources.LoadAsset<Sprite>("assets/SpireRelics/textures/icons/item/BloodyIdol.png");
            item.pickupModelPrefab = SpireItems.cardPrefab;

            // standard
            item.canRemove = true;
            item.hidden = false;

            ItemTag[] tags = new ItemTag[] { ItemTag.WorldUnique, ItemTag.Healing };
            item.tags = tags;

            // Turn Tokens into strings
            AddTokens();

            var displayRules = new ItemDisplayRuleDict(null); // I can't do 3D

            ItemAPI.Add(new CustomItem(item, displayRules));

            // define what item does below
            // Whenever you gain gold, heal 5 HP
            //On.RoR2.ShrineBloodBehavior.Start += ShrineBloodBehavior_Start; // debug
            On.RoR2.ShrineBloodBehavior.AddShrineStack += ShrineBloodBehavior_AddShrineStack;
            On.RoR2.CharacterMaster.GiveMoney += CharacterMaster_GiveMoney;

            Log.LogInfo("BloodIdol done.");
        }

        private void ShrineBloodBehavior_Start(On.RoR2.ShrineBloodBehavior.orig_Start orig, ShrineBloodBehavior self)
        {
            Log.LogInfo("Blood Shrine started oomfie!!");
            orig(self);
        }

        private void ShrineBloodBehavior_AddShrineStack(On.RoR2.ShrineBloodBehavior.orig_AddShrineStack orig, ShrineBloodBehavior self, Interactor interactor)
        {
            orig(self, interactor);
            var cb = interactor.GetComponent<CharacterBody>();
            if (cb)
            {
                var inv = cb.inventory;
                if (inv)
                {
                    var ic = inv.GetItemCount(GoldenIdol.item.itemIndex);
                    if (ic >= 1)
                    {
                        inv.RemoveItem(GoldenIdol.item, ic);
                        inv.GiveItem(item.itemIndex, ic);

                        var bt = "";
                        if (ic == 1)
                        {
                            bt = "<style=cEvent>Your <color=#FFC733>golden idol</color> begins to dull in color and begins bleeding from its eyes. The bleeding never ceases.</style>";
                        } else
                        {
                            bt = "<style=cEvent>Your <color=#FFC733>golden idols</color> begin to dull in color and begin bleeding from their eyes. The bleeding never ceases.</style>";
                        }

                        Chat.SendBroadcastChat(new Chat.SubjectFormatChatMessage
                        {
                            baseToken = bt
                        });
                    }
                }
            }
        }

        private void CharacterMaster_GiveMoney(On.RoR2.CharacterMaster.orig_GiveMoney orig, CharacterMaster self, uint amount)
        {
            orig(self, amount);
            var cb = self.GetBody();
            if (cb)
            {
                var inv = cb.inventory;
                if (inv)
                {
                    var ic = inv.GetItemCount(item.itemIndex);
                    if (ic >= 1)
                    {
                        cb.healthComponent.Heal(5f * ic, default);
                    }
                }
            }
        }

        private void AddTokens()
        {
			LanguageAPI.Add("STSBLOODIDOL_NAME", "Bloody Idol");
			LanguageAPI.Add("STSBLOODIDOL_PICKUP", "Whenever you gain gold, heal 5 HP.");
			LanguageAPI.Add("STSBLOODIDOL_DESC", "Every time you gain gold, <style=cIsHealing>heal for 5 HP</style> <style=cStack>(+5 per stack)</style> ");
			LanguageAPI.Add("STSBLOODIDOL_LORE", "The idol now weeps a constant stream of blood.");
        }
    }
}
