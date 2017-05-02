using System.Collections.Generic;
using Blob.Managers;
using Blob.ResourcesProviders;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Util.Mediator;
using Microsoft.Xna.Framework;

namespace Blob.Models.CollisionHandler
{
    /*
     bounce = 0 => bounces on every collision --- terrorist and player
     merge = 1 => animation for merging into alliance --- dictators
     war = 2 => large explosion animation before sending missiles and new dictators --- alliances
     kill = 3 => if this collisiontype collides with player = death ---explosionAnimations
     missile = 4 => create explosion animation --- missile
         */
    public enum CollisionTypes { Bounce, Merge, War, kill, Explosion}

    public static class GameCollisionHandler
    {
        
        public static void CollisionHandler(List<MediatorMessage> collisions, GameTime gameTime)
        {
            Dictionary<int, EntityComponent> collisionsComponents =
                ComponentManager.Instance.getComponentDictionary<CollisionComponent>();
            Dictionary<int, EntityComponent> positions =
                ComponentManager.Instance.getComponentDictionary<PositionComponent>();
            foreach (MediatorMessage message in collisions)
            {
                //get collision components
                CollisionComponent collisionComponent1 = 
                    (CollisionComponent)collisionsComponents[message._entityId1];
                CollisionComponent collisionComponent2 = 
                    (CollisionComponent)collisionsComponents[message._entityId2];

                //compare collision components to activate handler
                if (((CollisionTypes) collisionComponent1.CollisionType == CollisionTypes.kill &&
                     isPlayer(collisionComponent2.EntityId)) ||
                    ((CollisionTypes) collisionComponent2.CollisionType == CollisionTypes.kill &&
                     isPlayer(collisionComponent1.EntityId)))
                {
                    //set player to dead
                }
                else if (collisionComponent1.CollisionType == collisionComponent2.CollisionType)
                {
                    Point animationCenter = getCollisionCenter(collisionComponent1.EntityId, collisionComponent2.EntityId);
                    
                    if ((CollisionTypes) collisionComponent1.CollisionType == CollisionTypes.Merge)
                    {                                        
                        EntityManager.getInstance().createAnimation(new Vector2(animationCenter.X, animationCenter.Y), "puff",
                            new Point(128, 128), new Point(10, 0), 25, 0, 2f);
                        Vector2 velocity = getAllianceVelocity(collisionComponent1.EntityId, collisionComponent2.EntityId);
                        EntityManager.getInstance().removeEntity(collisionComponent1.EntityId);
                        EntityManager.getInstance().removeEntity(collisionComponent2.EntityId);                    
                        EntityManager.getInstance().createAlliance(new Vector2(animationCenter.X, animationCenter.Y), velocity);
                       
                    }
                    else if ((CollisionTypes) collisionComponent1.CollisionType == CollisionTypes.War)
                    {
                        EntityManager.getInstance().createAnimation(new Vector2(animationCenter.X, animationCenter.Y),"explosion", new Point(50, 50), new Point(6, 6), 75, 1, 6f);
                        EntityManager.getInstance().removeEntity(collisionComponent1.EntityId);
                        EntityManager.getInstance().removeEntity(collisionComponent2.EntityId);
                    }
                }
                else if ((CollisionTypes) collisionComponent1.CollisionType == CollisionTypes.Explosion ||
                         (CollisionTypes) collisionComponent2.CollisionType == CollisionTypes.Explosion)
                {
                    //animate explosion och explosive entity
                }
                else
                {
                    BouncingBalls(message._entityId1, message._entityId2, gameTime);
                }
                
            }
            
        }

        private static Point getCollisionCenter(int EntityId1, int EntityId2)
        {
            RectangleComponent r1 = ComponentManager.Instance.getComponentByID<RectangleComponent>(EntityId1);
            RectangleComponent r2 = ComponentManager.Instance.getComponentByID<RectangleComponent>(EntityId2);

            float cpX = ((r1.BoundingRectangle.X * r2.BoundingSphere.Radius) + 
                (r2.BoundingRectangle.X * r1.BoundingSphere.Radius)) / 
                (r1.BoundingSphere.Radius + r2.BoundingSphere.Radius);
            float cpY = ((r1.BoundingRectangle.Y * r2.BoundingSphere.Radius) + 
                (r2.BoundingRectangle.Y * r1.BoundingSphere.Radius)) / 
                (r1.BoundingSphere.Radius + r2.BoundingSphere.Radius);

            return new Point((int)cpX, (int)cpY);

        }

        private static bool isPlayer(int collisionComponent2EntityId)
        {
            return false;
        }

        /*
         
        */

        public static void BouncingBalls(int entityId1, int entityId2, GameTime gameTime)
        {
            BoundingSphere s1 = ComponentManager.Instance.getComponentByID<RectangleComponent>(entityId1).BoundingSphere;
            BoundingSphere s2 = ComponentManager.Instance.getComponentByID<RectangleComponent>(entityId2).BoundingSphere;
            VelocityComponent v1 = ComponentManager.Instance.getComponentByID<VelocityComponent>(entityId1);
            VelocityComponent v2 = ComponentManager.Instance.getComponentByID<VelocityComponent>(entityId2);
            PositionComponent p1 = ComponentManager.Instance.getComponentByID<PositionComponent>(entityId1);
            PositionComponent p2 = ComponentManager.Instance.getComponentByID<PositionComponent>(entityId2);


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

            //Move away from each other to prevent multiple collisions
            p1.X += (float)(v1.VelX * gameTime.ElapsedGameTime.TotalSeconds);
            p1.Y += (float)(v1.VelY * gameTime.ElapsedGameTime.TotalSeconds);
            p2.X += (float)(v2.VelX * gameTime.ElapsedGameTime.TotalSeconds);
            p2.Y += (float)(v2.VelY * gameTime.ElapsedGameTime.TotalSeconds);

        }

        public static Vector2 getAllianceVelocity(int entityId1, int entityId2)
        {
            Dictionary<int, EntityComponent> velocities = ComponentManager.Instance.getComponentDictionary<VelocityComponent>();
            VelocityComponent vel1 = (VelocityComponent)velocities[entityId1];
            VelocityComponent vel2 = (VelocityComponent)velocities[entityId2];

                    
            float velXResult = vel1.VelX - vel2.VelX;
            float velYResult = vel1.VelY - vel2.VelY;
            return (new Vector2(velXResult, velYResult));

        }
    }
}