using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Crimson;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;

namespace Emperia.Items.Weapons
{ 
	public class CloudGrenade : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cloud Grenade");
			// Tooltip.SetDefault("Creates a powerful gust that can knock away both enemies and the thrower");
		}

        public override void SetDefaults()
        {
            Item.useStyle = 5;
            Item.width = 16;
            Item.height = 16;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.UseSound = SoundID.Item1;
            Item.damage = 1; //0
            //Item.DamageType = DamageClass.Ranged;
            Item.consumable = true;
            Item.maxStack = 999;
            Item.shoot = ModContent.ProjectileType<CloudGrenadeProj>();
            Item.shootSpeed = 8.0f;
            Item.knockBack = 10f;
			Item.value = Item.sellPrice(0, 0, 1, 50);
            Item.crit = 0;
            Item.rare = 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(25);
            recipe.AddIngredient(ItemID.Grenade, 25);
            recipe.AddIngredient(ItemID.Feather, 1);
            recipe.AddTile(TileID.SkyMill);
            recipe.AddCondition(Condition.NearWater);
            recipe.Register();
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine damage = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.Mod == "Terraria");
            if (damage != null)
            {
                tooltips.FirstOrDefault(x => x.Name == "Damage").Text = "No damage";
                //tooltips.Insert(tooltips.IndexOf(damage) + 1, new TooltipLine(Mod, "Damage", "No damage"));
                //tooltips.Remove(damage);
            }
            TooltipLine crit = tooltips.FirstOrDefault(x => x.Name == "CritChance" && x.Mod == "Terraria");
            if (damage != null)
            {
                tooltips.Remove(crit);
            }
        }
    }
    public class CloudGrenadeProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cloud Grenade");
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 40;
            Projectile.ignoreWater = false;
            Projectile.aiStyle = 2;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.damage = 0;
        }

        public override void AI()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (!Main.npc[i].dontTakeDamage && Main.npc[i].active && Projectile.Hitbox.Intersects(Main.npc[i].Hitbox))
                {
                    Projectile.Kill();
                    break;
                }
            }
            if (Projectile.velocity.Y > 11) Projectile.velocity.Y = 11;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 40; i++)
            {
                int smokeDust = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.width * 2, Projectile.position.Y - Projectile.height * 2), Projectile.width * 5, Projectile.height * 5, 16, 0.0f, 0.0f, 60, new Color(53f, 67f, 253f), 1f);
                /*Main.dust[smokeDust].velocity *= Main.rand.NextFloat(1.75f, 3.5f);
                Main.dust[smokeDust].scale *= Main.rand.NextFloat(1.75f, 2.75f);
                Main.dust[smokeDust].noGravity = true;*/
                Main.dust[smokeDust].velocity *= Main.rand.NextFloat(4.75f, 6.25f);
                Main.dust[smokeDust].scale *= Main.rand.NextFloat(1.5f, 2f);
                Main.dust[smokeDust].noGravity = true;
            }
            for (int i = 0; i < 4; i++)
            {
                Vector2 angle = Vector2.One.RotatedByRandom(6.28f);
                //Main.NewText(angle);
                angle.Y += 2; //this makes the gore even, no idea why??
                Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, angle, Main.rand.Next(11, 13), 1f);
            }
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                TryApplyKnockback(Main.npc[i]);
            }
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                TryApplyKnockback(Main.player[i]);
            }
        }
        public bool TryApplyKnockback(Entity target)
        {
            if (!target.active || target.Distance(Projectile.Center) > 112 || !Collision.CanHit(Projectile.Center, 1, 1, target.Center, 1, 1)) return false;
            Vector2 direction = target.Center - Projectile.Center;
            if (target.velocity.Y == 0 && direction.Y > 0) direction.Y = 0; //makes grounded enemies not get knocked into the ground
            if (direction == Vector2.Zero) direction.Y--; //prevents NaN error
            direction.Normalize();
            //Main.NewText(direction);
            direction *= new Vector2(11, 10) * (((112 - target.Distance(Projectile.Center)) / 280) + 0.4f); // the / 280 + 0.4f makes the effect fade off scaling not as strong
            
            if (target is NPC)
            {
                if ((target as NPC).dontTakeDamage || (target as NPC).knockBackResist == 0) return false;
                direction *= 1 - (1 - (target as NPC).knockBackResist) / 4; // reduces the effect of knockback resistance
                target.velocity *= 0.6f; //reduces the effect of their current velocity
            }
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
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return true;
        }
    }
}