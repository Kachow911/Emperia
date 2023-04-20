using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Audio.SoundEngine;

namespace Emperia.Projectiles.Desert
{

    public class ShellStrike : ModProjectile
    {
        NPC NPC;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shell Strike");
		}
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 140;
            Projectile.ignoreWater = false;
            Projectile.spriteDirection = Projectile.direction;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
		}
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 3; i++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 9);
                Main.dust[dust].noGravity = true;
				Vector2 vel = new Vector2(0, -1).RotatedBy(Main.rand.NextFloat() * 6.283f) * 3.5f;
			}
		}
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
            NPC = target;
            NPC.GetGlobalNPC<MyNPC>().strikeCount += 1;
            if (target.GetGlobalNPC<MyNPC>().strikeCount == 4)
            {
                modifiers.SourceDamage *= 2f;
                PlaySound(SoundID.Item14, Projectile.Center);
				for (int i = 0; i < Main.npc.Length; i++)
            	{
					if (Projectile.Distance(Main.npc[i].Center) < 40 && Main.npc[i] != NPC)
                    	Main.npc[i].SimpleStrikeNPC(Projectile.damage * 2, 0);
				}	
				for (int i = 0; i < 20; ++i)
				{
				    int dust = Dust.NewDust(new Vector2(Projectile.position.X - 14, Projectile.position.Y - 14), Projectile.width * 8, Projectile.height * 8, 262);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1f;
				}
			}
		}
    	public override bool OnTileCollide(Vector2 oldVelocity)
		{
            PlaySound(SoundID.Dig, Projectile.position);
            return true;
		}
        
        
    }
}
