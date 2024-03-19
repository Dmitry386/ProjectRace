using DVUnityUtilities.Other.Pools;
using Photon.Pun;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Core.Networking.Helpers
{
    internal static class NetworkHelper
    {
        public static bool IsHaveEntityWithId<T>(int vehicleId, out T veh) where T : MonoBehaviour
        {
            var vehicles = WCache.GetAll<T>();
            veh = vehicles.FirstOrDefault(x => x.GetComponent<PhotonView>().ViewID == vehicleId);
            return veh;
        }

        public static void SendRpcToAllByPlayer(PhotonView view, string rpcName, params object[] args)
        {
            var players = PhotonNetwork.PlayerList;
            
            foreach (var player in players)
            {
                if (!player.IsLocal)
                {
                    view.RPC(rpcName, player, args);
                }
            }
        }
    }
}