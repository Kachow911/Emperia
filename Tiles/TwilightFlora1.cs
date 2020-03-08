using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;

namespace Emperia.Tiles
{
	public class TwilightFlora1 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
			16,
			16,
			};
			TileObjectData.addTile(Type);
			//adjTiles = new int[] { mod.TileType("TwilightGrass") };
		}

	}
}