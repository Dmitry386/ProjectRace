using System.Collections.Generic;
using UnityEngine;

namespace Packages.DVVehicle.Entities.Parts
{
    public class VehicleAttachPrefabContainer : MonoBehaviour
    {
        [SerializeField] public List<VehicleAttachObject> PossibleAttaches = new();

        public void AddToContainer(VehicleAttachObject obj)
        {
            if (PossibleAttaches.Contains(obj)) return;
            PossibleAttaches.Add(obj);
        }

        public void RemoveFromContainer(VehicleAttachObject obj)
        {
            PossibleAttaches.Remove(obj);
        }
    }
}