using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Emperia.Projectiles.Mushroom;


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
            Item.damage = 36; 
            Item.DamageType = DamageClass.Ranged;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = 5;
            Item.knockBack = 3.5f;
            Item.value = 100;
            Item.rare = 3;
            Item.scale = 1f;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.useTurn = false;  
            Item.noMelee = true;

            Item.shoot = ModContent.ProjectileType<ShroomNade2>();
            Item.shootSpeed = 10f;
        }
    }
}
