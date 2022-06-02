using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;
using System;
using Terraria.GameContent;


namespace Emperia.Items
{
	public class NightFlame : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nocturnal Flame");
			Tooltip.SetDefault("Engulfs the holder in flames if held while unbound\nRight Click while holding a Night's Edge to bind the flame to the blade, empowering it");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 5));
			ItemID.Sets.ItemIconPulse[Item.type] = true;
			ItemID.Sets.ItemNoGravity[Item.type] = true;
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.rare = 3;
		}

		/*public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			//Texture2D texture = TextureAssets.Item[Item.type].Value;
			Texture2D texture = TextureAssets.Item[Item.type].Frame(1, 5);
			Rectangle frame;
			if (Main.itemAnimations[Item.type] != null)
			{
				// In case this item is animated, this picks the correct frame
				frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
			}
			else
			{
				frame = texture.Frame();
			}
			Vector2 frameOrigin = frame.Size() / 2f;
			Vector2 offset = new Vector2(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
			Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;
			spriteBatch.Draw(texture, drawPos, frame, default(Color), rotation, frameOrigin, scale, SpriteEffects.None, 0);
			//Main.spriteBatch.End();
			//Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
			//spriteBatch.Draw(TextureAssets.Item[Item.type].Frame(1, 5));
			return true;
		}*/ 
		//animatesassoul handles this so unnecessary ig i was trying to see how animation works lol
		public override void GrabRange(Player player, ref int grabRange)
		{
			grabRange = (int)(grabRange * 0.1f);
			base.GrabRange(player, ref grabRange);
		}
        public override void PostUpdate()
        {
			Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 0.65f, 0f, 1f);
        }
		public override bool CanPickup(Player player)
		{
			if (Item.timeSinceItemSpawned < 120) return false;
			else return base.CanPickup(player);
		}
		public virtual bool CanApply(Item Item)
		{
			if (Item.type == 273 && Item.GetGlobalItem<GItem>().nightFlame == false)
			{
				return true;
			}
			else return false;
		}

		public sealed override bool CanRightClick()
		{
			Item Item = Main.LocalPlayer.HeldItem;
			return CanApply(Item);
		}

		public override void RightClick(Player player)
		{
			Item Item = Main.LocalPlayer.HeldItem;
			Item.GetGlobalItem<GItem>().nightFlame = true;
		}
        public override void UpdateInventory(Player player)
        {
			player.AddBuff(ModContent.BuffType<NocturnalFlame>(), 1);
		}
        public override void HoldItem(Player player)
        {
			player.AddBuff(ModContent.BuffType<NocturnalFlame>(), 10);
		}
	}
}