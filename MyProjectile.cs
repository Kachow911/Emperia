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
    public class MyProjectile: GlobalProjectile
    {
		public override bool InstancePerEntity
		{
			get { return true; }
		}
		public bool scoriaExplosion = false;
        public bool chillEffect = false;
<<<<<<< Updated upstream
        public NPC latchedNPC;
        public override void ModifyHitNPC(Projectile Projectile,NPC target,ref int damage,ref float knockback,ref bool crit,ref int hitDirection)
        {
            Player player = Main.player[Projectile.owner];
            if (player.GetModPlayer<MyPlayer>().forestSetThrown && Projectile.CountsAsClass(DamageClass.Ranged))//Projectile.thrown
=======
        public override void ModifyHitNPC(Projectile Projectile,NPC target,ref int damage,ref float knockback,ref bool crit,ref int hitDirection)
        {
            Player player = Main.player[Projectile.owner];
            if (player.GetModPlayer<MyPlayer>().forestSetThrown && Projectile.thrown)
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            if (target.life <= 0 && Projectile.CountsAsClass(DamageClass.Ranged) && player.GetModPlayer<MyPlayer>().rotfireSet) //Projectile.thrown
=======
            if (target.life <= 0 && Projectile.thrown && player.GetModPlayer<MyPlayer>().rotfireSet)
>>>>>>> Stashed changes
            {
                for (int i = 0; i < 6; i++)
                {

                    Vector2 perturbedSpeed = new Vector2(0, 4).RotatedBy(MathHelper.ToRadians(90 + 60 * i));
                    Projectile.NewProjectile(Projectile.InheritSource(Projectile), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<CursedBoltSeeking>(), 50, 1, Main.myPlayer, 0, 0);

                }
            }
<<<<<<< Updated upstream
            if (target.life <= 0 && Projectile.CountsAsClass(DamageClass.Magic) && player.GetModPlayer<MyPlayer>().bloodboilSet)
=======
            if (target.life <= 0 && Projectile.magic && player.GetModPlayer<MyPlayer>().bloodboilSet)
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            if (player.GetModPlayer<MyPlayer>().chillsteelSet && Projectile.CountsAsClass(DamageClass.Ranged))
=======
            if (player.GetModPlayer<MyPlayer>().chillsteelSet && Projectile.ranged)
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
                PlaySound(2, (int)Projectile.position.X, (int)Projectile.position.Y, 14);
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Projectile.Distance(Main.npc[i].Center) < 64 && !Main.npc[i].townNPC)
                        Main.npc[i].StrikeNPC(Projectile.damage / 2, 0f, 0, false, false, false);
=======
                Main.PlaySound(2, (int)Projectile.position.X, (int)Projectile.position.Y, 14);
                for (int i = 0; i < Main.NPC.Length; i++)
                {
                    if (Projectile.Distance(Main.NPC[i].Center) < 64 && !Main.NPC[i].townNPC)
                        Main.NPC[i].StrikeNPC(Projectile.damage / 2, 0f, 0, false, false, false);
>>>>>>> Stashed changes
                }
      
			}
		}
	}
}
