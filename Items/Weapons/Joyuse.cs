using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Emperia.Items.Weapons
{
	public class Joyuse : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.Yoyo[item.type] = true;
			ItemID.Sets.GamepadExtraRange[item.type] = 18;
			ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
			DisplayName.SetDefault("Joyuse");
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
			item.shoot = mod.ProjectileType("JoyuseProj");
			item.useAnimation = 25;
			item.useTime = 25;
			item.shootSpeed = 16f;
			item.knockBack = 4f;
			item.damage = 81;
			item.value = Item.sellPrice(0, 4, 20, 0);
			item.rare = 5;
		}
			
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
	
		}
	}
}
