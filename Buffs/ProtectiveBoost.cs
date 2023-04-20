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
        public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Ironclad");
			// Description.SetDefault("6% increased damage reduction, +6 defense");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
			player.statDefense += 6;
			player.endurance += 0.06f;
        }
    }
}
