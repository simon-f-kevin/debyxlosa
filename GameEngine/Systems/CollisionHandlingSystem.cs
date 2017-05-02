using System.Collections.Generic;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Util.Mediator;
using Microsoft.Xna.Framework;

namespace GameEngine.Systems
{
    public delegate void CollisionHandlingDelegate(List<MediatorMessage> collisions, GameTime gameTime);

    public class CollisionHandlingSystem : ISystem
    {
        private CollisionHandlingDelegate collisionHandler;
        public List<MediatorMessage> Collisions { get; set; }

        public CollisionHandlingSystem()
        {
            collisionHandler = noCollisionhandling;
            Collisions = new List<MediatorMessage>();
        }

        public CollisionHandlingSystem(CollisionHandlingDelegate collisionHandler)
        {
            this.collisionHandler = collisionHandler;
            Collisions = new List<MediatorMessage>();
        }

        public void Update(GameTime gameTime)
        {
            collisionHandler(Collisions, gameTime);
            Collisions.Clear();
        }

        /*Default value for delegate if no function is provided in constructor*/
        private void noCollisionhandling(List<MediatorMessage> collisions, GameTime gameTime) {}
    }
}