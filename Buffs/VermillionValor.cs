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
    public class VermillionValor : ModBuff
    {
        public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Vermillion Valor");
			// Description.SetDefault("13% increased critical hit damage and defense increased by 4");
            Main.buffNoSave[Type] = true;
            //Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
			MyPlayer p = player.GetModPlayer<MyPlayer>();
			p.vermillionValor = true;
			player.statDefense += 4;
			if (Main.rand.Next(2) == 0)
			{
				int dust = Dust.NewDust(new Vector2(player.Center.X, player.Center.Y), 20, 20, 158);
			}
        }
    }
}
