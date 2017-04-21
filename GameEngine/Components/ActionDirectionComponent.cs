

namespace GameEngine.Components
{
    public class ActionDirectionComponent : Component
    {
        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool Up { get; set; }
        public bool Down { get; set; }
        public ActionDirectionComponent(int compID) : base(compID)
        {
            Left = false;
            Right = false;
            Up = false;
            Down = false;
        }
    }
}
