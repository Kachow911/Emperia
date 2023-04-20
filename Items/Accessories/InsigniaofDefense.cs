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
			// DisplayName.SetDefault("Ironclad Insignia");
			// Tooltip.SetDefault("Attacks may spawn metallic energy, increasing defenses when collected\nStrong attacks and sword strikes have a higher chance");
		}
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = 5;
            Item.value = Item.sellPrice(0, 2, 50, 0);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			player.GetModPlayer<MyPlayer>().defenseInsignia = true;
        }
    }
}
