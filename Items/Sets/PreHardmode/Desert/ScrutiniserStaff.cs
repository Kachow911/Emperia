using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Items.Sets.PreHardmode.Desert
{
	public class ScrutiniserStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scrutiniser Staff");
            Tooltip.SetDefault("Summons a scrounger to fight for you");

        }


		public override void SetDefaults()
		{
            item.width = 42;
            item.height = 36;
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
            item.shoot = mod.ProjectileType("SharkMinion");
            item.buffType = mod.BuffType("SharkMinionBuff");
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
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertEye", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}