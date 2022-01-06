using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

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
            Item.damage = 22;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 13;
            Item.width = 46;
            Item.height = 46;
            Item.useTime = 10;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<ShadowBolt>();
            Item.shootSpeed = 8f;
			Item.reuseDelay = 8;
            Item.autoReuse = false;
        }
    }
}
