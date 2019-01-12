using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Mystique   //where is located
{
    public class FloralCutter : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Floral Cutter");
			Tooltip.SetDefault("Enemies Killed by the sword explode into homing leaves");
		}
        public override void SetDefaults()
        {
			item.damage = 63;            
            item.melee = true;            //if it's melee
            item.width = 64;              //Sword width
            item.height = 64;             //Sword height
            item.useTime = 28;          //how fast 
            item.useAnimation = 28;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 5f;      //Sword knockback
            item.value = 100;        
            item.rare = 6;
			item.scale = 1f;
			item.UseSound = SoundID.Item18;
			//item.shoot = mod.ProjectileType("Leafy");
			item.shootSpeed = 8f;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;            
        }
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (target.life <= 0)
			{
				for (int i = 0; i < 50; ++i) //Create dust after teleport
				{
					int dust = Dust.NewDust(target.position, target.width, target.height, 76, (float)0, (float)0, 0, new Color(255, 113, 182), 1.1f);
					int dust1 = Dust.NewDust(target.position, target.width, target.height, 76, (float)0, (float)0, 0, new Color(255, 113, 182), 1.1f);
					Main.dust[dust1].scale = 0.8f;
					Main.dust[dust1].velocity *= 2f;
				}
				for (int i = 0; i < 12; i++)
				{
				
					Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(MathHelper.ToRadians(90 + 30 * i));
					Projectile.NewProjectile(target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("HomingLeaf"), damage / 3, 1, Main.myPlayer, 0, 0);
				
				}
			}
		}
    }
}