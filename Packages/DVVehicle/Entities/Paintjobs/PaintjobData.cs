using System;
using System.Collections.Generic;
using UnityEngine;

namespace Packages.DVVehicle.Entities.Paintjobs
{
    [Serializable]
    public class PaintjobData
    {
        public string Name; 

        [Tooltip("null element of the list means a material at that index will not be changed")]
        public List<Material> Materials = new();
    }
}