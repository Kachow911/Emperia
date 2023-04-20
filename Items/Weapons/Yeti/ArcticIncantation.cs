using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Emperia.Projectiles.Yeti;

namespace Emperia.Items.Weapons.Yeti
{
	public class ArcticIncantation : ModItem
	{
		public override void SetDefaults()
		{

			Item.damage = 24;
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.width = 22;
			Item.height = 24;
			Item.useTime = 45;
			Item.useAnimation = 45;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 4;
			Item.value = 52500;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item28;
			Item.autoReuse = false;
			Item.shoot = ModContent.ProjectileType<IceCrystal>();
			Item.shootSpeed = 5f;
			Item.mana = 26;
		}

   		public override void SetStaticDefaults()
   		{
   			// DisplayName.SetDefault("Arctic Star");
			// Tooltip.SetDefault("Shoots a magic ice crystal that splits into shards");
   		}
	}
}
