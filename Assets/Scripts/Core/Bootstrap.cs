using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core
{
    internal class Bootstrap : MonoInstaller
    {
        public override void InstallBindings()
        {

        }

        private void ContainerBindInstance<T>() where T : Object
        {
            Container.BindInstance<T>(GameObject.FindAnyObjectByType<T>());
        }
    }
}