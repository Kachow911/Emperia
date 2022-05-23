using Microsoft.Xna.Framework;
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
		public bool isGauntlet = false;
		public bool noWristBrace = false;
		public bool noGelGauntlet = false;

		public override void SetDefaults(Item item)
		{
			if (item.createTile != -1 && TileID.Sets.Platforms[item.createTile])
			{
				item.ammo = ItemID.WoodPlatform;
				item.notAmmo = true;
			}
		}
        public override void PickAmmo(Item weapon, Item ammo, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
			if (ammo.createTile != -1 && TileID.Sets.Platforms[ammo.createTile]) (weapon.ModItem as PlatformLayer).chosenPlatform = ammo; 
		}

        public override bool CanUseItem(Item Item, Player player)
        {
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (Item.healMana > 0 && player.HasBuff(ModContent.BuffType<ManaOverdose>())) return false;
			if (Item.type == 293 && modPlayer.warlockTorc) return false;
			else return true;
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
			if (modPlayer.wristBrace && !Item.noMelee && player.controlUseItem && !Item.GetGlobalItem<GItem>().noWristBrace && player.itemAnimation == player.itemAnimationMax)//janky!!!!
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
			if (!modPlayer.wristBrace && !Item.noMelee) //fixes Item.useTurn from the previous block :)
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
		int delay = 1;
		//bool goliathInit = false;
		float baseScale = 0;
		public override void UseItemHitbox(Item Item, Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			delay--;
            if (delay == 0)
            {
				delay = (int)(Item.useAnimation * player.GetAttackSpeed(DamageClass.Melee)) - 1;
				if (modPlayer.gauntletBonus > 0)
				{ 
					modPlayer.swordHitbox.Width = hitbox.Height;
					modPlayer.swordHitbox.Height = (int)((int)(hitbox.Height / 1.4f) * 1.1f);
				}
            }

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

        public override void HoldItem(Item Item, Player player)
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
				if (Item.type == 293 && modPlayer.warlockTorc)
				{
					TooltipLine line = new TooltipLine(Mod, "x", "[c/e65555:Cannot be consumed while wearing a torc]");
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
		}

		public override void LoadData(Item Item, TagCompound tag) {
			gelPad = tag.GetBool("gelPad");
		}

		public sealed override bool CanRightClick(Item Item)
		{
			if (gelPad) return true;
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
		}
		public override void UpdateAccessory(Item Item, Player player, bool hideVisibleAccessory)
        {
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (Item.GetGlobalItem<GItem>().gelPad == true && modPlayer.gelGauntlet < 1)
			{
				player.GetModPlayer<MyPlayer>().gelGauntlet = 0.6f;
			}
        }
	}
}
