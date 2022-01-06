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
			Tooltip.SetDefault("While under half life you will recieve boosts of healing");
		}
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 18;
            Item.rare = 3;
            Item.value = 54000;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			player.GetModPlayer<MyPlayer>().forbiddenOath = true;
		
        }
		
    }
}
