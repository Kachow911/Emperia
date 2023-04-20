using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using static Terraria.Audio.SoundEngine;

namespace Emperia.Items.Weapons
{
    public class FlowerGun : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flower Gun");
		}
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item11;
            Item.useTurn = false;  
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<FlowerGunBulb>();
            Item.shootSpeed = 10f;
        }
    }

    public class FlowerGunBulb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flower Bulb");
        }
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 2;
            Projectile.timeLeft = 180;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position, Vector2.Zero, ModContent.ProjectileType<FlowerGunBlossom>(), 0, 0, Projectile.owner);
            return true;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                int grass = Dust.NewDust(Projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), DustID.JungleGrass);
                //Main.dust[grass].scale = 0.75f;
            }
            PlaySound(SoundID.Item14, Projectile.Center);
        }
    }

    public class FlowerGunBlossom : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flower Pad");
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Main.projFrames[Projectile.type] = 2;
            Projectile.scale *= 1.4f;
            //Projectile.aiStyle = 2;
            //Projectile.timeLeft = 180;
        }
        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter == 8) Projectile.frame++;

            for (int i = 0; i < Main.maxPlayers; i++)
            {
                if (Projectile.Hitbox.Intersects(Main.player[i].Hitbox))
                {
                    //if (Main.player[i].velocity.Y >= 0) Main.player[i].velocity.Y = 0;
                    if (Main.player[i].justJumped)
                    {
                        TryApplyKnockback(Main.player[i]);
                        Projectile.Kill();
                    }
                }
            }
        }
        public bool TryApplyKnockback(Entity target)
        {

            Vector2 direction = target.Center - Projectile.Center;
            if (target.velocity.Y == 0 && direction.Y > 0) direction.Y = 0; //makes grounded enemies not get knocked into the ground
            if (direction == Vector2.Zero) direction.Y--; //prevents NaN error
            direction.Normalize();
            //Main.NewText(direction);
            direction *= new Vector2(11, 10) * (((112 - target.Distance(Projectile.Center)) / 280) + 0.4f); // the / 280 + 0.4f makes the effect fade off scaling not as strong

            if (target is Player)
            {
                if ((target as Player).noKnockback) return false;
                target.velocity *= 0.9f;
                if (target.velocity.Y > 0 && direction.Y < 0) target.velocity.Y *= 0.3f; //reduces the effect of their current velocity, especially to break falls
            }

            target.velocity += direction;
            if (target.velocity.Y <= 0 || target is Player && direction.Y < 0) target.velocity.Y -= 2.5f; //gives a vertical boost to the player and non-falling enemies
            if (target.velocity.Y <= 0 && target is Player) (target as Player).fallStart = (int)((target as Player).position.Y / 16f);
            //Main.NewText(target.velocity.Y);
            return true;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                int petal = Dust.NewDust(Projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), DustID.Ice_Red);
                Main.dust[petal].color = Color.Red; //new Color(255, 0, 0);
            }
            PlaySound(SoundID.Grass);
        }
    }
}
