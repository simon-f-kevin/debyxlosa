using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Components;

namespace GameEngine.Managers
{
    public class ComponentManager
    {
        private Dictionary<Type, Dictionary<int, Component>> _componentsByType;
        private Dictionary<int, List<Component>> _componentsById;

        private static ComponentManager instance;

        private ComponentManager()
        {
            _componentsByType  = new Dictionary<Type, Dictionary<int, Component>>();
            _componentsById = new Dictionary<int, List<Component>>();
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

        public void addComponent(Component component)   //snabbare än --^
        {

            //TODO skriver över existerande
            //Skriver till "entiteter av typ"-lista
            Dictionary<int, Component> temp;
            if (!_componentsByType.TryGetValue(component.GetType(), out temp))
            {
                temp = new Dictionary<int, Component>();
                _componentsByType[component.GetType()] = temp;
            }
            _componentsByType[component.GetType()][component.EntityId] = component;
            
            // Skriver till "componenter för Entitet"-lista
            List<Component> list;
            if (!_componentsById.TryGetValue(component.EntityId, out list))
            {
                list = new List<Component>();
                _componentsById[component.EntityId] = list;
            }
            _componentsById[component.EntityId].Add(component);
        }

        public List<T> getComponentsOfType<T>() where T : Component
        {
            Dictionary<int, Component> tempDict;
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

        public T getComponentByID<T>(int iD) where T : Component
        {
            Dictionary<int, Component> tempDict;
            if (_componentsByType.TryGetValue(typeof(T), out tempDict))
            {
                Component component;
                if (tempDict.TryGetValue(iD, out component))
                {
                    return (T)component;
                }
            }
            return null;
        }

        public void removeComponent<T>(int iD) where T : Component
        {
            //private Dictionary<Type, Dictionary<int, Component>> _componentsByType;
            //private Dictionary<int, List<Component>> _componentsById;
            Component component;
            Dictionary<int, Component> tempDict;
            if (_componentsByType.TryGetValue(typeof(T), out tempDict))
            {
                if (tempDict.TryGetValue(iD, out component))
                {
                    List<Component> tempList;
                    if (_componentsById.TryGetValue(iD, out tempList))
                    {
                        tempList.Remove(component);
                        tempDict.Remove(iD);
                    }
                }
            }
        }
    }
}
