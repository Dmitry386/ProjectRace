using UnityEngine;

namespace Assets.Scripts.Core.Inputing
{
    internal interface IInputSystem
    {
        public float Forward { get; set; }
        public float Side { get; set; }

        public string KeyDown { get; set; }
    }
}