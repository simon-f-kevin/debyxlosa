using System;
using System.Collections.Generic;
using Blob.Models;
using Blob.ResourcesProviders;
using GameEngine;
using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Util.Observer;
using Blob.Models.CollisionHandler;

namespace Blob.Managers
{
    public class EntityManager : ISystemObserver
    {
        private static Dictionary<string, Texture2D> _TextureDict;
        private static EntityManager entityManager;
        public static EntityManager getInstance()
        {
            if (entityManager == null)
            {
                entityManager = new EntityManager();
            }
            return entityManager;
        }
        private EntityManager()
        {
            _TextureDict = new Dictionary<string, Texture2D>();
        }
        public int createEntity()
        {
            return ComponentManager.Instance.newId();
        }
        public void update(int id, int animationEffect)
        {
            switch (animationEffect)
            {
                case 0:
                    removeEntity(id);
                    break;
                case 1:
                    removeEntity(id);
                    break;


            }
            
        }
        public void addTexture(string name, Texture2D texture)
        {
            if(_TextureDict == null)
            {
                _TextureDict = new Dictionary<string, Texture2D>();
            }
            _TextureDict.Add(name, texture);
        }

        public int createPlayer(Vector2 position, Vector2 velocity, KeyMappings keys)
        {
            Texture2D heroSprite;
            if (!_TextureDict.TryGetValue("player", out heroSprite))
            {
                return -1;// if error
            };
            int id = ComponentManager.Instance.newId();           
            //Texture2D heroSprite = GameProvider.getInstance().Game.Content.Load<Texture2D>("player");
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
            BarComponent barComp = ComponentManager.Instance.getNewComponent<BarComponent>(id);
            barComp.bar = 1f;
            barComp.span = 5.0f;
            ComponentManager.Instance.addComponent(barComp);

            RectangleComponent rec = ComponentManager.Instance.getNewComponent<RectangleComponent>(id);
            rec.BoundingRectangle = new Rectangle((int)position.X, (int)position.Y, heroSprite.Width,heroSprite.Height);
            rec.BoundingSphere = new BoundingSphere(new Vector3(rec.BoundingRectangle.Center.X, rec.BoundingRectangle.Center.Y, 0), heroSprite.Width/2);
            ComponentManager.Instance.addComponent(rec);
            CollisionComponent cp = ComponentManager.Instance.getNewComponent<CollisionComponent>(id);
            cp.CollisionType = 0;
            ComponentManager.Instance.addComponent(cp);
            
            return id;
        }

        public int createDictator(Vector2 position, Vector2 velocity)
        {
            Texture2D dictatorSprite;
            if (!_TextureDict.TryGetValue("dictator", out dictatorSprite))
            {
                return -1; // if error
            };
            int id = ComponentManager.Instance.newId();
            //Texture2D dictatorSprite = GameProvider.getInstance().Game.Content.Load<Texture2D>("dictator");

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
            rec.BoundingRectangle = new Rectangle((int)position.X, (int)position.Y, dictatorSprite.Width, dictatorSprite.Height);
            rec.BoundingSphere = new BoundingSphere(new Vector3(rec.BoundingRectangle.Center.X, rec.BoundingRectangle.Center.Y, 0), dictatorSprite.Width / 2);
            ComponentManager.Instance.addComponent(rec);
            CollisionComponent cp = ComponentManager.Instance.getNewComponent<CollisionComponent>(id);
            cp.CollisionType = 1;
            ComponentManager.Instance.addComponent(cp);
            return id;
        }
        public int createAlliance(Vector2 position, Vector2 velocity)
        {
            Texture2D dictatorSprite;
            if (!_TextureDict.TryGetValue("alliance", out dictatorSprite))
            {
                return -1;// if error
            };
            int id = ComponentManager.Instance.newId();
            //Texture2D dictatorSprite = GameProvider.getInstance().Game.Content.Load<Texture2D>("football");
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
            rCompDictator.Orgin = new Vector2(dictatorSprite.Width / 2, dictatorSprite.Height / 2);
            rCompDictator.Rotation = 0;
            ComponentManager.Instance.addComponent(rCompDictator);
            RectangleComponent rec = ComponentManager.Instance.getNewComponent<RectangleComponent>(id);
            rec.BoundingRectangle = new Rectangle((int)position.X, (int)position.Y, dictatorSprite.Width, dictatorSprite.Height);
            rec.BoundingSphere = new BoundingSphere(new Vector3(rec.BoundingRectangle.Center.X, rec.BoundingRectangle.Center.Y, 0), dictatorSprite.Width / 2);
            ComponentManager.Instance.addComponent(rec);
            CollisionComponent collisionComponent = ComponentManager.Instance.getNewComponent<CollisionComponent>(id);
            collisionComponent.CollisionType = (int)CollisionTypes.War;
            ComponentManager.Instance.addComponent(collisionComponent);

            return id;
        }

