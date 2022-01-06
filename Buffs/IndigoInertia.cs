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
    public class IndigoInertia : ModBuff
    {
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Indigo Inertia");
			Description.SetDefault("Life regeneration increased by 1");
            Main.buffNoSave[Type] = true;
            //Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 1;
        }
    }
}
