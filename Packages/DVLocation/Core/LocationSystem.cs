using System;
using UnityEngine;

namespace Assets.Scripts.World.Locations
{
    public class LocationSystem : MonoBehaviour
    {
        public event Action<Location> OnLocationChanged;

        public Func<Location, Location> InstantiateMethod = (prefab) => Location.Instantiate(prefab);
        public Action<Location> DestroyMethod = (obj) => Location.Destroy(obj);

        private Location _current;

        public Location GetCurrentLocation()
        {
            return _current;
        }

        public void SetLocation(Location locationPrefab)
        {
            UnloadLocation();

            _current = InstantiateMethod.Invoke(locationPrefab);
            OnLocationChanged?.Invoke(_current);
        }

        public void UnloadLocation()
        {
            if (!_current) return;

            GameObject.Destroy(_current.gameObject);
            _current = null;
        }
    }
}