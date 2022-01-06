using Terraria;
using Terraria.ModLoader;

using Emperia.Mounts;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Buffs
{
	public class YetiMount : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yetiling");
			Description.SetDefault("The Yetiling is a pretty chill dude");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(MountType<Mounts.Yetiling>(), player);
			player.buffTime[buffIndex] = 10;
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			modPlayer.yetiMount = true;
		}
	}
}
