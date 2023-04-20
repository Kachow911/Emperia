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
    public class BerylBrutalism : ModBuff
    {
        public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Beryl Brutalism");
			// Description.SetDefault("8% increased melee speed");
            Main.buffNoSave[Type] = true;
            //Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.08f;
        }
    }
}
