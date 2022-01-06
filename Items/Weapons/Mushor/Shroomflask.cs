using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Mushroom;

namespace Emperia.Items.Weapons.Mushor 
{
public class Shroomflask : ModItem
{
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shroomy Flask");
			Tooltip.SetDefault("Explodes into a potent cloud of mushroom gas on enemy hits\nSummons spores that rise from the ground on tile hits\nDirect hits will return a little mana");
		}
	public override void SetDefaults()
	{
		Item.width = 28;  
		Item.damage = 24;  
		Item.mana = 20;//Keep this reasonable please.
		Item.DamageType = DamageClass.Magic;  
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 45;
		Item.useStyle = 1;
		Item.useTime = 45;
		Item.knockBack = 0f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item106;
		Item.autoReuse = false;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 30;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 60000;  //Value is calculated in copper coins.
		Item.rare = 3;  //Ranges from 1 to 11.
		Item.shoot = ModContent.ProjectileType<ShroomFlask>();
		Item.shootSpeed = 9f;
	}
}}
