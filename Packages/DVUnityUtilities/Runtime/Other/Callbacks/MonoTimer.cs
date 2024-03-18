using DVUnityUtilities.Other.Coroutines;
using UnityEngine;
using UnityEngine.Events;

namespace DVUnityUtilities.Other.Callbacks
{
    internal class MonoTimer : MonoBehaviour
    {
        [SerializeField] private float _interval = 1f;
        [SerializeField] private bool _repeat = true;
        [SerializeField] private UnityEvent _onTick;

        private CoroutineTimer _timer;

        private void Awake()
        {
            _timer = new CoroutineTimer(_interval, _repeat);
            _timer.OnTick += OnTick;
            _timer.Start();
        }

        private void OnTick(CoroutineTimer obj)
        {
            _onTick.Invoke();
        }

        private void OnDestroy()
        {
            _timer?.Dispose();
        }
    }
}