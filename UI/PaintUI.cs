using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent;
using static Emperia.EmperiaSystem;
using Terraria.GameContent.UI.Elements;
using ReLogic.Content;
using System.Collections.Generic;
using Emperia.Items;

namespace Emperia.UI
{
	// ExampleUIs visibility is toggled by typing "/coin" in chat. (See CoinCommand.cs)
	// ExampleUI is a simple UI example showing how to use UIPanel, UIImageButton, and even a custom UIElement.
	class PaintUI : UIState
	{
		public UIElement smallIcon;
		public UIElement largeIcon;
		public UIElement modeSwap;
		public List<UIElement> smallIconList = new List<UIElement>();
		public List<UIElement> largeIconList = new List<UIElement>();
		//public UIElement BrushUI;
		//public static bool Visible = true;
		Texture2D iconTexture;
		bool mousedOver = false;
		bool canBeClicked = true;
		//public bool canBeClosed = false;
		public bool mousedOverAny = false;
		OldMastersPalette mastersPalette = Main.LocalPlayer.HeldItem.ModItem as OldMastersPalette;
		public PaintUI(Vector2 paintUIActivationPosition)
		{
		//	paintUIActivationPosition = Vector2.Zero;
		}

		public override void OnInitialize()
		{
			iconTexture = ModContent.Request<Texture2D>("Emperia/UI/Icon_0", AssetRequestMode.ImmediateLoad).Value;
			MakeSmallIcons();
			MakeLargeIcons();

			Vector2 swapPosition = paintUIActivationPosition - new Vector2(24, 28);//+ new Vector2(12, 10); // 26
			modeSwap = new ModeSwap(swapPosition);
			modeSwap.Left.Set(swapPosition.X, 0);
			modeSwap.Top.Set(swapPosition.Y, 0);
			modeSwap.Width.Set(14, 0);
			modeSwap.Height.Set(16, 0);
			Append(modeSwap);
			EmperiaSystem.modeSwapActive = modeSwap;
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
				smallIcon = new BucketSmall(i, iconPosition); //ModContent.Request<Texture2D>("Emperia/UI/Icon_0")
				smallIcon.Left.Set(iconPosition.X, 0);
				smallIcon.Top.Set(iconPosition.Y, 0);
				smallIcon.Width.Set(26, 0);
				smallIcon.Height.Set(26, 0);
				Append(smallIcon);
				smallIconList.Add(smallIcon);
			}
			EmperiaSystem.smallPaintIconList = smallIconList; //this updates them
		}
		public void MakeLargeIcons()
		{
			int iconCount = 5; //mastersPalette.selectedColors.Count;
			int distance = -45;
			for (int i = 0; i < iconCount; i++)
			{
				Vector2 iconCenter = new Vector2(0, distance).RotatedBy(MathHelper.ToRadians((360 / 5) * i)) + paintUIActivationPosition;
				Vector2 iconPosition = iconCenter - new Vector2(iconTexture.Width / 2, iconTexture.Height / 2);
				//iconPosition.X -= iconPosition.X % 1;
				//iconPosition.Y -= iconPosition.Y % 1;
				iconPosition.X = (int)Math.Round(iconPosition.X / 2) * 2;
				iconPosition.Y = (int)Math.Round(iconPosition.Y / 2) * 2;
				largeIcon = new BucketLarge(i, iconPosition);
				largeIcon.Left.Set(iconPosition.X, 0);
				largeIcon.Top.Set(iconPosition.Y, 0);
				largeIcon.Width.Set(40, 0);
				largeIcon.Height.Set(40, 0);
				Append(largeIcon);
				largeIconList.Add(largeIcon);
			}
			EmperiaSystem.largePaintIconList = largeIconList; //this updates them
		}

