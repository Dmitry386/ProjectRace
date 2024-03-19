using DVUnityUtilities;
using Packages.DVVehicle.Entities.Vehicles;
using System.Linq;
using UnityEngine;

namespace Packages.DVVehicle.Core.Movement
{
    [RequireComponent(typeof(VehicleEntity))]
    public class VehicleMovementSystem : MonoBehaviour
    {
        private VehicleEntity _veh;
        private Rigidbody _rb;

        /// <summary>
        /// -1.0 - 1.0
        /// </summary>
        private float _forwardAxis;

        /// <summary>
        /// -1.0 - 1.0
        /// </summary>
        private float _sideAxis;

        private float _torque;
        private bool _isHandBrakeActive;

        private void Awake()
        {
            _veh = GetComponent<VehicleEntity>();
            _rb = GetComponent<Rigidbody>();

            _rb.centerOfMass = _veh.Handling.CenterOfMass;
        }

        private void FixedUpdate()
        {
            _torque = GetTorque();
            float brake = _isHandBrakeActive ? _veh.Handling.HandBrakeForce : 0.0f;
            float wheelRot = GetWheelRotation();

            ApplyMotion(_torque);
            ApplyRotation(wheelRot);
            ApplyBrake(brake * Time.deltaTime);
            UpdateVisualization(); 
        }

        private void UpdateVisualization()
        {
            _veh.Parts.WheelsBack.ForEach(w => w.UpdateRotationVisualization());
            _veh.Parts.WheelsFront.ForEach(w => w.UpdateRotationVisualization());
        }

        private void ApplyMotion(float torque)
        {
            _veh.Parts.WheelsBack.ForEach(w => w.SetMotionTorque(torque));
            _veh.Parts.WheelsFront.ForEach(w => w.SetMotionTorque(torque));
        }

        private void ApplyBrake(float brake)
        {
            _veh.Parts.WheelsBack.ForEach(w => w.SetBrakeTorque(brake));
            _veh.Parts.WheelsFront.ForEach(w => w.SetBrakeTorque(brake));
        }

        private void ApplyRotation(float angle)
        {
            _veh.Parts.WheelsFront.ForEach(w => w.SetSteerAngle(angle));
        }

        private float GetWheelRotation()
        {
            return _veh.Handling.SteeringLock * _sideAxis;
        }

        private float GetTorque()
        {
            if (!IsCanRotateWheelInThisSide()) return 0;

            float force = _veh.Handling.InitialDriveForce * _forwardAxis;
            return force;
        }

        public bool IsCanRotateWheelInThisSide()
        {
            return _torque == 0 ||
                DigitUtils.SameSign(GetForwardVelocity(), _torque);
        }

        /// <summary>
        /// all values = -1.0 - 1.0
        /// </summary>
        /// <param name="forward"></param>
        /// <param name="side"></param>
        public void SetMoveDirection(float forward, float side)
        {
            _forwardAxis = Mathf.Clamp(forward, -1, 1);
            _sideAxis = Mathf.Clamp(side, -1, 1);
        }

        public void SetHandBrake(bool isHandBrakeActive)
        {
            _isHandBrakeActive = isHandBrakeActive;
        }

        public Vector3 GetLocalSpaceVelocity()
        {
            return transform.InverseTransformDirection(GetWorldSpaceVelocity());
        }

        public Vector3 GetWorldSpaceVelocity()
        {
            return _rb.velocity;
        }

        public float GetForwardVelocity()
        {
            return GetLocalSpaceVelocity().z;
        }

        public bool IsDrifting()
        {
            if (_veh.Parts.WheelsBack.Count == 0) return false;
            return _veh.Parts.WheelsBack.First().IsDrifting();
        }
    }
}