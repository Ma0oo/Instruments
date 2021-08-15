using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plugins.HabObject.CommunicatorParts
{
    public class Communicator
    {
        private Dictionary<Type, object> _dictionaryEvent = new Dictionary<Type, object>();

        public void Fire<TSignal>(TSignal instanceSignal) where TSignal : class
        {
            if(instanceSignal==null)
                throw new Exception("Null reference");
            CheckAndFixIntegrityDictionary<TSignal>();
            GetPublisherByT<TSignal>().Awake(instanceSignal);
        }
        
        public void Track<TSignal>(Action<TSignal> tracker) where TSignal : class
        {
            CheckAndFixIntegrityDictionary<TSignal>();
            GetPublisherByT<TSignal>().AddListen(tracker);
        }

        public void Untrack<TSignal>(Action<TSignal> tracker) where TSignal : class
        {
            CheckAndFixIntegrityDictionary<TSignal>();
            GetPublisherByT<TSignal>().RemoveListen(tracker);
        }

        public async void Clear()
        {
            await Task.Run(() =>
            {
                foreach (var pair in _dictionaryEvent) ((IClearEvent) pair.Value).Clear();
            });
        }

        private Publisher<TSignal> GetPublisherByT<TSignal>() where TSignal : class 
            => (_dictionaryEvent[typeof(TSignal)] as Publisher<TSignal>);

        private bool CheckDictionaryAtHasKeyT<T>() where T : class => _dictionaryEvent.ContainsKey(typeof(T));
        
        private void CreatePublisher<T>() where T : class => _dictionaryEvent.Add(typeof(T), new Publisher<T>());

        private void CheckAndFixIntegrityDictionary<T>() where T : class
        {
            if(!CheckDictionaryAtHasKeyT<T>())
                CreatePublisher<T>();
        }
    }
}