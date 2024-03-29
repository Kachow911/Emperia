﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Emperia.Buffs
{
    public class LimeLegerity : ModBuff
    {
        public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Lime Legerity");
			// Description.SetDefault("20% increased movement speed and 10% increased melee speed");
            Main.buffNoSave[Type] = true;
            //Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
			MyPlayer p = player.GetModPlayer<MyPlayer>();
			
			player.moveSpeed *= 1.20f;
            player.GetAttackSpeed(DamageClass.Melee) *= 1.10f;
			if (Main.rand.Next(2) == 0)
			{
				int dust = Dust.NewDust(new Vector2(player.Center.X, player.Center.Y), 20, 20, DustID.CursedTorch);
			}
        }
    }
}
