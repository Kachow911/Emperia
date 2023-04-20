using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Emperia.Projectiles;

namespace Emperia.Items.Weapons
{
	public class TrueJoyuse : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.Yoyo[Item.type] = true;
			ItemID.Sets.GamepadExtraRange[Item.type] = 18;
			ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
			// DisplayName.SetDefault("True Joyuse");
		}

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.width = 24;
			Item.height = 24;
			Item.noUseGraphic = true;
			Item.UseSound = SoundID.Item1;
			Item.DamageType = DamageClass.Melee;
			Item.channel = true;
			Item.noMelee = true;
			Item.shoot = ModContent.ProjectileType<TrueJoyuseProj>();
			Item.useAnimation = 25;
			Item.useTime = 25;
			Item.shootSpeed = 16f;
			Item.knockBack = 4f;
			Item.damage = 94;
			Item.value = Item.sellPrice(0, 14, 20, 0);
			Item.rare = ItemRarityID.Pink;
		}
			
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Joyuse", 1);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddIngredient(ItemID.CrystalShard, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			
	
		}
	}
}
