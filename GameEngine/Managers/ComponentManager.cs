using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Components;

namespace GameEngine.Managers
{
    public class ComponentManager
    {
        private Dictionary<Type, Dictionary<int, EntityComponent>> _componentsByType;
        private Dictionary<int, List<EntityComponent>> _componentsById;
        private Dictionary<Type,Queue<EntityComponent>> _reusableComponents;

        /*Handling of entity-id:s*/
        private static int _idCount;
        private Queue<int> _freeIds;
        private const int _expandSize = 1000;

        private static ComponentManager instance;

        private ComponentManager()
        {
            _componentsByType  = new Dictionary<Type, Dictionary<int, EntityComponent>>();
            _componentsById = new Dictionary<int, List<EntityComponent>>();
            _reusableComponents = new Dictionary<Type, Queue<EntityComponent>>();
            _freeIds = new Queue<int>();
        }

        public static ComponentManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ComponentManager();
                }
                return instance;
            }
        }

        public int newId()
        {
            return getEntityId();
        }

        public T getNewComponent<T>(int id) where T: EntityComponent, new()
        {
            Queue<EntityComponent> queue;
            if (_reusableComponents.TryGetValue(typeof(T), out queue))
            {
                if (queue.Count > 0)
                {
                   T component = (T)queue.Dequeue();
                    component.EntityId = id;
                    return (component);
                }
            }
            T tResult = new T();
            tResult.EntityId = id;
            return tResult;
        }

        public void addComponent(EntityComponent component)   //snabbare än --^
        {

            //TODO skriver över existerande
            //Skriver till "entiteter av typ"-lista
            Dictionary<int, EntityComponent> temp;
            if (!_componentsByType.TryGetValue(component.GetType(), out temp))
            {
                temp = new Dictionary<int, EntityComponent>();
                _componentsByType[component.GetType()] = temp;
            }
            _componentsByType[component.GetType()][component.EntityId] = component;
            
            // Skriver till "componenter för Entitet"-lista
            List<EntityComponent> list;
            if (!_componentsById.TryGetValue(component.EntityId, out list))
            {
                list = new List<EntityComponent>();
                _componentsById[component.EntityId] = list;
            }
            _componentsById[component.EntityId].Add(component);
        }

        public List<T> getComponentsOfType<T>() where T : EntityComponent
        {
            Dictionary<int, EntityComponent> tempDict;
            List<T> list = new List<T>();
            if (_componentsByType.TryGetValue(typeof(T), out tempDict))
            {

                foreach (var item in tempDict.Values)
                {
                    list.Add((T)item);
                }
                return list;
            }
            return null;
        }

        public T getComponentByID<T>(int iD) where T : EntityComponent
        {
            Dictionary<int, EntityComponent> tempDict;
            if (_componentsByType.TryGetValue(typeof(T), out tempDict))
            {
                EntityComponent component;
                if (tempDict.TryGetValue(iD, out component))
                {
                    return (T)component;
                }
            }
            return null;
        }

        public List<EntityComponent> getAllComponentsByID(int iD)
        {
            List<EntityComponent> tempList;
            if (_componentsById.TryGetValue(iD, out tempList))
            {
                return tempList;
            }
            return null;
        }

        public void removeEntity(int entityID)
        {
            List<EntityComponent> listwithComponets;
            if (_componentsById.TryGetValue(entityID, out listwithComponets))
            {
                removeEntityFromComponentDictionary(listwithComponets);
                _componentsById.Remove(entityID);
                _freeIds.Enqueue(entityID);
            }
        }

        private bool removeEntityFromComponentDictionary(List<EntityComponent> listComp)
        {
            foreach (EntityComponent comp in listComp)
            {
                Dictionary<int, EntityComponent> editlist;
                Type type = comp.GetType();
                if (_componentsByType.TryGetValue(type, out editlist))
                {
                    editlist.Remove(comp.EntityId);
                    Queue<EntityComponent> reusableTempList;
                    if (!_reusableComponents.TryGetValue(type, out reusableTempList))
                    {
                        reusableTempList = new Queue<EntityComponent>();
                        _reusableComponents[type] = reusableTempList;
                    }
                    reusableTempList.Enqueue(comp);
                }
                else
                    return false;

            }
            return true;
        }

        public Dictionary<int, EntityComponent> getComponentDictionary<T>() where T : EntityComponent
        {
            Dictionary<int, EntityComponent> compDictionary;
            if (_componentsByType.TryGetValue(typeof(T), out compDictionary))
            {
                return compDictionary;
            }
            return null;
        }

        private int getEntityId()
        {
            if (_freeIds.Count == 0)
                expandIdQueue();
            return _freeIds.Dequeue();
        }

        private void expandIdQueue()
        {
            for (int i = _idCount; i < _idCount + _expandSize; i++)
            {
                _freeIds.Enqueue(i);
            }
            _idCount += _expandSize;
        }
    }
}
