using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.GoblinArmy
{
	public class ShadowBall : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadowspikes");
			Tooltip.SetDefault("");
		}


        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Shuriken);        
            item.shoot = mod.ProjectileType("ShadowBallProj");
            item.shootSpeed = 8f;
			item.rare = 3;
            item.autoReuse = true;
            item.damage = 16;
            item.knockBack = 3f;
			item.value = Terraria.Item.sellPrice(0, 0, 60, 0);

        }
    }
}
