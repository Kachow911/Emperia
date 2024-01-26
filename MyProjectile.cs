using Emperia.Buffs;
using Emperia.Projectiles;
using Emperia.Projectiles.Corrupt;
using Emperia.Projectiles.Crimson;
using Emperia.Projectiles.Yeti;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Audio.SoundEngine;
using static Terraria.ModLoader.ModContent;

namespace Emperia
{
    public class GProj : GlobalProjectile
    {
        public override bool InstancePerEntity
        {
            get { return true; }
        }
        public bool scoriaExplosion = false;
        public bool chillEffect = false;
        public NPC latchedNPC;
        public bool forceReflect = false;

        public override void SetDefaults(Projectile proj)
        {
            if (proj.ModProjectile is not null && proj.ModProjectile.Mod == Emperia.instance)
            {
                //if (proj.damage > 0) forceReflect = true; would like to bring this back but damage isnt checked for here
                forceReflect = true;
                if (proj.DamageType == DamageClass.SummonMeleeSpeed || proj.minion || proj.aiStyle == 99 ||
                proj.type == ProjectileType<Needle>() || proj.type == ProjectileType<Items.StickyHandProj>() || proj.type == ProjectileType<Splinter>() || proj.type == ProjectileType<EnchantedBlade>()) forceReflect = false;
            }
        }
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[projectile.owner];
            if (player.GetModPlayer<MyPlayer>().forestSetThrown && projectile.CountsAsClass(DamageClass.Ranged))//Projectile.thrown
            {
                if (Main.rand.Next(4) == 0)
                {
                    modifiers.ScalingArmorPenetration += 1f;
                    //CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height), Color.White, "Defense Ignored!", false, false);
                }
            }

        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (Main.player[projectile.owner].HasBuff(ModContent.BuffType<Goliath>()) && projectile.aiStyle == 161 ) projectile.scale *= 1.2f;
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[projectile.owner];
            if (target.life <= 0 && projectile.CountsAsClass(DamageClass.Ranged) && player.GetModPlayer<MyPlayer>().rotfireSet) //Projectile.thrown
            {
                for (int i = 0; i < 6; i++)
                {

                    Vector2 perturbedSpeed = new Vector2(0, 4).RotatedBy(MathHelper.ToRadians(90 + 60 * i));
                    Projectile.NewProjectile(Projectile.InheritSource(projectile), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<CursedBoltSeeking>(), 50, 1, Main.myPlayer, 0, 0);

                }
            }
            if (target.life <= 0 && projectile.CountsAsClass(DamageClass.Magic) && player.GetModPlayer<MyPlayer>().bloodboilSet)
            {
                for (int i = 0; i < 4; i++)
                {

                    Vector2 perturbedSpeed = new Vector2(0, 4).RotatedByRandom(MathHelper.ToRadians(360));
                    Projectile.NewProjectile(Projectile.InheritSource(projectile), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<IchorBoltSeeking>(), 40, 1, Main.myPlayer, 0, 0);

                }
            }
            if (chillEffect)
            {
                target.GetGlobalNPC<MyNPC>().chillStacks += 1;
                target.AddBuff(ModContent.BuffType<CrushingFreeze>(), 300);
            }
            if (player.GetModPlayer<MyPlayer>().chillsteelSet && projectile.CountsAsClass(DamageClass.Ranged))
            {
                target.AddBuff(BuffID.Frostburn, 300);
            }
        }
        public override bool? CanHitNPC(Projectile projectile, NPC target)
        {
            if (target.GetGlobalNPC<MyNPC>().reflectsProjectilesCustom && target.CanReflectProjectile(projectile) && projectile.DamageType != DamageClass.SummonMeleeSpeed)
            {
                TryReflectOrKillProjectile(projectile, target);
                if (Main.player[projectile.owner].heldProj != projectile.whoAmI) return false;
            }
            return null;
        }

        public static void TryReflectOrKillProjectile(Projectile projectile, NPC target)
        {
            if (CanReflectCustom(projectile) && projectile.velocity != Vector2.Zero)
            {
                PlaySound(SoundID.Item150, projectile.position);
                for (int i = 0; i < 3; i++)
                {
                    int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Smoke);
                    Main.dust[dust].velocity *= 0.3f;
                }
                projectile.hostile = true;
                projectile.friendly = false;
                Vector2 vector = Main.player[projectile.owner].Center - projectile.Center;
                vector.Normalize();
                vector *= projectile.oldVelocity.Length();
                projectile.velocity = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                projectile.velocity.Normalize();
                projectile.velocity *= vector.Length();
                projectile.velocity += vector * 20f;
                projectile.velocity.Normalize();
                projectile.velocity *= vector.Length();
                projectile.damage /= 2; //projectile damage scales independent of its damage stat in expert/master mode when applied to player. so this is still like 3x dmg in expert
                projectile.penetrate = 1;

                if (target.GetGlobalNPC<MyNPC>().reflectVelocity != 0f) projectile.velocity *= target.GetGlobalNPC<MyNPC>().reflectVelocity;
            }
            //else if (projectile.tileCollide && projectile.penetrate == 1) projectile.Kill(); //breaks w boomerangs and some minions + other stuff
            else projectile.penetrate--; //this would run each frame wouldnt it
        }
        public static bool CanReflectCustom(Projectile proj)
        {
            if (proj.active && proj.friendly && !proj.hostile && proj.damage > 0 && proj.velocity != Vector2.Zero && //unsure about the velocity check
            (proj.aiStyle == 1 || proj.aiStyle == 2 || proj.aiStyle == 8 || proj.aiStyle == 21 || proj.aiStyle == 24 || proj.aiStyle == 28 || proj.aiStyle == 29 || proj.aiStyle == 131
            || proj.GetGlobalProjectile<GProj>().forceReflect))
            {
                return true;
            }
            return false;
        }

        public override void OnKill(Projectile Projectile, int timeLeft)
        {
            if (scoriaExplosion)
            {

                for (int num621 = 0; num621 < 20; num621++)
                {
                    int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.LavaMoss, 0f, 0f, 100, default(Color));
                    Main.dust[num622].velocity *= 3f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num622].scale = 0.5f;
                        Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                    }
                }
                for (int num623 = 0; num623 < 35; num623++)
                {
                    int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.LavaMoss, 0f, 0f, 100, default(Color));
                    Main.dust[num624].noGravity = true;
                    Main.dust[num624].velocity *= 5f;
                    num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.LavaMoss, 0f, 0f, 100, default(Color));
                    Main.dust[num624].velocity *= 2f;
                }
                PlaySound(SoundID.Item14, Projectile.position);
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Projectile.Distance(Main.npc[i].Center) < 64 && !Main.npc[i].townNPC)
                        Main.npc[i].SimpleStrikeNPC(Projectile.damage / 2, 0);
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
