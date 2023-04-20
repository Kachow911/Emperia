using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles.Summon;
using Emperia.Buffs;
namespace Emperia.Items.Weapons.Volcano
{
	public class EmberTyrantStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ember Tyrant Staff");
            // Tooltip.SetDefault("Summons an Ember Tyrant to fight for you");

        }


		public override void SetDefaults()
		{
            Item.width = 46;
            Item.height = 44;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3;
            Item.mana = 10;
            Item.damage = 38;
            Item.knockBack = 7;
            Item.useStyle = 1;
            Item.useTime = 30;
            Item.useAnimation = 30;        
            Item.DamageType = DamageClass.Summon;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<EmberTyrant>();
            Item.buffType = ModContent.BuffType<EmberTyrantBuff>();
            Item.buffTime = 3600;
            Item.UseSound = SoundID.Item44;
        
		
    }
	    public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        
        /*public override bool? UseItem(Player player)
        {
            if(player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
            }
            return base.UseItem(player);
        }*/

        //this might get handled by vanilla now, idk
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            position = Main.MouseWorld;
            //speedX = speedY = 0;
            velocity = Vector2.Zero;
            return player.altFunctionUse != 2;
            return true;
        }
	}
}