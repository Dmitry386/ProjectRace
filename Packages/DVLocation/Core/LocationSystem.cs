using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.World.Locations
{
    public class LocationSystem : MonoBehaviour
    {
        public event Action<Location> OnLocationChanged;

        public Func<Location, Location> InstantiateMethod = (prefab) => Location.Instantiate(prefab);
        public Action<Location> DestroyMethod = (obj) => Location.Destroy(obj);

        private Location _current;
        private List<Location> _allLocationsPrefabs = new();

        private void Awake()
        {
            _allLocationsPrefabs = Resources.LoadAll<Location>(string.Empty).ToList();
        }

        public Location GetCurrentLocation()
        {
            return _current;
        }

        public void SetLocation(Location locationPrefab)
        {
            UnloadLocation();

            if (locationPrefab)
            {
                _current = InstantiateMethod.Invoke(locationPrefab);
            }
            OnLocationChanged?.Invoke(_current);
        }

        public void SetLocation(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                SetLocation(locationPrefab: null);
            }
            else
            {
                if (IsHaveLocation(name, out var loc))
                {
                    SetLocation(loc);
                }
                else
                {
                    Debug.LogWarning(@$"No location with name ""{name}""");
                }
            }
        }

        public bool IsHaveLocation(string name, out Location loc)
        {
            loc = _allLocationsPrefabs.FirstOrDefault(x => x.ToString() == name);
            return loc != null;
        }

        public bool IsLoadedLocation(out Location loc)
        {
            loc = GetCurrentLocation();
            return loc;
        }

        public void UnloadLocation()
        {
            if (!_current) return;

            GameObject.Destroy(_current.gameObject);
            _current = null;
        }
    }
}