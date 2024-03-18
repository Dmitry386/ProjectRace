using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

namespace Packages.DVTimecycle.Weather
{
    public class WeatherSystem : MonoBehaviour
    {
        [SerializeField] private Light _directionLight;
        [SerializeField] private WeatherData _weatherData;

        private Camera _cam;
        private CinemachineVirtualCamera _cam2;

        private void Start()
        {
            _cam = Camera.main;
            _cam2 = FindAnyObjectByType<CinemachineVirtualCamera>();
            VisualizeDataValues();
        }

        private void Update()
        {
            VisualizeDataValues();
        }

        private void OnDrawGizmos()
        {
            VisualizeDataValues();
        }

        public void VisualizeDataValues()
        {
            var c = _cam;
            var l = _directionLight;

            if (l != null)
            {
                l.color = _weatherData.SunColor;
                l.intensity = _weatherData.SunIntensity;
                l.shadows = _weatherData.ShadowType;
            }

            if (c != null)
            {
                c.farClipPlane = _weatherData.DrawDistance;
                c.clearFlags = CameraClearFlags.SolidColor;
                c.backgroundColor = _weatherData.SkyColor;
            }

            if (_cam2)
            {
                _cam2.m_Lens.FarClipPlane = _weatherData.DrawDistance;
            }

            RenderSettings.fog = _weatherData.FogDensity > 0;
            RenderSettings.ambientMode = AmbientMode.Trilight;
            RenderSettings.ambientLight = _weatherData.Ambient;
            RenderSettings.ambientEquatorColor = _weatherData.Ambient;
            RenderSettings.ambientGroundColor = _weatherData.Ambient;
            RenderSettings.ambientIntensity = _weatherData.AmbientIntensity;

            RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.fogColor = _weatherData.SkyColor;
            RenderSettings.fogDensity = _weatherData.FogDensity;
        }

        public void SetWeather(WeatherData weather)
        {
            _weatherData = weather;
            VisualizeDataValues();
        }
    }
}