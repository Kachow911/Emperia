using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Audio.SoundEngine;
using Terraria.Audio;

namespace Emperia.Items.Sets.PreHardmode.Granite {
public class GranitePickaxe : ModItem
{
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Pickaxe");
            Tooltip.SetDefault("Enhanced mining abilties when used while wearing granite armor");
        }
        public override void SetDefaults()
    {
        Item.damage = 8;
        Item.DamageType = DamageClass.Melee;
        Item.width = 46;
        Item.height = 46;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.useTurn = true;
        Item.pick = 59;
        Item.useStyle = 1;
        Item.knockBack = 2f;
        Item.value = 22500;
        Item.rare = 1;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "GraniteBar", 6);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        
    }

        int delay = 0;
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.graniteSet && modPlayer.graniteTime >= 900 && !Main.SmartCursorIsUsed)
            {
                if (delay == 0 && player.controlUseItem)
                {
                    delay = Item.useTime;
                    int i = (int)(Main.MouseWorld.X / 16);
                    int j = (int)(Main.MouseWorld.Y / 16);
                    if (Main.SmartCursorShowing)
                    {
                        i = Main.SmartCursorX;
                        j = Main.SmartCursorY;
                    }
                    //int rangeX = Player.tileRangeX + Item.tileBoost;
                    //int rangeY = Player.tileRangeY + Item.tileBoost;
                    int playerTileX = (int)((player.position.X + player.width * 0.5) / 16.0);
                    int playerTileY = (int)((player.position.Y + player.height * 0.5) / 16.0);
                    if (playerTileX >= i - Player.tileRangeX && playerTileX <= i + Player.tileRangeX && playerTileY >= j - Player.tileRangeY && playerTileY <= j + Player.tileRangeY && Main.tileSolid[Framing.GetTileSafely(i, j).TileType])
                    {
                        int maxRange = 5;
                        int radius = (maxRange - 1) / 2;
                        for (int x = -radius; x <= radius; x++)
                        {
                            for (int y = -radius; y <= radius; y++)
                            {
                                Tile tile = Framing.GetTileSafely(i + x, j + y);
                                if (radius + 1 >= Math.Abs(x) + Math.Abs(y) && Math.Abs(x) + Math.Abs(y) != 0 && tile.HasTile && Main.tileSolid[tile.TileType])
                                {
                                    player.PickTile(i + x, j + y, Item.pick);
                                    for (int d = 0; d < 2; d++)
                                    {
                                        int index2 = Dust.NewDust(new Vector2((i + x) * 16 + 8, (j + y) * 16 + 8), 16, 16, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
                                        Main.dust[index2].noGravity = true;
                                        Main.dust[index2].velocity *= 3f;
                                    }
                                }
                            }
                        }
                        PlaySound(2, i * 16, j * 16, 14);
                        modPlayer.graniteTime = 0;
                    }
                }
                if (delay > 0) delay--;
                if (player.itemAnimation < delay) delay = player.itemAnimation - 1;
            }
        }
    }
}
