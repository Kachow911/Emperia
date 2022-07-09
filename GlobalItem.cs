using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using Terraria.DataStructures;
using Emperia;
using Emperia.Projectiles;
using Emperia.Buffs;
using Emperia.Items;
using Emperia.Items.Weapons.Skeletron;
using Emperia.Items.Accessories.Gauntlets;
using static Terraria.Audio.SoundEngine;
using Microsoft.Xna.Framework.Graphics;

namespace Emperia
{
	public class GItem : GlobalItem
	{	
		public override bool InstancePerEntity {get{return true;}}
        //public override bool CloneNewInstances {get{return true;}}		replacing this with code i found elsewhere. hella sketchy!
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            return base.Clone(item, itemClone);
        }

        public int forestSetShots = 2;
		public bool gelPad = false;
		public bool noWristBrace = false;
		public bool noGelGauntlet = false;
		public bool inactiveGauntlet = false;
		public float gauntletPower = 0f;

		public bool nightFlame = false;
		public Item oldItem = null;

		public override void SetDefaults(Item item)
		{
			if (item.createTile != -1 && TileID.Sets.Platforms[item.createTile])
			{
				item.ammo = ItemID.WoodPlatform;
				item.notAmmo = true;
			}
		}
        public override void PickAmmo(Item weapon, Item ammo, Player player, ref int type, ref float speed, ref StatModifier damage, ref float knockback)
        {
			if (ammo.createTile != -1 && TileID.Sets.Platforms[ammo.createTile]) (weapon.ModItem as PlatformLayer).chosenPlatform = ammo; 
		}

        /*public override void HoldItem(Item item, Player player)
        {
			if (!item.noMelee && item.CountsAsClass(DamageClass.Melee))
			{
				for (int i = 0; i < Main.recipe.Length; i++)
				{
					if (Main.recipe[i] != null && Main.recipe[i].HasIngredient(item.type))
					{
						Item recipeResult = new Item();
						recipeResult.SetDefaults(Main.recipe[i].createItem.type);
						bool craftedWithOneSword = true;
						foreach (Item material in Main.recipe[i].requiredItem)
                        {
							if (material.type != item.type && !material.noMelee && material.CountsAsClass(DamageClass.Melee))
                            {
								craftedWithOneSword = false;
								break;
                            }
                        }
						if (!recipeResult.noMelee && recipeResult.CountsAsClass(DamageClass.Melee)) //&& craftedWithOneSword)
						{
							Item upgradedSword = recipeResult;
							upgradedSword.GetGlobalItem<GItem>().oldItem = item;
							for (int j = 0; j < 58; j++)
							{
								if (player.inventory[j] == player.HeldItem)
								{
									player.inventory[j] = upgradedSword;
									Main.NewText(j);
									break;
								}
							}
							break;
							//ItemLoader.UseItem(upgradedSword, player);
						}
					}
				}
			}
		}*/
        public override bool CanUseItem(Item Item, Player player)
        {
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (Item.healMana > 0 && player.HasBuff(ModContent.BuffType<ManaOverdose>())) return false;
			if (Item.type == 293 && modPlayer.warlockTorc) return false;
			else return true;
		}

