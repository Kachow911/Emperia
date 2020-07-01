using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons
{
    public class Escargun : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Escargun");
            Tooltip.SetDefault("Fires lasers in a wave pattern\nAfter ten crits, the gun will briefly supercharge, firing lasers rapidly for 0 mana");
		}

        public override void SetDefaults()
        {
            item.damage = 41;  
            item.magic = true;
            item.crit = -4;
            item.width = 52;     
            item.height = 30;    
            item.useStyle = 5;    
            item.noMelee = true; 
            item.knockBack = 2.75f;
            item.useTurn = false;
            item.value = 215000;
            item.rare = 5;
            item.UseSound = SoundID.Item12;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Escarbeam"); 
            item.useTime = 13;
            item.useAnimation = 13;
            item.shootSpeed = 16f;
            item.mana = 7;  
        }
        int shootAngle = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) //if it needs nerfs, make only angled lasers have the ability to crit
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(((shootAngle % 2 == 0) ? 0 : (shootAngle * 5))));
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            speedX = perturbedSpeed.X;
		    speedY = perturbedSpeed.Y;
            if (shootAngle < 2 && modPlayer.eschargo < 0)
            {
                shootAngle++;
            }
            else if (modPlayer.eschargo >= 0)
            {
                shootAngle = 0;
            }
            else  //loop the firing pattern
            {
                shootAngle = -1; 
            }
            return true;
		}
        public override void ModifyManaCost(Player player, ref float reduce, ref float mult) {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (modPlayer.eschargo >= 0) mult *= 0;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }
}