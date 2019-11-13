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
    public class Spored : ModBuff
    {
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Spore Stimulants");
			Description.SetDefault("3% increased damage\n3% increased crit chance");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (modPlayer.sporeCount > 0)
			{
				player.buffTime[buffIndex] = 2;
			}
			player.meleeCrit += 3;
			player.magicCrit += 3;
			player.rangedCrit += 3;
			player.thrownCrit += 3;
			player.meleeDamage += 0.03f;
			player.magicDamage += 0.03f;
			player.rangedDamage += 0.03f;
			player.thrownDamage += 0.03f;
			
        }
    }
}
