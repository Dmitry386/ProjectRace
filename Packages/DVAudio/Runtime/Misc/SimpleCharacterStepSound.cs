using DVUnityUtilities;
using System.Collections;
using UnityEngine;

namespace DVAudio.Misc
{
    internal class SimpleCharacterStepSound : MonoBehaviour
    {
        [SerializeField] private CharacterController _character;
        [SerializeField] private float _playDistance = 0.5f;
        [SerializeField] private float _interval = 0.1f;
        [SerializeField] private AudioClip[] _clips;

        private AudioSource _source;
        private Vector3 _lastPlayPoint;

        private void Awake()
        {
            Audio.TryCreateAudioSource(_clips[0], false, out _source);
        }

        private void OnEnable()
        {
            StartCoroutine(Tick());
        }

        private IEnumerator Tick()
        {
            while (true)
            {
                if (NeedToPlay())
                {
                    _lastPlayPoint = transform.position;
                    PlayStepSound();
                }
                yield return new WaitForSeconds(_interval);
            }
        }

        private bool NeedToPlay()
        {
            return _character.isGrounded && Vector3.Distance(_lastPlayPoint, transform.position) >= _playDistance;
        }

        private void PlayStepSound()
        {
            _source.clip = _clips.GetRandomElement();
            _source.PlayOneShot(_source.clip);
        }
    }
}