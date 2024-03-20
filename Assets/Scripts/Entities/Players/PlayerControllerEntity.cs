using Assets.Scripts.Core.Inputing;
using Assets.Scripts.Core.Other.DriftPoints;
using Cinemachine;
using Packages.DVVehicle.Core.Movement;
using Packages.DVVehicle.Entities.Vehicles;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Entities.Players
{
    internal class PlayerControllerEntity : MonoBehaviour
    {
        [SerializeField] private Transform _vehicleParentTransform;
        [SerializeField] private CinemachineVirtualCamera _camPrefab;
        [SerializeField] private VehicleDriftPointCounter _driftCounter;

        [Inject] private IInputSystem _inputSystem;

        private VehicleEntity _veh;
        private VehicleMovementSystem _moveSystem;
        private CinemachineVirtualCamera _cam;

        public void SetControllableVehicle(VehicleEntity veh)
        {
            _veh = veh;

            if (_veh)
            {
                if (!_cam) _cam = Instantiate(_camPrefab);

                _moveSystem = _veh.GetComponent<VehicleMovementSystem>();
                _cam.Follow = _veh.transform;
                _cam.LookAt = _veh.transform;

                _driftCounter.SetTarget(_veh.GetComponent<VehicleMovementSystem>());
            }
            else
            {

                if (_cam) GameObject.Destroy(_cam.gameObject);
                _moveSystem = null;
                _cam.Follow = null;
                _cam.LookAt = null;
            }
        }

        private void Update() // todo: mobile and pc input system
        {
            if (_moveSystem)
            {
                _moveSystem.SetMoveDirection(_inputSystem.Forward, _inputSystem.Side);
                _moveSystem.SetHandBrake(_inputSystem.KeyDown == "HandBrake");
            }
        }

        private void OnDestroy()
        {
            if (_cam) GameObject.Destroy(_cam.gameObject);
        }
    }
}