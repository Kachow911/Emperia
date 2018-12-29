using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Jungle
{
    public class ForlornSkull : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Forlorn Skull");
			Tooltip.SetDefault("10% increased damage and critical strike chance while in the jungle\nIf you are not in the jungle, you lose 5 defense\n You move slower while in the jungle");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			if (player.ZoneJungle)
			{
				player.rangedDamage *= 1.1f;
				player.magicDamage *= 1.1f;
				player.meleeDamage *= 1.1f;
				player.minionDamage *= 1.1f;
				player.meleeCrit += 10;
				player.magicCrit += 10;
				player.rangedCrit += 10;
				player.thrownCrit += 10;
				player.moveSpeed *= .9f;
			}
			else
			{
				player.statDefense -= 5;
			}
		
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "JungleMaterial", 7);
			recipe.AddIngredient(ItemID.TurtleShell, 1);
			recipe.AddIngredient(ItemID.SoulofNight, 2);
			recipe.AddIngredient(ItemID.SoulofLight, 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
    }
}
