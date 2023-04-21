using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.UI;
using Emperia.UI;
using Terraria.GameInput;

namespace Emperia
{

    public class UISystem : ModSystem
    {
        internal static UserInterface MyInterface;
        private GameTime _lastUpdateUiGameTime;
        public class EmperiaUIState : UIState
        {
            public Vector2 activationPosition;
            public int? heldItemType = null;
            public EmperiaUIState(Vector2? activationPos = null)
            {
                if (activationPos == null) activationPos = Vector2.Zero;
                activationPosition = (Vector2)activationPos;
                Activate();
            }
            public void TryDeactivate()
            {
                if (MyInterface?.CurrentState != null && MyInterface?.CurrentState == this) SetUIStateNull();
            }
        }
        private static List<EmperiaUIState> emperiaUITypes = new List<EmperiaUIState>();

        public static bool canRightClick = false;
        public static int cursorIsFreeForUI = 1;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                MyInterface = new UserInterface();

                emperiaUITypes.Add(new PaintUI());
                emperiaUITypes.Add(new CursorUI());
                emperiaUITypes.Add(new LcdUI());  
                //when adding new UI elements, simply add a line here, as well as in Activate(). Also make sure to set a heldItemType. MouseIsOffUI required for anything not the standard circle
            }
        }
        public override void Unload()
        {
            emperiaUITypes.Clear();
        }
        public override void UpdateUI(GameTime gameTime)
        {
            if (GetCurrentUI() != null) ManageActivationByHeldItem(Main.LocalPlayer.HeldItem.ModItem, GetCurrentUI());

            if (MyInterface?.CurrentState != null && MyInterface?.CurrentState is CursorUI)
            {
                if (Main.LocalPlayer.mouseInterface) SetUIStateNull();
            }

            _lastUpdateUiGameTime = gameTime;
            if (MyInterface?.CurrentState != null)
            {
                MyInterface.Update(gameTime);
            }
        }

        public EmperiaUIState GetCurrentUI()
        {
            if (MyInterface?.CurrentState == null || MyInterface?.CurrentState is CursorUI)
            {
                foreach (var ui in emperiaUITypes)
                {
                    if (ui.heldItemType == null) continue;
                    if (Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].type == ui.heldItemType)
                    {
                        return ui;
                    }
                }
            }
            else
            {
                EmperiaUIState ui = (MyInterface?.CurrentState as EmperiaUIState);
                if (ui.heldItemType != null && Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].type == ui.heldItemType) return ui;
            }
            return null;
        }

        public void ManageActivationByHeldItem(ModItem heldItem, EmperiaUIState ui)
        {
            if (Main.mouseRightRelease) canRightClick = true;

            if (!PlayerInput.LockGamepadTileUseButton && Main.LocalPlayer.noThrow == 0 && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)
            {
                if (cursorIsFreeForUI < 1) cursorIsFreeForUI++; //i guess so it takes two frames to activate?
            }
            else cursorIsFreeForUI = -1;

            if (MyInterface?.CurrentState != null && ui == MyInterface?.CurrentState) //right click to deactivate
            {
                if (Main.mouseRight && canRightClick && cursorIsFreeForUI == 1
                || Main.LocalPlayer.mouseInterface && MouseIsOffUI(ui.activationPosition, heldItem))
                {
                    SetUIStateNull();
                    if (Main.mouseRight) canRightClick = false;
                    return;
                }
            }

            if (!Main.LocalPlayer.mouseInterface)
            {
                if (MyInterface?.CurrentState == null || MyInterface?.CurrentState is CursorUI)
                {

                    if (Main.mouseRight && canRightClick && cursorIsFreeForUI == 1) //right click to activate
                    {
                        Activate(ui);
                    }
                    else if (MyInterface?.CurrentState is not CursorUI && Main.LocalPlayer.cursorItemIconID == 0 && heldItem.Item.GetGlobalItem<GItem>().TileInRange(heldItem.Item, Main.LocalPlayer)) //activates in range
                    { //im surprised this doesnt already run for non-pallette items. it shouldn't, but idk why it doesnt
                        Activate(new CursorUI());
                    }
                }
            }

            if (Main.mouseRight) canRightClick = false;
        }
        public void Activate(EmperiaUIState ui)
        {
            if (MyInterface?.CurrentState == null || MyInterface.CurrentState is CursorUI)
            {
                SetUIStateNull();
                EmperiaUIState newUI;
                switch (ui)
                {
                    case PaintUI: newUI = new PaintUI(Main.MouseScreen); break;
                    case CursorUI: newUI = new CursorUI(Main.MouseScreen); break;
                    case LcdUI: newUI = new LcdUI(Main.MouseScreen); break;
                    default: newUI = null; break;
                }
                //Type t = ui.GetType();
                //var newUI = Activator.CreateInstance(t); would require a parameterless constructor
                MyInterface?.SetState(newUI);
            }
        }
        public static void SetUIStateNull()
        {
            if (MyInterface?.CurrentState != null)
            {
                MyInterface?.SetState(null);
            }
        }
        public static bool MouseIsOffUI(Vector2 uiActivationPosition, ModItem heldItem)
        {
            //first option is for squares of radius 84, second is for circles of radius 64
            if (heldItem is Items.OldMastersPalette && !(heldItem as Items.OldMastersPalette).curatedMode) return Math.Abs(Main.MouseScreen.X - uiActivationPosition.X) > 84 || Math.Abs(Main.MouseScreen.Y - uiActivationPosition.Y) > 84;
            else return Vector2.Distance(uiActivationPosition, Main.MouseScreen) > 64f;
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
    }
    public class PlayerUIBehavior : ModPlayer
    {
        public bool scrollingInUI;
        public override void ResetEffects()
        {
            scrollingInUI = false;

        }
        public override void PreUpdate()
        {
            if (scrollingInUI) PlayerInput.ScrollWheelDelta = 0;
        }
    }
}