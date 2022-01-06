using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Items.Weapons
{
	public class BarbarianWaraxe : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Barbarian Waraxe");
			Tooltip.SetDefault("Hit enemies take more damage");
		}


        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Shuriken);        
            Item.shoot = ModContent.ProjectileType<AxeProj>();
            Item.shootSpeed = 8f;
			Item.rare = 1;
            Item.autoReuse = true;
            Item.damage = 16;
            Item.knockBack = 3f;
			Item.value = Terraria.Item.sellPrice(0, 0, 60, 0);

        }
    }
}
