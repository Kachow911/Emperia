using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
{
    public class BreakingPoint : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Breaking Point");
			Tooltip.SetDefault("All weapons set enemies aflame\n");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 4;
            item.value = Item.sellPrice(0, 0, 3, 0);
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			if (player.statLife <= player.statLifeMax2 / 2)
            {
                player.moveSpeed *= 1.2f;
            }
        }
		
    }
}
