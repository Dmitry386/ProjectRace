using Assets.Scripts.Core.Advertising;
using Assets.Scripts.Core.Advertising.Types;
using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.NetworkControllers;
using Assets.Scripts.Core.Networking.Sync;
using Assets.Scripts.Core.Other;
using Assets.Scripts.Core.Other.Awards;
using Assets.Scripts.Core.Other.DriftPoints;
using Assets.Scripts.Core.Saving;
using Assets.Scripts.Entities.Buyable;
using Assets.Scripts.World.Locations;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core
{
    internal class Bootstrap : MonoInstaller
    {
        public override void InstallBindings()
        {
            ContainerBindInstance<SaveSystem>();
            ContainerBindInstance<BuySystem>();
            ContainerBindInstance<VehicleSwitcher>();
            ContainerBindInstance<TuningControl>();
            ContainerBindInstance<LocationSystem>();
            ContainerBindInstance<SyncPlayerSpawn>();
            ContainerBindInstance<VehicleDriftGlobalController>();
            ContainerBindInstance<GameAwardSystem>();

            Container.BindInstance<INetworkControl>(GameObject.FindAnyObjectByType<PhotonNetworkControl>());
            Container.BindInstance<IAdSystem>(GameObject.FindAnyObjectByType<IronSourceAdSystem>());
        }

        private void ContainerBindInstance<T>() where T : Object
        {
            Container.BindInstance<T>(GameObject.FindAnyObjectByType<T>());
        }
    }
}