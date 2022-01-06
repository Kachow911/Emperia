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
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Spore Stimulants");
			Description.SetDefault("3% increased damage\n3% increased crit chance");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (modPlayer.sporeCount > 0)
			{
				player.buffTime[buffIndex] = 2;
			}
			player.GetCritChance(DamageClass.Generic) += 3;
			player.GetDamage(DamageClass.Generic) += 0.03f;
			
        }
    }
}
