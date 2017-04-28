using Microsoft.Xna.Framework.Input;


namespace GameEngine.Components
{
    public class KeyboardControllComponent: EntityComponent
    {
        public Keys UpKey { get; set; }
        public Keys DownKey { get; set; }
        public Keys LeftKey { get; set; }
        public Keys RightKey { get; set; }
        public KeyboardControllComponent() { }
        public KeyboardControllComponent(int compID):base(compID)
        {

        }
    }
}
