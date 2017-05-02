using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameEngine.Systems
{

    public class InputSystem : IInputHandler, ISystem
    {   
        private bool allowsExiting;
        private KeyBoardHandler keyboard;
        private double counter;
        private Game game;

        public InputSystem(Game game) : this(game, false)
        {
        }
        public InputSystem(Game game, bool allowsExiting)
        {
            this.game = game;
            this.allowsExiting = allowsExiting;
            //game.Services.AddService(typeof(IInputHandler), this);
            keyboard = new KeyBoardHandler();
            game.IsMouseVisible = true;
        }

        public KeyBoardHandler KeyboardState
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public void Update(GameTime gameTime)
        {
            counter += gameTime.ElapsedGameTime.TotalSeconds;
            keyboard.Update();
            counter = 0.0;
            if (allowsExiting)
            {
                if (keyboard.anyKeyPress())
                {
                    if (keyboard.IsKeyDown(Keys.Escape))
                    {
                        /*Can't exit a program in a library/DLL, and thus we need to set a status somewhere that the monogame can read and execute the closeure*/
                        //ComponentManager.getInstance().setGameStatus(true);
                    }
                }
            }
            Dictionary<int, EntityComponent> _KeyboardUsers = ComponentManager.Instance.getComponentDictionary<KeyboardControlComponent>();
            Dictionary<int, EntityComponent> _ActionDirection = ComponentManager.Instance.getComponentDictionary<ActionDirectionComponent>();
            EntityComponent actionComp;
            if (_KeyboardUsers == null)
            {
                return;
            }
            if (_KeyboardUsers.Keys.Count > 0)
            {
                foreach (KeyboardControlComponent controlls in _KeyboardUsers.Values)
                {

                    if (_ActionDirection.TryGetValue(controlls.EntityId, out actionComp))
                    {
                        ActionDirectionComponent actionDir = (ActionDirectionComponent)actionComp;
                        handleIsKeypressedReleased(actionDir, controlls);
                        handleIsKeypressed(actionDir, controlls);                       
                        //handleIsKeypressedReleased(actionDir, controls);
                        //handleIsHoldingKeyDown(actionDir, controls);
                    }
                }
            }          
        }
        private void handleIsKeypressedReleased(ActionDirectionComponent actionDir, KeyboardControlComponent controls)
        {
            if (keyboard.isKeyReleased(controls.RightKey))
            {
                actionDir.Right = false;               
            }
            if (keyboard.isKeyReleased(controls.LeftKey))
            {               
                actionDir.Left = false;             
            }
            if (keyboard.isKeyReleased(controls.UpKey))
            {
                actionDir.Up = false;
            }
            if (keyboard.isKeyReleased(controls.DownKey))
            {                
                actionDir.Down = false;
            }
            if (keyboard.isKeyReleased(controls.SpecialKey))
            {
                actionDir.Special = false;
            }
        }
        private void handleIsKeypressed(ActionDirectionComponent actionDir, KeyboardControlComponent controls)
        {
            if (keyboard.IsKeyDown(controls.RightKey))
            {
                actionDir.Right = true;
                actionDir.Left = false;

            }
            if (keyboard.IsKeyDown(controls.LeftKey))
            {
                actionDir.Left = true;
                actionDir.Right = false;

            }
            if (keyboard.IsKeyDown(controls.UpKey))
            {
                actionDir.Up = true; 
                actionDir.Down = false;

            }
            if (keyboard.IsKeyDown(controls.DownKey))
            {

                actionDir.Down = true;
                actionDir.Up = false;

            }
            if (keyboard.IsKeyDown(controls.SpecialKey))
            {
                actionDir.Special = true;
            }
        }

        private void handleIsHoldingKeyDown(ActionDirectionComponent actionDir, KeyboardControlComponent controls)
        {
            if (keyboard.IsHoldingKeyDown(controls.DownKey))
            {
                actionDir.Down = true;
                actionDir.Up = false;

            }
            if (keyboard.IsHoldingKeyDown(controls.UpKey))
            {
                actionDir.Up = true;
                actionDir.Down = false;

            }
            if (keyboard.IsHoldingKeyDown(controls.LeftKey))
            {
                actionDir.Left = true;
                actionDir.Right = false;

            }
            if (keyboard.IsHoldingKeyDown(controls.RightKey))
            {
                actionDir.Right = true;
                actionDir.Left = false;

            }
            if(keyboard.IsHoldingKeyDown(controls.RightKey) && keyboard.IsHoldingKeyDown(controls.LeftKey))
            {
                actionDir.Right = false;
                actionDir.Left = false;
            }
        }
    }

    public class KeyBoardHandler
    {
        private KeyboardState prevKeyboardState;
        private KeyboardState keyboardState;
        public KeyBoardHandler()
        {
            prevKeyboardState = Keyboard.GetState();
        }
        public bool IsKeyDown(Keys key)
        {
            return (keyboardState.IsKeyDown(key));
        }
        public bool IsHoldingKeyDown(Keys key)
        {
            return (keyboardState.IsKeyDown(key) && prevKeyboardState.IsKeyDown(key));
        }
        public bool isKeyReleased(Keys key)
        {
            return (keyboardState.IsKeyUp(key) && prevKeyboardState.IsKeyDown(key));
        }
        public bool anyKeyPress()
        {
            if (keyboardState.GetPressedKeys().Length > 0)
            {
                return true;
            }
            return false;
        }

        public void Update()
        {
            this.prevKeyboardState = this.keyboardState;
            keyboardState = Keyboard.GetState();
        }
    }
}