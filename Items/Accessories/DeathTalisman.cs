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
			Tooltip.SetDefault("Critical hits inflict 'Demise of Fate'\nEnemies that die with Demise of Fate explode into fate's flames\nFlames deal additional damage based on HP of the enemy");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 4;
            item.value = 199800;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<MyPlayer>().deathTalisman = true;
		
        }
		
    }
}
