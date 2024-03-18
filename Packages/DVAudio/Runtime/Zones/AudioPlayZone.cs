using System.Collections;
using UnityEngine;

namespace DVAudio.Zones
{
    public class AudioPlayZone : MonoBehaviour
    {
        private static AudioListener _listener;

        [SerializeField] private AudioClip _clip;
        [SerializeField] public bool _is3D = false;
        [SerializeField] private bool _disableIfOutOfZone = false;

        private float _updateInterval = 0.5f;

        private AudioSource _source;
        private Transform _sourceTransform;

        private BoxCollider _boxCol;
        private SphereCollider _sphereCol;

        private void Awake()
        {
            _boxCol = GetComponent<BoxCollider>();
            _sphereCol = GetComponent<SphereCollider>();

            if (!_boxCol && !_sphereCol)
            {
                Debug.LogWarning($"No collider on {name}. {GetType().Name} set as ambient.");
                _disableIfOutOfZone = false;
            }

            RestartSource();            
        }

        private void RestartSource()
        {
            if (!_is3D) _source = Audio.Play2DLooped(_clip);
            else _source = Audio.Play3DLooped(_clip, transform.position);

            _sourceTransform = _source.transform;
            StartCoroutine(Tick());
        }

        private IEnumerator Tick()
        {
            while (_source)
            {
                if (IsActiveAudioListener() &&
                    IsInsideZone(_listener.transform.position))
                {
                    if (_disableIfOutOfZone)
                    {
                        _source.enabled = true;
                    }
                    _sourceTransform.position = _listener.transform.position;
                }
                else
                {
                    if (_disableIfOutOfZone)
                    {
                        _source.enabled = false;
                    }
                }

                yield return new WaitForSeconds(_updateInterval);
            }

            //if (this)
            //{
            //    RestartSource();
            //}
        }

        private bool IsInsideZone(in Vector3 point)
        {
            if (_boxCol)
            {
                return _boxCol.bounds.Contains(point);
            }
            else if (_sphereCol)
            {
                return Vector3.Distance(_sphereCol.bounds.center, point) <= _sphereCol.radius;
            }
            return false;
        }

        private static bool IsActiveAudioListener()
        {
            if (!_listener)
            {
                _listener = GameObject.FindAnyObjectByType<AudioListener>();
            }

            return _listener;
        }

        private void OnValidate()
        {
            if (TryGetComponent<Collider>(out var c) && !c.isTrigger)
            {
                c.isTrigger = true;
            }
        }
    }
}