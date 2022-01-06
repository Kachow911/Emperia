using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Items.Weapons   //where is located
{
    public class FireBlade : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flameforged Blade");
			Tooltip.SetDefault("Enemies Killed by the sword explode into balls of fire");
		}
        public override void SetDefaults()
        {
			Item.CloneDefaults(ItemID.IceBlade);    //Sword name
            Item.shoot = 0;    
        }
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (target.life <= 0)
			{
				for (int i = 0; i < 12; i++)
				{
				
					Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(MathHelper.ToRadians(90 + 30 * i));
					Projectile.NewProjectile(player.GetProjectileSource_Item(Item), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<FireBall>(), damage / 3, 1, Main.myPlayer, 0, 0);
				
				}
			}
		}
	}
}