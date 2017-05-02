using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components
{
    public class CollisionComponent : EntityComponent
    {
        public int CollisionType { get; set; }

        public CollisionComponent()
        {
            CollisionType = 0;
        }

        public CollisionComponent(int id) : base(id)
        {
            CollisionType = 0;
        }
    }
}
