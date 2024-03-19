using DVUnityUtilities;
using DVUnityUtilities.Other.Pools;
using Packages.DVVehicle.Definitions;
using UnityEngine;

namespace Packages.DVVehicle.Entities.Vehicles
{
    [RequireComponent(typeof(Rigidbody))]
    public class VehicleEntity : MonoBehaviour
    {
        [SerializeField] public VehiclePartsData Parts;
        [SerializeField] public HandlingData Handling;

        private void Awake()
        {
            WCache.Register(this);
        }

        private void OnValidate()
        {
            if (TryGetComponent<Rigidbody>(out var rb))
            {
                if (rb.mass != Handling.Mass)
                {
                    rb.mass = Handling.Mass;
                }

                if (rb.isKinematic)
                {
                    rb.isKinematic = false;
                }

                if (!rb.useGravity)
                {
                    rb.useGravity = true;
                }
            }
        }

        public override string ToString()
        {
            return StringUtils.FromSceneNameToObjectName(name);
        }

        private void OnDestroy()
        {
            WCache.DeRegister(this);
        }
    }
}