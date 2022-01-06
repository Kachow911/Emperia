using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Emperia.Tiles
{
	public class TwilightWorkbench : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolidTop[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileTable[Type] = true;
			Main.tileLavaDeath[Type] = true;

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);

			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
			TileObjectData.addTile(Type);

			AdjTiles = new int[] { TileID.WorkBenches };
		}
	}
}
