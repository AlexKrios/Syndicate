using System;
using System.Collections.Generic;

namespace Syndicate.Core.StateMachine
{
    public abstract class AbstractStateMachine
    {
        private readonly Dictionary<Type, IState> _stateMap = new();
        private IState _currentState;

        protected void AddState<TState>(IState state) where TState : IState
        {
            _stateMap.Add(typeof(TState), state);
        }

        private IState GetState<T>() where T : IState
        {
            return _stateMap[typeof(T)];
        }

        public void Enter<TState>() where TState : class, IState
        {
            _currentState?.Exit();
            _currentState = GetState<TState>();
            _currentState.Enter();
        }
    }
}