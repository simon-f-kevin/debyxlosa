using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components
{
    public class AIComponent : EntityComponent
    {
        public List<int> AIComponents { get; set; }

        public AIComponent()
        {
            AIComponents = new List<int>();
        }
        public AIComponent(int id) : base(id)
        {
            AIComponents = new List<int>();
        }
    }
}
