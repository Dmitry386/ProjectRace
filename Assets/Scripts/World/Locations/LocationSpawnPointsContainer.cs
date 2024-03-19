using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World.Locations
{
    internal class LocationSpawnPointsContainer : MonoBehaviour
    {
        [SerializeField] public List<Transform> SpawnPoints = new();
    }
}