		public override void Update(GameTime gameTime)
		{
			if (Vector2.Distance(paintUIActivationPosition, Main.MouseScreen) < 19f)
			{
				mousedOver = true;
				mousedOverAny = true;
				Main.LocalPlayer.mouseInterface = true;
			}
			else
			{
				mousedOver = false;
				mousedOverAny = false;
			}

			int iconType = 0;
			if (mousedOver)
			{
				iconType = 1;
				if (Main.mouseLeft && canBeClicked)
				{
					mastersPalette.brushMode = (mastersPalette.brushMode + 1) % 3;
					canBeClicked = false;
				}
			}
			//if (mastersPalette.brushMode == 2) iconType += 2;
			iconTexture = ModContent.Request<Texture2D>("Emperia/UI/Icon_" + iconType).Value;
			if (Main.mouseLeftRelease) canBeClicked = true;
			//if (Main.mouseRightRelease) EmperiaSystem.canBeClosed = true;
			//if (Main.LocalPlayer.mouseInterface && (Math.Abs(Main.MouseScreen.X - paintUIActivationPosition.X) > 84 || Math.Abs(Main.MouseScreen.Y - paintUIActivationPosition.Y) > 84) || canBeClosed && Main.mouseRight) EmperiaSystem.paintUIActive = false;
			//mousedoverany may not be necessary
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			var circleSourceRectangle = new Rectangle(0, 0, iconTexture.Width, iconTexture.Height);
			Vector2 position = paintUIActivationPosition - new Vector2(iconTexture.Width / 2, iconTexture.Height / 2); //offsets to middle of mouse
			/*Color brightness = new Color(255, 255, 255);
			if (!active)
			{
				brightness = new Color(150, 150, 150);
				if (!mousedOver) brightness = new Color(80, 80, 80);
			}*/
			spriteBatch.Draw(iconTexture, position, circleSourceRectangle, Color.White);
			Texture2D brushTexture = ModContent.Request<Texture2D>("Emperia/UI/Brush_" + mastersPalette.brushMode).Value;
			var brushSourceRectangle = new Rectangle(0, 0, brushTexture.Width, brushTexture.Height);
			spriteBatch.Draw(brushTexture, position, brushSourceRectangle, Color.White);
			//WorldGen.paintColor(item.paint)
		}
	}
	class BucketSmall : UIElement
	{
		public BucketSmall(int index, Vector2 pos) //: base(texture) Asset<Texture2D> texture
		{
			iconIndex = index;
			position = pos;
        }
        int iconIndex;
		int paintType;
		int[] paintForPosition = new int[] { 28, 13, 14, 15, 16, 17, 27, 1, 2, 3, 4, 18, 25, 12, 5, 29, 26, 11, 6, 31, 24, 10, 9, 8, 7, 30, 23, 22, 21, 20, 19, 0 };
		Vector2 position;
		Texture2D iconTexture;
		Texture2D paintTexture;
		bool active = false; //this could be replaced with just mastersPalette.selectedColors.Contains(paintType)
		bool mousedOver = false;
		bool canBeClicked = true;
		public bool visible = true;

		OldMastersPalette mastersPalette = Main.LocalPlayer.HeldItem.ModItem as OldMastersPalette;


		public override void OnInitialize()
		{
			iconTexture = ModContent.Request<Texture2D>("Emperia/UI/IconSmall_0", AssetRequestMode.ImmediateLoad).Value;
			paintType = (int)paintForPosition.GetValue(iconIndex);
			paintTexture = ModContent.Request<Texture2D>("Terraria/Images/Item_" + PaintToItemID(paintType)).Value;
			if (!Main.gameMenu && mastersPalette.selectedColors.Contains(paintType)) active = true;
			if (!Main.gameMenu && mastersPalette.curatedPalette) visible = false;
			//OnClick += OnButtonClick;
			//OnMouseOver += OnButtonMouseOver;
		}
		public override void Update(GameTime gameTime)
        {
			if (!visible) return;
			if (Main.MouseScreen.X >= position.X && Main.MouseScreen.X <= position.X + iconTexture.Width && Main.MouseScreen.Y >= position.Y && Main.MouseScreen.Y <= position.Y + +iconTexture.Height)
			{
				mousedOver = true;
				(Parent as PaintUI).mousedOverAny = true;
				//Main.player[Main.myPlayer].GetModPlayer<MyPlayer>().mouseOverUI = true;
				Main.LocalPlayer.mouseInterface = true; //this also prevents altfunctionuse aka it wont let you close the menu so nope
			}
			else
			{
				mousedOver = false;
				(Parent as PaintUI).mousedOverAny = false;
			}

			int iconType = 0;

			if (mousedOver)
			{
				iconType = 1;
				if (Main.mouseLeft && canBeClicked)
				{
					if (paintType > 0)
					{
						if (!active)
						{
							active = true;
							mastersPalette.selectedColors.Add(paintType);
						}
						else
						{
							active = false;
							mastersPalette.selectedColors.Remove(paintType);
						}
					}
					else
                    {
						mastersPalette.selectedColors.Clear();
                    }
					canBeClicked = false;
				}
			}
			//if (mastersPalette.brushMode == 2) iconType += 2;
			iconTexture = ModContent.Request<Texture2D>("Emperia/UI/IconSmall_" + iconType).Value;
			if (Main.mouseLeftRelease) canBeClicked = true;
			if (!mastersPalette.selectedColors.Contains(paintType)) active = false;
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!visible) return;

			base.Draw(spriteBatch);
			var squareSourceRectangle = new Rectangle(0, 0, iconTexture.Width, iconTexture.Height);
			Color brightness = new Color(255, 255, 255);
			if (!active)
			{
				brightness = new Color(150, 150, 150);
				if (!mousedOver) brightness = new Color(80, 80, 80);
			}
			spriteBatch.Draw(iconTexture, position, squareSourceRectangle, brightness);

			if (brightness == new Color(150, 150, 150)) brightness = new Color(190, 190, 190); //buckets need to be brighter to be distinguishable
			else if (brightness == new Color(80, 80, 80)) brightness = new Color(140, 140, 140);
			var paintCrop = new Rectangle(6, 0, 14, 10);
			spriteBatch.Draw(paintTexture, position + new Vector2(6, 10), paintCrop, brightness);

			if (paintType == 0)
            {
				Texture2D trashTexture = ModContent.Request<Texture2D>("Emperia/UI/Trash").Value;
				spriteBatch.Draw(trashTexture, position, new Rectangle(0, 0, trashTexture.Width, trashTexture.Height), brightness);
			}

			if (mastersPalette.selectedColors.LastOrDefault() == paintType && paintType != 0)
            {
				Texture2D ribbonTexture = ModContent.Request<Texture2D>("Emperia/UI/SelectedIconRibbon").Value;
				spriteBatch.Draw(ribbonTexture, position, new Rectangle(0, 0, ribbonTexture.Width, ribbonTexture.Height), Color.White);
				Texture2D brushTexture = ModContent.Request<Texture2D>("Emperia/UI/BrushMode_0").Value;
				spriteBatch.Draw(brushTexture, position + new Vector2(0, 2), new Rectangle(0, 0, brushTexture.Width, brushTexture.Height), Color.White);
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

	class BucketLarge : UIElement
	{
		public BucketLarge(int index, Vector2 pos)
		{
			iconIndex = index;
			position = pos;
		}
		int iconIndex;
		public int paintType;
		int[] paintForPosition = new int[] { 28, 13, 14, 15, 16, 17, 27, 1, 2, 3, 4, 18, 25, 12, 5, 29, 26, 11, 6, 31, 24, 10, 9, 8, 7, 30, 23, 22, 21, 20, 19, 0 };
		Vector2 position;
		Texture2D iconTexture;
		Texture2D bucketTexture;
		Texture2D paintTexture;
		bool active = false; //this could be replaced with just mastersPalette.selectedColors.Contains(paintType)
		bool mousedOver = false;
		bool canBeClicked = false;
		public bool visible = false;

		OldMastersPalette mastersPalette = Main.LocalPlayer.HeldItem.ModItem as OldMastersPalette;


		public override void OnInitialize()
		{
			iconTexture = ModContent.Request<Texture2D>("Emperia/UI/Icon_0", AssetRequestMode.ImmediateLoad).Value;
			paintType = (int)paintForPosition.GetValue(iconIndex);
			bucketTexture = ModContent.Request<Texture2D>("Emperia/UI/Bucket", AssetRequestMode.ImmediateLoad).Value;
			paintTexture = ModContent.Request<Texture2D>("Emperia/UI/PaintSplatter", AssetRequestMode.ImmediateLoad).Value;
			//OnClick += OnButtonClick;
			//OnMouseOver += OnButtonMouseOver;
		}
		public override void Update(GameTime gameTime)
		{
			if (!Main.gameMenu && mastersPalette.curatedPalette) visible = true;
			if (paintType < 0) visible = false;

			if (mastersPalette.selectedColors.Any() && mastersPalette.selectedColors.Count > iconIndex)
            {
				paintType = mastersPalette.CuratedColorList(mastersPalette.selectedColors)[iconIndex];
			}
			else visible = false;

			if (!visible) return;


			if (mastersPalette.curatedColor == paintType) active = true;

			/*int visuals;
			if (paintType >= 13 && paintType <= 24 || paintType == 29) visuals = 1;
			else if (paintType == 30) visuals = 2;
			else if (paintType == 31) visuals = 3;
			else visuals = 0;

			//bucketTexture = ModContent.Request<Texture2D>("Emperia/UI/Bucket_" + visuals).Value;
			//if (paintType == 29) visuals = 0;
			//paintTexture = ModContent.Request<Texture2D>("Emperia/UI/PaintSplatter_" + visuals).Value;*/
			string visuals = mastersPalette.SpecialVFX(paintType);
			bucketTexture = ModContent.Request<Texture2D>("Emperia/UI/Bucket" + visuals).Value;
			if (paintType == 29) visuals = "";
			paintTexture = ModContent.Request<Texture2D>("Emperia/UI/PaintSplatter" + visuals).Value;



			if (Vector2.Distance(position + new Vector2(iconTexture.Width / 2, iconTexture.Height / 2), Main.MouseScreen) < 19f)
			{
				mousedOver = true;
				(Parent as PaintUI).mousedOverAny = true;
				//Main.player[Main.myPlayer].GetModPlayer<MyPlayer>().mouseOverUI = true;
				Main.LocalPlayer.mouseInterface = true; //this also prevents altfunctionuse aka it wont let you close the menu so nope
			}
			else
			{
				mousedOver = false;
				(Parent as PaintUI).mousedOverAny = false;
			}

			int iconType = 0;
			if (mousedOver)
			{
				iconType = 1;
				if (Main.mouseLeft && canBeClicked)
				{
					//if (paintType > 0)
					{
						if (!active)
						{
							active = true;
							//mastersPalette.selectedCuratedColors.Add(paintType);
							mastersPalette.curatedColor = paintType;
						}
						else
						{
							active = false;
							mastersPalette.curatedColor = 0;
						}
					}
					canBeClicked = false;
				}
			}
			//if (mastersPalette.brushMode == 2) iconType += 2;
			iconTexture = ModContent.Request<Texture2D>("Emperia/UI/Icon_" + iconType).Value;
			if (Main.mouseLeftRelease) canBeClicked = true;
			if (mastersPalette.curatedColor != paintType) active = false;
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!visible) return;

			base.Draw(spriteBatch);
			var circleSourceRectangle = new Rectangle(0, 0, iconTexture.Width, iconTexture.Height);
			Color brightness = new Color(255, 255, 255);
			if (!active)
			{
				brightness = new Color(150, 150, 150);
				if (!mousedOver) brightness = new Color(80, 80, 80);
			}
			spriteBatch.Draw(iconTexture, position, circleSourceRectangle, brightness);
			if (brightness == new Color(150, 150, 150)) brightness = new Color(190, 190, 190); //buckets need to be brighter to be distinguishable
			else if (brightness == new Color(80, 80, 80)) brightness = new Color(140, 140, 140);
			spriteBatch.Draw(bucketTexture, position, circleSourceRectangle, brightness);
			Color color = mastersPalette.PaintToColor(paintType, true).MultiplyRGB(brightness);
			spriteBatch.Draw(paintTexture, position, circleSourceRectangle, color);
		}
	}
	class ModeSwap : UIElement
	{
		public ModeSwap(Vector2 pos)
		{
			position = pos;
		}
		Vector2 position;
		Texture2D iconTexture;
		bool mousedOver = false;
		bool canBeClicked = true;
		OldMastersPalette mastersPalette = Main.LocalPlayer.HeldItem.ModItem as OldMastersPalette;


		public override void OnInitialize()
		{
			iconTexture = ModContent.Request<Texture2D>("Emperia/UI/ModeSwap_0", AssetRequestMode.ImmediateLoad).Value;

		}
		public override void Update(GameTime gameTime)
		{
			if (Vector2.Distance(position + new Vector2(6.5f, 7.5f), Main.MouseScreen) < 6.5f)
			{
				mousedOver = true;
				(Parent as PaintUI).mousedOverAny = true;
				Main.LocalPlayer.mouseInterface = true;
			}
			else
			{
				mousedOver = false;
				(Parent as PaintUI).mousedOverAny = false;
			}
			if (mousedOver)
			{
				iconTexture = ModContent.Request<Texture2D>("Emperia/UI/ModeSwap_1").Value;
				if (Main.mouseLeft && canBeClicked && mastersPalette.selectedColors.Any())
				{
					//if (smallPaintIconList.Any())
						for (int i = 0; i < 32; i++)
						{
							(smallPaintIconList[i] as BucketSmall).visible = mastersPalette.curatedPalette;
							//Parent.RemoveChild(smallPaintIconList[i]);
						}
						mastersPalette.curatedPalette = !mastersPalette.curatedPalette;
						int iterations = (mastersPalette.selectedColors.Count > 5) ? 5 : mastersPalette.selectedColors.Count;
						for (int i = 0; i < iterations; i++)
						{
							(largePaintIconList[i] as BucketLarge).visible = mastersPalette.curatedPalette;
							//Parent.RemoveChild(smallPaintIconList[i]);
						}
					//smallPaintIconList?.Clear();
					//mastersPalette.brushMode = (mastersPalette.brushMode + 1) % 3;
					canBeClicked = false;
				}
			}
			else iconTexture = ModContent.Request<Texture2D>("Emperia/UI/ModeSwap_0").Value;
			if (!mastersPalette.selectedColors.Any()) iconTexture = ModContent.Request<Texture2D>("Emperia/UI/ModeSwap_2").Value;
			if (Main.mouseLeftRelease) canBeClicked = true;
			//if (Main.mouseRightRelease) EmperiaSystem.canBeClosed = true;
			//if (Main.LocalPlayer.mouseInterface && (Math.Abs(Main.MouseScreen.X - paintUIActivationPosition.X) > 84 || Math.Abs(Main.MouseScreen.Y - paintUIActivationPosition.Y) > 84) || canBeClosed && Main.mouseRight) EmperiaSystem.paintUIActive = false;
			//mousedoverany may not be necessary
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			var circleSourceRectangle = new Rectangle(0, 0, iconTexture.Width, iconTexture.Height);
			spriteBatch.Draw(iconTexture, position, circleSourceRectangle, Color.White);
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
			Texture2D cursorTexture = ModContent.Request<Texture2D>("Emperia/UI/BrushMode_" + mastersPalette.brushMode).Value;
			spriteBatch.Draw(cursorTexture, Main.MouseScreen + new Vector2(16, 16), new Rectangle(0, 0, cursorTexture.Width, cursorTexture.Height), Color.White * alpha);
			if (mastersPalette.brushMode != 2 && mastersPalette.color != 0)
			{
				Color color = mastersPalette.PaintToColor(mastersPalette.color);
				Texture2D paintTexture = ModContent.Request<Texture2D>("Emperia/UI/BrushModePaint_" + mastersPalette.brushMode + mastersPalette.SpecialVFX(mastersPalette.color)).Value;
				spriteBatch.Draw(paintTexture, Main.MouseScreen + new Vector2(16, 16), new Rectangle(0, 0, cursorTexture.Width, cursorTexture.Height), color * alpha);
			}
		}
    }
}