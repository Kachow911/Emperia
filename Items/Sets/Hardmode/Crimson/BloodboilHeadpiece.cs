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
            // DisplayName.SetDefault("Bloodboil Headpiece");
            // Tooltip.SetDefault("8% increased magic damage");
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 140000;
            Item.rare = 4;
            Item.defense = 8;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<BloodboilBreastpiece>() && legs.type == ModContent.ItemType<BloodboilLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Enemies killed by magic weapons explode into seeking ichor bolts";
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.bloodboilSet = true;

        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) *= 1.08f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Vertebrae, 5);
            recipe.AddIngredient(ItemID.Ichor, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            
        }
    }
}