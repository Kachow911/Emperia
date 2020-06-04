using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Granite
{
	public class GraniteCanister : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Canister");
			Tooltip.SetDefault("Creates a forcefield to do something");
		}


        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.width = 16;
            item.height = 16;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.thrown = true;
            item.channel = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("GraniteCanisterProj");
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 8.0f;
            item.knockBack = 3.5f;
			item.value = Item.sellPrice(0, 0, 1, 50);
            item.crit = 4;
            item.rare = 1;
            item.autoReuse = true;
            item.maxStack = 1;
            item.consumable = true;
        }
		
    }
}