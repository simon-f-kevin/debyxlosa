using Microsoft.Xna.Framework.Input;


namespace GameEngine.Components
{
    public class KeyboardControlComponent: EntityComponent
    {
        public Keys UpKey { get; set; }
        public Keys DownKey { get; set; }
        public Keys LeftKey { get; set; }
        public Keys RightKey { get; set; }
        public Keys SpecialKey { get; set; }

        public KeyboardControlComponent() { }
        public KeyboardControlComponent(int compID):base(compID)
        {

        }
    }
}
