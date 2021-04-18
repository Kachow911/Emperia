using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class ScalyGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scaly Gauntlet");
			Tooltip.SetDefault("While not in the ocean, you become 'Enraged'\nThis state makes you deal increased damage and have increased flight time and move speed ");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 6;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.accessory = true;
			item.GetGlobalItem<GItem>().isGauntlet = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			if (!player.ZoneBeach)
			{
				player.thrownDamage += 0.10f;
				player.meleeDamage += 0.10f;
				player.minionDamage += 0.10f;
				player.magicDamage += 0.10f;
				player.rangedDamage += 0.10f;
				player.moveSpeed += 0.25f;
				player.wingTimeMax = (int) (player.wingTimeMax * 1.1f);
			}
        }
    }
}
