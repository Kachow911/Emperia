using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles;
using Emperia.Buffs;

namespace Emperia.Items.Weapons.Color1   //where is located
{
    public class Mellow : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mellow");
			// Tooltip.SetDefault("Striking an enemy will increase movement and melee speed briefly, longer on bosses\nWhen empowered, fires a blade lowering enemy defense as your MPH rises");
		}
        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.DamageType = DamageClass.Melee;
            Item.width = 42;
            Item.height = 50;
            Item.useTime = 13;
            Item.useAnimation = 13;     
            Item.useStyle = 1;
            Item.knockBack = 1.5f;  
            Item.value = 232500;        
            Item.rare = 5;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<MellowSlice>();
			Item.shootSpeed = 12f;
			Item.scale = 1f;
            Item.autoReuse = true;
            Item.useTurn = true;               
        }
		
        public override void AddRecipes()
        {
			Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(null, "BerylBlade", 1); 
			recipe.AddIngredient(null, "SaffronSaber", 1); 
			recipe.AddIngredient(null, "PearlyPrism", 1); 
			recipe.AddIngredient(ItemID.SoulofSight, 4); 	
			recipe.AddIngredient(ItemID.TitaniumBar, 2); 
			recipe.AddIngredient(ItemID.MythrilBar, 2);  	
            recipe.AddTile(TileID.MythrilAnvil); 			
            recipe.Register();
             

        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 75);
			}
		}
		 public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
            if (target.boss)
			    player.AddBuff(ModContent.BuffType<LimeLegerity>(), Main.rand.Next(420, 600) + 300);
            else
                player.AddBuff(ModContent.BuffType<LimeLegerity>(), Main.rand.Next(420, 600));
        }
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			MyPlayer p = player.GetModPlayer<MyPlayer>();
			if (p.isMellowProjectile)
				return false;
			//Projectile.NewProjectile(source, Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<MellowSlice>(), 28, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}
    }
}
