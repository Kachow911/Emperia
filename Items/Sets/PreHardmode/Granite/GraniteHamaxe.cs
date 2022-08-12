using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Audio.SoundEngine;
using Terraria.Audio;
using Emperia.Projectiles;

namespace Emperia.Items.Sets.PreHardmode.Granite {
public class GraniteHamaxe : ModItem
{
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Hamaxe");
            Tooltip.SetDefault("Enhanced destructive abilties when used while wearing granite armor");
        }
    public override void SetDefaults()
    {
        Item.damage = 9;
        Item.DamageType = DamageClass.Melee;
        Item.width = 46;
        Item.height = 46;
        Item.useTime = 16;
        Item.useAnimation = 32;
        Item.useTurn = true;
        Item.axe = 14;
		Item.hammer = 65; //65
        Item.useStyle = 1;
        Item.knockBack = 2f;
        Item.value = 22500;
        Item.rare = 1;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }
        int delay = 0;
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (modPlayer.graniteSet && modPlayer.graniteTime >= 900)
            {
                if (delay == 0 && player.controlUseItem) //this allows the effect can trigger mid swing, since usetime != useanimation
                {
                    delay = Item.useTime;
                    int i = Player.tileTargetX;
                    int j = Player.tileTargetY;
                    if (Item.GetGlobalItem<GItem>().TileInRange(Item, player))
                    {
                        if (Main.tileAxe[Framing.GetTileSafely(i, j).TileType]) //hits tree stump multiple times to instantly break it. could just multiply the axe power but i figure that could break progression of some modded trees
                        {
                            for (int k = 0; k < 7; k++)
                            {
                                player.PickTile(i, j, Item.axe);
                                for (int d = 0; d < 3; d++)
                                {
                                    int index2 = Dust.NewDust(new Vector2(i * 16 + 8, j * 16 + 8), 16, 16, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
                                    Main.dust[index2].noGravity = true;
                                    Main.dust[index2].velocity *= 3f;
                                    PlaySound(SoundID.Item14, new Vector2(i * 16, j * 16));
                                    modPlayer.graniteTime = 0;
                                }
                            }
                        }
                        else if ((!Framing.GetTileSafely(i, j).HasTile || !Main.tileSolid[Framing.GetTileSafely(i, j).TileType]) && !Main.SmartCursorIsUsed) //if the player tries to break a wall
                        {
                            int wX = i;
                            int wY = j;
                            if (modPlayer.targetedWallTypePre <= 0 || !Collision.HitWallSubstep(i, j)) //if there's no wall at the cursor location
                            {
                               int num = -1;
                               if (((Main.mouseX + Main.screenPosition.X) / 16f) < Math.Round((Main.mouseX + Main.screenPosition.X) / 16f))
                               {
                                   num = 0;
                               }
                               int num2 = -1;
                               if (((Main.mouseY + Main.screenPosition.Y) / 16f) < Math.Round((Main.mouseY + Main.screenPosition.Y) / 16f))
                               {
                                   num2 = 0;
                               }
                               for (int x = num; x <= num + 1; x++)
                               {
                                   for (int y = num2; y <= num2 + 1; y++) //see how close the cursor is to nearby tiles and run through the ones that are close enough to see if they can be broken
                                   {
                                        if ((int)modPlayer.wallsAroundCursorPre.GetValue(x + 1, y + 1) > 0 && (Collision.HitWallSubstep(i + x, j + y) || Framing.GetTileSafely(i, j).WallType <= 0)) // the last condition means it will trigger on walls that were broken this frame
                                        {
                                            wX = i + x; //all this is to mimic vanilla wall targeting behavior btw. otherwise the effect wouldnt trigger on the same tile vanilla chooses. yes this is insane. after 8 hours it mostly works though :)
                                            wY = j + y;
                                            break;
                                        }
                                   }
                                   if (wX != i || wY != j) break;
		                       }
                            }
                            if (modPlayer.targetedWallTypePre > 0 && (Collision.HitWallSubstep(i, j) || Framing.GetTileSafely(i, j).WallType <= 0) || wX != i || wY != j)//if the selected tile or an adjacent tile withing cursor range has a breakable wall
                            {                            
                                //Projectile.NewProjectile(Entity.GetSource_None(), new Vector2(wX * 16 + 8, wY * 16 + 8), Vector2.Zero, ModContent.ProjectileType<RedPixel>(), 0, 0);
                                int maxRange = 5;
                                int radius = (maxRange - 1) / 2;
                                for (int x = -radius; x <= radius; x++)
                                {
                                    for (int y = -radius; y <= radius; y++)
                                    {
                                        Tile tile = Framing.GetTileSafely(wX + x, wY + y);
                                        if (radius + 1 >= Math.Abs(x) + Math.Abs(y) && tile.WallType > 0 && Collision.HitWallSubstep(wX + x, wY + y))
                                        {
                                            WorldGen.KillWall(wX + x, wY + y); //explode boooom
                                            for (int d = 0; d < 2; d++)
                                            {
                                                int index2 = Dust.NewDust(new Vector2((wX + x) * 16 + 8, (wY + y) * 16 + 8), 16, 16, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
                                                Main.dust[index2].noGravity = true;
                                                Main.dust[index2].velocity *= 3f;
                                            }
                                        }
                                    }
                                }
                                PlaySound(SoundID.Item14, new Vector2(wX * 16, wY * 16));
                                modPlayer.graniteTime = 0;
                            }
                        }
                    }
                }
            }
            if (delay > 0) delay--;
            if (player.itemAnimation < delay) delay = player.itemAnimation - 1;
        }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "GraniteBar", 6);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        
    }
}}