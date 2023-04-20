using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Items.Sets.PreHardmode.Aquatic
{
    public class Wave : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Wave");
			// Tooltip.SetDefault("Summons magic waves to defeat enemies");
		}


        public override void SetDefaults()
        {
            Item.damage = 34;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 13;
            Item.width = 46;
            Item.height = 46;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = 5;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<WaveProj>();
            Item.shootSpeed = 8f;
            Item.autoReuse = false;
        }
    }
}
