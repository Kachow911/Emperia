using System;
using Terraria;
using Terraria.ModLoader;
using Emperia.Projectiles.Granite;

namespace Emperia.Buffs
{
	public class GraniteMinionBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Elemental");
			Description.SetDefault("The granite elemental will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<GraniteMinion>()] > 0)
			{
				modPlayer.graniteMinion = true;
			}
			if (!modPlayer.graniteMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
