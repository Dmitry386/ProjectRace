using System;

namespace Assets.Scripts.Entities.Buyable
{
    [Serializable]
    public class BuyableObjectData
    {
        public string Name;

        public string ObjectType = "Paintjob";

        public int InGamePrice = 100000;
    }
}