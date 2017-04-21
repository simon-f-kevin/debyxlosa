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
        //private List<PositionComponent> _listOfPositions;
        private List<RectangleComponent> _listOfRectangles;

        public CollisionSystem()
        {
            
        }

        public void Update()
        {
            CollisionDetection();
            
        }


        private void CollisionDetection()
        {
            _listOfRectangles = ComponentManager.Instance.getComponentsOfType<RectangleComponent>();
            foreach (var rectangle in _listOfRectangles)
            {
                RectangleComponent rectComp = rectangle;
                foreach (var rectangle1 in _listOfRectangles)
                {
                    RectangleComponent rectComp1 = rectangle1;
                    if (rectangle.EntityId != rectangle1.EntityId && rectComp.BoundingRectangle.Intersects(rectComp1.BoundingRectangle))
                    {
                        var tempComp = ComponentManager.Instance.getComponentByID<CollisionComponent>(rectangle.EntityId);
                        tempComp._collisions.Add(rectComp1.EntityId);
                        var tempComp1 = ComponentManager.Instance.getComponentByID<PositionComponent>(tempComp.EntityId);
                        //get the position of the collision
                        //move the player away from position of intersection
                    }

                }

            }
        }
       
    }
}
