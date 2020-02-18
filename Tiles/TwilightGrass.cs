using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Emperia.Tiles
{
    public class TwilightGrass : ModTile
    {
        public override void SetDefaults()
        {
			//AddToArray(ref TileID.Sets.Conversion.Grass);
			//TileID.Sets.Conversion.Grass[Type]=true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            AddMapEntry(new Color(117, 241, 255));
			mineResist = 3f;
			Main.tileBrick[Type] = true;
            drop = ItemID.DirtBlock;
			//SetModTree(new TwilightTree());
			dustType = 72;
      
			minPick = 100;
      
			soundType = 6; //6 is grass //11 //18 is money //20 is girl sound
      
			soundStyle = 6;
      
			
			
        }
		public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return mod.TileType("TwilightTreeSap");       
        }
		
		
		
		public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                fail = true;
                Main.tile[i, j].type = TileID.Dirt;
                WorldGen.SquareTileFrame(i, j, true);
			
            }
        }
		
	
		public override void RandomUpdate(int i, int j)
        {
            if(Framing.GetTileSafely(i,j-1).type==0&&Framing.GetTileSafely(i,j-2).type==0&&Main.rand.Next(5) == 0)
            {
                WorldGen.GrowTree(i, j-1);
				if(Main.rand.Next(20)==0)
                {		
                    if(Framing.GetTileSafely(i,j-1).type==0&&Framing.GetTileSafely(i,j-2).type==0)
					{		
                    WorldGen.PlaceObject(i,j-1,mod.TileType("TwilightTreeSap"));
                    NetMessage.SendObjectPlacment(-1,i,j-1,mod.TileType("TwilightTreeSap"),0,0,-1,-1);
					}       
				}
				else
                {
					switch(Main.rand.Next(7)) 
						   {
								case 0: 
								    WorldGen.PlaceObject(i-1,j-1,mod.TileType("TwilightFlora1"));
									NetMessage.SendObjectPlacment(-1,i-1,j-1,mod.TileType("TwilightFlora1"),0,0,-1,-1);
								break;
								case 1: 
								    WorldGen.PlaceObject(i-1,j-1,mod.TileType("TwilightFlora2"));
									NetMessage.SendObjectPlacment(-1,i-1,j-1,mod.TileType("TwilightFlora2"),0,0,-1,-1);
								break;
								case 2: 
								    WorldGen.PlaceObject(i-1,j-1,mod.TileType("TwilightFlora3"));
									NetMessage.SendObjectPlacment(-1,i-1,j-1,mod.TileType("TwilightFlora3"),0,0,-1,-1);
								break;
								case 3: 
								    WorldGen.PlaceObject(i-1,j-1,mod.TileType("TwilightFlora4"));
									NetMessage.SendObjectPlacment(-1,i-1,j-1,mod.TileType("TwilightFlora4"),0,0,-1,-1);
								break;
								case 4: 
								    WorldGen.PlaceObject(i-1,j-1,mod.TileType("MagnificentMushroom"));
									NetMessage.SendObjectPlacment(-1,i-1,j-1,mod.TileType("MagnificentMushroom"),0,0,-1,-1);
								break;
								case 5: 
								    WorldGen.PlaceObject(i-1,j-1,mod.TileType("SunsoakedChestnut"));
									NetMessage.SendObjectPlacment(-1,i-1,j-1,mod.TileType("SunsoakedChestnut"),0,0,-1,-1);
								break;
								default:
								    WorldGen.PlaceObject(i-1,j-1,mod.TileType("TwilightFlora5"));
									NetMessage.SendObjectPlacment(-1,i-1,j-1,mod.TileType("TwilightFlora5"),0,0,-1,-1);
								break;
						   }
					 
               }
                
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