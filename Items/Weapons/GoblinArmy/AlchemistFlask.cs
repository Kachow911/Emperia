using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Flasks;

namespace Emperia.Items.Weapons.GoblinArmy 
{
public class AlchemistFlask : ModItem
{
	private int mode = 1;
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Alchemical Flask");
		// Tooltip.SetDefault("Normal fire throws multiple colors of flasks, each having different effects\nRight click to throw a flask that will heal you and your teammates");
	}
	public override void SetDefaults()
	{
		Item.width = 16;  
		Item.damage = 59;  
		Item.mana = 12;//Keep this reasonable please.
		Item.DamageType = DamageClass.Magic;  
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 28;
		Item.useStyle = 1;
		Item.useTime = 28;
		Item.knockBack = 0f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item106;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 16;  //The height of the .png file in pixels divided by 2.
		Item.value = 60000;  //Value is calculated in copper coins.
		Item.rare = 5;  //Ranges from 1 to 11.
		Item.shoot = ModContent.ProjectileType<GoblinFlask1>();
		Item.shootSpeed = 9f;
	}
	public override bool AltFunctionUse(Player player)
	{
		return true;
	}	
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
	{
		if (!(player.altFunctionUse == 2))
		{
			int x = Main.rand.Next(3);
			if (x == 0)
			{
				damage = 70;
				type = ModContent.ProjectileType<GoblinFlask1F>();
			}
			if (x == 1)				
				type = ModContent.ProjectileType<GoblinFlask2F>();
			if (x == 2)
				type = ModContent.ProjectileType<GoblinFlask3F>();
			return true;
		}
		else
		{
			damage = 0;
			type = ModContent.ProjectileType<GoblinFlask4F>();
			return true;
		}

	}
}}
