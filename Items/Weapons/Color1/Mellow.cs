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
			Tooltip.SetDefault("Striking an enemy will increase movement speed, melee speed and flight time for a short time");
		}
        public override void SetDefaults()
        {   //Sword name
            item.damage = 45;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 32;              //Sword width
            item.height = 32;             //Sword height
            item.useTime = 10;          //how fast 
            item.useAnimation = 10;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 1.5f;  
			item.crit = 4;			//Sword knockback
            item.value = 100;        
            item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("MellowSlice");
			item.shootSpeed = 12f;
			item.scale = 1f;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;             //projectile speed                 
        }
		
        public override void AddRecipes()  //How to craft this sword
        {
			ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "BerylBlade", 1); 
			recipe.AddIngredient(null, "SaffronSaber", 1); 
			recipe.AddRecipeGroup("Emperia:PalBar", 4);
			recipe.AddRecipeGroup("Emperia:AdBar", 4);
			recipe.AddIngredient(ItemID.HallowedBar, 2); 
			recipe.AddIngredient(ItemID.SoulofLight, 4); 
			recipe.AddIngredient(ItemID.SoulofSight, 4); 
			recipe.AddIngredient(ItemID.CrystalShard, 20);		
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
			MyPlayer p = player.GetModPlayer<MyPlayer>(mod);
			if (p.isMellowProjectile)
				return false;
			//Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, mod.ProjectileType("MellowSlice"), 28, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}
    }
}