using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;


namespace Emperia.Items.Sets.PreHardmode.FrousForest
{
    public class Autumn : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Autumn");
			// Tooltip.SetDefault("Shoots a bolt of coalesced energy that splits into leaves");
		}


        public override void SetDefaults()
        {
            Item.damage = 22;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 13;
            Item.width = 46;
            Item.height = 46;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<AutumnProj>();
            Item.shootSpeed = 8f;
            Item.autoReuse = false;
        }
    }
}
