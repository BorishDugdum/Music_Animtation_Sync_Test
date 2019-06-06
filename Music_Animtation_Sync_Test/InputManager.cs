using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Music_Animtation_Sync_Test
{
    public static class InputManager
    {
        private static KeyboardState prevKeyState, keyState;
        private static TouchCollection touchCollection;

        /// <summary>
        /// Update input - call once per update
        /// </summary>
        public static void Update()
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();

            touchCollection = TouchPanel.GetState();
        }

        public static bool IsTouched()
        {
            if (touchCollection.Count > 0)
            {
                //Only Fire Select Once it's been released
                if (touchCollection[0].State == TouchLocationState.Pressed)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks to see if a key has been pressed
        /// </summary>
        /// <param name="key">Microsoft.Xna.Framework.Input.Keys</param>
        /// <returns>true/false</returns>
        public static bool IsKeyPressed(Keys key)
        {
            if (KeyPressed(key))
                return true;
            else
                return false;
        }

        private static bool KeyPressed(Keys key)
        {
            if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
            {
                return true;
            }

            return false;
        }

    }
}