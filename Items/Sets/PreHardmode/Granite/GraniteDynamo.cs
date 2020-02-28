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
			DisplayName.SetDefault("Granite Dynamo Staff");
            Tooltip.SetDefault("Tooltip tooltip");

        }


		public override void SetDefaults()
		{
            item.width = 48;
            item.height = 46;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 2;
            item.mana = 20;
            item.damage = 18;
            item.knockBack = 7;
            item.useStyle = 1;
            item.useTime = 31;
            item.useAnimation = 31;        
            item.summon = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("GraniteMinion");
            item.shootSpeed = 5f;
            item.buffType = mod.BuffType("GraniteMinionBuff");
            item.buffTime = 3600;
            item.UseSound = SoundID.Item44;
            item.knockBack = 0f;
        
		
        }
	   
	}
}