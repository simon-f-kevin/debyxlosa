using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Managers
{
    public class SystemManager : DrawableGameComponent
    {
        private CollisionSystem _CollisionSystem;
        private InputSystem _InputSystem;
        private MoveSystem _MoveSystem;
        private FrameCounter _FrameCounter;
        public SystemManager(Game game) : base(game)
        {
        }
        public override void Initialize()
        {
            //_TestSystem = new TestSystem(this.Game);
            //Game.Components.Add(_TestSystem);

            _FrameCounter = new FrameCounter(this.Game);
            Game.Components.Add(_FrameCounter);

            _InputSystem = new InputSystem(this.Game);
            Game.Components.Add(_InputSystem);

            _MoveSystem = new MoveSystem(this.Game);
            Game.Components.Add(_MoveSystem);

            _CollisionSystem = new CollisionSystem(this.Game);
            Game.Components.Add(_CollisionSystem);

            //_TextureRender = new TextureRenderer(this.Game);
            //Game.Components.Add(_TextureRender);

            //_SoundSystem = new SoundSystem(this.Game);
            //Game.Components.Add(_SoundSystem);
            //base.Initialize();
        }
    }
}
