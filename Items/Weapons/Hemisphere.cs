using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Emperia.Items.Weapons
{
	public class Hemisphere : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.Yoyo[item.type] = true;
			ItemID.Sets.GamepadExtraRange[item.type] = 18;
			ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
			DisplayName.SetDefault("Hemisphere");
			Tooltip.SetDefault("Attacks inflict 'Burning Night', a more potent form of On Fire!");
		}

		public override void SetDefaults()
		{
			item.useStyle = 5;
			item.width = 24;
			item.height = 24;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.melee = true;
			item.channel = true;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("HemisphereProj");
			item.useAnimation = 25;
			item.useTime = 25;
			item.shootSpeed = 16f;
			item.knockBack = 4f;
			item.damage = 47;
			item.value = Item.sellPrice(0, 1, 20, 0);
			item.rare = 3;
		}
			
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.JungleYoyo, 1);
			recipe.AddIngredient(ItemID.Code1, 1);
			recipe.AddIngredient(ItemID.Valor, 1);
			recipe.AddIngredient(ItemID.CorruptYoyo, 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.JungleYoyo, 1);
			recipe.AddIngredient(ItemID.Code1, 1);
			recipe.AddIngredient(ItemID.Valor, 1);
			recipe.AddIngredient(ItemID.CrimsonYoyo, 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
