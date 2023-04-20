using Terraria.ID;
using Terraria.ModLoader;

using Emperia.Mounts;

namespace Emperia.Items
{
	public class ChilledFootprint : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Summons a ridable yeti mount");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 30;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = 30000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item79;
			Item.noMelee = true;
			Item.mountType = ModContent.MountType<Yetiling>();
		}
	}
}