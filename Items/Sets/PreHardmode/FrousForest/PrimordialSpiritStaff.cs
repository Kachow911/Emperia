using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles.Summon;
using Emperia.Buffs;
namespace Emperia.Items.Sets.PreHardmode.FrousForest
{
	public class PrimordialSpiritStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Primordial Spirit Staff");
            // Tooltip.SetDefault("Summons a primordial spirit that launches bursts of leaves at foes");

        }


		public override void SetDefaults()
		{
            Item.width = 42;
            Item.height = 36;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.mana = 10;
            Item.damage = 12;
            Item.knockBack = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;        
            Item.DamageType = DamageClass.Summon;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<ForestMinion>();
            //Item.buffType = ModContent.BuffType<ForestMinionBuff>();
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
	}
}