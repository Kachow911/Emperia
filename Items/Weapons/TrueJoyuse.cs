using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Emperia.Items.Weapons
{
	public class TrueJoyuse : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.Yoyo[item.type] = true;
			ItemID.Sets.GamepadExtraRange[item.type] = 18;
			ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
			DisplayName.SetDefault("True Joyuse");
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
			item.shoot = mod.ProjectileType("TrueJoyuseProj");
			item.useAnimation = 25;
			item.useTime = 25;
			item.shootSpeed = 16f;
			item.knockBack = 4f;
			item.damage = 94;
			item.value = Item.sellPrice(0, 14, 20, 0);
			item.rare = 5;
		}
			
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Joyuse", 1);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddIngredient(ItemID.CrystalShard, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
	
		}
	}
}
