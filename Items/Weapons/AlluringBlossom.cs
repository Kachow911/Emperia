using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Emperia.Projectiles;

namespace Emperia.Items.Weapons
{
	public class AlluringBlossom : ModItem
	{
		public override void SetDefaults()
		{

			Item.damage = 15;
			Item.DamageType = DamageClass.Magic;
			Item.width = 22;
			Item.height = 18;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = 22500;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<AlluringPulse>();
			//Item.shoot = ModContent.ProjectileType<FlameTendril>();
			Item.shootSpeed = 8f;
			Item.mana = 7;
		}

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Alluring Blossom");
	  // Tooltip.SetDefault("Shoots forth a pulse of pink energy that pulls enemies towards you");
	Item.staff[Item.type] = true;
    }
	/*public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "MarbleBar", 9);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        
    }*/
	}
}
