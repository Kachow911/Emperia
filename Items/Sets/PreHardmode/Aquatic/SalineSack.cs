using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Aquatic
{
    public class SalineSack : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 34;
            item.noUseGraphic = true;
            item.useTime = 22;
            item.useAnimation = 22;
			item.thrown = true;
            item.width = 18;
            item.height = 40;
            item.shoot = mod.ProjectileType("SaltClump");
            item.shootSpeed = 8f;
            item.useStyle = 1;
            item.knockBack = 5f;
            item.value = Item.sellPrice(0, 0, 12, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
			item.consumable = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Saline Sack");
			Tooltip.SetDefault("Launches clumps of salt");
        }
    }
}
