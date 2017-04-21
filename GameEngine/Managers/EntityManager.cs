using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Managers
{
    public class EntityManager
    {
        private Dictionary<int, Entity> _enteties;
        private static int _uniqueID = 0;

        private static EntityManager instance;

        private EntityManager()
        {
            _enteties = new Dictionary<int, Entity>();
        }

        public static EntityManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EntityManager();
                }
                return instance;
            }
        }

        public Entity createUniqueId()
        {
            var tempId = _uniqueID++;
            _enteties[tempId] = new Entity(tempId);

            return _enteties[tempId];
        }
    }
}
