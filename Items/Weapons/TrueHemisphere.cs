using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Emperia.Items.Weapons
{
	public class TrueHemisphere : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.Yoyo[item.type] = true;
			ItemID.Sets.GamepadExtraRange[item.type] = 18;
			ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
			DisplayName.SetDefault("True Hemisphere");
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
			item.shoot = mod.ProjectileType("TrueHemisphereProj");
			item.useAnimation = 22;
			item.useTime = 22;
			item.shootSpeed = 16f;
			item.knockBack = 6f;
			item.damage = 72;
			item.value = Item.sellPrice(0, 12, 20, 0);
			item.rare = 7;
		}
			
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Hemisphere", 1);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddIngredient(ItemID.CursedFlame, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
