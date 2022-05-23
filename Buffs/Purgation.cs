using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Emperia.Buffs
{
    public class Purgation : ModBuff
    {
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Purgation");
			Description.SetDefault("Shows the location of infectious blocks");
        }
    }
}
