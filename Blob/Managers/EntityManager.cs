using System.Collections.Generic;
using Blob.ResourcesProviders;
using GameEngine;
using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace Blob.Managers
{
    public class EntityManager
    {
        public static int createEntity()
        {
            return createEntity("");
        }

        public static int createEntity(string entityType)
        {
            int id = ComponentManager.Instance.newId();
            switch (entityType)
            {
                case "player":
                    Texture2D heroSprite = GameProvider.getInstance().Game.Content.Load<Texture2D>("player");
                    ComponentManager.Instance.addComponent(new PositionComponent(id,
                        GameProvider.getInstance().Game.GraphicsDevice.Viewport.Width/2,
                        GameProvider.getInstance().Game.GraphicsDevice.Viewport.Width/2));
                    ComponentManager.Instance.addComponent(new KeyboardControllComponent(id));
                    ComponentManager.Instance.addComponent(new RotationComponent(id, 0, heroSprite.Width, heroSprite.Height));
                    ComponentManager.Instance.addComponent(new VelocityComponent(id, 0, 0));
                    ComponentManager.Instance.addComponent(new TextureComponent(id, heroSprite));
                    ComponentManager.Instance.addComponent(new ActionDirectionComponent(id));
                    break;
                case "dictator":
                    Texture2D dictatorSprite = GameProvider.getInstance().Game.Content.Load<Texture2D>("dictator");
                    ComponentManager.Instance.addComponent(new VelocityComponent(id, 200, 300));
                    ComponentManager.Instance.addComponent(new PositionComponent(id, 200, 200));
                    ComponentManager.Instance.addComponent(new TextureComponent(id, dictatorSprite));
                    ComponentManager.Instance.addComponent(new RotationComponent(id,0,dictatorSprite.Width,dictatorSprite.Height));
                    break;
                case "alliance":
                    break;
                case "terrorist":
                    break;
                case "explosion":
                    break;
                case "item":
                    break;
                default:
                    break;
            }
            return id;
        }
    }
}
