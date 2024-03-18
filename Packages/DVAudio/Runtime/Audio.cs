using DVUnityUtilities;
using UnityEngine;

namespace DVAudio
{
    public class Audio : SingletonAuto<Audio>
    {
        private static void ShowDebugNullableAudioclip()
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Nullable audioclip.");
#endif
        }

        public static AudioSource Play2D(AudioClip clip)
        {
            if (TryCreateAudioSource(clip, true, out var s))
            {
                s.clip = clip;
                s.Play();

                return s;
            }

            return null;
        }

        public static AudioSource Play3D(AudioClip clip, Transform t)
        {
            return Play3D(clip, t.position);
        }

        public static AudioSource Play3D(AudioClip clip, in Vector3 pos)
        {
            if (TryCreateAudioSource(clip, true, out var s))
            {
                s.transform.position = pos;
                Apply3DSettings(s);

                s.PlayOneShot(clip);
                GameObject.Destroy(s.gameObject, clip.length);

                return s;
            }

            return null;
        }

        public static AudioSource Play2DLooped(AudioClip clip)
        {
            if (TryCreateAudioSource(clip, false, out var s))
            {
                s.loop = true;
                s.clip = clip;
                s.Play();

                return s;
            }

            return null;
        }

        public static AudioSource Play3DLooped(AudioClip clip, in Vector3 pos)
        {
            if (TryCreateAudioSource(clip, false, out var s))
            {
                s.transform.position = pos;
                Apply3DSettings(s);

                s.loop = true;
                s.clip = clip;
                s.Play();

                return s;
            }

            return null;
        }

        public static AudioSource Play3DLooped(AudioClip clip, in Transform t)
        {
            var p = Play3DLooped(clip, t.position);

            if (p)
            {
                p.transform.SetParent(t, true);
            }

            return p;
        }

        public static bool TryCreateAudioSource(AudioClip clip, bool autoDestroy, out AudioSource s)
        {
            s = null;
            if (!Application.isPlaying) return false;

            if (clip == null)
            {
                ShowDebugNullableAudioclip();
                return false;
            }

            s = Util.CreateGameObjectWithComponent<AudioSource>(null);
            s.hideFlags = HideFlags.HideInHierarchy;
            if (autoDestroy) GameObject.Destroy(s.gameObject, clip.length);

            return true;
        }

        private static void Apply3DSettings(AudioSource s)
        {
            s.spatialBlend = 1f;
            s.dopplerLevel = 0;

            s.rolloffMode = AudioRolloffMode.Linear;
            s.maxDistance = 20f;
        }
    }
}