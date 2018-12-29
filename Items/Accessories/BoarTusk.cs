using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
{
    public class BoarTusk : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boar's Tusk");
			Tooltip.SetDefault("6% increased damage and movement speed\nYou lose 5 defense");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 3;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			if (player.statDefense < 5)
				player.statDefense = 0;
			else 
				player.statDefense -= 5;
			player.moveSpeed *= 1.06f;
			player.meleeDamage += 0.06f;
			player.magicDamage += 0.06f;
			player.rangedDamage += 0.06f;
			player.thrownDamage += 0.06f;
        }
		
    }
}
