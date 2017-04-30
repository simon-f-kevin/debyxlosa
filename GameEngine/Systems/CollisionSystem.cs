using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;

namespace GameEngine.Systems
{
    public class CollisionSystem
    {
        private List<PositionComponent> _listOfPositions;
        private List<RectangleComponent> _listOfRectangles;
        private List<CollisionComponent> _listOfCollisions;
        private Dictionary<int, EntityComponent> _velocities;
        private GameTime _gameTime;

        private bool _collisionDetected;

        public CollisionSystem()
        {
            _collisionDetected = false;
        }

        public void Update(GameTime gameTime)
        {
            _listOfRectangles = ComponentManager.Instance.getComponentsOfType<RectangleComponent>();
            _listOfCollisions = ComponentManager.Instance.getComponentsOfType<CollisionComponent>();
            _listOfPositions = ComponentManager.Instance.getComponentsOfType<PositionComponent>();
            _velocities = ComponentManager.Instance.getComponentDictionary<VelocityComponent>();
            _gameTime = gameTime;
            PositionUpdate();
            CollisionDetection();
            if (_collisionDetected)
                CollisionHandler();

        }


        private void CollisionDetection()
        {
            foreach (var rectangle in _listOfRectangles)
            {
                CollisionComponent cc =
                    _listOfCollisions.SingleOrDefault(e => e.EntityId == rectangle.EntityId);

                foreach (var anotherRectangle in _listOfRectangles)
                {
                    if (rectangle.EntityId != anotherRectangle.EntityId &&
                       rectangle.BoundingRectangle.Intersects(anotherRectangle.BoundingRectangle))
                    {
                        PositionUpdateSpheres();

                        //Räkna ut distans med Pythagoras sats
                        //                        double distance = Math.Sqrt(
                        //                            ((rectangle.BoundingRectangle.X - anotherRectangle.BoundingRectangle.X)
                        //                             * (rectangle.BoundingRectangle.X - anotherRectangle.BoundingRectangle.X))
                        //                            + ((rectangle.BoundingRectangle.Y - anotherRectangle.BoundingRectangle.Y)
                        //                               * (rectangle.BoundingRectangle.Y - anotherRectangle.BoundingRectangle.Y)));

                        var rect1 = rectangle.BoundingSphere.Center;
                        var rect2 = anotherRectangle.BoundingSphere.Center;

                        double distance2 = Math.Sqrt(
                            ((rect1.X - rect2.X)
                             * (rect1.X - rect2.X))
                            + ((rect1.Y - rect2.Y)
                               * (rect1.Y - rect2.Y)));

                        if (distance2 < ((rectangle.BoundingSphere.Radius) + (anotherRectangle.BoundingSphere.Radius)))
                        {
                            cc.Collisions.Add(anotherRectangle.EntityId);
                            _collisionDetected = true;
                        }
                    }
                }
            }
        }

        /*When collision is detected prevent overlapping entities by moving entites back one step.
         Both position and boundingrectangle is modified*/


        private void PositionUpdate()
        {
            foreach (var rectangle in _listOfRectangles)
            {
                PositionComponent pos =
                    _listOfPositions.SingleOrDefault(e => e.EntityId == rectangle.EntityId);
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
                PositionComponent pos =
                    ComponentManager.Instance.getComponentByID<PositionComponent>(rectangle.EntityId);
                RotationComponent rot =
                    ComponentManager.Instance.getComponentByID<RotationComponent>(rectangle.EntityId);

                var radius = rectangle.BoundingSphere.Radius;
                BoundingSphere BoundingSphere = rectangle.BoundingSphere;
                BoundingSphere.Center.X = rectangle.BoundingRectangle.Center.X;
                BoundingSphere.Center.Y = rectangle.BoundingRectangle.Center.Y;
                //new Vector3(pos.X + (float)Math.Cos(rot.Rotation) * radius,
                //    pos.Y + (float)Math.Sin(rot.Rotation) * radius, 0);
                //BoundingSphere.Center = new Vector3(rectangle.BoundingRectangle.X + radius, rectangle.BoundingRectangle.Y + radius, 0);
                rectangle.BoundingSphere = BoundingSphere;
            }
        }

        // Skall nog flyttas ut i game
        private void CollisionHandler()
        {
            foreach (CollisionComponent component in _listOfCollisions)
            {
                if (component.Collisions.Count > 0)
                {
                    for (int i = 0; i < component.Collisions.Count; i++)
                    {
                        int id = component.Collisions[i];

                        //GÖR DET DU SKALL GÖRA VID KOLLISION//
                        if (id > component.EntityId)
                        {
                            BouncingBalls(id, component);
                        }
                            

                    }
                    component.Collisions.Clear();
                }
            }
            _collisionDetected = false;
        }

        // Skall nog flyttas ut i game och fysik
        public void BouncingBalls(int id, CollisionComponent component)
        {
            BoundingSphere s1 = ComponentManager.Instance.getComponentByID<RectangleComponent>(id).BoundingSphere;
            BoundingSphere s2 = ComponentManager.Instance.getComponentByID<RectangleComponent>(component.EntityId).BoundingSphere;
            VelocityComponent v1 = ComponentManager.Instance.getComponentByID<VelocityComponent>(id);
            VelocityComponent v2 = ComponentManager.Instance.getComponentByID<VelocityComponent>(component.EntityId);

            //Ändra riktning och hastighet
            //jag räknar radien som massan för entiteterna
            float velX1 = (v1.VelX * (s1.Radius - s2.Radius) + (2 * s2.Radius * v2.VelX)) / (s1.Radius + s2.Radius);
            float velY1 = (v1.VelY * (s1.Radius - s2.Radius) + (2 * s2.Radius * v2.VelY)) / (s1.Radius + s2.Radius);
            float velX2 = (v2.VelX * (s2.Radius - s1.Radius) + (2 * s1.Radius * v1.VelX)) / (s1.Radius + s2.Radius);
            float velY2 = (v2.VelY * (s2.Radius - s1.Radius) + (2 * s1.Radius * v1.VelY)) / (s1.Radius + s2.Radius);

            //Kollisionspunkt
            //var cpX = ((r1.X * (r2.Width / 2)) + (r2.X * (r1.Width / 2))) / (r1.Width / 2 + r2.Width / 2);
            //var cpY = ((r1.Y * (r2.Width / 2)) + (r2.Y * (r1.Width / 2))) / (r1.Width / 2 + r2.Width / 2);

            v1.VelX = velX1;
            v1.VelY = velY1;
            v2.VelX = velX2;
            v2.VelY = velY2;

        }
    }
}
