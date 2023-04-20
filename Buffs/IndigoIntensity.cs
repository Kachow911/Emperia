using System;
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
    public class IndigoIntensity : ModBuff
    {
        public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Indigo Intensity");
			// Description.SetDefault("8% increased melee damage and life regeneration increased by 1.5");
            Main.buffNoSave[Type] = true;
            //Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
			MyPlayer p = player.GetModPlayer<MyPlayer>();
			
			player.GetDamage(DamageClass.Melee) *= 1.08f;
			player.lifeRegen += 1; //1 hp is added every 2 seconds in MyPlayer.cs
			if (Main.rand.Next(2) == 0)
			{
				int dust = Dust.NewDust(new Vector2(player.Center.X, player.Center.Y), 20, 20, DustID.VilePowder);
			}
        }
    }
}
