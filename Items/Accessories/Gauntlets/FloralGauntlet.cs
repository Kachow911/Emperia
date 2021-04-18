using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class FloralGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Floral Gauntlet");
			Tooltip.SetDefault("Attacks have a chance to lifesteal\nIncreased damage and max life");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 5;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.accessory = true;
            item.GetGlobalItem<GItem>().isGauntlet = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<MyPlayer>().floralGauntlet = true;
			player.thrownDamage += 0.08f;
			player.meleeDamage += 0.08f;
			player.minionDamage += 0.08f;
			player.magicDamage += 0.08f;
			player.rangedDamage += 0.08f;
			player.statLifeMax2 += 75;
        }
    }
}
