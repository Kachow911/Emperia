using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Audio.SoundEngine;
using Emperia.Projectiles;
using Terraria.Audio;

namespace Emperia.Items.Sets.PreHardmode.Granite   //where is located
{
    public class GraniteSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Sword");
			Tooltip.SetDefault("Hilt strikes have an increased critical hit chance\nCritical hits release explosions of energy");
		}
        public override void SetDefaults()
        {    //Sword name
            Item.damage = 23;           
            Item.DamageType = DamageClass.Melee;            //if it's melee
            Item.width = 24;              //Sword width
            Item.height = 24;             //Sword height
            Item.useTime = 27;          //how fast 
            Item.useAnimation = 27;     
            Item.useStyle = 1;        //Style is how this Item is used, 1 is the style of the sword
            Item.knockBack = 4f;      //Sword knockback
            Item.value = 27000;      
            Item.rare = 1;
			//Item.scale = 1.1f;
            Item.autoReuse = false;   //if it's capable of autoswing.    
			Item.UseSound = SoundID.Item1;
			//Item.crit = 6;			
        }

		int currentHitbox = 0;
		int lastFrameWidth = 0;
		int hiltSize = 0;
		Rectangle hiltHitbox = Rectangle.Empty;
		public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
		{
			if (player.itemAnimation == (int)(Item.useAnimation * player.GetAttackSpeed(DamageClass.Melee)))
			{
				currentHitbox = 0;
				lastFrameWidth = hitbox.Width;
			}
			if (lastFrameWidth != hitbox.Width)
			{
				lastFrameWidth = hitbox.Width;
				currentHitbox++;
			}

			switch (currentHitbox) //melee weapons cycle through 3 hitboxes. this makes the hilt hitbox adjust accordingly and match with where the hilt is visually
			{
				case 0:
					hiltSize = (int)(hitbox.Width / 2 * 0.55f); //hitbox.Width at the start of a Melee swing is 2x as wide. 0.55f is the arbitrary hilt size, at 1f it'd be the size of the whole sword
					hiltHitbox.Height = hiltSize;
					hiltHitbox.Width = hiltSize * 2;
					hiltHitbox.X = (int)player.Center.X + 2 * player.direction - hiltSize;
					hiltHitbox.Y = player.gravDir == 1 ? hitbox.Bottom - hiltSize : hitbox.Top;
					break;
				case 1:
					hiltHitbox.Height = hiltSize;
					hiltHitbox.Width = hiltSize;
					hiltHitbox.X = player.direction == 1 ? hitbox.Left : hitbox.Right - hiltSize;
					hiltHitbox.Y = player.gravDir == 1 ? hitbox.Bottom - hiltSize : hitbox.Top;
					break;
				case 2:
					hiltHitbox.Height = hitbox.Height;
					hiltHitbox.Width = hiltSize;
					hiltHitbox.X = player.direction == 1 ? hitbox.Left : hitbox.Right - hiltSize;
					hiltHitbox.Y = hitbox.Top;
					break;
			}
			/*for (int h = 0; h < hiltHitbox.Width; h += 2)
			{
				for (int g = 0; g <= hiltHitbox.Height; g += 2)
				{
					Projectile.NewProjectile(player.GetSource_Item(Item), new Vector2(hiltHitbox.Left + h, hiltHitbox.Top + g), Vector2.Zero, ModContent.ProjectileType<RedPixel>(), 0, 0);
				}
			}*/

			/*Main.spriteBatch.Begin();
			Utils.DrawRect(Main.spriteBatch, hiltHitbox, Color.DarkRed);
			Main.spriteBatch.End();*/
		}
		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (target.getRect().Intersects(hiltHitbox))
			{
				if (Main.rand.Next(99) + 1 > (Item.crit + player.GetCritChance(DamageClass.Melee) + player.GetCritChance(DamageClass.Generic) + 15)) crit = false;
				else crit = true;
				if (crit && modPlayer.graniteSet && modPlayer.graniteTime >= 900) PlaySound(new SoundStyle("Emperia/Sounds/Custom/HeavyThud3") with { Volume = 1.35f, PitchVariance = 0.2f }, player.Center);
				else PlaySound(new SoundStyle("Emperia/Sounds/Custom/HeavyThud2") with { Volume = 1.0f, PitchVariance = 0.2f }, player.Center);
			}
			if (crit)
			{
				if (modPlayer.graniteSet && modPlayer.graniteTime >= 900)
				{
					damage = (int)((float)damage * 1.875f);
				}
				else
				{
					damage = (int)((float)damage * 1.25f);
				}
			}
		}
		public override void OnHitNPC (Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (crit)
			{
				MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
				if (modPlayer.graniteSet && modPlayer.graniteTime >= 900)
				{
					PlaySound(SoundID.Item14, target.position);
					for (int i = 0; i < Main.npc.Length; i++)
					{
						if (target.Distance(Main.npc[i].Center) < 126 && Main.npc[i] != target)
							Main.npc[i].StrikeNPC((int) (damage), 0f, 0, false, false, false);
					}
					for (int i = 0; i < 45; ++i)
					{
						int index2 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
						Main.dust[index2].noGravity = true;
						Main.dust[index2].velocity *= 5.5f;
					}
					modPlayer.graniteTime = 0;
				}
				else
				{
					PlaySound(SoundID.Item10, target.position);
					for (int i = 0; i < Main.npc.Length; i++)
					{
						if (target.Distance(Main.npc[i].Center) < 90 && Main.npc[i] != target)
							Main.npc[i].StrikeNPC((int) (damage), 0f, 0, false, false, false);
					}
					for (int i = 0; i < 30; ++i)
					{
						int index2 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 1.5f);
						Main.dust[index2].noGravity = true;
						Main.dust[index2].velocity *= 3.75f;
					}
				}
			}
		}
        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(null, "GraniteBar", 8); 
            recipe.AddTile(TileID.Anvils); 			//you need 1 DirtBlock  //at work bench
            recipe.Register();
            

        }
    }
}
