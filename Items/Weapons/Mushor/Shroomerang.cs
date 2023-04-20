using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Emperia.Projectiles.Mushroom;

namespace Emperia.Items.Weapons.Mushor
{
	public class Shroomerang : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shroomerang");
			// Tooltip.SetDefault("You can throw up to 5 at a time\nEnemies killed explode into spores");
		}
        public override void SetDefaults()
        {
            Item.damage = 34;
            Item.DamageType = DamageClass.Melee;
            Item.width = 24;
            Item.height = 46;
            Item.useTime = 20;
            Item.shootSpeed = 14f;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Mushroom.Shroomerang>();
            Item.value = Item.sellPrice(0, 0, 16, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override bool CanUseItem(Player player)
        {
			int count= 0;
            for (int i = 0; i < 255; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == ModContent.ProjectileType<Projectiles.Mushroom.Shroomerang>())
                {
                    count++;
                }
            }
            return count < 5;
		}
	}
}
