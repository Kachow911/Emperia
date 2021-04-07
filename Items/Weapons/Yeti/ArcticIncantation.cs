using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace Emperia.Items.Weapons.Yeti
{
	public class ArcticIncantation : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 24;
			item.magic = true;
			item.noMelee = true;
			item.width = 22;
			item.height = 24;
			item.useTime = 45;
			item.useAnimation = 45;
			item.useStyle = 5;
			item.knockBack = 4;
			item.value = 52500;
			item.rare = 1;
			item.UseSound = SoundID.Item28;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("IceCrystal");
			item.shootSpeed = 5f;
			item.mana = 26;
		}

   		public override void SetStaticDefaults()
   		{
   			DisplayName.SetDefault("Arctic Star");
			Tooltip.SetDefault("Shoots a magic ice crystal that splits into shards");
   		}
	}
}
