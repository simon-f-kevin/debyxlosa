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
        private const float speed = 0.00005f;


        public AISystem(Game game)
        {
            this.game = game;
        }

        public void Update(GameTime _gameTime)
        {
            terrorists = ComponentManager.Instance.getComponentsOfType<AIComponent>();
            players = ComponentManager.Instance.getComponentsOfType<KeyboardControlComponent>();

            AICalc(players, terrorists);
            
        }
        private void AICalc(List<KeyboardControlComponent> player, List<AIComponent> terrorists)
        {
            var playerPos = ComponentManager.Instance.getComponentByID<PositionComponent>(player[0].EntityId);
            var terroristPos = ComponentManager.Instance.getComponentByID<PositionComponent>(terrorists[0].EntityId);

            var dist = Vector2.Distance(new Vector2(playerPos.X, playerPos.Y), new Vector2(terroristPos.X, terroristPos.Y));
            var dir = new Vector2(terroristPos.X, terroristPos.Y) - (new Vector2(playerPos.X, playerPos.Y));
            

            VelocityComponent vCompTerror = ComponentManager.Instance.getComponentByID<VelocityComponent>(terrorists[0].EntityId);
            if (dist > 150)
            {
                vCompTerror.VelY += MathHelper.Clamp(-dir.Y , -1, 1);
                vCompTerror.VelX += MathHelper.Clamp(-dir.X, -1, 1);
            }
            System.Diagnostics.Debug.WriteLine(vCompTerror.VelX);
        }
    }
}
