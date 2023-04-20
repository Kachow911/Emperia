using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Ice;

namespace Emperia.Items.Sets.PreHardmode.Chillsteel
{
	public class ChillsteelDagger : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chillsteel Dagger");
			// Tooltip.SetDefault("Pierces one enemy, inflicting 'Crushing Freeze'");
		}


        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.width = 16;
            Item.height = 16;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            //Item.thrown = true;
            Item.DamageType = DamageClass.Ranged;
            Item.channel = true;
            Item.noMelee = true;
            Item.consumable = true;
            Item.shoot = ModContent.ProjectileType<ChillDaggerProj>();
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.shootSpeed = 8.0f;
            Item.damage = 16;
            Item.knockBack = 3.5f;
			Item.value = Item.sellPrice(0, 0, 1, 50);
            Item.crit = 4;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.consumable = true;
        }
    }
}