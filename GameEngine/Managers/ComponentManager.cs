﻿using System;
using System.Collections.Generic;
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

        /*Handling of entity-id:s*/
        private static int _idCount;
        private Queue<int> _freeIds;
        private const int _expandSize = 1000;

        private static ComponentManager instance;

        private ComponentManager()
        {
            _componentsByType  = new Dictionary<Type, Dictionary<int, EntityComponent>>();
            _componentsById = new Dictionary<int, List<EntityComponent>>();
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

        public void removeComponent<T>(int iD) where T : EntityComponent
        {
            //private Dictionary<Type, Dictionary<int, Component>> _componentsByType;
            //private Dictionary<int, List<Component>> _componentsById;
            EntityComponent component;
            Dictionary<int, EntityComponent> tempDict;
            if (_componentsByType.TryGetValue(typeof(T), out tempDict))
            {
                if (tempDict.TryGetValue(iD, out component))
                {
                    List<EntityComponent> tempList;
                    if (_componentsById.TryGetValue(iD, out tempList))
                    {
                        tempList.Remove(component);
                        tempDict.Remove(iD);
                    }
                }
            }
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
