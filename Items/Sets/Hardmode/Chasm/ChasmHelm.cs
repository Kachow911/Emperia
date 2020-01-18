using Emperia.Tiles;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace Emperia.Items.Sets.Hardmode.Chasm
{
	[AutoloadEquip(EquipType.Head)]
	public class ChasmHelm : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Chasm Helm");
			Tooltip.SetDefault("12% increased melee damage and critikal strike chance");
		}
    	public override void UpdateEquip(Player player)
    	{
    		player.meleeDamage *= 1.12f;
			player.meleeCrit += 12;
    	}
		public override void SetDefaults() {
			item.width = 22;
			item.height = 26;
			item.value = 41000;
			item.rare = 5;
			item.defense = 12;
		}
	}
}