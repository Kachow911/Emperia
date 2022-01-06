using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Granite;

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
            Item.useStyle = 1;
            Item.width = 16;
            Item.height = 16;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            //Item.thrown = true;
            Item.DamageType = DamageClass.Ranged;
            Item.channel = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<GraniteCanisterProj>();
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.shootSpeed = 8.0f;
            Item.knockBack = 3.5f;
			Item.value = Item.sellPrice(0, 0, 1, 50);
            Item.crit = 4;
            Item.rare = 1;
            Item.autoReuse = true;
            Item.maxStack = 1;
            Item.consumable = true;
        }
		
    }
}