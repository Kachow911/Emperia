using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Emperia.Tiles
{
	public class TFLeaf : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlendAll[this.Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			AddMapEntry(new Color(128, 128, 128));
		}

public override bool CanExplode(int i, int j)
	{
		return true;
	}
	}
	}

