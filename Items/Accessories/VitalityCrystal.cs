using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
{
    public class VitalityCrystal : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vitality Crystal");
			Tooltip.SetDefault("Healing potions heal for 25 additional HP");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 4;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<MyPlayer>().vitalityCrystal = true;
		
        }
		
    }
}
