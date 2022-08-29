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
    public class UISystem : ModSystem
    {
        internal UserInterface MyInterface;
        private GameTime _lastUpdateUiGameTime;

        public static bool canRightClick = false;
        public static int cursorIsFreeForUI = 1;

        internal PaintUI MyPaintUI;
        public static bool paintUIActive;
        public static Vector2 paintUIActivationPosition;
        public static List<UIElement> smallPaintIconList = new List<UIElement>();
        public static List<UIElement> largePaintIconList = new List<UIElement>();
        public static UIState CurrentPaintUI = null;

        internal CursorUI MyCursorUI;
        public static bool cursorUIActive = false;
        public static bool canStartDrawingCursorUI = false;

        internal LcdUI MyLcdUI;
        public static bool lcdUIActive = false;
        public static Vector2 lcdUIActivationPosition;
        public static UIState CurrentLcdUI = null;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                MyInterface = new UserInterface();

                MyPaintUI = new PaintUI();
                MyPaintUI.Activate();

                MyCursorUI = new CursorUI();
                MyCursorUI.Activate();

                MyLcdUI = new LcdUI();
                MyLcdUI.Activate();
            }
        }
        public override void Unload()
        {
            MyPaintUI = null;
            MyCursorUI = null;
            MyLcdUI = null;
        }
        public override void OnWorldUnload()
        {
            paintUIActive = false;
            lcdUIActive = false;
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
                ManageUIActivationAndDeactivation(ref paintUIActive, ref paintUIActivationPosition, Main.LocalPlayer.HeldItem.ModItem);
            }
            else paintUIActive = false;

            if (Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].type == ItemType<Items.LCDWrench>())
            {
                ManageUIActivationAndDeactivation(ref lcdUIActive, ref lcdUIActivationPosition, Main.LocalPlayer.HeldItem.ModItem);
            }
            else lcdUIActive = false;

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
                else if (!lcdUIActive && MyInterface.CurrentState.ToString() == "Emperia.UI.LcdUI")
                {
                    HideMyUI();
                }
            }

            if (paintUIActive && (MyInterface?.CurrentState == null || cursorUIActive)) ShowMyUI("PaintUI");
            else if (canStartDrawingCursorUI && !paintUIActive && MyInterface?.CurrentState == null && !Main.LocalPlayer.mouseInterface)
            {
                ShowMyUI("CursorUI");
            }

            if (lcdUIActive && MyInterface?.CurrentState == null) ShowMyUI("LcdUI");

            canStartDrawingCursorUI = false; //use it or lose it
        }

        public void ManageUIActivationAndDeactivation(ref bool UIActive, ref Vector2 uiActivationPosition, ModItem heldItem)
        {
            if (Main.mouseRightRelease) canRightClick = true;
            if (!PlayerInput.LockGamepadTileUseButton && Main.LocalPlayer.noThrow == 0 && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)
            {
                if (cursorIsFreeForUI < 1) cursorIsFreeForUI++;
            }
            else cursorIsFreeForUI = -1;

            if (UIActive)
            {
                if (Main.mouseRight && canRightClick && cursorIsFreeForUI == 1
                    || Main.LocalPlayer.mouseInterface && MouseIsOffUI(uiActivationPosition, heldItem)
                    )
                {
                    UIActive = false;
                }
            }
            else
            {
                if (Main.mouseRight && canRightClick && !Main.LocalPlayer.mouseInterface && cursorIsFreeForUI == 1)
                {
                    UIActive = true;
                    uiActivationPosition = Main.MouseScreen;
                }
            }
            if (Main.mouseRight) canRightClick = false;
        }
        public static bool MouseIsOffUI(Vector2 uiActivationPosition, ModItem heldItem)
        {
            return false;
            //first option is for squares of radius 84, second is for circles of radius 64
            if (heldItem is Items.OldMastersPalette && !(heldItem as Items.OldMastersPalette).curatedMode) return Math.Abs(Main.MouseScreen.X - uiActivationPosition.X) > 84 || Math.Abs(Main.MouseScreen.Y - uiActivationPosition.Y) > 84;
            else return Vector2.Distance(uiActivationPosition, Main.MouseScreen) > 64f;
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
            if (UIType == "LcdUI")
            {
                HideMyUI();
                MyInterface?.SetState(new LcdUI());
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

            CurrentLcdUI = null;
        }

        public static void AddIconScrollWheelFunctionality(ref int toolSelectedValue, int iconValue, List<int> iconValues, int iconIndex, ref bool canScroll)
        {
            if (toolSelectedValue == iconValue && PlayerInput.ScrollWheelDeltaForUI != 0 && canScroll)
            {
                int iconToScrollTo = (iconIndex - Math.Sign(PlayerInput.ScrollWheelDeltaForUI)) % (iconValues.Count);
                if (iconToScrollTo < 0) iconToScrollTo += iconValues.Count;
                toolSelectedValue = iconValues[iconToScrollTo];
                canScroll = false;
                // PlayerInput.ScrollWheelDelta = 0; is set in ModPlayer PreUpdate()
            }
        }
    }
}