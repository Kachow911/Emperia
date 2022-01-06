using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

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
            Item.CloneDefaults(ItemID.Shuriken);        
            Item.shoot = ModContent.ProjectileType<SeaStarProj>();
            Item.shootSpeed = 8f;
			Item.rare = 3;
            Item.autoReuse = true;
            Item.damage = 32;
            Item.knockBack = 3f;
			Item.value = Terraria.Item.sellPrice(0, 0, 60, 0);

        }
    }
}
