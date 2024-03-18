using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DVUnityUtilities.Other.StateMachines.Wrappers
{
    [DefaultExecutionOrder(-1000)]
    public class DefaultStateMachineMono : MonoBehaviour
    {
        [SerializeField] private bool _activeFirstState = true;
        [SerializeField] private List<string> _registerStatesOnAwake = new();
        private DefaultStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new DefaultStateMachine(name);
            _stateMachine.Init();
            _registerStatesOnAwake.ForEach(state => _stateMachine.RegisterState(state));

            if (_activeFirstState && _registerStatesOnAwake.Count > 0)
            {
                _stateMachine.SetState(_registerStatesOnAwake.First());
            }
        }

        public DefaultStateMachine GetInternalStateMachine()
        {
            return _stateMachine;
        }

        private void OnDestroy()
        {
            _stateMachine?.Dispose();
        }
    }
}