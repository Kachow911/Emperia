using Terraria.WorldBuilding;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Emperia.Tiles
{
	public class TFWood : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlendAll[this.Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			ItemDrop = ModContent.ItemType<Items.Grotto.GrottoWood>();
			AddMapEntry(new Color(74, 107, 140));
			DustType = 121;
		}

public override bool CanExplode(int i, int j)
	{
		return true;
	}
	}
	}

