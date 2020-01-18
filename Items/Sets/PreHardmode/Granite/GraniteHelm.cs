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
			Tooltip.SetDefault("+2% damage");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 50000;
        item.rare = 2;
        item.defense = 5; //15
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == mod.ItemType("GraniteChestplate") && legs.type == mod.ItemType("GraniteLeggings");
    }
    
  

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "7% damage reduction \nSparkles Provide Light";
        player.endurance += 0.07f;
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
    	player.meleeDamage *= 1.02f;
        player.thrownDamage *= 1.02f;
        player.rangedDamage *= 1.02f;
        player.magicDamage *= 1.02f;
        player.minionDamage *= 1.02f;
    }
    
    public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "GraniteBar", 8);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }
}}