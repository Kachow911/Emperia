using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Emperia.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class YetiMask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.rare = 3;
			item.vanity = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yeti Mask");
            Tooltip.SetDefault("Let the fury of the beast flow through you!");
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }
    }
}
