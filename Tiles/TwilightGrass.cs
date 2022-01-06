using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;


namespace Emperia.Tiles
{
    public class TwilightGrass : ModTile
    {
        public override void SetStaticDefaults()
        {
			//AddToArray(ref TileID.Sets.Conversion.Grass);
			TileID.Sets.Conversion.Grass[Type]=true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
			Main.tileMerge[Type][TileType<Tiles.TwilightStone>()] = true;
            AddMapEntry(new Color(117, 241, 255));
			MineResist = 3f;
			Main.tileBrick[Type] = true;
            ItemDrop = ItemID.DirtBlock;
			SetModTree(new TwilightTree());
			DustType = 72;
      
			MinPick = 100;
      
			SoundType = 6; //6 is grass //11 //18 is money //20 is girl sound
      
			SoundStyle = 6;
      
			
			
        }
		//public override int SaplingGrowthType(ref int style)
        //{
        //    style = 0;
        //    return ModContent.TileType<TwilightTreeSap>();       
        //}
		
		
		
		public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                fail = true;
                Main.tile[i, j].type = TileID.Dirt;
                WorldGen.SquareTileFrame(i, j, true);
			
            }
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
                //   Terraria.Audio.SoundEngine.PlaySound(0, x * 16, y * 16, 1, 1f, 0f);
            }
            return false;
        }

        public override void RandomUpdate(int i, int j)
        {
            if (!Framing.GetTileSafely(i, j - 1).IsActive)// && Main.rand.Next(40) == 0)
            {
                TwilightGrass.PlaceObject(i, j - 1, TileType<Tiles.TwilightFlora>());
                NetMessage.SendObjectPlacment(-1, i, j - 1, TileType<Tiles.TwilightFlora>(), 0, 0, -1, -1);
            }
        }
		public void SpreadAncientGrassAcrossTheWorld()
        {
            for (int k = 0; k < Main.maxTilesX; k++)
            {
                bool flag2 = true;
                int num = 0;
                while ((double)num < Main.maxTilesY)
                {
                    if (Main.tile[k, num].IsActive)
                    {
                        if (flag2 && Main.tile[k, num].type == TileID.Dirt)
                        {
                            try
                            {
                                WorldGen.SpreadGrass(k, num, TileID.Dirt, TileType<Tiles.TwilightGrass>(), true, Main.tile[k, num].Color);
                            }
                            catch
                            {
                                WorldGen.SpreadGrass(k, num, TileID.Dirt, TileType<Tiles.TwilightGrass>(), true, Main.tile[k, num].Color);
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