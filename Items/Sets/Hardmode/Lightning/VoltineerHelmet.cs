using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Lightning {
    [AutoloadEquip(EquipType.Head)]
    public class VoltineerHelmet : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Voltineer Helmet");
            Tooltip.SetDefault("9% increased magic damage");
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 140000;
            item.rare = 4;
            item.defense = 11;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("VoltineerChestplate") && legs.type == mod.ItemType("VoltineerLeggings");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Using lightning weapons causes you to shock nearby enemies\nDealing 500 damage with lightning weapons will supercharge you for 5 seconds";
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.lightningSet = true;

        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage *= 1.09f;
        }

        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RottenChunk, 5);
            recipe.AddIngredient(ItemID.CursedFlame, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}