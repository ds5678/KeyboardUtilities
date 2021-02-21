using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KeyboardUtilities
{
    public interface IHandleInput
    {
        Vector2 MousePosition { get; }

        Vector2 MouseScrollDelta { get; }

        bool GetKeyDown(KeyCode key);
        bool GetKey(KeyCode key);

        bool GetKeyUp(KeyCode key);

        bool GetMouseButtonDown(int btn);
        bool GetMouseButton(int btn);
        bool GetMouseButtonUp(int btn);

        BaseInputModule UIModule { get; }

        PointerEventData InputPointerEvent { get; }

        void AddUIInputModule();
        void ActivateModule();
    }
}
