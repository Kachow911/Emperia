using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

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
            item.damage = 16;
            item.melee = true;
            item.width = 16;
            item.height = 16;
            item.useTime = 25;
            item.useAnimation = 25;     
            item.useStyle = 1;
            item.knockBack = 3.5f;
            item.value = 100;        
            item.rare = 1;
			item.scale = 1f;
            item.autoReuse = true;
            item.useTurn = true;
        }
		
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.Seashell, 4);
            recipe.AddIngredient(ItemID.Coral, 4);
            recipe.AddIngredient(null, "SeaCrystal", 1); 			
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        /*public override bool CanUseItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.seaBladeCount == 0 && modPlayer.seaBladeTimer > 5)
            {
                return false;
            }
            else return true;
        }*/
        bool firstHit = false;
        bool forSomeUngodlyReasonUseItemRunsAfterOnHitNPCSoThisMakesTheCodeWorkIfYouHitOnTheFirstFrame = false;
        public override bool UseItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            //Main.NewText("new use");
            //Main.NewText(modPlayer.seaBladeCount.ToString(), 255, 20, 20);
            if (!forSomeUngodlyReasonUseItemRunsAfterOnHitNPCSoThisMakesTheCodeWorkIfYouHitOnTheFirstFrame)
            {
                firstHit = true;
            }
            else { forSomeUngodlyReasonUseItemRunsAfterOnHitNPCSoThisMakesTheCodeWorkIfYouHitOnTheFirstFrame = false; }
            Main.PlaySound(2, (int)player.Center.X, (int)player.Center.Y, 1, 1f, (-0.25f + 0.15f * modPlayer.seaBladeCount)); //rises in pitch over time
            return true;
        }
		public override void ModifyHitNPC (Player player, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.seaBladeTimer = (int)(item.useAnimation * player.meleeSpeed) + 10;
            if (firstHit || player.itemAnimation + 1 == (int)(item.useAnimation * player.meleeSpeed))
            {

                firstHit = false;
                if (modPlayer.seaBladeCount >= 4)
                {
                    if (crit) { knockback *= 1.5f; }
                    else { knockback *= 3f; }
                    knockback *= (1.4f - target.knockBackResist);//more accurate on flimsy npcs
                    Main.PlaySound(SoundID.Item70.WithVolume(.5f));
                    modPlayer.seaBladeCount = 0;
                    /*for (int i = 0; i < 4; ++i)
                    {
                        //Main.NewText(((Main.rand.Next(30) + 20 * i) * (Main.rand.NextBool(2) == true ? 1 : -1)).ToString());
                        Vector2 placePosition = target.Center - new Vector2((Main.rand.Next(30) + 20 * i) * (Main.rand.NextBool(2) == true ? 1 : -1), (Main.rand.Next(30) + 20 * i) * (Main.rand.NextBool(2) == true ? 1 : -1)); //minimum distance, random max distance, 50% to turn it negative
                		Vector2 direction = target.Center - placePosition;
                		direction.Normalize();
                		Projectile.NewProjectile(placePosition.X, placePosition.Y, direction.X * (4f + i), direction.Y * (4f + i), mod.ProjectileType("CoralBurst"), damage, 0, Main.myPlayer, 0, 0);
                    }*/
			        Vector2 direction = target.Center - player.Center;
			        direction.Normalize();
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, direction.X * 5f, direction.Y * 5f, mod.ProjectileType("CoralShard"), damage * 2, 1, Main.myPlayer, 0, 0);
                    //Main.NewText(modPlayer.seaBladeCount.ToString());
                }
                else { 
                    modPlayer.seaBladeCount++;
                    //Main.NewText(modPlayer.seaBladeCount.ToString());
                }
            }
            if (player.itemAnimation + 1 == (int)(item.useAnimation * player.meleeSpeed))
            {
                forSomeUngodlyReasonUseItemRunsAfterOnHitNPCSoThisMakesTheCodeWorkIfYouHitOnTheFirstFrame = true;
            }
			/*if (crit)
			{
				Vector2 placePosition = player.Center + new Vector2(0, -400);
				Vector2 direction = target.Center - placePosition;
				direction.Normalize();
				Projectile.NewProjectile(player.Center.X, player.Center.Y - 400, direction.X * 10f, direction.Y * 10f, mod.ProjectileType("SeashellBladeProj"), 20, 1, Main.myPlayer, 0, 0);
			}*/
		}
    }
}
