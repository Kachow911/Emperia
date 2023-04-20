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
			// DisplayName.SetDefault("Vitality Crystal");
			// Tooltip.SetDefault("Healing potions heal for 25 additional HP");
		}
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = 4;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			player.GetModPlayer<MyPlayer>().vitalityCrystal = true;
		
        }
		
    }
}
