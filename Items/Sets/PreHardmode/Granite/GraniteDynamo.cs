using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Granite;
using Emperia.Buffs;
namespace Emperia.Items.Sets.PreHardmode.Granite
{
	public class GraniteDynamo : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Dynamo Staff");
            Tooltip.SetDefault("Summons a granite elemental to fight for you\nFires like a Projectile when first summoned\nThe first minion does not count towards your max");
			Item.staff[Item.type] = true;
        }

		public override void SetDefaults()
		{
            Item.width = 38;
            Item.height = 36;
            Item.value = 27000;
            Item.rare = 1;
            Item.damage = 16;
            Item.knockBack = 0f;
            Item.useStyle = 5;
            Item.useTime = 55;
            Item.useAnimation = 55;
            Item.mana = 20; 
            Item.DamageType = DamageClass.Summon;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<GraniteMinion>();
            Item.shootSpeed = 10f;
            Item.buffType = ModContent.BuffType<GraniteMinionBuff>();
            Item.buffTime = 3600;
            Item.UseSound = SoundID.Item44;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(null, "GraniteBar", 8); 
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
        }
	}
}