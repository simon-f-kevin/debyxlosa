using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components
{
    public class CollisionComponent : EntityComponent
    {
        public List<int> Collisions { get; set; }

        public CollisionComponent()
        {
            Collisions = new List<int>();
        }
        public CollisionComponent(int id) : base(id)
        {
            Collisions = new List<int>();
        }
    }
}
