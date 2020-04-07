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
    public class Supercharged : ModBuff
    {
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Supercharged!");
			Description.SetDefault("20% increased movement speed and 10% increased damage");
            Main.buffNoSave[Type] = true;
            //Main.buffNoTimeDisplay[Type] = true;

            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
			MyPlayer p = player.GetModPlayer<MyPlayer>();
			
			player.moveSpeed *= 1.20f;
            player.meleeDamage *= 1.10f;
            player.thrownDamage *= 1.10f;
            player.rangedDamage *= 1.10f;
            player.magicDamage *= 1.10f;
            player.minionDamage *= 1.10f;
        }
    }
}
