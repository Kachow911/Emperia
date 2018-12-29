using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;


namespace Emperia.Items.Weapons.Mushor
{
    public class Mushdisc : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mush-Disc");
			Tooltip.SetDefault("Shoots an explosive disc which splits into several shards");
		}
        public override void SetDefaults()
        {
            item.damage = 32; 
            item.thrown = true;
            item.width = 64;
            item.height = 64;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.knockBack = 3.5f;
            item.value = 100;
            item.rare = 3;
            item.scale = 1f;
			item.consumable = false;
			item.noUseGraphic = true;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.useTurn = true;    
            item.noMelee = true;
            item.maxStack = 1;
            item.shoot = mod.ProjectileType("MushDisc");
            item.shootSpeed = 7f;
        }
    }
}
