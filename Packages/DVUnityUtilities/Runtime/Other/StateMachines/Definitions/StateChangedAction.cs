using System;
using UnityEngine;
using UnityEngine.Events;

namespace DVUnityUtilities.Other.StateMachines.Definitions
{
    [Serializable]
    internal class StateChangedAction
    {
        [SerializeField] public string StateName;
        [SerializeField] public StateChangeStatus OnStateStatus;
        [SerializeField] public UnityEvent ToDo;
    }
}