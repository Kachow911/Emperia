using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Emperia
{
	public class VolcanoScreenShaderData : ScreenShaderData
	{
		private int InqIndex;

		public VolcanoScreenShaderData(string passName)
			: base(passName)
		{
		}

		public override void Apply()
		{
			UseTargetPosition(Main.LocalPlayer.Center);

			base.Apply();
		}
	}
}