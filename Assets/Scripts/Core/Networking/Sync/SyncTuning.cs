using Assets.Scripts.Core.Networking.Helpers;
using DVUnityUtilities.Other.Coroutines;
using Packages.DVVehicle.Core.Serialization;
using Packages.DVVehicle.Core.Serialization.Data;
using Packages.DVVehicle.Entities.Vehicles;
using Photon.Pun;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Networking.Sync
{
    [RequireComponent(typeof(PhotonView))]
    internal class SyncTuning : MonoBehaviourPun
    {
        [SerializeField] private float _interval = 1f;
        [Inject] private SyncPlayerSpawn _playerSpawnTest;

        private PhotonView _view;

        private void Awake()
        {
            _view = GetComponent<PhotonView>();
        }

        private void OnEnable()
        { 
            new CoroutineTimer(this, _interval, true).Start().OnTick += SyncTuning_OnTick;
        }

        private void SyncTuning_OnTick(CoroutineTimer obj)
        {
            if (_playerSpawnTest.IsPlayerSpawned(out _, out var veh))
            {
                SendTuningForAllPlayers(veh);
            }
        }

        private void SendTuningForAllPlayers(VehicleEntity veh)
        {
            var tuningData = VehicleSerialization.GetDataFromVehicle(veh).Tuning;
            NetworkHelper.SendRpcToAllByPlayer(_view, nameof(SendTuningRPC), veh.GetComponent<PhotonView>().ViewID, tuningData.PaintJob, tuningData.Attaches.ToArray());
        }

        [PunRPC]
        private void SendTuningRPC(int vehicleId, string paintJob, string[] attaches)
        {
            if (NetworkHelper.IsHaveEntityWithId<VehicleEntity>(vehicleId, out var veh))
            {
                var tuningData = new TuningData() { PaintJob = paintJob, Attaches = attaches };
                VehicleSerialization.ApplyTuning(veh, tuningData);
            }
        }
    }
}