using DVUnityUtilities.Other.StateMachines.Callbacks;
using DVUnityUtilities.Other.StateMachines.Definitions;
using System.Collections.Generic;
using UnityEngine;

namespace DVUnityUtilities.Other.StateMachines.Wrappers
{
    internal class StateMachineFunctions : MonoBehaviour
    {
        [SerializeField] private DefaultStateMachineMono _stateMachine;
        [Header("/// STATE CHANGES HANDLE ///")]
        [SerializeField] public List<StateChangedAction> StateActions = new();

        private DefaultStateMachine _stateMachineDefault;

        private void Awake()
        {
            if (_stateMachine)
            {
                _stateMachineDefault = _stateMachine.GetInternalStateMachine();
            }
            else
            {
                enabled = false;
            }
        }

        private void OnValidate()
        {
            _stateMachine ??= GetComponent<DefaultStateMachineMono>();
        }

        public void ExecuteAllStateActions()
        {
            CallbackHelper.ExecuteAllStateActions(StateActions, _stateMachineDefault);
        }
    }
}