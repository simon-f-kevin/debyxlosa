using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Util.Mediator;
using Microsoft.Xna.Framework;

namespace GameEngine.Systems
{
    public class CollisionDetectionSystem : ISystem
    {
        //private List<PositionComponent> _listOfPositions;
        //private List<CollisionComponent> _listOfCollisions;

        private List<RectangleComponent> _listOfRectangles;
        private Dictionary<int, EntityComponent> _positions;
        private Dictionary<int, EntityComponent> _collisions;
        private Dictionary<int, EntityComponent> _velocities;
        private ISystemsMediator _mediator;
        private GameTime _gameTime;

        private bool _collisionDetected;

        public CollisionDetectionSystem(ISystemsMediator mediator)
        {
            _mediator = mediator;
        }

        public void Update(GameTime gameTime)
        {
            _listOfRectangles = ComponentManager.Instance.getComponentsOfType<RectangleComponent>();
            //_listOfCollisions = ComponentManager.Instance.getComponentsOfType<CollisionComponent>();
            //_listOfPositions = ComponentManager.Instance.getComponentsOfType<PositionComponent>();
            _positions = ComponentManager.Instance.getComponentDictionary<PositionComponent>();
            _collisions = ComponentManager.Instance.getComponentDictionary<CollisionComponent>();
            _velocities = ComponentManager.Instance.getComponentDictionary<VelocityComponent>();
            _gameTime = gameTime;
            //TODO Motivate in design-document why this fits more here than in MoveSystem
            PositionUpdate();
            CollisionDetection();
        }


        private void CollisionDetection()
        {
            foreach (var rectangle in _listOfRectangles)
            {
                CollisionComponent cc = (CollisionComponent)_collisions[rectangle.EntityId];

                foreach (var anotherRectangle in _listOfRectangles)
                {
                    if (rectangle.EntityId < anotherRectangle.EntityId &&
                       rectangle.BoundingRectangle.Intersects(anotherRectangle.BoundingRectangle))
                    {
                        PositionUpdateSpheres();

                        var rect1 = rectangle.BoundingSphere.Center;
                        var rect2 = anotherRectangle.BoundingSphere.Center;

                        double distance = Math.Sqrt(
                            ((rect1.X - rect2.X)
                             * (rect1.X - rect2.X))
                            + ((rect1.Y - rect2.Y)
                               * (rect1.Y - rect2.Y)));

                        if (distance < (rectangle.BoundingSphere.Radius + anotherRectangle.BoundingSphere.Radius))
                        {
                            _mediator.sendMessage(new MediatorMessage(rectangle.EntityId, anotherRectangle.EntityId));
                        }
                    }
                }
            }
        }

        private void PositionUpdate()
        {
            foreach (var rectangle in _listOfRectangles)
            {
                PositionComponent pos = (PositionComponent)_positions[rectangle.EntityId];
                Rectangle BoundingRectangle = rectangle.BoundingRectangle;
                BoundingRectangle.X = (int) pos.X;
                BoundingRectangle.Y = (int) pos.Y;
                rectangle.BoundingRectangle = BoundingRectangle;
            }

        }

        private void PositionUpdateSpheres()
        {
            foreach (var rectangle in _listOfRectangles)
            {
                PositionComponent pos = (PositionComponent)_positions[rectangle.EntityId];
                BoundingSphere BoundingSphere = rectangle.BoundingSphere;
                BoundingSphere.Center.X = rectangle.BoundingRectangle.Center.X;
                BoundingSphere.Center.Y = rectangle.BoundingRectangle.Center.Y;
                rectangle.BoundingSphere = BoundingSphere;

            }
        }


    }
}
