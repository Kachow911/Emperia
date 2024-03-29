using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Items.Sets.PreHardmode.FrousForest
{
	public class SpineVine : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Spine Vine");
			// Tooltip.SetDefault("Sticks in enemies and poisons them");
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
                        Item.shoot = ModContent.ProjectileType<SpineVineProj>();
            Item.maxStack = Terraria.Item.CommonMaxStack;
            Item.useAnimation = 36;
            Item.useTime = 36;
            Item.shootSpeed = 6.0f;
            Item.damage = 24;
            Item.knockBack = 3.5f;
			Item.value = Item.sellPrice(0, 0, 1, 50);
            Item.crit = 4;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.consumable = true;
        }
    }
}