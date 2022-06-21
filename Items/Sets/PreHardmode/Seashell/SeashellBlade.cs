using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;
using static Terraria.Audio.SoundEngine;

namespace Emperia.Items.Sets.PreHardmode.Seashell
{
    public class SeashellBlade : ModItem
    {
		 public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seashell Blade");
			Tooltip.SetDefault("Land five strikes in quick succession to fire a powerful coral blast");
		}
        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Melee;
            Item.width = 16;
            Item.height = 16;
            Item.useTime = 25;
            Item.useAnimation = 25;     
            Item.useStyle = 1;
            Item.knockBack = 3.5f;
            Item.value = 22000;
            Item.rare = 1;
			Item.scale = 1f;
            Item.autoReuse = true;
            Item.useTurn = true;
        }
		
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.Seashell, 4);
            recipe.AddIngredient(ItemID.Coral, 4);
            recipe.AddIngredient(null, "SeaCrystal", 1); 			
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
        }

        bool hitEnemy = false;
        int seaBladeCount = 0;
        int seaBladeTimer = 0;
        public override bool? UseItem(Player player)
        {
            if (player.itemAnimation == player.itemAnimationMax) PlaySound(SoundID.Item1 with { Pitch = -0.25f + 0.15f * seaBladeCount }, player.Center);
            if (player.itemAnimation == 1)
            {
                if (hitEnemy)
                {
                    seaBladeCount++;
                    seaBladeTimer = 50;
                    //Main.NewText(seaBladeCount);
                }
                else seaBladeCount = 0;
                hitEnemy = false;
            } 
            return null;
        }
        public override void UpdateInventory(Player player)
        {
            if (seaBladeTimer > 0) seaBladeTimer--;
            else seaBladeCount = 0;
        }
        //could use holditem to reset timer
        public override void ModifyHitNPC (Player player, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
                hitEnemy = true;
                if (seaBladeCount >= 4)
                {
                    hitEnemy = false;
                    if (crit) { knockback *= 1.5f; }
                    else { knockback *= 3f; }
                    knockback *= (1.4f - target.knockBackResist);//more accurate on flimsy npcs
                    PlaySound(SoundID.Item70 with { Volume = 0.5f }, player.Center);
                    seaBladeCount = 0;
                    /*for (int i = 0; i < 4; ++i)
                    {
                        //Main.NewText(((Main.rand.Next(30) + 20 * i) * (Main.rand.NextBool(2) == true ? 1 : -1)).ToString());
                        Vector2 placePosition = target.Center - new Vector2((Main.rand.Next(30) + 20 * i) * (Main.rand.NextBool(2) == true ? 1 : -1), (Main.rand.Next(30) + 20 * i) * (Main.rand.NextBool(2) == true ? 1 : -1)); //minimum distance, random max distance, 50% to turn it negative
                		Vector2 direction = target.Center - placePosition;
                		direction.Normalize();
                		Projectile.NewProjectile(source, placePosition.X, placePosition.Y, direction.X * (4f + i), direction.Y * (4f + i), ModContent.ProjectileType<CoralBurst>(), damage, 0, Main.myPlayer, 0, 0);
                    }*/
			        Vector2 direction = target.Center - player.Center;
			        direction.Normalize();
                    Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center.X, player.Center.Y, direction.X * 5f, direction.Y * 5f, ModContent.ProjectileType<CoralShard>(), damage * 2, 1, Main.myPlayer, 0, 0);
                    //Main.NewText(modPlayer.seaBladeCount.ToString());
                }
			/*if (crit)
			{
				Vector2 placePosition = player.Center + new Vector2(0, -400);
				Vector2 direction = target.Center - placePosition;
				direction.Normalize();
				Projectile.NewProjectile(source, player.Center.X, player.Center.Y - 400, direction.X * 10f, direction.Y * 10f, ModContent.ProjectileType<SeashellBladeProj>(), 20, 1, Main.myPlayer, 0, 0);
			}*/
		}
    }
}
