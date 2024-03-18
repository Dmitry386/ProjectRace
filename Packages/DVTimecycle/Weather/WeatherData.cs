using UnityEngine;

namespace Packages.DVTimecycle.Weather
{
    [CreateAssetMenu(menuName = "cfg/World/Weather")]
    public class WeatherData : ScriptableObject
    {
        public Color32 Ambient = Color.white;

        public Color32 SkyColor = Color.white;

        [Range(0f, 1f)]
        public float AmbientIntensity = 1f;

        public Color32 SunColor = Color.white;

        [Range(0, 5)]
        public float SunIntensity = 1f;

        [Range(0f, 0.1f)]
        public float FogDensity = 0.025f;

        public LightShadows ShadowType = LightShadows.Hard;

        public float DrawDistance = 50f;
    }
}