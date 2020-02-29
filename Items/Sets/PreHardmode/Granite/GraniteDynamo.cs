using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Items.Sets.PreHardmode.Granite
{
	public class GraniteDynamo : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Dynamo");
            Tooltip.SetDefault("Summons a granite chunk to fight for you\nFires like a projectile when first summoned");
			Item.staff[item.type] = true;
        }

		public override void SetDefaults()
		{
            item.width = 38;
            item.height = 36;
            item.value = 22500;
            item.rare = 2;
            item.damage = 18;
            item.knockBack = 0f;
            item.useStyle = 5;
            item.useTime = 31;
            item.useAnimation = 31;
            item.mana = 20; 
            item.summon = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("GraniteMinion");
            item.shootSpeed = 8f;
            item.buffType = mod.BuffType("GraniteMinionBuff");
            item.buffTime = 3600;
            item.UseSound = SoundID.Item44;
        }
	}
}