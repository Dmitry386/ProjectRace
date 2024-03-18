using DVUnityUtilities;
using Packages.DVVehicle.Definitions;
using Packages.DVVehicle.Entities.Parts;
using System.Linq;
using UnityEngine;

namespace Packages.DVVehicle.Entities.Vehicles
{
    [RequireComponent(typeof(Rigidbody))]
    public class VehicleEntity : MonoBehaviour
    {
        [SerializeField] public VehiclePartsData Parts;
        [SerializeField] public HandlingData Handling;

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
    }
}