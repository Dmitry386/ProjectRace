using Packages.DVVehicle.Core.Movement;
using UnityEngine;

namespace Packages.DVVehicle.Testing
{
    internal class TestVehicleInput : MonoBehaviour
    {
        [SerializeField] private VehicleMovementSystem _veh;

        private void Update()
        {
            _veh.SetMoveDirection(Input.GetAxis("Vertical"), Input.GetAxisRaw("Horizontal"));
            _veh.SetHandBrake(Input.GetKey(KeyCode.Space));
        }
    }
}