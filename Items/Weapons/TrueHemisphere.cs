using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Emperia.Projectiles;

namespace Emperia.Items.Weapons
{
	public class TrueHemisphere : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.Yoyo[Item.type] = true;
			ItemID.Sets.GamepadExtraRange[Item.type] = 18;
			ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
			DisplayName.SetDefault("True Hemisphere");
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
			Item.shoot = ModContent.ProjectileType<TrueHemisphereProj>();
			Item.useAnimation = 22;
			Item.useTime = 22;
			Item.shootSpeed = 16f;
			Item.knockBack = 6f;
			Item.damage = 72;
			Item.value = Item.sellPrice(0, 12, 20, 0);
			Item.rare = 7;
		}
			
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Hemisphere", 1);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddIngredient(ItemID.CursedFlame, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			
		}
	}
}
