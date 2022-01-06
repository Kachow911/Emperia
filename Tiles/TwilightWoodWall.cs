using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Tiles
{
    public class TwilightWoodWall : ModWall
    {
        public override void SetStaticDefaults() {
            AddMapEntry(new Color(117, 241, 255));
        }
    }
}