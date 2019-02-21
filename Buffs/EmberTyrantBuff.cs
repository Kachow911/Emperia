using System;
using Terraria;
using Terraria.ModLoader;

namespace Emperia.Buffs
{
	public class EmberTyrantBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Ember Tyrant");
			Description.SetDefault("They are all very angry");

			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("EmberTyrant")] > 0)
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
