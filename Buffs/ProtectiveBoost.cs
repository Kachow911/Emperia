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
    public class ProtectiveBoost : ModBuff
    {
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Defensive Boost");
			Description.SetDefault("6% Increased Damage Reduction, +6 Defense");
            Main.buffNoSave[Type] = true;
            //Main.buffNoTimeDisplay[Type] = true;

            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
			player.statDefense += 6;
			player.endurance += 0.06f;
        }
    }
}
