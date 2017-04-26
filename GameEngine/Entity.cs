using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Components;
using GameEngine.Managers;

namespace GameEngine
{
    public class Entity
    {
        public int _entityID { get; }
        public Entity(int id)
        {
            _entityID = id;
        }

        public void addComponent(EntityComponent component)
        {
            ComponentManager.Instance.addComponent(component);
        }
        public void removeComponent(EntityComponent component)
        {
            //ComponentManager.Instance.removeComponent<(component, component.EntityId);
        }
    }
}
