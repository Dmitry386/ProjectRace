using UnityEngine;
using UnityEngine.Events;

namespace DVUnityUtilities.Other.Callbacks
{
    [DefaultExecutionOrder(10)]
    internal class UnityNativeCallbacks : MonoBehaviour
    {
        [SerializeField] public UnityEvent OnAwaked;
        [SerializeField] public UnityEvent OnStarted;
        [SerializeField] public UnityEvent OnUpdated;
        [SerializeField] public UnityEvent OnDestroyed;

        [SerializeField] public UnityEvent OnEnabled;
        [SerializeField] public UnityEvent OnDisabled;

        private void Awake()
        {
            OnAwaked.Invoke();
        }

        private void OnEnable()
        {
            OnEnabled.Invoke();
        }

        private void OnDisable()
        {
            OnDisabled.Invoke();
        }

        private void Start()
        {
            OnStarted.Invoke();
        }

        private void Update()
        {
            OnUpdated.Invoke();
        }

        private void OnDestroy()
        {
            OnDestroyed.Invoke();
        }
    }
}