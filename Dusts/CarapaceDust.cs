using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Emperia.Dusts
{
	public class CarapaceDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noLight = true;
			dust.scale = 1.4f;
			dust.velocity /= 4f;
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X;
			dust.scale -= 0.03f;
			if (dust.scale < 0.1f)
			{
				dust.active = false;
			}
			return false;
			dust.alpha += 2;
		}
	}
}