using Terraria;
using Terraria.ModLoader;

namespace Emperia.Backgrounds
{
	public class VolcanoUG : ModUgBgStyle
	{
		public override bool ChooseBgStyle()
		{
			return Main.player[Main.myPlayer].GetModPlayer<MyPlayer>(mod).ZoneVolcano;
		}

		public override void FillTextureArray(int[] textureSlots)
		{
			textureSlots[0] = mod.GetBackgroundSlot("Backgrounds/VolcanoUG");
			textureSlots[1] = mod.GetBackgroundSlot("Backgrounds/VolcanoUG");
			textureSlots[2] = mod.GetBackgroundSlot("Backgrounds/VolcanoUG");
			textureSlots[3] = mod.GetBackgroundSlot("Backgrounds/VolcanoUG");
		}
	}
}