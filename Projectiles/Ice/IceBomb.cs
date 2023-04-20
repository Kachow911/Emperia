using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Ice;
using Emperia.Buffs;

namespace Emperia.Projectiles.Ice
{

    public class IceBomb: ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ice Crystal");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 32;       //Projectile width
            Projectile.height = 32;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 200;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {                                                           // |
			if (Main.rand.NextBool(20))
			{
				Color rgb = new Color(135,206,250);
				int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Snow, (float) Projectile.velocity.X, (float) Projectile.velocity.Y, 0, rgb, 0.9f);
			}
			Projectile.velocity.X *= 0.99f;
			Projectile.velocity.Y *= 0.99f;
			Projectile.rotation += .05f;
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			Color rgb = new Color(135,206,250);
			int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Snow, (float) Projectile.velocity.X, (float) Projectile.velocity.Y, 0, rgb, 0.9f);
			target.GetGlobalNPC<MyNPC>().chillStacks += 1;
			target.AddBuff(ModContent.BuffType<CrushingFreeze>(), 300);
		}
		public override void Kill(int timeLeft)
        {
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Item107, Projectile.Center);
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Projectile.Distance(Main.npc[i].Center) < 90)
					Main.npc[i].SimpleStrikeNPC((int)(Projectile.damage * 1.5f), 0);
			}
			for (int i = 0; i < 45; ++i)
			{
				int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Snow, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 3.25f;
			}
			/*for (int i = 0; i < 6; i++)
			{
				Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(MathHelper.ToRadians(90 + 60 * i));
				int p = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<IceShard>(), Projectile.damage / 3, 1, Main.myPlayer, 0, 0);
				Main.projectile[p].rotation = MathHelper.ToRadians(Main.rand.Next(360));
			}*/
		}
        
    }
}