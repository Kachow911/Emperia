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
			// DisplayName.SetDefault("Forlorn Skull");
			// Tooltip.SetDefault("10% increased damage and critical strike chance while in the jungle\nIf you are not in the jungle, you lose 5 defense\n You move slower while in the jungle");
		}
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			if (player.ZoneJungle)
			{
				player.GetDamage(DamageClass.Ranged) *= 1.1f;
				player.GetDamage(DamageClass.Magic) *= 1.1f;
				player.GetDamage(DamageClass.Melee) *= 1.1f;
				player.GetDamage(DamageClass.Summon) *= 1.1f;
				player.GetCritChance(DamageClass.Generic) += 10;
				player.moveSpeed *= .9f;
			}
			else
			{
				player.statDefense -= 5;
			}
		
        }
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "JungleMaterial", 7);
			recipe.AddIngredient(ItemID.TurtleShell, 1);
			recipe.AddIngredient(ItemID.SoulofNight, 2);
			recipe.AddIngredient(ItemID.SoulofLight, 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			

		}
    }
}
