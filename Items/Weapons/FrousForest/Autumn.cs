using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.FrousForest
{
    public class Autumn : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Autumn");
			Tooltip.SetDefault("Shoots a bolt of coalesced energy that splits into leaves");
		}


        public override void SetDefaults()
        {
            item.damage = 22;
            item.magic = true;
            item.mana = 13;
            item.width = 46;
            item.height = 46;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item34;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("AutumnProj");
            item.shootSpeed = 8f;
            item.autoReuse = false;
        }
    }
}
