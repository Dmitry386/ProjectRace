using Packages.DVVehicle.Core.Movement;
using Packages.DVVehicle.Entities.Parts.Wheels;
using Packages.DVVehicle.Entities.Vehicles;
using UnityEngine;

namespace Packages.DVVehicle.Entities.Effects
{
    [RequireComponent(typeof(VehicleEntity))]
    [RequireComponent(typeof(VehicleMovementSystem))]
    internal class WheelSmokeEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particlePrefab;

        private ParticleSystem[] _particles;
        private VehicleMovementSystem _movementSystem;
        private VehicleEntity _vehicle;

        private void Awake()
        {
            _vehicle = GetComponent<VehicleEntity>();
            _movementSystem = GetComponent<VehicleMovementSystem>();

            InitVehicleSmokeParticles();
        }

        private void InitVehicleSmokeParticles()
        {
            _particles = new ParticleSystem[_vehicle.Parts.WheelsFront.Count + _vehicle.Parts.WheelsBack.Count];

            int particle_id = 0;

            foreach (var wheel in _vehicle.Parts.WheelsFront)
            {
                _particles[particle_id] = CreateParticleForWheel(wheel, _particlePrefab);
                particle_id++;
            }

            foreach (var wheel in _vehicle.Parts.WheelsBack)
            {
                _particles[particle_id] = CreateParticleForWheel(wheel, _particlePrefab);
                particle_id++;
            }
        }

        private ParticleSystem CreateParticleForWheel(WheelPart wheel, ParticleSystem ps)
        {
            var copy = Instantiate(ps);
            var col = wheel.GetCollider();

            copy.transform.position = col.bounds.center + (Vector3.down * col.center.y);
            copy.transform.SetParent(wheel.transform, true);

            return copy;
        }

        private void Update()
        {
            SetSmokeStatus(_movementSystem.IsDrifting());
        }

        private void SetSmokeStatus(bool isEnable)
        {
            for (int i = 0; i < _particles.Length; i++)
            {
                var emi = _particles[i].emission;
                emi.enabled = isEnable;
            }
        }
    }
}