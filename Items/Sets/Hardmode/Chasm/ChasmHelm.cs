using Emperia.Tiles;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace Emperia.Items.Sets.Hardmode.Chasm
{
	[AutoloadEquip(EquipType.Head)]
	public class ChasmHelm : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Chasm Helm");
			// Tooltip.SetDefault("12% increased melee damage and critikal strike chance");
		}
    	public override void UpdateEquip(Player player)
    	{
    		player.GetDamage(DamageClass.Melee) *= 1.12f;
			player.GetCritChance(DamageClass.Melee) += 12;
    	}
		public override void SetDefaults() {
			Item.width = 22;
			Item.height = 26;
			Item.value = 41000;
			Item.rare = ItemRarityID.Pink;
			Item.defense = 12;
		}
	}
}