using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles.Desert;
using Emperia.Buffs;
namespace Emperia.Items.Sets.PreHardmode.Desert
{
	public class ScrutiniserStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Scrutiniser Staff");
            // Tooltip.SetDefault("Summons a scrounger to fight for you");

        }


		public override void SetDefaults()
		{
            Item.width = 42;
            Item.height = 36;
            Item.value = 27000;
            Item.rare = 1;
            Item.mana = 10;
            Item.damage = 38;
            Item.knockBack = 7;
            Item.useStyle = 1;
            Item.useTime = 30;
            Item.useAnimation = 30;        
            Item.DamageType = DamageClass.Summon;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Summon.SharkMinion>();
            Item.buffType = ModContent.BuffType<Buffs.SharkMinionBuff>();
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

        //might be handled by vanilla now idk
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
	        return player.altFunctionUse != 2;
            position = Main.MouseWorld;
            //speedX = speedY = 0;
            velocity = Vector2.Zero;
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DesertEye", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }
    }
}