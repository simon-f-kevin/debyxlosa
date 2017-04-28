using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components
{
    public class VelocityComponent : EntityComponent
    {
        public float VelX { get; set; }
        public float VelY  { get; set; }

        public VelocityComponent() { }
        public VelocityComponent(int id) : base(id)
        {
            
        } 
        public VelocityComponent(int Id, float x, float y) : base(Id)
        {
            VelX = x;
            VelY = y;
        }

        public void setValues(float x, float y)
        {
            VelX = x;
            VelY = y;
        }
    }
}
