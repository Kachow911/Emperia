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
    public class GoblinsCelerity : ModBuff
    {
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Goblin's Celerity");
			Description.SetDefault("10% increased movement and melee speed");
            Main.buffNoSave[Type] = true;
            //Main.buffNoTimeDisplay[Type] = true;

            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
			player.moveSpeed += 0.1f;
			player.meleeSpeed += 0.1f;
        }
    }
}
