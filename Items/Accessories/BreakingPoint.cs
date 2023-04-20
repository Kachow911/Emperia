using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
{
    public class BreakingPoint : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Breaking Point");
			// Tooltip.SetDefault("All weapons set enemies aflame\nImmunity to ice-based debuffs\nYou move faster while under 50% hp");
		}
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 0, 3, 0);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			player.buffImmune[BuffID.Frostburn] = true;
			player.buffImmune[BuffID.Frozen] = true;
			player.buffImmune[BuffID.Chilled] = true;
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			modPlayer.breakingPoint = true;
			if (player.statLife <= player.statLifeMax2 / 2)
            {
                player.moveSpeed *= 1.2f;
            }
        }
		
    }
}
