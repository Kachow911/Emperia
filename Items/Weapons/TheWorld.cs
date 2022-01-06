using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Emperia.Projectiles;

namespace Emperia.Items.Weapons
{
	public class TheWorld : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.Yoyo[Item.type] = true;
			ItemID.Sets.GamepadExtraRange[Item.type] = 18;
			ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
			DisplayName.SetDefault("The World");
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
			Item.shoot = ModContent.ProjectileType<TheWorldProj>();
			Item.useAnimation = 22;
			Item.useTime = 22;
			Item.shootSpeed = 16f;
			Item.knockBack = 6f;
			Item.damage = 94;
			Item.value = Item.sellPrice(0, 12, 20, 0);
			Item.rare = 7;
		}
			
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "TrueHemisphere", 1);
			recipe.AddIngredient(null, "TrueJoyuse", 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			
		}
	}
}
