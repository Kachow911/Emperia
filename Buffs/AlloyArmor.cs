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
    public class AlloyArmor : ModBuff
    {
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Alloy Armor");
			Description.SetDefault("Defense increased by 4");
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 4;
        }
    }
}
