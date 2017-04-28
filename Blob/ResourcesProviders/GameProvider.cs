namespace Blob.ResourcesProviders
{
    public class GameProvider
    {
        private Game1 _game;
        private static GameProvider _instance;

        private GameProvider() { }

        public static GameProvider getInstance()
        {
            if (_instance == null)
            {
                _instance = new GameProvider();
            }
            return _instance;
        }

        public Game1 Game
        {
            get { return _game; }
            set { _game = value; }
        }
    }
}