using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Entities.Buyable
{
    internal class BuySystem : MonoBehaviour
    {
        public UnityEvent<BuyableObjectData> OnBuyed;

        [SerializeField] private List<BuyableObjectData> _fullAssortiment = new();

        public void AddToAssortiment(BuyableObjectData obj)
        {
            if (!IsHaveInAssortiment(name, out _))
            {
                _fullAssortiment.Add(obj);
            }
        }

        public void RemoveFromAssortiment(string name)
        {
            if (IsHaveInAssortiment(name, out var bod))
            {
                _fullAssortiment.Remove(bod);
            }
        }

        public bool IsHaveInAssortiment(string name, out BuyableObjectData bod)
        {
            bod = _fullAssortiment.FirstOrDefault(x => x.Name == name);
            return bod != null;
        }
    }
}