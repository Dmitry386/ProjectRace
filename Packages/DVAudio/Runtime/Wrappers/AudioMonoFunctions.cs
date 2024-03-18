using UnityEngine;

namespace DVAudio.Wrappers
{
    internal class AudioMonoFunctions : MonoBehaviour
    {
        public void Play2D(AudioClip clip)
        {
            Audio.Play2D(clip);
        }

        public void Play3D(AudioClip clip, Transform t)
        {
            Audio.Play3D(clip, t);
        }

        public void Play3D(AudioClip clip, in Vector3 pos)
        {
            Audio.Play3D(clip, pos);
        }

        public void Play2DLooped(AudioClip clip)
        {
            Audio.Play2DLooped(clip);
        }

        public void Play3DLooped(AudioClip clip, in Vector3 pos)
        {
            Audio.Play3DLooped(clip, pos);
        }

        public void Play3DLooped(AudioClip clip, in Transform t)
        {
            Audio.Play3DLooped(clip, t);
        }
    }
}