        public int createTerrorist(Vector2 position, Vector2 velocity)
        {
            Texture2D terroristSprite;
            if (!_TextureDict.TryGetValue("terrorist", out terroristSprite))
            {
                return -1;// if error
            };
            int id = ComponentManager.Instance.newId();
            //Texture2D terroristSprite = GameProvider.getInstance().Game.Content.Load<Texture2D>("terrorist");

            VelocityComponent vCompTerror = ComponentManager.Instance.getNewComponent<VelocityComponent>(id);
            vCompTerror.VelY = 0;
            vCompTerror.VelX = 0;
            ComponentManager.Instance.addComponent(vCompTerror);

            PositionComponent posCompTerror = ComponentManager.Instance.getNewComponent<PositionComponent>(id);
            posCompTerror.Y = position.Y;
            posCompTerror.X = position.X;
            ComponentManager.Instance.addComponent(posCompTerror);

            TextureComponent tCompDTerror = ComponentManager.Instance.getNewComponent<TextureComponent>(id);
            tCompDTerror.setValue(terroristSprite);
            ComponentManager.Instance.addComponent(tCompDTerror);

            RotationComponent rCompTerror = ComponentManager.Instance.getNewComponent<RotationComponent>(id);
            rCompTerror.Orgin = new Vector2(terroristSprite.Width / 2, terroristSprite.Height / 2);
            rCompTerror.Rotation = 0;
            ComponentManager.Instance.addComponent(rCompTerror);

            RectangleComponent rec = ComponentManager.Instance.getNewComponent<RectangleComponent>(id);
            rec.BoundingRectangle = new Rectangle((int)position.X - terroristSprite.Width / 2, (int)position.Y - terroristSprite.Height / 2, terroristSprite.Width, terroristSprite.Height);
            rec.BoundingSphere = new BoundingSphere(new Vector3(rec.BoundingRectangle.Center.X, rec.BoundingRectangle.Center.Y, 0), terroristSprite.Width / 2);
            ComponentManager.Instance.addComponent(rec);

            CollisionComponent collisionComponent = ComponentManager.Instance.getNewComponent<CollisionComponent>(id);
            collisionComponent.CollisionType = (int)CollisionTypes.kill;
            ComponentManager.Instance.addComponent(collisionComponent);

            ComponentManager.Instance.addComponent(ComponentManager.Instance.getNewComponent<AIComponent>(id));

            return id;
        }

        //public static int createAnimatedDictator(Vector2 position, Vector2 velocity)
        //{
        //    Texture2D dictatorSprite;
        //    if (!_TextureDict.TryGetValue("smileyWalk", out dictatorSprite))
        //    {
        //        return -1; // if error
        //    };
        //    int id = ComponentManager.Instance.newId();
        //    //Texture2D dictatorSprite = GameProvider.getInstance().Game.Content.Load<Texture2D>("dictator");

        //    VelocityComponent vCompDictator = ComponentManager.Instance.getNewComponent<VelocityComponent>(id);
        //    vCompDictator.VelY = velocity.Y;
        //    vCompDictator.VelX = velocity.X;
        //    ComponentManager.Instance.addComponent(vCompDictator);

        //    PositionComponent posCompDictator = ComponentManager.Instance.getNewComponent<PositionComponent>(id);
        //    posCompDictator.Y = position.Y;
        //    posCompDictator.X = position.X;
        //    ComponentManager.Instance.addComponent(posCompDictator);

        //    RotationComponent rCompDictator = ComponentManager.Instance.getNewComponent<RotationComponent>(id);
        //    rCompDictator.Orgin = new Vector2(dictatorSprite.Width / 2, dictatorSprite.Height / 2);
        //    rCompDictator.Rotation = 0;
        //    ComponentManager.Instance.addComponent(rCompDictator);

        //    RectangleComponent rec = ComponentManager.Instance.getNewComponent<RectangleComponent>(id);
        //    rec.BoundingRectangle = new Rectangle((int)position.X - dictatorSprite.Width / 2, (int)position.Y - dictatorSprite.Height / 2, dictatorSprite.Width, dictatorSprite.Height);
        //    rec.BoundingSphere = new BoundingSphere(new Vector3(rec.BoundingRectangle.Center.X, rec.BoundingRectangle.Center.Y, 0), dictatorSprite.Width / 2);
        //    ComponentManager.Instance.addComponent(rec);

        //    ComponentManager.Instance.addComponent(ComponentManager.Instance.getNewComponent<CollisionComponent>(id));
        //    AnimationComponent animationComp = ComponentManager.Instance.getNewComponent<AnimationComponent>(id);
        //    animationComp.Texture = dictatorSprite;
        //    animationComp.Rows = 4;
        //    animationComp.Columns = 6;
        //    animationComp.totalFrames = animationComp.Rows * animationComp.Columns;
        //    ComponentManager.Instance.addComponent(animationComp);

        //    return id;
        //}

        public int createAnimation(Vector2 position, string spriteSheet, Point frameSize, Point sheetSize,
            int millisecondsPerFrame, int animationEffect, float scale)
        {
            int id = ComponentManager.Instance.newId();
            Texture2D animation = GameProvider.getInstance().Game.Content.Load<Texture2D>(spriteSheet);

            PositionComponent pos = ComponentManager.Instance.getNewComponent<PositionComponent>(id);
            pos.Y = position.Y;
            pos.X = position.X;
            ComponentManager.Instance.addComponent(pos);
            AnimationComponent ac = ComponentManager.Instance.getNewComponent<AnimationComponent>(id);
            ac.setValues(animation, frameSize, sheetSize, millisecondsPerFrame, animationEffect);
            ac.addListener(this);
            ComponentManager.Instance.addComponent(ac);
            ScaleComponent scalecomp = ComponentManager.Instance.getNewComponent<ScaleComponent>(id);
            scalecomp.scale = scale;
            ComponentManager.Instance.addComponent(scalecomp);

            return id;
        }

        public void removeEntity(int entityId)
        {
            ComponentManager.Instance.removeEntity(entityId);
        }
    }
}

/*
 timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }
                ++currentFrameRun.X;
                if (currentFrameRun.X >= sheetSizeRun.X)
                {
                    currentFrameRun.X = 0;
                    ++currentFrameRun.Y;
                    if (currentFrameRun.Y >= sheetSizeRun.Y)
                        currentFrameRun.Y = 0;
                }
            }*/
