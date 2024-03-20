using DVUnityUtilities.Other.Cooldowners;
using Packages.DVVehicle.Core.Movement;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Other.DriftPoints
{
    internal class VehicleDriftPointCounter : MonoBehaviour
    {
        [SerializeField] private bool _registerDriftPointsOnDestroy = true;
        [Inject] private VehicleDriftGlobalController _controller;

        private VehicleMovementSystem _moveSystem;

        private float _fullLevelDriftPoints;
        private float _driftPoints;

        private Cooldown _cooldownToEndDrift = new();

        public void SetTarget(VehicleMovementSystem veh)
        {
            _moveSystem = veh;
        }

        private void Update()
        {
            if (_moveSystem?.IsDrifting() == true)
            {
                _driftPoints += Time.deltaTime * _controller.DriftPointsPerSec;
                _controller.UpdatePreviewDriftPoints(_driftPoints);
                _cooldownToEndDrift.UpdateLastUseTime();
            }

            if (_driftPoints > 0 && _cooldownToEndDrift.IsTimeOver(_controller.SecToResetCounter))
            {
                _fullLevelDriftPoints += _driftPoints;
                _driftPoints = 0;
            }
        }

        public void RegisterFinalDriftPoints()
        {
            _controller.RegisterFinalDriftPoints(_fullLevelDriftPoints);
            _driftPoints = 0;
        }

        private void OnDestroy()
        {
            if (_registerDriftPointsOnDestroy && _controller)
            {
                RegisterFinalDriftPoints();
            }
        }
    }
}