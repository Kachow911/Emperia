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
    public class RougeRage : ModBuff
    {
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Rouge Rage");
			Description.SetDefault("10% increased critical hit damage");
            Main.buffNoSave[Type] = true;
            //Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
			MyPlayer p = player.GetModPlayer<MyPlayer>();
			p.rougeRage = true;
        }
    }
}
