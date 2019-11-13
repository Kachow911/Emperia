using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Color1
{
    public class Dusk : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dusk");
			Tooltip.SetDefault("Slicing an enemy will increase melee damage and life regeneration for a short time\nStriking a boss increases the duration\nShooting an enemy will deal damage over time that increases as your health rises");
		}
        public override void SetDefaults()
        {   //Sword name
            item.damage = 126;
            item.melee = true;
            item.width = 48;
            item.height = 62;
            item.useTime = 33;
            item.useAnimation = 33;     
            item.useStyle = 1;
            item.knockBack = 5.5f;  
            item.value = 232500;        
            item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("DuskProj");
			item.shootSpeed = 16f;
			item.scale = 1f;
            item.autoReuse = true;
            item.useTurn = true;                
        }
		
        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "CeruleanClaymore", 1); 
			recipe.AddIngredient(null, "IndigoIaito", 1); 
			recipe.AddIngredient(null, "PearlyPrism", 1); 
			recipe.AddIngredient(ItemID.SoulofMight, 4); 	
			recipe.AddIngredient(ItemID.CobaltBar, 2); 
			recipe.AddIngredient(ItemID.OrichalcumBar, 2); 
            recipe.AddTile(TileID.MythrilAnvil); 			
            recipe.SetResult(this);
            recipe.AddRecipe(); 

        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 21);
			}
		}
		 public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            if (target.boss)
                player.AddBuff(mod.BuffType("IndigoIntensity"), Main.rand.Next(420, 600) + 300);
            else
                player.AddBuff(mod.BuffType("IndigoIntensity"), Main.rand.Next(420, 600));
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type1, ref int damage, ref float knockBack)
		{
			damage = damage / 2;
			return true;
		}
    }
}
