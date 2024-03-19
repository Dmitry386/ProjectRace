using Assets.Scripts.Core.Saving;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Other.DriftPoints
{
    internal class VehicleDriftGlobalController : MonoBehaviour
    {
        public event Action<DriftPointsEventArgs> OnMoneyPerDriftPointRequsted;
        public event Action<DriftPointsEventArgs> OnUpdatePreviewDriftValue;

        [SerializeField] public float DriftPointsPerSec = 50f;
        [SerializeField] public float SecToResetCounter = 2f;
        [SerializeField] public float MoneyPerOneDriftPoint = 0.5f;

        [Inject] private SaveSystem _saveSystem;

        public void UpdatePreviewDriftPoints(float driftPoints)
        {
            OnUpdatePreviewDriftValue?.Invoke(new() { Points = driftPoints });
        }

        public void RegisterFinalDriftPoints(float driftPoints)
        {
            if (_saveSystem.Load(out var save))
            {
                int money = GetMoneyForDriftPoints(driftPoints);

                OnMoneyPerDriftPointRequsted?.Invoke(new() { Money = money, Points = driftPoints });
            }
        }

        public int GetMoneyForDriftPoints(float driftPoints)
        {
            return (int)(driftPoints * MoneyPerOneDriftPoint);
        }

        private void OnDestroy()
        {
            OnMoneyPerDriftPointRequsted = null;
            OnUpdatePreviewDriftValue = null;
        }
    }
}