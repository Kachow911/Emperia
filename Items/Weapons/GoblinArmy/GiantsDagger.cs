using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Items.Weapons.GoblinArmy
{
	public class GiantsDagger : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Giant's Dagger");
			// Tooltip.SetDefault("Sticks in enemies\nEnemies impaled with the dagger bleed, losing life and defense\nEnemies that die with the dagger in them drop more coins");
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
            Item.shoot = ModContent.ProjectileType<GiantsDaggerProj>();
            Item.useAnimation = 32;
            Item.useTime = 32;
            Item.shootSpeed = 8.0f;
            Item.damage = 22;
            Item.knockBack = 3.5f;
			Item.value = Item.sellPrice(0, 0, 1, 50);
            Item.crit = 4;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.consumable = false;
        }
    }
}