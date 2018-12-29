using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
{
    public class InsigniaofDefense : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Insignia of Defense");
			Tooltip.SetDefault("Attacks that deal over 150 damage have a chance to spawn 'Protective Energy'\n Protective Energy increases Defense and Damage Reduction when collected");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 5;
            item.value = Item.sellPrice(0, 2, 50, 0);
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<MyPlayer>(mod).defenseInsignia = true;
		
        }
		
    }
}
