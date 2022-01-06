using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Granite {
	[AutoloadEquip(EquipType.Head)]
public class GraniteHelm : ModItem
{
    
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Helm");
			Tooltip.SetDefault("3% increased critical strike chance");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 140000;
        Item.rare = 2;
        Item.defense = 6;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<GraniteChestplate>() && legs.type == ModContent.ItemType<GraniteLeggings>();
    }
    
    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Generates charge over time, powering up your next granite energy blast";
		if (Main.rand.Next(5) == 0)
		{
			int num622 = Dust.NewDust(new Vector2(player.position.X + player.width / 2, player.position.Y + player.height / 2),1, 1, 15, 0f, 0f, 74, new Color(53f, 67f, 253f), 0.8f);
		}
		player.armorEffectDrawShadow = true;
		player.armorEffectDrawShadowSubtle = true;
		player.armorEffectDrawOutlines = true;
		MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
		modPlayer.graniteSet = true;
		
    }
    
    public override void UpdateEquip(Player player)
    {
		player.GetCritChance(DamageClass.Generic) += 3;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "GraniteBar", 8);
        recipe.AddRecipeGroup("Emperia:EvilHide", 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        
    }
}}