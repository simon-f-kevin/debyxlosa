using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Managers
{
    class InputManager
    {
        //Singelton??//

        private KeyboardState previousKeyState;
        private KeyboardState keyState;

        public KeyboardState PreviouKeyboardState
        {
            get { return previousKeyState; }
            set { previousKeyState = value; }
        }

        public KeyboardState KeyState
        {
            get { return keyState; }
            set { keyState = value; }
        }

        public void update()
        {
            previousKeyState = keyState;
            keyState = Keyboard.GetState();
        }

        public bool KeyPressed(Keys key)
        {
            if (keyState.IsKeyDown(key) && previousKeyState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }

        public bool KeyReleased(Keys key)
        {
            if (keyState.IsKeyUp(key) && previousKeyState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public bool KeyDown(Keys key)
        {
            if (keyState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }
    }
}
