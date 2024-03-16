using System;
using UnityEngine;

namespace Assets.Scripts.Entities.Buyable
{
    [Serializable]
    public class BuyableObjectData
    {
        public string Name;

        public string ObjectType = "Paintjob";

        public double InGamePrice = 100000;

        [Tooltip("If RealPrice = 0, object will available without IAP")]
        public double RealPrice = 0;
    }
}