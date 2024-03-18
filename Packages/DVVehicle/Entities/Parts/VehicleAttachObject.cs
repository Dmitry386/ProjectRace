using System;
using UnityEngine;

namespace Packages.DVVehicle.Entities.Parts
{
    [Serializable]
    public class VehicleAttachObject
    {
        [SerializeField] public string Name;
        [SerializeField] public string AttachPositionName = string.Empty;
        [SerializeField] public Transform Prefab;
    }
}