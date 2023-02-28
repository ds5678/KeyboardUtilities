﻿using System.Reflection;
using UnityEngine;

namespace KeyboardUtilities;

internal sealed class LegacyInput : IHandleInput
{
	public LegacyInput()
	{
		Implementation.Log("Initializing Legacy Input support...");

		m_mousePositionProp = TInput.GetProperty("mousePosition");
		m_mouseScrollDeltaProp = TInput.GetProperty("mouseScrollDelta");
		m_getKeyMethod = TInput.GetMethod("GetKey", new Type[] { typeof(KeyCode) });
		m_getKeyDownMethod = TInput.GetMethod("GetKeyDown", new Type[] { typeof(KeyCode) });
		m_getKeyUpMethod = TInput.GetMethod("GetKeyUp", new Type[] { typeof(KeyCode) });
		m_getMouseButtonMethod = TInput.GetMethod("GetMouseButton", new Type[] { typeof(int) });
		m_getMouseButtonDownMethod = TInput.GetMethod("GetMouseButtonDown", new Type[] { typeof(int) });
		m_getMouseButtonUpMethod = TInput.GetMethod("GetMouseButtonUp", new Type[] { typeof(int) });
	}

	public static Type TInput => m_tInput ?? (m_tInput = ReflectionHelpers.GetTypeByName("UnityEngine.Input"));
	private static Type m_tInput;

	private static PropertyInfo m_mousePositionProp;
	private static PropertyInfo m_mouseScrollDeltaProp;
	private static MethodInfo m_getKeyMethod;
	private static MethodInfo m_getKeyDownMethod;
	private static MethodInfo m_getKeyUpMethod;
	private static MethodInfo m_getMouseButtonMethod;
	private static MethodInfo m_getMouseButtonDownMethod;
	private static MethodInfo m_getMouseButtonUpMethod;

	public bool GetKey(KeyCode key) => (bool)m_getKeyMethod.Invoke(null, new object[] { key });

	public bool GetKeyDown(KeyCode key) => (bool)m_getKeyDownMethod.Invoke(null, new object[] { key });

	public bool GetKeyUp(KeyCode key) => (bool)m_getKeyUpMethod.Invoke(null, new object[] { key });

	public bool GetMouseButton(int btn) => (bool)m_getMouseButtonMethod.Invoke(null, new object[] { btn });

	public bool GetMouseButtonDown(int btn) => (bool)m_getMouseButtonDownMethod.Invoke(null, new object[] { btn });

	public bool GetMouseButtonUp(int btn) => (bool)m_getMouseButtonUpMethod.Invoke(null, new object[] { btn });

	public Vector2 MousePosition => (Vector3)m_mousePositionProp.GetValue(null, null);

	public Vector2 MouseScrollDelta => (Vector2)m_mouseScrollDeltaProp.GetValue(null, null);
}
