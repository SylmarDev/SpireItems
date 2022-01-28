﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SylmarDev.SpireItems
{
    public class OrichalcumItemBehavior : RoR2.CharacterBody.ItemBehavior
    {
        // ghor said himself they're moving this to its own namespace in like.. a month. So this mod is not long for this world
        public void FixedUpdate()
        {
            int stack = this.stack;
            bool flag = stack > 0 && this.body.GetNotMoving();
            if (flag)
            {
                var interval = 0.25f;
                var healAmount = (0.006f * stack) * this.body.maxBarrier * interval;
                //Log.LogMessage($"we be orichalcuming for {healAmount} health per quarter second");
                body.healthComponent.AddBarrier(healAmount);
            }
        }
    }
}
