﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Lightning {
    [AutoloadEquip(EquipType.Head)]
    public class VoltineerHeadpiece : ModItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Voltineer Headpiece");
            // Tooltip.SetDefault("9% increased summon damage");
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 140000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<VoltineerChestplate>() && legs.type == ModContent.ItemType<VoltineerLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Using lightning weapons causes you to shock nearby enemies\nDealing 500 damage with lightning weapons will supercharge you for 5 seconds";
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.lightningSet = true;

        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) *= 1.09f;
        }

        /*public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.RottenChunk, 5);
            recipe.AddIngredient(ItemID.CursedFlame, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            
        }*/
    }
}