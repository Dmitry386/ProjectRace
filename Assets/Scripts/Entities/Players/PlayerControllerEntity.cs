﻿using Cinemachine;
using Packages.DVVehicle.Core.Movement;
using Packages.DVVehicle.Entities.Vehicles;
using UnityEngine;

namespace Assets.Scripts.Entities.Players
{
    internal class PlayerControllerEntity : MonoBehaviour
    {
        [SerializeField] private Transform _vehicleParentTransform;
        [SerializeField] private CinemachineVirtualCamera _cam;

        private VehicleEntity _veh;
        private VehicleMovementSystem _moveSystem;

        public void SetControllableVehicle(VehicleEntity veh)
        {
            _veh = veh;

            if (_veh)
            {
                _moveSystem = _veh.GetComponent<VehicleMovementSystem>();
                _cam.Follow = _veh.transform;
                _cam.LookAt = _veh.transform;
            }
            else
            {
                _moveSystem = null;
                _cam.Follow = null;
                _cam.LookAt = null;
            }
        }

        private void Update() // todo: mobile and pc control
        {
            if (_moveSystem)
            {
                _moveSystem.SetMoveDirection(Input.GetAxis("Vertical"), Input.GetAxisRaw("Horizontal"));
                _moveSystem.SetHandBrake(Input.GetKey(KeyCode.Space));
            }
        }
    }
}