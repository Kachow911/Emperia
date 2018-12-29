using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.GoblinArmy 
{
public class AlchemistFlask : ModItem
{
	private int mode = 1;
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Alchemical Flask");
		Tooltip.SetDefault("Normal fire throws multiple colors of flasks, each having different effects\nRight click to throw a flask that will heal you and your teammates");
	}
	public override void SetDefaults()
	{
		item.width = 16;  
		item.damage = 59;  
		item.mana = 12;//Keep this reasonable please.
		item.magic = true;  
		item.noMelee = true;
		item.noUseGraphic = true;
		item.useAnimation = 28;
		item.useStyle = 1;
		item.useTime = 28;
		item.knockBack = 0f;  //Ranges from 1 to 9.
		item.UseSound = SoundID.Item106;
		item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		item.height = 16;  //The height of the .png file in pixels divided by 2.
		item.maxStack = 1;
		item.value = 60000;  //Value is calculated in copper coins.
		item.rare = 5;  //Ranges from 1 to 11.
		item.shoot = mod.ProjectileType("GoblinFlask1");
		item.shootSpeed = 9f;
	}
	public override bool AltFunctionUse(Player player)
	{
		return true;
	}	
	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		if (!(player.altFunctionUse == 2))
		{
			int x = Main.rand.Next(3);
			if (x == 0)
			{
				damage = 70;
				type = mod.ProjectileType("GoblinFlask1F");
			}
			if (x == 1)				
				type = mod.ProjectileType("GoblinFlask2F");
			if (x == 2)
				type = mod.ProjectileType("GoblinFlask3F");
			return true;
		}
		else
		{
			damage = 0;
			type = mod.ProjectileType("GoblinFlask4F");
			return true;
		}

	}
}}
