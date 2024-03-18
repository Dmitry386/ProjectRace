using UnityEngine;

namespace DVUnityUtilities
{
    public static class ParticleUtils
    {
        public static ParticleSystem PlayParticleFromPrefab(ParticleSystem prefab, Vector3 pos, bool autoDestroy)
        {
            if (!Application.isPlaying) return null;
            if (prefab == null) return null;

            var copy = Object.Instantiate(prefab);
            copy.transform.position = pos;
            if (autoDestroy) DestoyParticleAfterPlay(copy);

            return copy;
        }

        public static void DestoyParticleAfterPlay(ParticleSystem ps)
        {
            Object.Destroy(ps.gameObject, ps.main.duration);
        }
    }
}