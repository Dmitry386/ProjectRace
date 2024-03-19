using Assets.Scripts.World.Locations;
using DVUnityUtilities.Other.StateMachines;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.States
{
    internal class StateMachineUpdator : MonoBehaviour
    {
        [Inject] private LocationSystem _locationSystem;

        private void Update()
        {
            UpdateGameStateMacine();
        }

        private void UpdateGameStateMacine()
        {
            if (DefaultStateMachineManager.IsHaveStateMachine("Game State Machine", out var uiStateMachine))
            {
                uiStateMachine.SetState(_locationSystem.GetCurrentLocation() == null ? "None" : "Playing");
            }
        }
    }
}