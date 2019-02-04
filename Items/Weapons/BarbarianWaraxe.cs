using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
            item.CloneDefaults(ItemID.Shuriken);        
            item.shoot = mod.ProjectileType("AxeProj");
            item.shootSpeed = 8f;
			item.rare = 1;
            item.autoReuse = true;
            item.damage = 16;
            item.knockBack = 3f;
			item.value = Terraria.Item.sellPrice(0, 0, 60, 0);

        }
    }
}
