using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Crimson {
    [AutoloadEquip(EquipType.Head)]
    public class BloodboilHeadpiece : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodboil Headpiece");
            Tooltip.SetDefault("8% increased magic damage");
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 140000;
            item.rare = 4;
            item.defense = 8;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("BloodboilBreastpiece") && legs.type == mod.ItemType("BloodboilLeggings");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Enemies killed by magic weapons explode into seeking ichor bolts";
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.bloodboilSet = true;

        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage *= 1.08f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Vertebrae, 5);
            recipe.AddIngredient(ItemID.Ichor, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}