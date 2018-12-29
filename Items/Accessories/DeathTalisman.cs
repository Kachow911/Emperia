using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
{
    public class DeathTalisman : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Talisman of Death");
			Tooltip.SetDefault("Critical hits inflict 'Fate's Demise'\nEnemies that die with Fate's Demise explode into fate's flames\nFlames deal additional damage based on HP of the enemy");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 4;
            item.value = Item.sellPrice(0, 1, 50, 0);
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<MyPlayer>(mod).deathTalisman = true;
		
        }
		
    }
}
