using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles.Yeti;

namespace Emperia.Items.Sets.PreHardmode.Frostleaf
{
    public class Coniferocious : ModItem //make grenade actually do splash damage
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Coniferocious");
			// Tooltip.SetDefault("Occasionally throws out a pinecone grenade");
		}
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
			Item.damage = 14;
			Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.noMelee = true;
			Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useTurn = true;
            Item.autoReuse = false;
			Item.knockBack = 6f;
            Item.value = 24000;        
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<ConiferociousProj>();
            Item.shootSpeed = 9f;
            Item.UseSound = SoundID.Item1;
        }
        int pineconeCharge = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            pineconeCharge++;
            if(pineconeCharge >= 4)
            {
				Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 1.4f, velocity.Y * 1.4f, ModContent.ProjectileType<PineconeGrenade>(), damage * 2, knockBack + 1f, player.whoAmI);
                pineconeCharge = 0;
            }
            return true;
        }
		public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 250; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(null, "Frostleaf", 7); 
            recipe.AddIngredient(ItemID.BorealWood, 15); 			
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
        }
	}
}