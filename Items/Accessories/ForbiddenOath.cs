using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
{
    public class ForbiddenOath : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Forbidden Oath");
			Tooltip.SetDefault("While under 40% HP you will recieve boosts of healing");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 3;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<MyPlayer>().forbiddenOath = true;
		
        }
		
    }
}
