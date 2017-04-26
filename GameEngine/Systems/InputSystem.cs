using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameEngine.Systems
{
    interface IInputHandler
    {
    KeyBoardHandler KeyboardState { get; }
    }
    public class InputSystem : GameComponent, IInputHandler
    {   
        private bool allowsExiting;
        private KeyBoardHandler keyboard;
        private double counter;

        public InputSystem(Game game) : this(game, false)
        {
        }
        public InputSystem(Game game, bool allowsExiting) : base(game)
        {
            this.allowsExiting = allowsExiting;
            game.Services.AddService(typeof(IInputHandler), this);
            keyboard = new KeyBoardHandler();
            Game.IsMouseVisible = true;
        }

        public KeyBoardHandler KeyboardState
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public override void Update(GameTime gameTime)
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
            Dictionary<int, EntityComponent> _KeyboardUsers = ComponentManager.Instance.getComponentDictionary<KeyboardControllComponent>();
            Dictionary<int, EntityComponent> _ActionDirection = ComponentManager.Instance.getComponentDictionary<ActionDirectionComponent>();
            EntityComponent actionComp;
            if (_KeyboardUsers == null)
            {
                return;
            }
            if (_KeyboardUsers.Keys.Count > 0)
            {
                foreach (KeyboardControllComponent controlls in _KeyboardUsers.Values)
                {

                    if (_ActionDirection.TryGetValue(controlls.EntityId, out actionComp))
                    {
                        ActionDirectionComponent actionDir = (ActionDirectionComponent)actionComp;
                        handleIsKeypressedReleased(actionDir, controlls);
                        handleIsKeypressed(actionDir, controlls);
                        //handleIsKeypressedReleased(actionDir, controlls);
                        //handleIsHoldingKeyDown(actionDir, controlls);
                    }
                }
            }          
        }
        private void handleIsKeypressedReleased(ActionDirectionComponent actionDir, KeyboardControllComponent controlls)
        {
            if (keyboard.isKeyReleased(controlls.RightKey))
            {
                actionDir.Right = false;               
            }
            if (keyboard.isKeyReleased(controlls.LeftKey))
            {               
                actionDir.Left = false;             
            }
            if (keyboard.isKeyReleased(controlls.UpKey))
            {
                actionDir.Up = false;
            }
            if (keyboard.isKeyReleased(controlls.DownKey))
            {                
                actionDir.Down = false;
            }
        }
        private void handleIsKeypressed(ActionDirectionComponent actionDir, KeyboardControllComponent controlls)
        {
            if (keyboard.IsKeyDown(controlls.RightKey))
            {
                actionDir.Right = true;
                actionDir.Left = false;

            }
            if (keyboard.IsKeyDown(controlls.LeftKey))
            {
                actionDir.Left = true;
                actionDir.Right = false;

            }
            if (keyboard.IsKeyDown(controlls.UpKey))
            {
                actionDir.Up = true; 
                actionDir.Down = false;

            }
            if (keyboard.IsKeyDown(controlls.DownKey))
            {

                actionDir.Down = true;
                actionDir.Up = false;

            }
        }

        private void handleIsHoldingKeyDown(ActionDirectionComponent actionDir, KeyboardControllComponent controlls)
        {
            if (keyboard.IsHoldingKeyDown(controlls.DownKey))
            {
                actionDir.Down = true;
                actionDir.Up = false;

            }
            if (keyboard.IsHoldingKeyDown(controlls.UpKey))
            {
                actionDir.Up = true;
                actionDir.Down = false;

            }
            if (keyboard.IsHoldingKeyDown(controlls.LeftKey))
            {
                actionDir.Left = true;
                actionDir.Right = false;

            }
            if (keyboard.IsHoldingKeyDown(controlls.RightKey))
            {
                actionDir.Right = true;
                actionDir.Left = false;

            }
            if(keyboard.IsHoldingKeyDown(controlls.RightKey) && keyboard.IsHoldingKeyDown(controlls.LeftKey))
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