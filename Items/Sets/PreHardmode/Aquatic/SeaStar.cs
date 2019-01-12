using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Aquatic
{
	public class SeaStar : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sea's Star");
			Tooltip.SetDefault("Bounces on tile hits");
		}


        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Shuriken);        
            item.shoot = mod.ProjectileType("SeaStarProj");
            item.shootSpeed = 8f;
			item.rare = 3;
            item.autoReuse = true;
            item.damage = 32;
            item.knockBack = 3f;
			item.value = Terraria.Item.sellPrice(0, 0, 60, 0);

        }
    }
}
