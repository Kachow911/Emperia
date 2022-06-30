using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using ReLogic.Content;
using static Emperia.EmperiaSystem;
using Emperia.Items;

namespace Emperia.UI
{

	class PaintUI : UIState
	{
        Texture2D iconTexture;
		public bool mousedOverAny = false;
		public override void OnInitialize()
		{
			EmperiaSystem.CurrentPaintUI = this;
			iconTexture = ModContent.Request<Texture2D>("Emperia/UI/Icon_0", AssetRequestMode.ImmediateLoad).Value;
			MakeSmallIcons();
			MakeLargeIcons();

			Vector2 position = paintUIActivationPosition - new Vector2(iconTexture.Width / 2, iconTexture.Height / 2);
			UIElement mainIcon = new BrushUI(position); //ModContent.Request<Texture2D>("Emperia/UI/Icon_0")
			MakeIcon(mainIcon, position, 40);
			Vector2 swapPosition = paintUIActivationPosition - new Vector2(24, 28);//+ new Vector2(12, 10); // 26
			UIElement modeSwap = new ModeSwap(swapPosition);
			MakeIcon(modeSwap, swapPosition, 14, 16);
		}

		public void MakeSmallIcons()
		{
			int row = 0;
			int iconPosOnRow = -1;
			int[] linesPerRow = { 6, 6, 4, 4, 6, 6 };
			for (int i = 0; i < 32; i++)
			{
				iconPosOnRow++;
				if (iconPosOnRow == (int)linesPerRow.GetValue(row))
				{
					row++;
					iconPosOnRow = 0;
				}
				Vector2 iconPosition = new Vector2(-84 + iconPosOnRow * 28, -84 + row * 28) + paintUIActivationPosition;
				if ((int)linesPerRow.GetValue(row) == 4 && iconPosOnRow > 1) iconPosition.X += 28 * 2;
				UIElement smallIcon = new BucketSmall(i, iconPosition); //ModContent.Request<Texture2D>("Emperia/UI/Icon_0")
				MakeIcon(smallIcon, iconPosition, 26);
				EmperiaSystem.smallPaintIconList.Add(smallIcon);
			}
		}
		public void MakeLargeIcons()
		{
			int iconCount = 5; //mastersPalette.selectedColors.Count;
			int distance = -45;
			for (int i = 0; i < iconCount; i++)
			{
				Vector2 iconCenter = new Vector2(0, distance).RotatedBy(MathHelper.ToRadians((360 / 5) * i)) + paintUIActivationPosition;
				Vector2 iconPosition = iconCenter - new Vector2(iconTexture.Width / 2, iconTexture.Height / 2);
				iconPosition.X = (int)Math.Round(iconPosition.X / 2) * 2;
				iconPosition.Y = (int)Math.Round(iconPosition.Y / 2) * 2;
				UIElement largeIcon = new BucketLarge(i, iconPosition);
				MakeIcon(largeIcon, iconPosition, 40);
				EmperiaSystem.largePaintIconList.Add(largeIcon);
			}
		}
		public void MakeIcon(UIElement icon, Vector2 position, int width, int height = -1)
		{
			if (height == -1) height = width;
			icon.Left.Set(position.X, 0);
			icon.Top.Set(position.Y, 0);
			icon.Width.Set(width, 0);
			icon.Height.Set(height, 0);
			Append(icon);
		}
	}
	class PaintUIElement : UIElement
	{
		internal Vector2 position;
		internal Texture2D iconTexture;
		internal int iconType;
		internal int iconIndex;
		internal bool mousedOver = false;
		internal bool canBeClicked = true;
		public bool visible = true;
		public void GeneralUpdate()
		{
			iconType = 0;
			//if (mastersPalette.brushMode == 2) iconType += 2;
			if (Main.mouseLeftRelease) canBeClicked = true;
		}
		public void MouseOver(UIElement element)
		{
			mousedOver = true;
			(Parent as PaintUI).mousedOverAny = true;
			Main.LocalPlayer.mouseInterface = true;
			iconType += 1;
		}
	}
	class BrushUI : PaintUIElement
	{
		public BrushUI(Vector2 pos) => position = pos;
		OldMastersPalette mastersPalette = Main.LocalPlayer.HeldItem.ModItem as OldMastersPalette;
		public override void OnInitialize()
		{
			iconTexture = ModContent.Request<Texture2D>("Emperia/UI/Icon_0", AssetRequestMode.ImmediateLoad).Value;
		}
		public override void Update(GameTime gameTime)
		{
			GeneralUpdate();

			if (Vector2.Distance(paintUIActivationPosition, Main.MouseScreen) < 19f)
			{
				MouseOver(this);
				if (Main.mouseLeft && canBeClicked)
				{
					if (Main.mouseLeft && canBeClicked)
					{
						mastersPalette.brushMode = (mastersPalette.brushMode + 1) % 3;
						canBeClicked = false;
					}
				}
			}
			else
			{
				mousedOver = false;
				(Parent as PaintUI).mousedOverAny = false;
			}
			iconTexture = ModContent.Request<Texture2D>("Emperia/UI/Icon_" + iconType).Value;
			if (mastersPalette != (Main.LocalPlayer.HeldItem.ModItem as Items.OldMastersPalette)) EmperiaSystem.paintUIActive = false; //this check only seems to work in PaintUI

		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			Main.hoverItemName = "wow";
			base.Draw(spriteBatch);
			spriteBatch.Draw(iconTexture, position, null, Color.White);
			Texture2D brushTexture = ModContent.Request<Texture2D>("Emperia/UI/Brush_" + mastersPalette.brushMode).Value;
			spriteBatch.Draw(brushTexture, position, null, Color.White);
		}
	}
	class BucketSmall : PaintUIElement
	{
		public BucketSmall(int index, Vector2 pos)
		{
			iconIndex = index;
			position = pos;
        }
		int paintType;
		static int[] paintForPosition = new int[] { 28, 13, 14, 15, 16, 17, 27, 1, 2, 3, 4, 18, 25, 12, 5, 29, 26, 11, 6, 31, 24, 10, 9, 8, 7, 30, 23, 22, 21, 20, 19, 0 };
		Texture2D paintTexture;
		bool locked = false;

