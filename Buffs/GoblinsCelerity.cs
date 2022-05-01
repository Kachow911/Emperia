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
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Goblin's Celerity");
			Description.SetDefault("10% increased movement and melee speed");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
			player.moveSpeed += 0.1f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
        }
    }
}
