using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Emperia.Backgrounds
{
	public class GrottoBG : ModSurfaceBgStyle
	{
        public override bool ChooseBgStyle()
        {
            return !Main.gameMenu && (Main.player[Main.myPlayer].GetModPlayer<MyPlayer>().ZoneGrotto);
        }

        public override void ModifyFarFades(float[] fades, float transitionSpeed)
		{
			for (int i = 0; i < fades.Length; i++)
			{
				if (i == Slot)
				{
					fades[i] += transitionSpeed;
					if (fades[i] > 1f)
					{
						fades[i] = 1f;
					}
				}
				else
				{
					fades[i] -= transitionSpeed;
					if (fades[i] < 0f)
					{
						fades[i] = 0f;
					}
				}
			}
		}
		public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
		{
            b -= 750;
            scale = 1.1f;
            return mod.GetBackgroundSlot("Backgrounds/GrottoBackground");
		}
	}
}
