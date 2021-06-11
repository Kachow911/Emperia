using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using Emperia;

namespace Emperia
{
	public class GItem : GlobalItem
	{	
		public override bool InstancePerEntity {get{return true;}}
		public override bool CloneNewInstances {get{return true;}}		
		public int forestSetShots = 2;
		public bool gelPad = false;
		public bool isGauntlet = false;
		public bool noWristBrace = false;
		public bool noGelGauntlet = false;
		public override bool UseItem(Item item, Player player)
        {
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (item.potion == true)
			{
				if (modPlayer.vitalityCrystal)
				{
					player.statLife += 25;
					player.HealEffect(25);
				}
			}
			if (item.type == 5 || item.type == mod.ItemType("MushroomPlatter") || item.type == mod.ItemType("MushroomPlatterCrim"))
			{
				if (modPlayer.frostleafSet)
				{
					player.AddBuff(mod.BuffType("FrostleafBuff"), 1200);
				}
			}
			if (modPlayer.wristBrace && !item.noMelee && Main.mouseLeft && !item.GetGlobalItem<GItem>().noWristBrace)//janky!!!!
			{
				if (Main.MouseWorld.X > player.position.X && player.direction == -1)
				{
					player.direction = 1;
					item.useTurn = false; 
				}
				if (Main.MouseWorld.X < player.position.X && player.direction == 1)
				{
					player.direction = -1;
					item.useTurn = false;
				}
			}
			if (!modPlayer.wristBrace && !item.noMelee) //fixes item.useTurn from the previous block :)
			{
				Item defaultStats = new Item();
				defaultStats.SetDefaults(item.type);
				if (defaultStats.useTurn == true) { item.useTurn = true; }
			}
			return false;
		}
		public override void OpenVanillaBag(string context, Player player, int arg)
		{
			if (context == "bossBag" && arg == ItemID.SkeletronBossBag)
			{
				int x = Main.rand.Next(3);
				if (x == 0)
				{
					Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("Skelebow")); 
				}
				else if (x == 1)
				{
					Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("NecromanticFlame")); 
				}
				else if (x == 2)
				{
					Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("BoneWhip")); 
				}
			}
		}
		public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockback)
		{
			if (item.ranged && player.GetModPlayer<MyPlayer>().forestSetRanged)
			{
				forestSetShots--;
				if (forestSetShots == 0)
				{
					forestSetShots = 3;
					Projectile.NewProjectile(position.X, position.Y, speedX * .75f, speedY * .75f, mod.ProjectileType("VineLeaf"), damage, knockback, player.whoAmI);
					
				}
				return true;
			}
			return true;
			
		}
        public override bool ConsumeItem(Item item, Player player)
        {
            if (item.thrown && player.GetModPlayer<MyPlayer>().forestSetThrown)
            {
                return (Main.rand.Next(3) != 0);
            }
            return true;
        }
		public override void VerticalWingSpeeds(Item item, Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            if (player.HasBuff(mod.BuffType("Waxwing")))
            {
				maxAscentMultiplier *= 1.25f;
            }
            return;
        }
		public override void HorizontalWingSpeeds(Item item, Player player, ref float speed, ref float acceleration)
        {
            if (player.HasBuff(mod.BuffType("Waxwing")))
            {
				speed *= 1.25f;
            }
            return;
        }
		int delay = 1;
		bool goliathInit = false;
		float baseScale = 0;
		public override void UseItemHitbox(Item item, Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			delay--;
            if (delay == 0)
            {
				delay = (int)(item.useAnimation * player.meleeSpeed) - 1;
				if (modPlayer.gauntletBonus > 0)
				{ 
					modPlayer.swordHitbox.Width = hitbox.Height;
					modPlayer.swordHitbox.Height = (int)((int)(hitbox.Height / 1.4f) * 1.1f);
				}
            }

			if (!goliathInit)
			{
				baseScale = item.scale;
				goliathInit = true;
				if (player.HasBuff(mod.BuffType("Goliath")))
				{
					item.scale *= 1.2f;
				}
			}
			if (delay == 1) //last frame of sword swing
			{
				item.scale = baseScale; //shrinking it here means it's visible, but barely noticable
				goliathInit = false;
				//Main.NewText(item.scale.ToString(), 200, 20, 20, false);
				//Main.NewText(baseScale.ToString(), 20, 200, 20, false);
			}
			return;
        }
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
			if (item.GetGlobalItem<GItem>().gelPad) {
				TooltipLine line = new TooltipLine(mod, "x", "Gel Pad"); //no clue what the first string does here, gives the tooltip a name for other code to reference?
				line.overrideColor = new Color(150, 150, 255);
				TooltipLine line2 = new TooltipLine(mod, "x2", "Sword strikes on knockback-immune foes bounce you slightly back\nRight Click to detach");
				tooltips.Add(line);
				tooltips.Add(line2);
				if (item.type == mod.ItemType("GelGauntlet"))
				{
					TooltipLine line3 = new TooltipLine(mod, "x3", "'Dear God... we're reaching levels of gel that shouldn't even be possible'");
					tooltips.Add(line3);
				}
			}
		}
		public override bool NeedsSaving(Item item)
		{
			return gelPad;
		}
		public override TagCompound Save(Item item) {
			TagCompound saveData = new TagCompound();
			saveData.Add("gelPad", gelPad);
			return saveData;
		}

		public override void Load(Item item, TagCompound tag) {
			gelPad = tag.GetBool("gelPad");
		}

		public sealed override bool CanRightClick(Item item)
		{
			return gelPad;
		}
		public override void RightClick(Item item, Player player)
		{
            if (item.GetGlobalItem<GItem>().gelPad == true)
			{
				item.GetGlobalItem<GItem>().gelPad = false;
				Item.NewItem(player.getRect(), mod.ItemType("GelPad"));
				Item gauntletCopy = Main.item[Item.NewItem(player.getRect(), item.type)];
				gauntletCopy.prefix = item.prefix;
				gauntletCopy.rare = item.rare;
				gauntletCopy.value = item.value;
			}
		}
		public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (item.GetGlobalItem<GItem>().gelPad == true && modPlayer.gelGauntlet < 1)
			{
				player.GetModPlayer<MyPlayer>().gelGauntlet = 0.6f;
			}
        }
	}
}
