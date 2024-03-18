using Packages.DVUnityUtilities.Runtime.Other.Coroutines;
using System;
using System.Collections;
using UnityEngine;

namespace DVUnityUtilities.Other.Coroutines
{
    public class CoroutineFor : IDisposable
    {
        /// <summary>
        /// Invoke if not destroyed/quit game
        /// </summary>
        public event Action<CoroutineFor> OnSuccess;

        private MonoBehaviour _mono;
        private Action<int> _action;

        private int _currentId;
        private int _endId;

        private int _yieldEveryCount = 1;
        private int _timeToYield = 0;

        private bool _needDestroyMono;

        public CoroutineFor(MonoBehaviour mono, Action<int> action, int startId, int endId, int yieldEveryCount = 1)
        {
            _yieldEveryCount = yieldEveryCount;
            _currentId = startId;
            _endId = endId;

            _action = action;
            _mono = mono;
        }

        public CoroutineFor(Action<int> action, int startId, int endId, int yieldEveryCount = 1)
        {
            _yieldEveryCount = yieldEveryCount;
            _currentId = startId;
            _endId = endId;

            _action = action;
            _needDestroyMono = true;
            _mono = new GameObject().AddComponent<TempTimerMono>();
        }

        public void Start()
        {
            if (_mono != null && _action != null)
            {
                _mono.StartCoroutine(DoAction());
            }
        }

        private IEnumerator DoAction()
        {
            while (_currentId < _endId)
            {
                UpdateYieldCounter();
                if (IsTimeToYield())
                {
                    ResetYieldCounter();
                    yield return null;
                }

                _action.Invoke(_currentId);
                _currentId++;
            }

            OnSuccess?.Invoke(this);
        }

        private void ResetYieldCounter()
        {
            _timeToYield = _yieldEveryCount;
        }

        private bool IsTimeToYield()
        {
            return _timeToYield <= 0;
        }

        private void UpdateYieldCounter()
        {
            _timeToYield--;
        }

        public void Dispose()
        {
            if (_needDestroyMono && _mono != null)
            {
                GameObject.Destroy(_mono.gameObject);
            }

            _mono = null;
            _action = null;
        }
    }
}