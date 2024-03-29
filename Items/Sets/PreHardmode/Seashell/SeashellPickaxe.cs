using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Seashell {
public class SeashellPickaxe : ModItem
{
	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Seashell Pickaxe");
            // Tooltip.SetDefault("Mining valuables while submerged in water will restore breath\nIncreases mobility in water when held\nHold UP to descend slower");
        }
        public override void SetDefaults()
    {
        Item.damage = 9;
        Item.DamageType = DamageClass.Melee;
        Item.width = 46;
        Item.height = 46;
        Item.useTime = 15;
        Item.useAnimation = 30;
        Item.useTurn = true;
        Item.pick = 55;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 2f;
        Item.value = 19000;
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

     public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.Seashell, 2);
            recipe.AddIngredient(ItemID.Coral, 2);
            recipe.AddIngredient(null, "SeaCrystal", 1); 			
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        /* public override void HoldItem(Player player) //runs too late to affect movement. code in myplayer
         {
             player.trident = true;
         }*/

        int delay = 0;
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            if (delay == 0 && player.controlUseItem)
            {
                delay = Item.useTime;
                int i = Player.tileTargetX;
                int j = Player.tileTargetY;
                if (Item.GetGlobalItem<GItem>().TileInRange(Item, player))
                {
                    //TileID.Meteorite, TileID.Demonite, TileID.Crimtane lol these SUCK cus they can be broken on the surface but not underground and youd have to check lol. too lazy
                    int[] indestructibleOres = { TileID.Hellstone, TileID.Cobalt, TileID.Palladium, TileID.Mythril, TileID.Orichalcum, TileID.Adamantite, TileID.Titanium, TileID.Chlorophyte };
                    bool isIndestructible = false;
                    if (indestructibleOres.Contains(Framing.GetTileSafely(i, j).TileType) || ModContent.GetModTile(Framing.GetTileSafely(i, j).TileType) is ModTile modTile && modTile.MinPick > Item.pick || Main.tileContainer[Framing.GetTileSafely(i, j).TileType]) isIndestructible = true;
                    if (!isIndestructible && player.GetModPlayer<MyPlayer>().targetedTileIsSpelunker)
                    {
                        if (player.breath < player.breathMax) player.breath += 10;
                        if (player.breath > player.breathMax) player.breath = player.breathMax - 1;
                        if (player.wet && !player.lavaWet && !player.honeyWet)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                //Lighting.AddLight(new Vector2(i * 16, j * 16), new Vector3(0, 255, 30));
                                int dust1 = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.TerraBlade, 0.0f, 0.0f, 0, default, 1.1f); //267, new Color(60, 255, 20)
                                Main.dust[dust1].noGravity = true;
                            }
                        }
                    }
                }
            }
            if (delay > 0) delay--;
            if (player.itemAnimation < delay) delay = player.itemAnimation - 1;
        }
    }
}
