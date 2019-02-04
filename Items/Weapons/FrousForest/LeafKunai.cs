using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.FrousForest
{
	public class LeafKunai : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leaf Kunai");
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
            item.consumable = true;
            item.maxStack = 999;
            item.shoot = mod.ProjectileType("LeafKunaiProj");
            item.useAnimation = 18;
            item.useTime = 18;
            item.shootSpeed = 8.0f;
            item.damage = 9;
            item.knockBack = 3.5f;
			item.value = Item.sellPrice(0, 0, 1, 50);
            item.crit = 4;
            item.rare = 2;
            item.autoReuse = true;
            item.maxStack = 999;
            item.consumable = true;
        }
    }
}