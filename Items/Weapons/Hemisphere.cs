using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Emperia.Projectiles;

namespace Emperia.Items.Weapons
{
	public class Hemisphere : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.Yoyo[Item.type] = true;
			ItemID.Sets.GamepadExtraRange[Item.type] = 18;
			ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
			// DisplayName.SetDefault("Hemisphere");
			// Tooltip.SetDefault("Attacks inflict 'Burning Night', a more potent form of On Fire!");
		}

		public override void SetDefaults()
		{
			Item.useStyle = 5;
			Item.width = 24;
			Item.height = 24;
			Item.noUseGraphic = true;
			Item.UseSound = SoundID.Item1;
			Item.DamageType = DamageClass.Melee;
			Item.channel = true;
			Item.noMelee = true;
			Item.shoot = ModContent.ProjectileType<HemisphereProj>();
			Item.useAnimation = 25;
			Item.useTime = 25;
			Item.shootSpeed = 16f;
			Item.knockBack = 4f;
			Item.damage = 47;
			Item.value = Item.sellPrice(0, 1, 20, 0);
			Item.rare = 3;
		}
			
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.JungleYoyo, 1);
			recipe.AddIngredient(ItemID.Code1, 1);
			recipe.AddIngredient(ItemID.Valor, 1);
			recipe.AddIngredient(ItemID.CorruptYoyo, 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
			
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.JungleYoyo, 1);
			recipe.AddIngredient(ItemID.Code1, 1);
			recipe.AddIngredient(ItemID.Valor, 1);
			recipe.AddIngredient(ItemID.CrimsonYoyo, 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
			
		}
	}
}
