using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components
{
    public class BarComponent : EntityComponent
    {
        public float seconds;
        public float bar;
        public float span { get; set; }
        public BarComponent()
        {

        }
        public BarComponent(int id):base(id)
        {

        }
    }
}
