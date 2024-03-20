using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.Definitions;
using DVUnityUtilities.Other.Coroutines;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.World.Locations
{
    internal class LocationTimer : MonoBehaviour
    {
        [SerializeField] private float _timeInSeconds = 120f;

        [Inject] private LocationSystem _locationSystem;
        [Inject] private INetworkControl _networkControl;

        private CoroutineTimer _timer;
        private float _startTimerMoment;

        private void Awake()
        {
            _locationSystem.OnLocationChanged += OnLocationChanged;
        }

        private void OnTimeIsOver()
        {
            _locationSystem.SetLocation(locationPrefab: null);
        }

        private void OnLocationChanged(Location obj)
        {
            if (_networkControl.GetNetworkStatus() == NetworkStatus.Host)
            {
                StartTimer();
            }
        }

        private void StartTimer()
        {
            if (_networkControl.GetNetworkStatus() == NetworkStatus.Host)
            {
                _timer?.Dispose();
                _timer = new CoroutineTimer(this, _timeInSeconds, false).Start();
                _timer.OnTick += OnTick;
                _startTimerMoment = Time.time;
            }
        }

        private void OnTick(CoroutineTimer obj)
        {
            if (_networkControl.GetNetworkStatus() == NetworkStatus.Host)
            {
                OnTimeIsOver();
            }
        }

        public float GetRemainingTime()
        {
            return _timeInSeconds - Mathf.Clamp(Time.time - _startTimerMoment, 0, _timeInSeconds);
        }
    }
}