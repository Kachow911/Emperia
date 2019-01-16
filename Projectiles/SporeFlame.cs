using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Emperia.Projectiles       //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class SporeFlame : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flaming Goo");
		}
        public override void SetDefaults()
        { //Name of the projectile, only shows this if you get killed by it
            projectile.width = 12;  //Set the hitbox width
            projectile.height = 12; //Set the hitbox height
            projectile.friendly = true;  //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.ignoreWater = true;  //Tells the game whether or not projectile will be affected by water
            projectile.ranged = true;  //Tells the game whether it is a ranged projectile or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed, -1 infinity
            projectile.timeLeft = 150;  //The amount of time the projectile is alive for  
            projectile.extraUpdates = 3;
			projectile.alpha = 255;
        }
 
        public override void AI()
        {
            
            if (projectile.timeLeft > 100)
            {
                projectile.timeLeft = 100;
            }
            if (projectile.ai[0] > 3f)  //this defines where the flames starts
            {
                if (Main.rand.Next(3) == 0)     //this defines how many dust to spawn
                {
                    int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 59, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 59, default(Color), 3.75f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                    Main.dust[dust].noGravity = true; //this make so the dust has no gravity
                    Main.dust[dust].velocity *= 2.5f;
                    int dust2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 59, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 59, default(Color), 1.5f); //this defines the flames dust and color parcticles, like when they fall thru ground, change DustID to wat dust you want from Terraria
                }
            }
            else
            {
                projectile.ai[0] += 1f;
            }
            return;
        }
 
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			int buffTime = ((projectile.timeLeft / 5) * 60);
            target.AddBuff(mod.BuffType("SporeStorm"), buffTime);   //this make so when the projectile/flame hit a npc, gives it the buff  onfire , 80 = 3 seconds
        }
 
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
    }
}