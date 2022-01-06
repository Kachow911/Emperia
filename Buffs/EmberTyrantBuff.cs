using System;
using Terraria;
using Terraria.ModLoader;
using Emperia.Projectiles.Summon;

namespace Emperia.Buffs
{
	public class EmberTyrantBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ember Tyrant");
			Description.SetDefault("They are all very angry");

			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<EmberTyrant>()] > 0)
			{
				modPlayer.EmberTyrant = true;
			}
			if (!modPlayer.EmberTyrant)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
