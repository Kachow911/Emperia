using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Emperia.Walls
{
	public class TwilightBrickWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			Main.wallHouse[Type] = true;
			AddMapEntry(new Color(96, 125, 162));
		}
	}
}
