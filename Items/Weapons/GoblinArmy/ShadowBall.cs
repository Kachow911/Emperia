using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Items.Weapons.GoblinArmy
{
	public class ShadowBall : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shadowspikes");
			// Tooltip.SetDefault("");
		}


        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Shuriken);        
            Item.shoot = ModContent.ProjectileType<ShadowBallProj>();
            Item.shootSpeed = 8f;
			Item.rare = 3;
            Item.autoReuse = true;
            Item.damage = 16;
            Item.knockBack = 3f;
			Item.value = Terraria.Item.sellPrice(0, 0, 60, 0);

        }
    }
}
