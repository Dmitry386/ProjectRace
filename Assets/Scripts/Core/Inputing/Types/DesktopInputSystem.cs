using UnityEngine;

namespace Assets.Scripts.Core.Inputing.Types
{
    internal class DesktopInputSystem : MonoBehaviour, IInputSystem
    {
        public float Forward { get; set; }
        public float Side { get; set; }
        public string KeyDown { get; set; }

        private void Update()
        {
            Forward = Input.GetAxis("Vertical");
            Side = Input.GetAxisRaw("Horizontal");

            if (Input.GetKey(KeyCode.Space)) KeyDown = "HandBrake";
            else KeyDown = string.Empty;
        }
    }
}