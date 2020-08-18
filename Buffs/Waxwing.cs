using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Emperia.Buffs
{
    public class Waxwing : ModBuff
    {
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Waxwing");
			Description.SetDefault("25% increased wing speed, but 10% decreased flight time");
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
        }
    }
}
