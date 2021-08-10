using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

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
            item.damage = 18;
            item.magic = true;
            item.width = 32;
            item.height = 38;
            item.useTime = 30;
            item.useAnimation = 30;   
            item.useStyle = 5;
            item.knockBack = 4f;  
            item.value = 36000;        
            item.rare = 1;
            item.autoReuse = true;
            item.useTurn = false; 
            item.noUseGraphic = true;
        }

        int delay = 0; //checks when the item starts and stops being used
        bool delaySet = false;

        public override bool? CanHitNPC(Player player, NPC target)
		{
            if (delay >= 17 && delay <= 25) return true;
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
            if (target.type != NPCID.TargetDummy)
            {
                player.statMana += 60;
                player.ManaEffect(60);
                Main.PlaySound(SoundID.MaxMana, player.Center);
                player.AddBuff(BuffID.ManaRegeneration, 480); // 360
            }
		}

        public override bool UseItem(Player player)
        {
            delaySet = false;
            return true;
        }

		public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
		{
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.arcaneShieldRaised = true;

            //if (delay == 1) Main.PlaySound(SoundID.Item1, player.Center);
            //if (delay == 1) Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ItemShield"));

            if (player.direction == 1) hitbox.X += 19;
            else hitbox.X += 13;
            hitbox.Y += 29;
            //hitbox.Height += 2;
            hitbox.Width -= 6;

            if (delay == 17)
            {
                //Main.PlaySound(SoundID.Item28, player.Center);
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
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ManaCrystal, 2);
			recipe.AddRecipeGroup("Emperia:AnySilverBar", 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}