using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Mushor
{
	public class Shroomerang : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shroomerang");
			Tooltip.SetDefault("You can throw up to 5 at a time\nEnemies killed explode into spores");
		}
        public override void SetDefaults()
        {
            item.damage = 34;
            item.melee = true;
            item.width = 24;
            item.height = 46;
            item.useTime = 20;
            item.shootSpeed = 14f;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 3f;
            item.shoot = mod.ProjectileType("Shroomerang");
            item.value = Item.sellPrice(0, 0, 16, 0);
            item.rare = 4;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override bool CanUseItem(Player player)
        {
			int count= 0;
            for (int i = 0; i < 255; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == mod.ProjectileType("Shroomerang"))
                {
                    count++;
                }
            }
            return count < 5;
		}
	}
}
