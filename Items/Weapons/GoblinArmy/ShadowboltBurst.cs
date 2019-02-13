using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.GoblinArmy
{
    public class ShadowboltBurst : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadowburst");
			Tooltip.SetDefault("Releases a burst of damaging shadow blasts");
		}


        public override void SetDefaults()
        {
            item.damage = 22;
            item.magic = true;
            item.mana = 13;
            item.width = 46;
            item.height = 46;
            item.useTime = 10;
            item.useAnimation = 30;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item34;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("ShadowBolt");
            item.shootSpeed = 8f;
			item.reuseDelay = 8;
            item.autoReuse = false;
        }
    }
}
