using Terraria;
using Terraria.ModLoader;

namespace Emperia.Buffs
{
    public class Bleed : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Bleeding");
            Main.buffNoTimeDisplay[Type] = false;
		}

		public override void Update(NPC NPC, ref int buffIndex)
		{
			NPC.lifeRegen -= 5;
			NPC.defense -= 5;

			if (Main.rand.NextBool(3))
			{
				int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, 5);
				Main.dust[dust].scale = 1.5f;
				Main.dust[dust].noGravity = true;
			}
		}
	}
}