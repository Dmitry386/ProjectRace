using Packages.DVUnityUtilities.Runtime.Other.Coroutines;
using System;
using System.Collections;
using UnityEngine;

namespace DVUnityUtilities.Other.Coroutines
{
    public class CoroutineTimer : IDisposable
    {
        public event Action<CoroutineTimer> OnTick;

        private MonoBehaviour _mono;
        private float _secondsInterval;
        private bool _isRepeating;

        private GameObject _tempObject;

        public CoroutineTimer(MonoBehaviour mono, float secondsInterval, bool isRepeating = false)
        {
            _mono = mono;
            _secondsInterval = secondsInterval;
            _isRepeating = isRepeating;
        }

        public CoroutineTimer(float secondsInterval, bool isRepeating = false)
        {
            _tempObject = new GameObject("[temp_timer]");
            _mono = _tempObject.AddComponent<TempTimerMono>();

            _secondsInterval = secondsInterval;
            _isRepeating = isRepeating;
        }

        public CoroutineTimer Start()
        {
            _mono.StartCoroutine(Tick());
            return this;
        }

        private IEnumerator Tick()
        {
            do
            {
                yield return new WaitForSeconds(_secondsInterval);

                if (!_mono) break;

                try { OnTick?.Invoke(this); }
                catch (Exception ex) { Debug.LogException(ex); }
            }
            while (_isRepeating);

            Dispose();
        }

        public void Dispose()
        {
            if (_tempObject != null)
            {
                GameObject.Destroy(_tempObject.gameObject);
            }

            OnTick = null;
            _mono = null;
        }
    }
}