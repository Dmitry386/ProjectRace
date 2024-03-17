using Assets.Scripts.Core.Saving;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Assets.Scripts.Entities.Buyable
{
    internal class BuySystem : MonoBehaviour
    {
        public UnityEvent<BuyableObjectData> OnBuyed;

        [SerializeField] private List<BuyableObjectData> _fullAssortiment = new();
        [Inject] private SaveSystem _saveSystem;

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

        public bool TryBuy(BuyableObjectData obj)
        {
            if (IsBuyed(obj.Name)) return false;

            if (_saveSystem.Load(out var saveData))
            {
                if (saveData.TryTakeMoney(obj.InGamePrice))
                {
                    saveData.AddBuyedObject(obj.Name);
                    return true;
                }
            }

            return false;
        }

        public bool IsBuyed(string name)
        {
            if (_saveSystem.Load(out var saveData))
            {
                return saveData.IsBuyed(name);
            }
            return false;
        }

    }
}