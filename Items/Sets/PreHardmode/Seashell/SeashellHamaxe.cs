using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Seashell {
public class SeashellHamaxe : ModItem
{
	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Seashell Hamaxe");
            // Tooltip.SetDefault("Swing speed increases dramatically in water or rain\nIncreases mobility in water when held\nHold UP to descend slower");
        }
    public override void SetDefaults()
    {
        Item.damage = 10;
        Item.DamageType = DamageClass.Melee;
        Item.width = 46;
        Item.height = 46;
        Item.useTime = 26;
        Item.useAnimation = 26;
        Item.useTurn = true;
        Item.axe = 22;
		Item.hammer = 70;
        Item.useStyle = 1;
        Item.knockBack = 5.5f;
        Item.value = 19000;
        Item.rare = 1;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        
    }

    public override float UseSpeedMultiplier(Player player)
	{
        if (player.wet && !player.lavaWet && !player.honeyWet || (Main.raining && WorldGen.InAPlaceWithWind(player.position, player.width, player.height))) return base.UseSpeedMultiplier(player) * 1.75f;
		return base.UseSpeedMultiplier(player);
	}

   public override void MeleeEffects(Player player, Rectangle hitbox)
   {
        if ((player.wet && !player.lavaWet && !player.honeyWet || (Main.raining && WorldGen.InAPlaceWithWind(player.position, player.width, player.height))) && player.itemAnimation % 2 == 0)
        {
            int dust1 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 107, 0.0f, 0.0f, 0, default, Main.rand.NextFloat(0.6f, 0.8f));
            Main.dust[dust1].noGravity = true;
            Main.dust[dust1].velocity.X = (player.itemAnimation / (float)player.itemAnimationMax + 0.25f) * player.direction;
            Main.dust[dust1].velocity.Y = (player.itemAnimationMax - player.itemAnimation) / (float)player.itemAnimationMax;
            Main.dust[dust1].velocity *= 2f;
        }
   }

   public override void AddRecipes()
    {
       Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.Seashell, 2);
            recipe.AddIngredient(ItemID.Coral, 2);
            recipe.AddIngredient(null, "SeaCrystal", 1);  			
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
    }
}}
