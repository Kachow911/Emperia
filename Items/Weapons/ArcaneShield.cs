using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Audio.SoundEngine;
using System.Linq;

namespace Emperia.Items.Weapons
{
    [AutoloadEquip(EquipType.Shield)]
    public class ArcaneShield : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Arcane Shield");
			Tooltip.SetDefault("Correctly time a shield strike to restore a high amount of mana");
		}
        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Magic;
            Item.width = 32;
            Item.height = 38;
            Item.useTime = 30;
            Item.useAnimation = 30;   
            Item.useStyle = 5;
            Item.knockBack = 4f;  
            Item.value = 36000;        
            Item.rare = 1;
            Item.autoReuse = true;
            Item.useTurn = false; 
            Item.noUseGraphic = true;
        }

        int delay = 0; //checks when the Item starts and stops being used
        bool delaySet = false;

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine vanity = tooltips.FirstOrDefault(x => x.Name == "VanityLegal" && x.Mod == "Terraria");
            if (vanity != null) tooltips.Remove(vanity);
        }
        public override bool? CanHitNPC(Player player, NPC target)
		{
            if (delay >= 17 && delay <= 25) return null;
            else return false;
        }

        public override void HoldItem(Player player)
		{
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.arcaneShieldHold = true;
            if (!player.controlUseItem && delay >= 28)
            {
                delay = 0;
                delaySet = true;
            }
            if (!delaySet) delay++;
        }

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            //-4 & -8 w/o speed reduction
            if (target.boss) player.velocity.X = -10f * player.direction;
            else player.velocity.X = -6f * player.direction;
            if (target.GetGlobalNPC<MyNPC>().IsNormalEnemy(target))
            {
                player.statMana += 60;
                player.ManaEffect(60);
                PlaySound(SoundID.MaxMana, player.Center);
                player.AddBuff(BuffID.ManaRegeneration, 480); // 360
            }
		}

        public override bool? UseItem(Player player)
        {
            delaySet = false;
            return true;
        }

		public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
		{
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.arcaneShieldRaised = true;

            //if (delay == 1) PlaySound(SoundID.Item1, player.Center);
            //if (delay == 1) PlaySound(SoundID.Item, -1, -1, Mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ItemShield"));

            if (player.direction == 1) hitbox.X += 19;
            else hitbox.X += 13;
            hitbox.Y += 29;
            //hitbox.Height += 2;
            hitbox.Width -= 6;

            if (delay == 17)
            {
                //PlaySound(SoundID.Item28, player.Center);
                for (int i = 0; i < 4; ++i)
                    {
                        int index2 = Dust.NewDust(new Vector2(hitbox.Center.X - 4, hitbox.Center.Y - 4), 6, 6, 56, 0.0f, 0.0f, 200, default(Color), 0.8f);
                    	Main.dust[index2].noGravity = true;
                    	Main.dust[index2].velocity *= 0.65f;
                    }
            }
		}
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ManaCrystal, 2);
			recipe.AddRecipeGroup("Emperia:SilverBar", 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
			
		}
	}
}