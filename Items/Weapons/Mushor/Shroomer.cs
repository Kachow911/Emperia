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
    public class Shroomer : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Shroomer");
		}
        public override void SetDefaults()
        {
            item.damage = 36; 
            item.ranged = true;
            item.width = 64;
            item.height = 64;
            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 5;
            item.knockBack = 3.5f;
            item.value = 100;
            item.rare = 3;
            item.scale = 1f;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.useTurn = false;  
            item.noMelee = true;

            item.shoot = mod.ProjectileType("ShroomNade2");
            item.shootSpeed = 10f;
        }
    }
}
