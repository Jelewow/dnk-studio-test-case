using UnityEngine;

namespace Jelewow.DNK.GameCamera.MonoBehaviours
{
    public class PlayerCamera : MonoBehaviour
    {
        public Camera MainCamera { get; private set; }

        private void Awake()
        {
            MainCamera = Camera.main;
        }
    }
}