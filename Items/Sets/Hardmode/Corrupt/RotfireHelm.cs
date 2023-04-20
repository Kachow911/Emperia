using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Corrupt {
    [AutoloadEquip(EquipType.Head)]
    public class RotfireHelm : ModItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rotfire Helm");
            // Tooltip.SetDefault("8% increased throwing damage");
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 140000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 7;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<RotfireChestplate>() && legs.type == ModContent.ItemType<RotfireLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Enemies killed by throwing weapons explode into seeking cursed bolts";
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.rotfireSet = true;

        }

        public override void UpdateEquip(Player player)
        {
            //player.thrownDamage *= 1.08f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.RottenChunk, 5);
            recipe.AddIngredient(ItemID.CursedFlame, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            
        }
    }
}