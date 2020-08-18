using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Emperia.Tiles
{
    public class TwilightDirt : ModTile
    {
        public override void SetDefaults()
        {
			//AddToArray(ref TileID.Sets.Conversion.Grass);
			//TileID.Sets.Conversion.Grass[Type]=true;
            //Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            AddMapEntry(new Color(61, 57, 92));
			Main.tileBrick[Type] = true;
            drop = mod.ItemType("GrottoDirt");
			SetModTree(new TwilightTree());
			dustType = 175;
            mineResist = 0.5f;
        }
		public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return mod.TileType("TwilightTreeSap");       
        }
		
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
                //   Main.PlaySound(0, x * 16, y * 16, 1, 1f, 0f);
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
                    if (Main.tile[k, num].active())
                    {
                        if (flag2 && Main.tile[k, num].type == TileID.Dirt)
                        {
                            try
                            {
                                WorldGen.SpreadGrass(k, num, TileID.Dirt, mod.TileType("TwilightGrass"), true, Main.tile[k, num].color());
                            }
                            catch
                            {
                                WorldGen.SpreadGrass(k, num, TileID.Dirt, mod.TileType("TwilightGrass"), true, Main.tile[k, num].color());
                            }
                        }
                        if ((double)num > WorldGen.worldSurfaceHigh)
                        {
                            break;
                        }
                        flag2 = false;
                    }
                    else if (Main.tile[k, num].wall == 0)
                    {
                        flag2 = true;
                    }
                    num++;
                }
            }
        }
    }
}