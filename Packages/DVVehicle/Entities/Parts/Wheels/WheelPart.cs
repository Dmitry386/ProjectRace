using System;
using UnityEngine;

namespace Packages.DVVehicle.Entities.Parts.Wheels
{
    [SelectionBase]
    public class WheelPart : MonoBehaviour
    {
        [SerializeField] private float _driftDetectionMinSidewaysSlip = 0.45f;

        [SerializeField] private WheelCollider _collider;
        [SerializeField] private Transform _visualization;

        public void SetMotionTorque(float torque)
        {
            _collider.motorTorque = torque;
        }

        public void SetSteerAngle(float angle)
        {
            _collider.steerAngle = angle;
        }

        public void UpdateRotationVisualization()
        {
            _collider.GetWorldPose(out var pos, out var rot);

            _visualization.rotation = rot;
            _visualization.position = pos;
        }

        public void SetBrakeTorque(float torque)
        {
            _collider.brakeTorque = torque;
        }

        public bool IsDrifting()
        {
            if (_collider.GetGroundHit(out var hit))
            {
                return Mathf.Abs(hit.sidewaysSlip) >= _driftDetectionMinSidewaysSlip;
            }
            return false;
        }

        public WheelCollider GetCollider()
        {
            return _collider;
        }

        public void SetRotation(Quaternion quaternion)
        {
            _collider.transform.rotation = quaternion;
            UpdateRotationVisualization();
        }
    }
}