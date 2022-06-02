using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Tiles
{
    public class TwilightDirt : ModTile
    {
        public override void SetStaticDefaults()
        {
			//AddToArray(ref TileID.Sets.Conversion.Grass);
			//TileID.Sets.Conversion.Grass[Type]=true;
            //Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            AddMapEntry(new Color(61, 57, 92));
			Main.tileBrick[Type] = true;
            ItemDrop = ModContent.ItemType<Items.Grotto.GrottoDirt>();
			//SetModTree(new TwilightTree());
			DustType = 175;
            MineResist = 0.5f;
        }
		//public override int SaplingGrowthType(ref int style)
        //{
        //    style = 0;
        //    return TileType<Tiles.TwilightTreeSap>();       
        //}
		
        public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int alternate = 0, int random = -1, int direction = -1)
        {
            TileObject toBePlaced;
            if (!TileObject.CanPlace(x, y, type, style, direction, out toBePlaced, false))
            {
                return false;
            }
            toBePlaced.random = random;
            if (TileObject.Place(toBePlaced) && !mute)
            {
                WorldGen.SquareTileFrame(x, y, true);
                //   Terraria.Audio.SoundEngine.PlaySound(0, x * 16, y * 16, 1, 1f, 0f);
            }
            return false;
        }

     
		public void SpreadAncientGrassAcrossTheWorld()
        {
            for (int k = 0; k < Main.maxTilesX; k++)
            {
                bool flag2 = true;
                int num = 0;
                while ((double)num < Main.maxTilesY)
                {
                    if (Main.tile[k, num].HasTile)
                    {
                        if (flag2 && Main.tile[k, num].TileType == TileID.Dirt)
                        {
                            try
                            {
                                WorldGen.SpreadGrass(k, num, TileID.Dirt, ModContent.TileType<Tiles.TwilightGrass>(), true, Main.tile[k, num].TileColor);
                            }
                            catch
                            {
                                WorldGen.SpreadGrass(k, num, TileID.Dirt, ModContent.TileType<Tiles.TwilightGrass>(), true, Main.tile[k, num].TileColor);
                            }
                        }
                        if ((double)num > WorldGen.worldSurfaceHigh)
                        {
                            break;
                        }
                        flag2 = false;
                    }
                    else if (Main.tile[k, num].WallType == 0)
                    {
                        flag2 = true;
                    }
                    num++;
                }
            }
        }
    }
}