using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Color1
{
    public class Vibrance : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vibrance");
			Tooltip.SetDefault("Slicing an enemy will increase defense and critical hit damage for a short time\nStriking a boss increases the duration\nShooting an enemy will decrease their contact damage as your DPS rises");
		}
        public override void SetDefaults()
        {
            item.damage = 96;
            item.melee = true;
            item.width = 52;
            item.height = 52;
            item.useTime = 27;
            item.useAnimation = 27;     
            item.useStyle = 1;
            item.knockBack = 4f;  
			item.crit = 10;	
            item.value = 232500;        
            item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("VibranceProj");
			item.shootSpeed = 8f;
			item.scale = 1f;
            item.autoReuse = true;
            item.useTurn = true;               
        }
		
        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "RougeRapier", 1); 
			recipe.AddIngredient(null, "TitianTalwar", 1); 
			recipe.AddIngredient(null, "PearlyPrism", 1); 
			recipe.AddIngredient(ItemID.SoulofFright, 4); 	
			recipe.AddIngredient(ItemID.AdamantiteBar, 2); 
			recipe.AddIngredient(ItemID.PalladiumBar, 2); 
            recipe.AddTile(TileID.MythrilAnvil); 			
            recipe.SetResult(this);
            recipe.AddRecipe(); 

        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 158);
			}
		}
		 public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            if (target.boss)
			    player.AddBuff(mod.BuffType("VermillionValor"), Main.rand.Next(420, 600) + 300);
            else
                player.AddBuff(mod.BuffType("VermillionValor"), Main.rand.Next(420, 600));
        }
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type1, ref int damage, ref float knockBack)
		{
			Main.PlaySound(SoundID.Item69, player.Center);
			damage = 54;
			return true;
		}
    }
}
