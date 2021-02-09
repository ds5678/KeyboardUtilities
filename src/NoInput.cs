using UnityEngine;
using UnityEngine.EventSystems;

namespace KeyboardUtilities
{
    public class NoInput : IHandleInput
    {
        public Vector2 MousePosition => Vector2.zero;

        public bool GetKey(KeyCode key) => false;
        public bool GetKeyDown(KeyCode key) => false;

        public bool GetMouseButton(int btn) => false;
        public bool GetMouseButtonDown(int btn) => false;

        public BaseInputModule UIModule => null;
        public PointerEventData InputPointerEvent => null;
        public void ActivateModule() { }
        public void AddUIInputModule() { }
    }
}
