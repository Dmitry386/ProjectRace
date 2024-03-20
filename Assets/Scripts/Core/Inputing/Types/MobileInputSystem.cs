using UnityEngine;

namespace Assets.Scripts.Core.Inputing.Types
{
    internal class MobileInputSystem : MonoBehaviour, IInputSystem
    {
        [SerializeField] private Joystick _moveJoystick;

        public float Forward { get; set; }
        public float Side { get; set; }
        public string KeyDown { get; set; }

        private void Update()
        {
            Side = _moveJoystick.Direction.x;
            Forward = _moveJoystick.Direction.y;
        }
    }
}