using System.Reflection;
using UnityEngine;

namespace KeyboardUtilities;

internal sealed class LegacyInput : IHandleInput
{
	public LegacyInput()
	{
		Implementation.Log("Initializing Legacy Input support...");

		TInput = ReflectionHelpers.GetTypeByName("UnityEngine.Input") ?? throw new NullReferenceException(nameof(TInput));

		m_mousePositionProp = TInput.GetProperty("mousePosition") ?? throw new NullReferenceException(nameof(m_mousePositionProp));
		m_mouseScrollDeltaProp = TInput.GetProperty("mouseScrollDelta") ?? throw new NullReferenceException(nameof(m_mouseScrollDeltaProp));
		m_getKeyMethod = TInput.GetMethod("GetKey", new Type[] { typeof(KeyCode) }) ?? throw new NullReferenceException(nameof(m_getKeyMethod));
		m_getKeyDownMethod = TInput.GetMethod("GetKeyDown", new Type[] { typeof(KeyCode) }) ?? throw new NullReferenceException(nameof(m_getKeyDownMethod));
		m_getKeyUpMethod = TInput.GetMethod("GetKeyUp", new Type[] { typeof(KeyCode) }) ?? throw new NullReferenceException(nameof(m_getKeyUpMethod));
		m_getMouseButtonMethod = TInput.GetMethod("GetMouseButton", new Type[] { typeof(int) }) ?? throw new NullReferenceException(nameof(m_getMouseButtonMethod));
		m_getMouseButtonDownMethod = TInput.GetMethod("GetMouseButtonDown", new Type[] { typeof(int) }) ?? throw new NullReferenceException(nameof(m_getMouseButtonDownMethod));
		m_getMouseButtonUpMethod = TInput.GetMethod("GetMouseButtonUp", new Type[] { typeof(int) }) ?? throw new NullReferenceException(nameof(m_getMouseButtonUpMethod));
	}

	public Type TInput { get; }

	private readonly PropertyInfo m_mousePositionProp;
	private readonly PropertyInfo m_mouseScrollDeltaProp;
	private readonly MethodInfo m_getKeyMethod;
	private readonly MethodInfo m_getKeyDownMethod;
	private readonly MethodInfo m_getKeyUpMethod;
	private readonly MethodInfo m_getMouseButtonMethod;
	private readonly MethodInfo m_getMouseButtonDownMethod;
	private readonly MethodInfo m_getMouseButtonUpMethod;

	public bool GetKey(KeyCode key) => (bool?)m_getKeyMethod.Invoke(null, new object[] { key }) ?? throw new NullReferenceException();

	public bool GetKeyDown(KeyCode key) => (bool?)m_getKeyDownMethod.Invoke(null, new object[] { key }) ?? throw new NullReferenceException();

	public bool GetKeyUp(KeyCode key) => (bool?)m_getKeyUpMethod.Invoke(null, new object[] { key }) ?? throw new NullReferenceException();

	public bool GetMouseButton(int btn) => (bool?)m_getMouseButtonMethod.Invoke(null, new object[] { btn }) ?? throw new NullReferenceException();

	public bool GetMouseButtonDown(int btn) => (bool?)m_getMouseButtonDownMethod.Invoke(null, new object[] { btn }) ?? throw new NullReferenceException();

	public bool GetMouseButtonUp(int btn) => (bool?)m_getMouseButtonUpMethod.Invoke(null, new object[] { btn }) ?? throw new NullReferenceException();

	public Vector2 MousePosition => (Vector3?)m_mousePositionProp.GetValue(null, null) ?? throw new NullReferenceException();

	public Vector2 MouseScrollDelta => (Vector2?)m_mouseScrollDeltaProp.GetValue(null, null) ?? throw new NullReferenceException();
}
