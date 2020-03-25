using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.Hardmode.Lightning
{
    public class PulsarFlail : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 52;
            item.noUseGraphic = true;
            item.useTime = 22;
            item.useAnimation = 22;
            item.melee = true;
            item.width = 18;
            item.height = 40;
            item.shoot = mod.ProjectileType("PulsarFlailProj");
            item.shootSpeed = 13f;
            item.useStyle = 1;
            item.knockBack = 5f;
            item.value = Item.sellPrice(0, 0, 60, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
			item.consumable = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pulsar Flail");
        }
		
        public override bool CanUseItem(Player player)
        {
			int count= 0;
            for (int i = 0; i < 255; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == mod.ProjectileType("PulsarFlailProj"))
                {
                    count++;
                }
            }
            return count < 1;
		}
    }
}
