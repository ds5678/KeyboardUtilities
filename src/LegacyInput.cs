﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KeyboardUtilities
{
    public class LegacyInput : IHandleInput
    {
        public LegacyInput()
        {
            Implementation.Log("Initializing Legacy Input support...");

            m_mousePositionProp = TInput.GetProperty("mousePosition");
            m_getKeyMethod = TInput.GetMethod("GetKey", new Type[] { typeof(KeyCode) });
            m_getKeyDownMethod = TInput.GetMethod("GetKeyDown", new Type[] { typeof(KeyCode) });
            m_getMouseButtonMethod = TInput.GetMethod("GetMouseButton", new Type[] { typeof(int) });
            m_getMouseButtonDownMethod = TInput.GetMethod("GetMouseButtonDown", new Type[] { typeof(int) });
        }

        public static Type TInput => m_tInput ?? (m_tInput = ReflectionHelpers.GetTypeByName("UnityEngine.Input"));
        private static Type m_tInput;

        private static PropertyInfo m_mousePositionProp;
        private static MethodInfo m_getKeyMethod;
        private static MethodInfo m_getKeyDownMethod;
        private static MethodInfo m_getMouseButtonMethod;
        private static MethodInfo m_getMouseButtonDownMethod;

        public Vector2 MousePosition => (Vector3)m_mousePositionProp.GetValue(null, null);

        public bool GetKey(KeyCode key) => (bool)m_getKeyMethod.Invoke(null, new object[] { key });

        public bool GetKeyDown(KeyCode key) => (bool)m_getKeyDownMethod.Invoke(null, new object[] { key });

        public bool GetMouseButton(int btn) => (bool)m_getMouseButtonMethod.Invoke(null, new object[] { btn });

        public bool GetMouseButtonDown(int btn) => (bool)m_getMouseButtonDownMethod.Invoke(null, new object[] { btn });

        // UI Input module

        public BaseInputModule UIModule => m_inputModule;
        internal StandaloneInputModule m_inputModule;

        public PointerEventData InputPointerEvent => m_inputModule.m_InputPointerEvent;

        public void AddUIInputModule()
        {
            m_inputModule = new StandaloneInputModule();
        }

        public void ActivateModule()
        {
            m_inputModule.ActivateModule();
        }
    }
}
