using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class ChilledFootprint : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Summons a ridable yeti mount");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 30;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.value = 30000;
			item.rare = 3;
			item.UseSound = SoundID.Item79;
			item.noMelee = true;
			item.mountType = mod.MountType("Yetiling");
		}
	}
}