		OldMastersPalette mastersPalette = Main.LocalPlayer.HeldItem.ModItem as OldMastersPalette;
		public override void OnInitialize()
		{
			iconTexture = ModContent.Request<Texture2D>("Emperia/UI/IconSmall_0", AssetRequestMode.ImmediateLoad).Value;
			paintType = (int)paintForPosition.GetValue(iconIndex);
			if (!Main.gameMenu && paintType >= 29) locked = (mastersPalette.specialPaintSlots[paintType - 29] == null);
			paintTexture = ModContent.Request<Texture2D>("Terraria/Images/Item_" + PaintToItemID(paintType)).Value;
			if (!Main.gameMenu && mastersPalette.curatedMode) visible = false;
		}
		public override void Update(GameTime gameTime)
        {
			if (!visible) return;
			GeneralUpdate();
			
			if (paintType >= 29) locked = (mastersPalette.specialPaintSlots[paintType - 29] == null);
			if (!locked && Main.MouseScreen.X >= position.X && Main.MouseScreen.X <= position.X + iconTexture.Width && Main.MouseScreen.Y >= position.Y && Main.MouseScreen.Y <= position.Y + +iconTexture.Height)
			{
				MouseOver(this);
				if (Main.mouseLeft && canBeClicked)
				{
					if (paintType > 0)
					{
						if (!mastersPalette.selectedColors.Contains(paintType)) mastersPalette.selectedColors.Add(paintType);
						else mastersPalette.selectedColors.Remove(paintType);
					}
					else
					{
						if (mastersPalette.selectedColors.Any())
                        {
							mastersPalette.selectedColorsBackup = mastersPalette.selectedColors.ToList();
							mastersPalette.selectedColors.Clear();
						}
						else mastersPalette.selectedColors = mastersPalette.selectedColorsBackup.ToList();

					}
					canBeClicked = false;
				}
			}
			else
			{
				mousedOver = false;
				(Parent as PaintUI).mousedOverAny = false;
				if (paintType == 0) canBeClicked = false;
			}
			iconTexture = ModContent.Request<Texture2D>("Emperia/UI/IconSmall_" + iconType).Value;
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!visible) return;

