using DVUnityUtilities.Other.StateMachines.Events;
using System;
using System.Collections.Generic;

namespace DVUnityUtilities.Other.StateMachines
{
    public class DefaultStateMachine : IDisposable
    {
        public event Action<StateChangeEventArgs> OnStateChanged;
        public event Action<DefaultStateMachine> Disposed;

        public string Name { get; private set; }
        public bool IsActive { get; private set; }

        private string _activeState;
        private List<string> _states = new();

        public DefaultStateMachine(string name)
        {
            Name = name;
        }

        public void Init()
        {
            if (!IsActive)
            {
                IsActive = true;
                DefaultStateMachineManager.AddToCache(this);
            }
        }

        public bool RegisterState(string state)
        {
            ErrorIfNotActive();

            if (string.IsNullOrEmpty(state) || _states.Contains(state))
            {
                return false;
            }
            else
            {
                _states.Add(state);
                return true;
            }
        }

        public string GetState()
        {
            return _activeState;
        }

        public bool IsState(string state)
        {
            return GetState() == state;
        }

        public bool SetState(string state, params object[] args)
        {
            ErrorIfNotActive();

            if (IsState(state) || !IsRegisteredState(state))
            {
                return false;
            }
            else
            {
                var oldState = state;
                _activeState = state;

                OnStateChanged?.Invoke(new StateChangeEventArgs() { StateMachine = this, NewState = state, OldState = oldState, Args = args });
                return true;
            }
        }

        private bool IsRegisteredState(string state)
        {
            return _states.Contains(state);
        }

        private void ErrorIfNotActive()
        {
            if (!IsActive) throw new Exception($"{Name} not active. Use {nameof(Init)}()");
        }

        public void RemoveState(string state)
        {
            _states.Remove(state);
        }

        public void Dispose()
        {
            _states.Clear();
            OnStateChanged = null;

            Disposed?.Invoke(this);
            Disposed = null;
            IsActive = false;

            DefaultStateMachineManager.RemoveFromCache(this);
        }
    }
}