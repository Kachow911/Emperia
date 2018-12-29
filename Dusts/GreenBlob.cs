using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Emperia.Dusts
{
	public class GreenBlob : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noLight = true;
			dust.color = new Color(0, 200, 0);
			dust.scale = 0.9f;
			dust.noGravity = true;
			dust.velocity /= 2f;
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X;
			Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0f, 0.2f, 0f);
			dust.scale -= 0.03f;
			if (dust.scale < 0.5f)
			{
				dust.active = false;
			}
			return false;
			dust.alpha++;
		}
	}
}