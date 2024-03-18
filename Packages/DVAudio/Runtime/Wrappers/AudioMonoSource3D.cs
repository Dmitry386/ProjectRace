using UnityEngine;

namespace DVAudio.Wrappers
{
    internal class AudioMonoSource3D : MonoBehaviour
    {
        [SerializeField] private bool _isLooped = true;
        [SerializeField] private AudioClip _clip;
        [SerializeField] private float _radius = 20f;

        private void Start()
        {
            AudioSource s;

            if (_isLooped) s = Audio.Play3DLooped(_clip, transform);
            else s = Audio.Play3D(_clip, transform);

            if (s) s.maxDistance = _radius;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}