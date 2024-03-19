using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.Core.Networking.Sync
{
    internal class NetworkPrefabsLoad : MonoBehaviour
    {
        private void Awake()
        {
            var prefabs = Resources.LoadAll<PhotonView>(string.Empty);
            DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;

            if (pool != null && prefabs != null)
            {
                foreach (var prefab in prefabs)
                {
                    pool.ResourceCache.Add(prefab.name, prefab.gameObject);
                }
            }
        }
    }
}