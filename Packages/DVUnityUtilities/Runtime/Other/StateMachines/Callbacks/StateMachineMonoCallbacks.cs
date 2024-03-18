using DVUnityUtilities.Other.StateMachines.Definitions;
using DVUnityUtilities.Other.StateMachines.Events;
using System.Collections.Generic;
using UnityEngine;

namespace DVUnityUtilities.Other.StateMachines.Callbacks
{
    internal class StateMachineMonoCallbacks : MonoBehaviour
    {
        [SerializeField] private string _stateMachineName;
        [Header("/// STATE CHANGES HANDLE ///")]
        [SerializeField] public List<StateChangedAction> StateActions = new();

        private DefaultStateMachine _stateMachine;

        private void Awake()
        {
            if (!DefaultStateMachineManager.IsHaveStateMachine(_stateMachineName, out _stateMachine))
            {
                Debug.LogError($"{_stateMachineName} not found.");
                return;
            }
            else
            {
                _stateMachine.OnStateChanged += OnStateChanged;
                InitPrewarm();
            }
        }

        private void InitPrewarm()
        {
            CallbackHelper.ExecuteAllStateActions(StateActions, _stateMachine);
        }

        private void OnStateChanged(StateChangeEventArgs args)
        {
            CallbackHelper.HandleStateChanged(args, StateActions);
        }
    }
}