using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;
using Emperia.Buffs;
using static Terraria.Audio.SoundEngine;

namespace Emperia.Items.Accessories
{
    public class WarlockTorc : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Warlock's Torc");
			Tooltip.SetDefault("15% increased magic damage\nDecreases maximum mana significantly\nIncreases susceptibility to mana sickness\nTightens around the neck when certain mana potions draw near, preventing their use");//Increased susceptibility to mana sickness when worn
		}
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = 1;
            Item.value = 36000;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			player.GetDamage(DamageClass.Magic) += 0.15f;
			modPlayer.warlockTorc = true;
			if (modPlayer.manaOverdoseTime > 0) player.AddBuff(ModContent.BuffType<ManaOverdose>(), modPlayer.manaOverdoseTime);

			if (player.HasBuff(BuffID.ManaRegeneration))
			{
				if (player.buffTime[player.FindBuffIndex(BuffID.ManaRegeneration)] > 900)
				{
					player.DelBuff(player.FindBuffIndex(BuffID.ManaRegeneration));
					player.AddBuff(BuffID.ManaRegeneration, 900);
					PlaySound(SoundID.NPCDeath13, player.Center);
					for (int i = 0; i < 8; ++i)
                    {
                        int index2 = Dust.NewDust(new Vector2(player.Center.X, player.Center.Y - 8), 4, 4, 4, 2.5f * player.direction, -0.5f, 200, new Color(255, 130, 200), 1.2f);
                    }
				}
			}
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ManaCrystal, 1);
			recipe.AddIngredient(ItemID.FallenStar, 2);
			recipe.AddRecipeGroup("Emperia:AnySilverBar", 6);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
			
		}
    }
}
