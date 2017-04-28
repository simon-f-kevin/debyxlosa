using System.Collections.Generic;
using Blob.Models;
using Blob.ResourcesProviders;
using GameEngine;
using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blob.Managers
{
    public class EntityManager
    {
        public static int createEntity()
        {
            return ComponentManager.Instance.newId();
        }

     

        public static int createPlayer(Vector2 position, Vector2 velocity, KeyMappings keys)
        {
            int id = ComponentManager.Instance.newId();
            Texture2D heroSprite = GameProvider.getInstance().Game.Content.Load<Texture2D>("player");
            PositionComponent posComp = ComponentManager.Instance.getNewComponent<PositionComponent>(id);
            posComp.Y = position.Y;
            posComp.X = position.X;
            ComponentManager.Instance.addComponent(posComp);
            KeyboardControlComponent kcComp =
                ComponentManager.Instance.getNewComponent<KeyboardControlComponent>(id);
            kcComp.UpKey = keys.UpKey;
            kcComp.DownKey = keys.DownKey;
            kcComp.LeftKey = keys.LeftKey;
            kcComp.RightKey = keys.RightKey;
            kcComp.SpecialKey = keys.SpecialKey;
            ComponentManager.Instance.addComponent(kcComp);
            RotationComponent rComp = ComponentManager.Instance.getNewComponent<RotationComponent>(id);
            rComp.Orgin = new Vector2(heroSprite.Width / 2, heroSprite.Height / 2); ;
            rComp.Rotation = 0;
            ComponentManager.Instance.addComponent(rComp);
            VelocityComponent vComp = ComponentManager.Instance.getNewComponent<VelocityComponent>(id);
            vComp.VelY = velocity.Y;
            vComp.VelX = velocity.X;
            ComponentManager.Instance.addComponent(vComp);
            TextureComponent tComp = ComponentManager.Instance.getNewComponent<TextureComponent>(id);
            tComp.setValue(heroSprite);
            ComponentManager.Instance.addComponent(tComp);
            ActionDirectionComponent adComp =
                ComponentManager.Instance.getNewComponent<ActionDirectionComponent>(id);
            ComponentManager.Instance.addComponent(adComp);
            RectangleComponent rec = ComponentManager.Instance.getNewComponent<RectangleComponent>(id);
            rec.BoundingRectangle = new Rectangle((int)position.X-heroSprite.Width/2, (int)position.Y-heroSprite.Height/2, heroSprite.Width,heroSprite.Height);
            rec.BoundingSphere = new BoundingSphere(new Vector3(rec.BoundingRectangle.Center.X, rec.BoundingRectangle.Center.Y, 0), heroSprite.Width/2);
            ComponentManager.Instance.addComponent(rec);
            ComponentManager.Instance.addComponent(ComponentManager.Instance.getNewComponent<CollisionComponent>(id));
            return id;
        }

        public static int createDictator(Vector2 position, Vector2 velocity)
        {
            int id = ComponentManager.Instance.newId();
            Texture2D dictatorSprite = GameProvider.getInstance().Game.Content.Load<Texture2D>("dictator");
            VelocityComponent vCompDictator = ComponentManager.Instance.getNewComponent<VelocityComponent>(id);
            vCompDictator.VelY = velocity.Y;
            vCompDictator.VelX = velocity.X;
            ComponentManager.Instance.addComponent(vCompDictator);
            PositionComponent posCompDictator = ComponentManager.Instance.getNewComponent<PositionComponent>(id);
            posCompDictator.Y = position.Y;
            posCompDictator.X = position.X;
            ComponentManager.Instance.addComponent(posCompDictator);
            TextureComponent tCompDictator = ComponentManager.Instance.getNewComponent<TextureComponent>(id);
            tCompDictator.setValue(dictatorSprite);
            ComponentManager.Instance.addComponent(tCompDictator);
            RotationComponent rCompDictator = ComponentManager.Instance.getNewComponent<RotationComponent>(id);
            rCompDictator.Orgin = new Vector2(dictatorSprite.Width/2, dictatorSprite.Height/2);
            rCompDictator.Rotation = 0;
            ComponentManager.Instance.addComponent(rCompDictator);
            RectangleComponent rec = ComponentManager.Instance.getNewComponent<RectangleComponent>(id);
            rec.BoundingRectangle = new Rectangle((int)position.X-dictatorSprite.Width/2, (int)position.Y-dictatorSprite.Height/2, dictatorSprite.Width, dictatorSprite.Height);
            rec.BoundingSphere = new BoundingSphere(new Vector3(rec.BoundingRectangle.Center.X, rec.BoundingRectangle.Center.Y, 0), dictatorSprite.Width / 2);
            ComponentManager.Instance.addComponent(rec);
            ComponentManager.Instance.addComponent(ComponentManager.Instance.getNewComponent<CollisionComponent>(id));
            return id;
        }
    }
}
