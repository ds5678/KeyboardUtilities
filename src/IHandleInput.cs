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

        bool GetKeyDown(KeyCode key);
        bool GetKey(KeyCode key);

        bool GetMouseButtonDown(int btn);
        bool GetMouseButton(int btn);

        BaseInputModule UIModule { get; }

        PointerEventData InputPointerEvent { get; }

        void AddUIInputModule();
        void ActivateModule();
    }
}
