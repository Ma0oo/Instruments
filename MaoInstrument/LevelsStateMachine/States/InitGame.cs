using System.Collections.Generic;
using Plugins.DIContainer;
using Plugins.GameStateMachines.Interfaces;
using Plugins.Interfaces;
using Plugins.Sound;
using Object = System.Object;

namespace Plugins.GameStateMachines.States
{
    public class InitGame : IEnterState
    {
        private DiBox _diBox = DiBox.MainBox;

        [DI] private LevelStateMachine _levelStateMachine;
        [DI] private ICoroutineRunner _coroutineRuuner;
        [DI] private ICurtain _curtain;

        public void Enter()
        {
            CreateSignal();
            CreateDi();
            //_curtain.Fade(()=>_gameStateMachine.Enter<MainMenu>());
        }

        public void Exit()
        {
            
        }

        private void CreateSignal()
        {
            
        }


        private void CreateDi()
        {
            
        }

        private void Inject(List<object> _objectToInjectDI)
        {
            foreach (var @object in _objectToInjectDI) _diBox.InjectSingle(@object);
        }

        private T CreateDI<T>() where T : class, new()
        {
            T result = new T();
            _diBox.RegisterSingle<T>(result);
            return result;
        }
    }
}