using System.Runtime.Remoting.Messaging;

namespace GameEngine.Util
{
    public interface ISystemsMediator
    {
        void sendMessage(MediatorMessage message);
    }
}