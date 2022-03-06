using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Emperia.Tiles
{
	public class TwilightPot : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileCut[Type] = true;
			Main.tileSpelunker[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = Point16.Zero;
			TileObjectData.addTile(Type);

			SoundType = SoundID.Shatter;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			// customize the possible drops to your liking
			var possibleDrops = new (int, int)[]
			{
				(ItemID.Rope, Main.rand.Next(20)),
				(ItemID.SilverCoin, Main.rand.Next(5, 10))
			};

			var (chosenItem, dropAmount) = possibleDrops[Main.rand.Next(possibleDrops.Length)];

			Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 32, 32, chosenItem, dropAmount);
		}
	}
}