			base.Draw(spriteBatch);
			Color brightness = new Color(255, 255, 255);
			if (!mastersPalette.selectedColors.Contains(paintType) && (paintType != 0 || mastersPalette.selectedColors.Any() || !mastersPalette.selectedColorsBackup.Any()))
			{
				brightness = new Color(150, 150, 150);
				if (!mousedOver) brightness = new Color(80, 80, 80);
			}
			spriteBatch.Draw(iconTexture, position, null, brightness);

			if (!locked)
			{
				if (brightness == new Color(150, 150, 150)) brightness = new Color(190, 190, 190); //buckets need to be brighter to be distinguishable
				else if (brightness == new Color(80, 80, 80)) brightness = new Color(140, 140, 140);
				var paintCrop = new Rectangle(6, 0, 14, 10);
				spriteBatch.Draw(paintTexture, position + new Vector2(6, 10), paintCrop, brightness);
			}
			else spriteBatch.Draw(ModContent.Request<Texture2D>("Emperia/UI/LockedPaint_0").Value, position, null, brightness);

			if (paintType == 0)
            {
				Texture2D trashTexture = ModContent.Request<Texture2D>("Emperia/UI/Trash").Value;
				spriteBatch.Draw(trashTexture, position, null, brightness);
			}

			if (mastersPalette.selectedColors.LastOrDefault() == paintType && paintType != 0)
            {
				Texture2D ribbonTexture = ModContent.Request<Texture2D>("Emperia/UI/SelectedIconRibbon_" + iconType).Value;
				spriteBatch.Draw(ribbonTexture, position, null, Color.White);
			}
		}
		internal int PaintToItemID(int PaintID)
        {
			if (PaintID > 0)
			{
				if (PaintID < 28) return PaintID + 1072;
				else if (PaintID < 31) return PaintID + 1938;
				else if (PaintID == 31) return 4668;
			}
			return 0;
        }
	}

	class BucketLarge : PaintUIElement
	{
		public BucketLarge(int index, Vector2 pos)
		{
			iconIndex = index;
			position = pos;
		}
		public int paintType;
		Texture2D bucketTexture;
		Texture2D paintTexture;

		OldMastersPalette mastersPalette = Main.LocalPlayer.HeldItem.ModItem as OldMastersPalette;


		public override void OnInitialize()
		{
			visible = false;
			iconTexture = ModContent.Request<Texture2D>("Emperia/UI/Icon_0", AssetRequestMode.ImmediateLoad).Value;
			paintType = 0;
			bucketTexture = ModContent.Request<Texture2D>("Emperia/UI/Bucket", AssetRequestMode.ImmediateLoad).Value;
			paintTexture = ModContent.Request<Texture2D>("Emperia/UI/PaintSplatter", AssetRequestMode.ImmediateLoad).Value;
		}
		public override void Update(GameTime gameTime)
		{
			if (!Main.gameMenu && mastersPalette.curatedMode && paintType > 0) visible = true;

			if (mastersPalette.selectedColors.Any() && mastersPalette.selectedColors.Count > iconIndex)
            {
				paintType = mastersPalette.CuratedColorList(mastersPalette.selectedColors)[iconIndex];
			}
			else visible = false;

			if (!visible) return;
			GeneralUpdate();

			if (Vector2.Distance(position + new Vector2(iconTexture.Width / 2, iconTexture.Height / 2), Main.MouseScreen) < 19f)
			{
				MouseOver(this);
				if (Main.mouseLeft && canBeClicked)
				{
					{
						if (mastersPalette.curatedColor != paintType) mastersPalette.curatedColor = paintType;
						else mastersPalette.curatedColor = 0;
					}
					canBeClicked = false;
				}
			}
			else
			{
				mousedOver = false;
				(Parent as PaintUI).mousedOverAny = false;
			}

			iconTexture = ModContent.Request<Texture2D>("Emperia/UI/Icon_" + iconType).Value;

			string visuals = mastersPalette.SpecialVFX(paintType);
			bucketTexture = ModContent.Request<Texture2D>("Emperia/UI/Bucket" + visuals).Value;
			if (paintType == 29) visuals = "";
			paintTexture = ModContent.Request<Texture2D>("Emperia/UI/PaintSplatter" + visuals).Value;
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!visible) return;

			base.Draw(spriteBatch);
			Color brightness = new Color(255, 255, 255);
			if (mastersPalette.curatedColor != paintType)
			{
				brightness = new Color(150, 150, 150);
				if (!mousedOver) brightness = new Color(80, 80, 80);
			}
			spriteBatch.Draw(iconTexture, position, null, brightness);
			if (brightness == new Color(150, 150, 150)) brightness = new Color(190, 190, 190); //buckets need to be brighter to be distinguishable
			else if (brightness == new Color(80, 80, 80)) brightness = new Color(140, 140, 140);
			spriteBatch.Draw(bucketTexture, position, null, brightness);
			Color color = mastersPalette.PaintToColor(paintType, true).MultiplyRGB(brightness);
			spriteBatch.Draw(paintTexture, position, null, color);
		}
	}
	class ModeSwap : PaintUIElement
	{
		public ModeSwap(Vector2 pos)
		{
			position = pos;
		}
		OldMastersPalette mastersPalette = Main.LocalPlayer.HeldItem.ModItem as OldMastersPalette;
		public override void OnInitialize()
		{
			iconTexture = ModContent.Request<Texture2D>("Emperia/UI/ModeSwap_2", AssetRequestMode.ImmediateLoad).Value;
			if (!Main.gameMenu && mastersPalette.selectedColors.Any()) iconTexture = ModContent.Request<Texture2D>("Emperia/UI/ModeSwap_0").Value;
		}
		public override void Update(GameTime gameTime)
		{
			GeneralUpdate();
			if (mastersPalette.selectedColors.Any())
			{
				if (Vector2.Distance(position + new Vector2(6.5f, 7.5f), Main.MouseScreen) < 6.5f)
				{
					MouseOver(this);
					if (Main.mouseLeft && canBeClicked && mastersPalette.selectedColors.Any())
					{
						for (int i = 0; i < 32; i++)
						{
							(smallPaintIconList[i] as BucketSmall).visible = mastersPalette.curatedMode;
						}
						mastersPalette.curatedMode = !mastersPalette.curatedMode;
						int iterations = (mastersPalette.selectedColors.Count > 5) ? 5 : mastersPalette.selectedColors.Count;
						for (int i = 0; i < iterations; i++)
						{
							(largePaintIconList[i] as BucketLarge).visible = mastersPalette.curatedMode;
						}
						canBeClicked = false;
					}
				}
				else
				{
					mousedOver = false;
					(Parent as PaintUI).mousedOverAny = false;
				}
			}
			else iconType = 2;
			iconTexture = iconTexture = ModContent.Request<Texture2D>("Emperia/UI/ModeSwap_" + iconType).Value;
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			spriteBatch.Draw(iconTexture, position, null, Color.White);
		}
	}
	class CursorUI : UIState
    {
		OldMastersPalette mastersPalette = Main.LocalPlayer.HeldItem.ModItem as OldMastersPalette;

		float alpha = 0f;

        public override void Update(GameTime gameTime)
        {
			Item item = Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem];
			if (item.type == ModContent.ItemType<OldMastersPalette>() && item.GetGlobalItem<GItem>().TileInRange(item, Main.LocalPlayer))
			{
				if (alpha < 1) alpha += 0.0625f;
			}
			else if (alpha > 0) alpha -= 0.0625f;
			if (alpha <= 0) EmperiaSystem.cursorUIActive = false;
		}
		public override void Draw(SpriteBatch spriteBatch)
        {
			Texture2D cursorTexture = ModContent.Request<Texture2D>("Emperia/UI/CursorBrush_" + mastersPalette.brushMode).Value;
			spriteBatch.Draw(cursorTexture, Main.MouseScreen + new Vector2(16, 16), null, Color.White * alpha);
			if (mastersPalette.brushMode != 2 && mastersPalette.color != 0)
			{
				Color color = mastersPalette.PaintToColor(mastersPalette.color);
				Texture2D paintTexture = ModContent.Request<Texture2D>("Emperia/UI/CursorBrushPaint_" + mastersPalette.brushMode + mastersPalette.SpecialVFX(mastersPalette.color)).Value;
				spriteBatch.Draw(paintTexture, Main.MouseScreen + new Vector2(16, 16), null, color * alpha);
			}
		}
    }
}