using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using Emperia.UI;
using Terraria.GameInput;

namespace Emperia
{
	public class PaintUISystem : ModSystem
	{
        internal UserInterface MyInterface;
        internal PaintUI MyPaintUI;
        internal CursorUI MyCursorUI;

        private GameTime _lastUpdateUiGameTime;

        public static bool paintUIActive;
        public static Vector2 paintUIActivationPosition;
        public static bool canRightClick = false;
        public static int cursorIsFreeForUI = 1;
        public static List<UIElement> smallPaintIconList = new List<UIElement>();
        public static List<UIElement> largePaintIconList = new List<UIElement>();
        public static UIState CurrentPaintUI = null;

        public static bool cursorUIActive = false;
        public static bool canStartDrawingCursorUI = false;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                MyInterface = new UserInterface();

                MyPaintUI = new PaintUI();
                MyPaintUI.Activate();

                MyCursorUI = new CursorUI();
                MyCursorUI.Activate();
            }
        }
        public override void Unload()
        {
            MyPaintUI = null;
            MyCursorUI = null;
        }
        public override void OnWorldUnload()
        {
            paintUIActive = false;
        }
        public override void UpdateUI(GameTime gameTime)
        {
            //Main.NewText(paintUIActive.ToString(), 0, 255, 255);
            //Main.NewText(cursorUIActive.ToString());
            _lastUpdateUiGameTime = gameTime;
            if (MyInterface?.CurrentState != null)
            {
                MyInterface.Update(gameTime);
                /*if (paintUIActive)
                {
                    if (CurrentPaintUI != null)
                    {
                        foreach (UIElement element in CurrentPaintUI.Children)
                        {
                            element.Update(gameTime);
                        }
                    }
                }*/
            }

            if (Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].type == ItemType<Items.OldMastersPalette>())
            {
                Items.OldMastersPalette mastersPalette = Main.LocalPlayer.HeldItem.ModItem as Items.OldMastersPalette;
                //if (Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem])
                if (Main.mouseRightRelease) canRightClick = true;
                if (!PlayerInput.LockGamepadTileUseButton && Main.LocalPlayer.noThrow == 0 && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)
                {
                    if (cursorIsFreeForUI < 1) cursorIsFreeForUI++;
                }
                else cursorIsFreeForUI = -1;

                if (paintUIActive)
                {
                    if (Main.mouseRight && canRightClick && cursorIsFreeForUI == 1
                        || !mastersPalette.curatedMode && Main.LocalPlayer.mouseInterface && (Math.Abs(Main.MouseScreen.X - paintUIActivationPosition.X) > 84 || Math.Abs(Main.MouseScreen.Y - paintUIActivationPosition.Y) > 84)
                        || mastersPalette.curatedMode && Main.LocalPlayer.mouseInterface && Vector2.Distance(paintUIActivationPosition, Main.MouseScreen) > 64f
                        )
                    {
                        paintUIActive = false;
                        //canRightClick = false;
                    }
                }
                else
                {
                    if (Main.mouseRight && canRightClick && !Main.LocalPlayer.mouseInterface && cursorIsFreeForUI == 1)
                    {
                        paintUIActive = true;
                        paintUIActivationPosition = Main.MouseScreen;
                        //canRightClick = false;
                    }
                }
                if (Main.mouseRight) canRightClick = false;
            }
            else paintUIActive = false;

            if (MyInterface?.CurrentState != null)
            {
                if (!paintUIActive && MyInterface.CurrentState.ToString() == "Emperia.UI.PaintUI")
                {
                    HideMyUI();
                }
                else if ((!cursorUIActive || Main.LocalPlayer.mouseInterface) && MyInterface.CurrentState.ToString() == "Emperia.UI.CursorUI")
                {
                    HideMyUI();
                }
            }

            if (paintUIActive && (MyInterface?.CurrentState == null || cursorUIActive)) ShowMyUI("PaintUI");
            else if (canStartDrawingCursorUI && !paintUIActive && MyInterface?.CurrentState == null && !Main.LocalPlayer.mouseInterface)
            {
                ShowMyUI("CursorUI");
            }
            canStartDrawingCursorUI = false; //use it or lose it
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Emperia: MyInterface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && MyInterface?.CurrentState != null)
                        {
                            MyInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));
            }
        }
        internal void ShowMyUI(string UIType)
        {
            if (UIType == "PaintUI")
            {
                HideMyUI();
                MyInterface?.SetState(new PaintUI());
            }
            if (UIType == "CursorUI")
            {
                MyInterface?.SetState(new CursorUI());
                cursorUIActive = true;
            }
            //MyInterface?.SetState(MyUI);
        }

        internal void HideMyUI()
        {
            MyInterface?.SetState(null);
            smallPaintIconList?.Clear();
            largePaintIconList?.Clear();
            CurrentPaintUI = null;

            cursorUIActive = false;
        }
    }
}