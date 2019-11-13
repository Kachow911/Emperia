using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Color1   //where is located
{
    public class Mellow : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mellow");
			Tooltip.SetDefault("Slicing an enemy will increase movement and melee speed for a short time\nStriking a boss increases the duration\nShooting an enemy will decrease their defense as your MPH rises");
		}
        public override void SetDefaults()
        {
            item.damage = 45;
            item.melee = true;
            item.width = 42;
            item.height = 50;
            item.useTime = 13;
            item.useAnimation = 13;     
            item.useStyle = 1;
            item.knockBack = 1.5f;  
            item.value = 232500;        
            item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("MellowSlice");
			item.shootSpeed = 12f;
			item.scale = 1f;
            item.autoReuse = true;
            item.useTurn = true;               
        }
		
        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "BerylBlade", 1); 
			recipe.AddIngredient(null, "SaffronSaber", 1); 
			recipe.AddIngredient(null, "PearlyPrism", 1); 
			recipe.AddIngredient(ItemID.SoulofSight, 4); 	
			recipe.AddIngredient(ItemID.TitaniumBar, 2); 
			recipe.AddIngredient(ItemID.MythrilBar, 2);  	
            recipe.AddTile(TileID.MythrilAnvil); 			
            recipe.SetResult(this);
            recipe.AddRecipe(); 

        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 75);
			}
		}
		 public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            if (target.boss)
			    player.AddBuff(mod.BuffType("LimeLegerity"), Main.rand.Next(420, 600) + 300);
            else
                player.AddBuff(mod.BuffType("LimeLegerity"), Main.rand.Next(420, 600));
        }
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type1, ref int damage, ref float knockBack)
		{
			MyPlayer p = player.GetModPlayer<MyPlayer>();
			if (p.isMellowProjectile)
				return false;
			//Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, mod.ProjectileType("MellowSlice"), 28, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}
    }
}
