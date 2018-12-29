using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Color1   //where is located
{
    public class Vibrance : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vibrance");
			Tooltip.SetDefault("Striking an enemy will increase defense, critical hit damage and critical hit chance for a short time\nHitting an enemy with a projectile will decrease their damage");
		}
        public override void SetDefaults()
        {   //Sword name
            item.damage = 118;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 32;              //Sword width
            item.height = 32;             //Sword height
            item.useTime = 25;          //how fast 
            item.useAnimation = 25;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 4f;  
			item.crit = 10;			//Sword knockback
            item.value = 100;        
            item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("VibranceProj");
			item.shootSpeed = 8f;
			item.scale = 1f;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;             //projectile speed                 
        }
		
        public override void AddRecipes()  //How to craft this sword
        {
			ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "RougeRapier", 1); 
			recipe.AddIngredient(null, "TitianTalwar", 1); 
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
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 158);
			}
		}
		 public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
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