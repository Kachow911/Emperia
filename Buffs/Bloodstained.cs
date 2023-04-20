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
    public class Bloodstained : ModBuff
    {
        public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Bloodstained");
			// Description.SetDefault("Your next damage taken can be healed back");
            Main.buffNoSave[Type] = true;
        }
    }
    public class Bloodstained2 : ModBuff
    {
        public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Rally!");
			// Description.SetDefault("Deal damage with sword strikes to rally back the damage taken!");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (Main.rand.Next(2) == 0)
            {
                int dust = Dust.NewDust(player.position, player.width, player.height, 183);
				Main.dust[dust].velocity = new Vector2(0, -2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1.5f;
            }
        }
    }
}
