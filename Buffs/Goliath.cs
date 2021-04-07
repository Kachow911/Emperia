using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Emperia.Buffs
{
    public class Goliath : ModBuff
    {
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Goliath");
			Description.SetDefault("20% increased sword size");
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
        }
    }
}
