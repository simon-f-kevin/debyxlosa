using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Managers
{
    public class GamePropertyManager
    {
        private static GamePropertyManager _Instance;
        private GraphicsDevice _Graphic;
        private Game _Game;
        public static GamePropertyManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new GamePropertyManager();
                }
                return _Instance;
            }
        }
        public Game getGame()
        {
            return _Game;
        }
        public void setGame(Game game)
        {
            this._Game = game;
        }
        public GraphicsDevice getGraphics()
        {
            return _Graphic;
        }
        public void setGraphics(GraphicsDevice graphics)
        {
            this._Graphic = graphics;
        }
    }
}
