using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Emperia.Tiles
{
	public class TwilightFlora1 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			TileObjectData.addTile(Type);
			Main.tileCut[Type] = true;
			Main.tileSolid[Type] = false;
			Main.tileMergeDirt[Type] = true;
			//Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16
			};
			TileObjectData.addTile(Type);
			dustType = 206;
			soundType = 6;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 10;
		}



	}
}