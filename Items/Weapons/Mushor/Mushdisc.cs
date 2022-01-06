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
    public class Mushdisc : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mush-Disc");
			Tooltip.SetDefault("Shoots an explosive disc which splits into several shards");
		}
        public override void SetDefaults()
        {
            Item.damage = 32; 
            //Item.thrown = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 5;
            Item.knockBack = 3.5f;
            Item.value = 100;
            Item.rare = 3;
            Item.scale = 1f;
			Item.consumable = false;
			Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.useTurn = true;    
            Item.noMelee = true;
            Item.maxStack = 1;
            Item.shoot = ModContent.ProjectileType<MushDisc>();
            Item.shootSpeed = 7f;
        }
    }
}
