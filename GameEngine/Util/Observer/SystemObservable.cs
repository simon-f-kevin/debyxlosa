using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Util.Observer
{
    public abstract class SystemObservable
    {
        private List<ISystemObserver> _observers;
        public SystemObservable()
        {
            _observers = new List<ISystemObserver>();
        }
        public void addListener(ISystemObserver observer)
        {
            _observers.Add(observer);
        }
        public void removeListener(ISystemObserver observer)
        {
            _observers.Remove(observer);
        }
        public void notify(int id, int animationEffect)
        {
            foreach(ISystemObserver observer in _observers)
            {
                observer.update(id, animationEffect);
            }
        }
    }
    
}
