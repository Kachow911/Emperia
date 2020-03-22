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
            DisplayName.SetDefault("Rotfire Helm");
            Tooltip.SetDefault("8% increased throwing damage");
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 140000;
            item.rare = 4;
            item.defense = 7;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("RotfireChestplate") && legs.type == mod.ItemType("RotfireLeggings");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Enemies killed by throwing weapons explode into seeking cursed bolts";
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.rotfireSet = true;

        }

        public override void UpdateEquip(Player player)
        {
            player.thrownDamage *= 1.08f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RottenChunk, 5);
            recipe.AddIngredient(ItemID.CursedFlame, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}