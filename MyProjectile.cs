using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using static Terraria.Audio.SoundEngine;
using Emperia;
using Emperia.Projectiles;
using Emperia.Projectiles.Corrupt;
using Emperia.Projectiles.Crimson;
using Emperia.Buffs;

namespace Emperia
{
    public class GProj: GlobalProjectile
    {
		public override bool InstancePerEntity
		{
			get { return true; }
		}
		public bool scoriaExplosion = false;
        public bool chillEffect = false;
        public NPC latchedNPC;
        public override void ModifyHitNPC(Projectile Projectile,NPC target,ref int damage,ref float knockback,ref bool crit,ref int hitDirection)
        {
            Player player = Main.player[Projectile.owner];
            if (player.GetModPlayer<MyPlayer>().forestSetThrown && Projectile.CountsAsClass(DamageClass.Ranged))//Projectile.thrown
            {
                if (Main.rand.Next(4) == 0)
                {
                    damage += target.defense;
                    //CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height), Color.White, "Defense Ignored!", false, false);
                }
            }
           
        }
        public override void OnHitNPC(Projectile Projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (target.life <= 0 && Projectile.CountsAsClass(DamageClass.Ranged) && player.GetModPlayer<MyPlayer>().rotfireSet) //Projectile.thrown
            {
                for (int i = 0; i < 6; i++)
                {

                    Vector2 perturbedSpeed = new Vector2(0, 4).RotatedBy(MathHelper.ToRadians(90 + 60 * i));
                    Projectile.NewProjectile(Projectile.InheritSource(Projectile), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<CursedBoltSeeking>(), 50, 1, Main.myPlayer, 0, 0);

                }
            }
            if (target.life <= 0 && Projectile.CountsAsClass(DamageClass.Magic) && player.GetModPlayer<MyPlayer>().bloodboilSet)
            {
                for (int i = 0; i < 4; i++)
                {

                    Vector2 perturbedSpeed = new Vector2(0, 4).RotatedByRandom(MathHelper.ToRadians(360));
                    Projectile.NewProjectile(Projectile.InheritSource(Projectile), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<IchorBoltSeeking>(), 40, 1, Main.myPlayer, 0, 0);

                }
            }
            if (chillEffect)
            {
                target.GetGlobalNPC<MyNPC>().chillStacks += 1;
                target.AddBuff(ModContent.BuffType<CrushingFreeze>(), 300);
            }
            if (player.GetModPlayer<MyPlayer>().chillsteelSet && Projectile.CountsAsClass(DamageClass.Ranged))
            { 
                target.AddBuff(BuffID.Frostburn, 300);
            }
        }


        public override void Kill(Projectile Projectile, int timeLeft)
		{
			if (scoriaExplosion)
			{
             
                for (int num621 = 0; num621 < 20; num621++)
                {
                    int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 258, 0f, 0f, 100, default(Color));
                    Main.dust[num622].velocity *= 3f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num622].scale = 0.5f;
                        Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                    }
                }
                for (int num623 = 0; num623 < 35; num623++)
                {
                    int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 258, 0f, 0f, 100, default(Color));
                    Main.dust[num624].noGravity = true;
                    Main.dust[num624].velocity *= 5f;
                    num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 258, 0f, 0f, 100, default(Color));
                    Main.dust[num624].velocity *= 2f;
                }
                PlaySound(SoundID.Item14, Projectile.position);
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Projectile.Distance(Main.npc[i].Center) < 64 && !Main.npc[i].townNPC)
                        Main.npc[i].StrikeNPC(Projectile.damage / 2, 0f, 0, false, false, false);
                }
      
			}
		}
        public void ApplyHeldProjOffsets(Player player, int bodyFrame, ref Vector2 offset, ref float projRotation, ref float armRotation, ref int stretchAmount)
        {
            int xFrame = 0;
            if (bodyFrame >= 7 && bodyFrame <= 8) xFrame = 1;
            if (bodyFrame >= 14 && bodyFrame <= 17) xFrame = -1;
            offset.X += 2 * xFrame * player.direction;

            int[] upFrames = { 7, 8, 9, 14, 15, 16 }; //10, 11, 12, 13, 17, 18, 19 down
            if (upFrames.Contains(bodyFrame)) offset.Y -= 2 * player.gravDir;
            if (xFrame != 0)
            {
                armRotation += -0.075f * xFrame;
                if (xFrame == 1) stretchAmount -= 3;
                else if (xFrame == -1) stretchAmount -= 1;
            }
            if (bodyFrame == 5) //falling
            {
                offset.X -= 2 * player.direction;
                offset.Y -= 8 * player.gravDir;
                armRotation += -0.3f;
                projRotation += player.direction * player.gravDir * -0.5f;
            }
            return;
        }
    }
}
