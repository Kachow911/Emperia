using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles;

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
            Item.damage = 41;  
            Item.DamageType = DamageClass.Magic;
            Item.crit = -4;
            Item.width = 52;     
            Item.height = 30;    
            Item.useStyle = 5;    
            Item.noMelee = true; 
            Item.knockBack = 2.75f;
            Item.useTurn = false;
            Item.value = 215000;
            Item.rare = 5;
            Item.UseSound = SoundID.Item12;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Escarbeam>(); 
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.shootSpeed = 16f;
            Item.mana = 7;  
        }
        int shootAngle = 0;
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.ToRadians(((shootAngle % 2 == 0) ? 0 : (shootAngle * 5))));
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            velocity = perturbedSpeed;
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