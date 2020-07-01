using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class ProtectiveEnergy : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Metallic Energy");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 4));
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.rare = 0;
		}
		public override bool OnPickup(Player player)
		{
			player.AddBuff(mod.BuffType("ProtectiveBoost"), 480);
			item.active = false;
			Main.PlaySound(SoundID.Grab, (int)player.position.X, (int)player.position.Y, 10);
			return false;
		}
		public override void GrabRange(Player player, ref int grabRange)
		{
			grabRange = (int)(grabRange * 1f);
			base.GrabRange(player, ref grabRange);
			
		}
	}
}