using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.FrousForest   //where is located
{
    public class Glucose : ModItem
    {
		private int canHit = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glucose");
			Tooltip.SetDefault("Hitting an enemy with the blade will cause a ring of leaves to jet into them");
		}
        public override void SetDefaults()
        {
			item.damage = 21;            
            item.melee = true;            
            item.width = 32;              //Sword width
            item.height = 32;             //Sword height
            item.useTime = 32;          //how fast 
            item.useAnimation = 32;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 3f;      //Sword knockback
            item.value = 204000;        
            item.rare = 2;
			item.scale = 1f;
			item.UseSound = SoundID.Item18;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;              
        }
		public override bool UseItem(Player player)
		{
			
			return true;
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (target.life <= 0)
			{
				for (int i = 0; i < 12; i++)
				{
				
					Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(MathHelper.ToRadians(90 + 30 * i));
					Projectile.NewProjectile(target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FireBall"), damage / 3, 1, Main.myPlayer, 0, 0);
				
				}
			}
		}
    }
}