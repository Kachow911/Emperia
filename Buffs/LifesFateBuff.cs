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
    public class LifesFateBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Bloodbath");
			// Description.SetDefault("Life's Fate damage increased by 15%, sword strikes will steal life\n'You are overflowing with life'");
            Main.buffNoSave[Type] = true;
            //Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().renewedLife = true;
            if (Main.rand.Next(3) == 0)
            {
                //dust = Main.dust[Terraria.Dust.NewDust(position, 30, 51, 183, 0f, -3.4883718f, 0, new Color(255, 255, 255), 1.5116279f)];
                int dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y + 20), player.width, player.height - 20, 183, 0f, -3f, 0, default(Color), 1.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].fadeIn = 1.7f;
                //int dust = Dust.NewDust(new Vector2(player.Center.X, player.Center.Y), 20, 20, 183);
				//Main.dust[dust].velocity = Vector2.Zero;
            }
        }
    }
}
