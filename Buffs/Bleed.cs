using Terraria;
using Terraria.ModLoader;

namespace Emperia.Buffs
{
    public class Bleed : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Bleeding");
            Main.buffNoTimeDisplay[Type] = false;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.lifeRegen -= 5;
			npc.defense -= 2;

			if (Main.rand.NextBool(3))
			{
				int dust = Dust.NewDust(npc.position, npc.width, npc.height, 5);
				Main.dust[dust].scale = 1.5f;
				Main.dust[dust].noGravity = true;
			}
		}
	}
}