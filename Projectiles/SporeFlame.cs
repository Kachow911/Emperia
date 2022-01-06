using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;
 
namespace Emperia.Projectiles       //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class SporeFlame : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flaming Goo");
		}
        public override void SetDefaults()
        { //Name of the Projectile, only shows this if you get killed by it
            Projectile.width = 12;  //Set the hitbox width
            Projectile.height = 12; //Set the hitbox height
            Projectile.friendly = true;  //Tells the game whether it is friendly to players/friendly npcs or not
            Projectile.ignoreWater = true;  //Tells the game whether or not Projectile will be affected by water
            Projectile.DamageType = DamageClass.Ranged;  //Tells the game whether it is a ranged Projectile or not
            Projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed, -1 infinity
            Projectile.timeLeft = 150;  //The amount of time the Projectile is alive for  
            Projectile.extraUpdates = 3;
			Projectile.alpha = 255;
        }
 
        public override void AI()
        {
            
            if (Projectile.timeLeft > 100)
            {
                Projectile.timeLeft = 100;
            }
            if (Projectile.ai[0] > 2f)  //this defines where the flames starts
            {
                if (Main.rand.Next(3) == 0)     //this defines how many dust to spawn
                {
                    int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 59, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 59, default(Color), 3.75f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add ModContent.DustType<CustomDustName>() for your custom dust
                    Main.dust[dust].noGravity = true; //this make so the dust has no gravity
                    Main.dust[dust].velocity *= 2.5f;
                    int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 59, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 59, default(Color), 1.5f); //this defines the flames dust and color parcticles, like when they fall thru ground, change DustID to wat dust you want from Terraria
                }
            }
            else
            {
                Projectile.ai[0] += 1f;
            }
            return;
        }
 
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			int buffTime = ((Projectile.timeLeft / 5) * 60);
            target.AddBuff(ModContent.BuffType<SporeStorm>(), buffTime);   //this make so when the Projectile/flame hit a NPC, gives it the buff  onfire , 80 = 3 seconds
        }
 
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
    }
}