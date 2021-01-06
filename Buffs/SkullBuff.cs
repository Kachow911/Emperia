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
    public class SkullBuff : ModBuff
    {
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Unveiled Death");
			Description.SetDefault("Next contact damage taken will be halved");
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
        }
    }
}
