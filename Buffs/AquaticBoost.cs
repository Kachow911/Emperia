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
    public class AquaticBoost : ModBuff
    {
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Aquatic Boost");
			Description.SetDefault("4% Increased Damage, +2 life regen");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 2;
            player.meleeDamage *= 1.04f;
            player.thrownDamage *= 1.04f;
            player.rangedDamage *= 1.04f;
            player.magicDamage *= 1.04f;
            player.minionDamage *= 1.04f;
        }
    }
}
