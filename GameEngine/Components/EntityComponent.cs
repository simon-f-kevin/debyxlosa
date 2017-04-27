using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components
{
    public abstract class EntityComponent
    {
        private int _entityId;

        public EntityComponent(int Id)
        {
            _entityId = Id;
        }

        public int EntityId
        {
            get { return _entityId; }
            set { _entityId = value; }
        }
    }
}
