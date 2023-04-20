using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Granite
{
    public class GraniteRock3 : ModProjectile
    {
		NPC hitNPC;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Granite Rock");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 22;       //Projectile width
            Projectile.height = 22;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 2000;   //how many time this Projectile has before disepire
            Projectile.light = 0.5f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the Projectile will face the corect way
        {                                                           // |
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			if (Main.rand.Next(5) == 0)
			{
				int num622 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, DustID.Granite, 0f, 0f, 74, new Color(53f, 67f, 253f), 1.3f);
				Main.dust[num622].velocity += Projectile.velocity * 0.2f;
				Main.dust[num622].noGravity = true;
			}
			if (Main.rand.Next(2) == 0)
			{
				int num622 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, DustID.MagicMirror, 0f, 0f, 74, new Color(53f, 67f, 253f), 1.3f);
				Main.dust[num622].velocity += Projectile.velocity * 0.4f;
				Main.dust[num622].noGravity = true;
			}
        }
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			hitNPC = target;
		}
		
		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
			Player player = Main.player[Projectile.owner];
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (modPlayer.graniteSet && modPlayer.graniteTime >= 900)
            {
				modifiers.SourceDamage *= 0.5f;
			}
		}
		public override void Kill(int timeLeft)
        {
			Player player = Main.player[Projectile.owner];
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (modPlayer.graniteSet && modPlayer.graniteTime >= 900)
			{
				for (int i = 0; i < Main.npc.Length; i++)
            	{
                	if (Projectile.Distance(Main.npc[i].Center) < 100 && Main.npc[i] != hitNPC && !Main.npc[i].townNPC)
                    	Main.npc[i].SimpleStrikeNPC((int)(Projectile.damage * 1.5f), 0);
            	}
				for (int i = 0; i < 45; ++i)
				{
					int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.MagicMirror, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 3.75f;
				}
				modPlayer.graniteTime = 0;
				Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
			}
			else
			{
				for (int i = 0; i < Main.npc.Length; i++)
            	{
					if (Projectile.Distance(Main.npc[i].Center) < 70 && Main.npc[i] != hitNPC && !Main.npc[i].townNPC)
                    	Main.npc[i].SimpleStrikeNPC(Projectile.damage, 0);
				}	
				for (int i = 0; i < 30; ++i)
				{
					int index2 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, DustID.MagicMirror, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 1.5f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 2.25f;
				}
				Terraria.Audio.SoundEngine.PlaySound(SoundID.Item89 with { Volume = 0.75f }, Projectile.Center);
			}
		}
		
    }
}