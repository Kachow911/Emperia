﻿using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Emperia.Buffs
{
    public class Goliath : ModBuff
    {
        public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Goliath");
			// Description.SetDefault("20% increased sword size and 10% increased sword damage");
            Main.buffNoSave[Type] = true;
        }
    }
}
