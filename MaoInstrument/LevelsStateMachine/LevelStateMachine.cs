using System;
using System.Collections.Generic;
using Plugins.DIContainer;
using Plugins.GameStateMachines.Interfaces;

namespace Plugins.GameStateMachines
{
    public class LevelStateMachine
    {
        private IExitableState _currentState;

        private Dictionary<Type, IExitableState> _dictStates = new Dictionary<Type, IExitableState>();
        
        public void Enter<T>() where T : class, IEnterState, new() => ChangeState<T>().Enter();

        public void Enter<T, PayLoaded>(PayLoaded loadedPay) where T : class, IPayLoadedState<PayLoaded>, new() => ChangeState<T>().Enter(loadedPay);

        private T ChangeState<T>() where T : class, IExitableState, new()
        {
            if(_currentState!=null)
                _currentState.Exit();
            T nextState = GetStateFromDict<T>();
            _currentState = nextState;
            return nextState;
        }

        private T GetStateFromDict<T>() where T : class, IExitableState, new()
        {
            if (_dictStates.ContainsKey(typeof(T)))
            {
                return _dictStates[typeof(T)] as T;
            }
            else
            {
                T createdState = new T();
                DiBox.MainBox.InjectSingle(createdState);
                _dictStates.Add(typeof(T), createdState);
                return _dictStates[typeof(T)] as T;
            }
        }
    }
}