using System;

namespace Plugins.HabObject.CommunicatorParts
{
    internal class Publisher<T> : IClearEvent where T : class
    {
        private event Action<T> _action;

        public void AddListen(Action<T> action) => _action += action;

        public void RemoveListen(Action<T> action) => _action -= action;

        public void Awake(T payLoad) => _action?.Invoke(payLoad);

        public void Clear() => _action = (Action<T>)Delegate.RemoveAll(_action, _action);
    }
}