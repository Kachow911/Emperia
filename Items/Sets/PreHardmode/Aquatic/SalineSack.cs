using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Emperia.Projectiles;


namespace Emperia.Items.Sets.PreHardmode.Aquatic
{
    public class SalineSack : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 34;
            Item.noUseGraphic = true;
            Item.useTime = 22;
            Item.useAnimation = 22;
			//Item.thrown = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 18;
            Item.height = 40;
            Item.shoot = ModContent.ProjectileType<SaltClump>();
            Item.shootSpeed = 8f;
            Item.useStyle = 1;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(0, 0, 12, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
			Item.consumable = false;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Saline Sack");
			// Tooltip.SetDefault("Launches clumps of salt");
        }
    }
}
