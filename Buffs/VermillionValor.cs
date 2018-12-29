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
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Vermillion Valor");
			Description.SetDefault("Defense increased by 4\n2% increased crit chance\nCrits deal 13% increased damage");
            Main.buffNoSave[Type] = true;
            //Main.buffNoTimeDisplay[Type] = true;

            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
			MyPlayer p = player.GetModPlayer<MyPlayer>(mod);
			p.vermillionValor = true;
			player.statDefense += 4;
			player.meleeCrit += 2;
			player.rangedCrit += 2;
			player.magicCrit += 2;
			player.thrownCrit += 2;
			if (Main.rand.Next(2) == 0)
			{
				int dust = Dust.NewDust(new Vector2(player.Center.X, player.Center.Y), 20, 20, 158);
			}
        }
    }
}
