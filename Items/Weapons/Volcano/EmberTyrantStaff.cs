using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Items.Weapons.Volcano
{
	public class EmberTyrantStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ember Tyrant Staff");
            Tooltip.SetDefault("Summons an Ember Tyrant to fight for you");

        }


		public override void SetDefaults()
		{
            item.width = 46;
            item.height = 44;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 3;
            item.mana = 10;
            item.damage = 38;
            item.knockBack = 7;
            item.useStyle = 1;
            item.useTime = 30;
            item.useAnimation = 30;        
            item.summon = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("EmberTyrant");
            item.buffType = mod.BuffType("EmberTyrantBuff");
            item.buffTime = 3600;
            item.UseSound = SoundID.Item44;
        
		
    }
	    public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        
        public override bool UseItem(Player player)
        {
            if(player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
            }
            return base.UseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
	        return player.altFunctionUse != 2;
            position = Main.MouseWorld;
            speedX = speedY = 0;
            return true;
        }
	}
}