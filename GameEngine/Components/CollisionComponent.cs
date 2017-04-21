using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components
{
    public class CollisionComponent : Component
    {
        public List<int> _collisions { get; set; }
        public CollisionComponent(int id) : base(id)
        {
            _collisions = new List<int>();
        }
    }
}
