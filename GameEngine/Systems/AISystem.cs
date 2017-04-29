using System.Collections.Generic;
using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace GameEngine.Systems
{
    public class AISystem
    {
        private Game game;
        private List<AIComponent> terrorists;
        private List<KeyboardControlComponent> players;

        public Ray ray;


        
        public AISystem(Game game)
        {
            this.game = game;
        }

        public void Update(GameTime _gameTime)
        {
            terrorists = ComponentManager.Instance.getComponentsOfType<AIComponent>();
            players = ComponentManager.Instance.getComponentsOfType<KeyboardControlComponent>();

            RayCalculation(players, terrorists);
            
        }
        private void RayCalculation(List<KeyboardControlComponent> player, List<AIComponent> terrorists)
        {
            var playerPos = (PositionComponent)ComponentManager.Instance.getComponentByID<PositionComponent>(player[0].EntityId);
            var terroristPos = (PositionComponent)ComponentManager.Instance.getComponentByID<PositionComponent>(terrorists[0].EntityId);
            var dist = Vector2.Distance(new Vector2(playerPos.X, playerPos.Y), new Vector2(terroristPos.X, terroristPos.Y));
            var ray = new Ray(new Vector3(terroristPos.X, terroristPos.Y, 0),
                new Vector3(terroristPos.X - playerPos.X, terroristPos.Y - playerPos.Y, 0));
            
            VelocityComponent vCompTerror = ComponentManager.Instance.getNewComponent<VelocityComponent>(terrorists[0].EntityId);
            vCompTerror.VelY = -ray.Direction.Y;
            vCompTerror.VelX = -ray.Direction.X;
            
            if (dist < 150)
            {
                vCompTerror.VelY = 0;
                vCompTerror.VelX = 0;
            }
            ComponentManager.Instance.addComponent(vCompTerror);

            System.Diagnostics.Debug.WriteLine(dist);
        }
    }
}
