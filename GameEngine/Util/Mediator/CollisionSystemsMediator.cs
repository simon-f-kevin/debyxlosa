using GameEngine.Systems;

namespace GameEngine.Util.Mediator
{
    public class CollisionSystemsMediator : ISystemsMediator
    {
        //Instances of collisionsystems
        public ISystem CollisionHandler { get; set; }
        public void sendMessage(MediatorMessage message)
        {
            ((CollisionHandlingSystem)CollisionHandler).Collisions.Add(message);
        }
    }
}