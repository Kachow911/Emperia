using Terraria;
using Terraria.ModLoader;

namespace Emperia.Buffs
{
	public class YetiMount : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Yetiling");
			Description.SetDefault("The Yetiling is a pretty chill dude");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(mod.MountType<Mounts.Yetiling>(), player);
			player.buffTime[buffIndex] = 10;
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			modPlayer.yetiMount = true;
		}
	}
}