        public override void UseAnimation(Item Item, Player player)
        {
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (modPlayer.wristBrace && !Item.noMelee && player.controlUseItem && !Item.GetGlobalItem<GItem>().noWristBrace)//janky!!!!
			{
				if (Main.MouseWorld.X > player.position.X && player.direction == -1)
				{
					player.direction = 1;
				}
				if (Main.MouseWorld.X < player.position.X && player.direction == 1)
				{
					player.direction = -1;
				}
				Item.useTurn = false;
			}
		}
        public override bool? UseItem(Item Item, Player player)
        {
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (Item.potion == true)
			{
				if (modPlayer.vitalityCrystal)
				{
					player.statLife += 25;
					player.HealEffect(25);
				}
			}
			if (Item.type == 5 || Item.type == ModContent.ItemType<MushroomPlatter>() || Item.type == ModContent.ItemType<MushroomPlatterCrim>())
			{
				if (modPlayer.frostleafSet)
				{
					player.AddBuff(ModContent.BuffType<FrostleafBuff>(), 1200);
				}
			}
			if (Item.healMana > 0 && !modPlayer.warlockTorc) modPlayer.manaOverdoseTime = 1800;
			if (Item.healMana > 0 && modPlayer.warlockTorc)
			{
				player.AddBuff(BuffID.ManaSickness, 900);
				player.AddBuff(ModContent.BuffType<ManaOverdose>(), 1800);
			}  
			if (Item.healMana > 0 && modPlayer.warlockTorc)
			{
				player.AddBuff(BuffID.ManaSickness, 900);
			}
			if (!modPlayer.wristBrace && !Item.noMelee && !Item.GetGlobalItem<GItem>().noWristBrace) //fixes Item.useTurn from the previous (now UseAnimation) block :)
			{
				Item defaultStats = new Item();
				defaultStats.SetDefaults(Item.type);
				if (defaultStats.useTurn == true) Item.useTurn = true;
			}
			return base.UseItem(Item, player);
		}
		public override void OpenVanillaBag(string context, Player player, int arg)
		{
			if (context == "bossBag" && arg == ItemID.SkeletronBossBag)
			{
				int x = Main.rand.Next(3);
				if (x == 0)
				{
					Item.NewItem(player.GetSource_OpenItem(arg), (int)player.position.X, (int)player.position.Y, player.width, player.height, ModContent.ItemType<Skelebow>()); 
				}
				else if (x == 1)
				{
					Item.NewItem(player.GetSource_OpenItem(arg), (int)player.position.X, (int)player.position.Y, player.width, player.height, ModContent.ItemType<NecromanticFlame>()); 
				}
				else if (x == 2)
				{
					Item.NewItem(player.GetSource_OpenItem(arg), (int)player.position.X, (int)player.position.Y, player.width, player.height, ModContent.ItemType<BoneWhip>()); 
				}
			}
		}
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			if (item.CountsAsClass(DamageClass.Ranged) && player.GetModPlayer<MyPlayer>().forestSetRanged)
			{
				forestSetShots--;
				if (forestSetShots == 0)
				{
					forestSetShots = 3;
					Projectile.NewProjectile(source, position, velocity * .75f, ModContent.ProjectileType<VineLeaf>(), damage, knockBack, player.whoAmI);
					
				}
				return true;
			}
			return true;
			
		}
        /*public override bool ConsumeItem(Item Item, Player player)
        {
            if (Item.thrown && player.GetModPlayer<MyPlayer>().forestSetThrown)
            {
                return (Main.rand.Next(3) != 0);
            }
            return true;
        }*/
		public override void VerticalWingSpeeds(Item Item, Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            if (player.HasBuff(ModContent.BuffType<Waxwing>()))
            {
				maxAscentMultiplier *= 1.25f;
            }
            return;
        }
		public override void HorizontalWingSpeeds(Item Item, Player player, ref float speed, ref float acceleration)
        {
            if (player.HasBuff(ModContent.BuffType<Waxwing>()))
            {
				speed *= 1.25f;
            }
            return;
        }
		//int delay = 1;
		//bool goliathInit = false;
		float baseScale = 0;
		float longestDistance;
		Vector2 hitboxEdge;

		public override void UseItemHitbox(Item Item, Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
			/*GetMeleeFrame(player);
			Main.NewText(player.itemAnimation.ToString(), 0, 255, 255);
			Main.NewText(hitbox.Left - player.Center.X);
			Main.NewText(hitbox.Bottom - player.Center.Y);*/
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			//delay--;
			if (player.itemAnimation == player.itemAnimationMax)
            {
				modPlayer.swordHitbox.Width = (int)Math.Ceiling(hitbox.Height * 0.785f); //0.775 //swordHitbox currently unused
				modPlayer.swordHitbox.Height = (int)Math.Ceiling(hitbox.Height * 0.6f); //0.595
				modPlayer.hitboxEdge = new Vector2(hitbox.X + (player.direction == 1 ? hitbox.Width : 0), hitbox.Y); //swap to 0 : hitbox.Width to get true max range (measuring the point of the hitbox behind the player instead of in front)
				modPlayer.itemLength = Vector2.Distance(player.Center, modPlayer.hitboxEdge);
				//Main.NewText(modPlayer.swordHitbox.ToString(), 255, 0, 0);
			}
			/*{
				hitboxEdge = new Vector2(hitbox.X, hitbox.Y);
				if (player.itemAnimation == player.itemAnimationMax) longestDistance = 0;
				hitboxEdge = new Vector2(hitbox.X + (player.direction == 1 ? hitbox.Width : 0), hitbox.Y);
				Projectile.NewProjectile(Entity.GetSource_None(), hitboxEdge, Vector2.Zero, ModContent.ProjectileType<RedPixel>(), 0, 0);
				if (Vector2.Distance(player.Center, hitboxEdge) > longestDistance) longestDistance = Vector2.Distance(player.Center, hitboxEdge);
				Main.NewText(longestDistance.ToString(), 255, 0, 0);
			}*/ //would be useful if you need more precise hitbox info ig

			/*if (player.itemAnimation == 1)
            {
				//delay = (int)(Item.useAnimation * player.GetAttackSpeed(DamageClass.Melee)) - 1;
				if (modPlayer.gauntletBonus > 0)
				{
					//Main.NewText((hitbox.Height / (float)modPlayer.swordHitbox.Width).ToString(), 255, 0, 180);
					//Main.NewText(((int)((int)(hitbox.Height / 1.4f) * 1.1f) / (float)modPlayer.swordHitbox.Height).ToString(), 255, 0, 180);
					modPlayer.swordHitbox.Width = hitbox.Height;
					modPlayer.swordHitbox.Height = (int)((int)(hitbox.Height / 1.4f) * 1.1f);
					Main.NewText(modPlayer.swordHitbox.ToString(), 255, 0, 0);
				}
            }*/ //old version, only activates last frame of a sword swing which made it worse. basically identical values

			/*if (!goliathInit) //old code, was jankier
			{
				baseScale = Item.scale;
				goliathInit = true;
				if (player.HasBuff(ModContent.BuffType<Goliath>()))
				{
					Item.scale *= 1.2f;
				}
			}
			if (delay == 1) //last frame of sword swing
			{
				Item.scale = baseScale; //shrinking it here means it's visible, but barely noticable
				goliathInit = false;
				//Main.NewText(Item.scale.ToString(), 200, 20, 20, false);
				//Main.NewText(baseScale.ToString(), 20, 200, 20, false);
			}*/
			return;
        }

        /*public override void HoldItem(Item Item, Player player)
        { //only bug here is when reforging items. might get janky if other mods have scale adjusting stuff. might want decrease the effect on items with already high scales.
			//yeah it temporarily changes modifier tooltips
			if (!Item.noMelee && Item.damage > 0)
			{
				if (baseScale == 0) baseScale = Item.scale;
				if (!player.HasBuff(ModContent.BuffType<Goliath>()))
				{
					if (Item.scale != baseScale) Item.scale = baseScale;
					baseScale = Item.scale;
				}
				else
				{
					if (Item.scale == baseScale) Item.scale *= 1.2f;
				}
			}
		}*/
        public override void ModifyItemScale(Item item, Player player, ref float scale)
        {
			if (!item.noMelee && item.damage > 0 && player.HasBuff(ModContent.BuffType<Goliath>())) scale *= 1.2f;
        }
        public override void ModifyTooltips(Item Item, List<TooltipLine> tooltips)
        {
			if (!Main.gameMenu) //i hope this works as intended but otherwise the mouseover on the main menu social media icons crashes
			{
				MyPlayer modPlayer = Main.player[Item.playerIndexTheItemIsReservedFor].GetModPlayer<MyPlayer>(); //because this is outside the index of the array
				if (Item.GetGlobalItem<GItem>().gelPad)
				{
					TooltipLine line = new TooltipLine(Mod, "x", "Gel Pad"); //no clue what the first string does here, gives the tooltip a name for other code to reference?
					line.OverrideColor = new Color(150, 150, 255);
					TooltipLine line2 = new TooltipLine(Mod, "x2", "Sword strikes on knockback-immune foes bounce you slightly back\nRight Click to detach");
					tooltips.Add(line);
					tooltips.Add(line2);
					if (Item.type == ModContent.ItemType<GelGauntlet>())
					{
						TooltipLine line3 = new TooltipLine(Mod, "x3", "'Dear God... we're reaching levels of gel that shouldn't even be possible'");
						tooltips.Add(line3);
					}
				}
				if (Item.GetGlobalItem<GItem>().nightFlame)
				{
					TooltipLine line = new TooltipLine(Mod, "x", "Nocturnal Flame"); //no clue what the first string does here, gives the tooltip a name for other code to reference?
					line.OverrideColor = new Color(255, 200, 150);
					TooltipLine line2 = new TooltipLine(Mod, "x2", "Sword strikes inflict Nocturnal Flame, a slow-burning fire that gradually increases in intensity\nRight Click to detach"); //Sword strikes inflict the slow burning Nocturnal Flame
					tooltips.Add(line);
					tooltips.Add(line2);
				}
				if (Item.type == 293 && modPlayer.warlockTorc)
				{
					TooltipLine line = new TooltipLine(Mod, "x", "[c/e65555:Cannot be consumed while wearing a torc]");
					tooltips.Add(line);
				}
				if (Item.GetGlobalItem<GItem>().inactiveGauntlet)
				{
					TooltipLine line = new TooltipLine(Mod, "x", "Currently granting no damage boost, gauntlet damage bonuses do not stack");
					tooltips.Add(line);
				}
				/*if (Item.ammo == ItemID.WoodPlatform)
				{
					TooltipLine line = new TooltipLine(Mod, "x", "This is platform ammo"); //no clue what the first string does here, gives the tooltip a name for other code to reference?
					tooltips.Add(line);
				}*/
			}
		}
		//public override bool NeedsSaving(Item Item)
		//{
		//	return gelPad;
		//}
		public override void SaveData(Item Item, TagCompound tag) {
			//TagCompound saveData = new TagCompound();
			//saveData.Add("gelPad", gelPad);
			//return saveData;
			tag["gelPad"] = gelPad;
			tag["nightFlame"] = nightFlame;
		}

		public override void LoadData(Item Item, TagCompound tag) {
			gelPad = tag.GetBool("gelPad");
			nightFlame = tag.GetBool("nightFlame");
		}

		public sealed override bool CanRightClick(Item Item)
		{
			if (gelPad || nightFlame) return true;
			else return base.CanRightClick(Item);
		}
		public override void RightClick(Item Item, Player player)
		{
            if (Item.GetGlobalItem<GItem>().gelPad == true)
			{
				Item.GetGlobalItem<GItem>().gelPad = false;
				Item.NewItem(player.GetSource_OpenItem(Item.type), player.getRect(), ModContent.ItemType<GelPad>()); //These are probably bad choices for item sources
				Item gauntletCopy = Main.item[Item.NewItem(player.GetSource_OpenItem(Item.type), player.getRect(), Item.type)]; //
				gauntletCopy.prefix = Item.prefix;
				gauntletCopy.rare = Item.rare;
				gauntletCopy.value = Item.value;
			}
			if (Item.GetGlobalItem<GItem>().nightFlame == true)
			{
				Item.GetGlobalItem<GItem>().nightFlame = false;
				Item.NewItem(player.GetSource_OpenItem(Item.type), player.getRect(), ModContent.ItemType<NightFlame>()); //These are probably bad choices for item sources
				Item swordCopy = Main.item[Item.NewItem(player.GetSource_OpenItem(Item.type), player.getRect(), Item.type)]; //
				swordCopy.prefix = Item.prefix;
				swordCopy.rare = Item.rare;
				swordCopy.value = Item.value;
			}
		}
        /*public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
			if (item.type < 5000)
			{
				Texture2D itemTexture = ModContent.Request<Texture2D>("Terraria/Images/Item_" + item.type).Value;
				spriteBatch.Draw(itemTexture, position + Vector2.One * 2, null, new Color(0, 0, 0, 100), 0f, Vector2.Zero, Main.inventoryScale, SpriteEffects.None, scale);
			}
			return true;
        }*/
        public override void UpdateAccessory(Item Item, Player player, bool hideVisibleAccessory)
        {
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (Item.GetGlobalItem<GItem>().gelPad == true && modPlayer.gelGauntlet < 1)
			{
				modPlayer.gelGauntlet = 0.6f;
			}
		}

        public override void OnCreate(Item item, ItemCreationContext context)
        {
			if (item.type == 273)
			{
				if (context is RecipeCreationContext)
				{
					Player player = Main.LocalPlayer;

					int i = (int)((player.position.X + player.width * 0.5) / 16.0);
					int j = (int)((player.position.Y + player.height * 0.5) / 16.0);
					bool foundTile = false;
					for (int x = -5; x <= 5 && !foundTile; x++)
					{
						for (int y = -5; y <= 5 && !foundTile; y++)
						{
							if (Framing.GetTileSafely(x + i, y + j).TileType == 26)
							{
								foundTile = true;
								i += x;
								j += y;
							}
						}
					}
					Tile tile = Framing.GetTileSafely(i, j);
					Vector2 position = new Vector2(8, 8);
					if (foundTile)
					{
						if (tile.TileFrameX % 54 == 0) position.X = 24;
						if (tile.TileFrameX % 54 == 36) position.X = -8;
						if (tile.TileFrameY == 18) position.Y = -16;
					}
					position += new Vector2(i * 16, j * 16);
					int spawnedItem = Item.NewItem(item.GetSource_Misc("Crafting"), position, 1, 1, ModContent.ItemType<NightFlame>());
					Main.item[spawnedItem].velocity.X = 0;
					Main.item[spawnedItem].velocity.Y = -3;
					PlaySound(SoundID.Item103, position);
					position.X -= 16;
					for (int x = 0; x < 8 && foundTile; x++)
					{
						Dust.NewDust(position, 32, 16, 14, 0f, Main.rand.NextFloat(-3f, -1f), 100, default(Color), 1.4f);
						int dust2 = Dust.NewDust(position, 32, 16, 27, 0f, Main.rand.NextFloat(-8f, -2f), 0, default(Color), 1.3f);
						Main.dust[dust2].noGravity = true;
					}
				}
			}
		}
        public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (item.type == 273 && item.GetGlobalItem<GItem>().nightFlame)
            {
				target.AddBuff(ModContent.BuffType<NocturnalFlame>(), 1200);
			}
        }
		public bool TileInRange(Item item, Player player, int? i = null, int? j = null)
		{
			if (i == null) i = Player.tileTargetX;
			if (j == null) j = Player.tileTargetY;
			int rangeX = Player.tileRangeX + item.tileBoost;
			int rangeY = Player.tileRangeY + item.tileBoost;
			/*int playerTileX = (int)((player.position.X + player.width * 0.5) / 16.0);
			int playerTileY = (int)((player.position.Y + player.height * 0.5) / 16.0);
			if (playerTileX >= i - rangeX && playerTileX <= i + rangeX && playerTileY >= j - rangeY && playerTileY <= j + rangeY) return true;
			else return false;*/
			if (player.position.X / 16f - rangeX <= i && (player.position.X + player.width) / 16f + rangeX - 1f >= (float)i && player.position.Y / 16f - rangeY <= (float)j) // i dont know if the float cast does anything but im too scared to change it
			{
				return (player.position.Y + player.height) / 16f + rangeY - 2f >= (float)j;
			}
			return false;
		}
		/*public int GetMeleeFrame(Player player)
		{
			int meleeFrame = 0;
            bool compensateByRoundingDown = false;
			if (player.itemAnimationMax % 3 != 0) compensateByRoundingDown = true;
            float swingFraction = ((float)player.itemAnimation + 1) / ((float)player.itemAnimationMax / 3);

			if (swingFraction == 2) meleeFrame = 1;
			if (swingFraction <= 1 || player.itemAnimation / ((float)player.itemAnimationMax / 3) <= 1 && compensateByRoundingDown) meleeFrame = 2;
			else if (swingFraction < 2) meleeFrame = 1;
			Main.NewText(swingFraction.ToString(), 0, 255, 0);
			Main.NewText(meleeFrame.ToString(), 255, 0, 0);
			return meleeFrame;
		}*/ //when called in useitemhitbox, seems to return accurately sometimes, but when % 3 != 0, meleeframe = 1 happens 1 frame too late, so make compensatebyrounding down affect both?
	}
}
