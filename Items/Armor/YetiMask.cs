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
            Item.width = 30;
            Item.height = 24;
            Item.rare = 3;
			Item.vanity = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Yeti Mask");
            // Tooltip.SetDefault("Let the fury of the beast flow through you!");
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }
    }
}
