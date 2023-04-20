using System;
using Terraria;
using Terraria.ModLoader;
using Emperia.Projectiles.Summon;

namespace Emperia.Buffs
{
	public class SharkMinionBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Depth Scrounger");
			// Description.SetDefault("Epic shark moment");

			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<SharkMinion>()] > 0)
			{
				modPlayer.sharkMinion = true;
			}
			if (!modPlayer.sharkMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
