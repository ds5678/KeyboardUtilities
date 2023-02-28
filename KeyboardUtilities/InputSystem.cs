using System.Reflection;
using UnityEngine;

namespace KeyboardUtilities;

internal sealed class InputSystem : IHandleInput
{
	public InputSystem()
	{
		Implementation.Log("Initializing new InputSystem support...");

		m_kbCurrentProp = TKeyboard.GetProperty("current");
		m_kbIndexer = TKeyboard.GetProperty("Item", new Type[] { TKey });

		var btnControl = ReflectionHelpers.GetTypeByName("UnityEngine.InputSystem.Controls.ButtonControl");
		m_btnIsPressedProp = btnControl.GetProperty("isPressed");
		m_btnWasPressedProp = btnControl.GetProperty("wasPressedThisFrame");
		m_btnWasReleasedProp = btnControl.GetProperty("wasReleasedThisFrame");

		m_mouseCurrentProp = TMouse.GetProperty("current");
		m_leftButtonProp = TMouse.GetProperty("leftButton");
		m_rightButtonProp = TMouse.GetProperty("rightButton");
		m_middleButtonProp = TMouse.GetProperty("middleButton");
		m_scrollProp = TMouse.GetProperty("scroll");

		m_positionProp = ReflectionHelpers.GetTypeByName("UnityEngine.InputSystem.Pointer")
						.GetProperty("position");

		m_readVector2InputMethod = ReflectionHelpers.GetTypeByName("UnityEngine.InputSystem.InputControl`1")
								  .MakeGenericType(typeof(Vector2))
								  .GetMethod("ReadValue");
	}

	public static Type TKeyboard => m_tKeyboard ?? (m_tKeyboard = ReflectionHelpers.GetTypeByName("UnityEngine.InputSystem.Keyboard"));
	private static Type m_tKeyboard;

	public static Type TMouse => m_tMouse ?? (m_tMouse = ReflectionHelpers.GetTypeByName("UnityEngine.InputSystem.Mouse"));
	private static Type m_tMouse;

	public static Type TKey => m_tKey ?? (m_tKey = ReflectionHelpers.GetTypeByName("UnityEngine.InputSystem.Key"));
	private static Type m_tKey;

	private static PropertyInfo m_btnIsPressedProp;
	private static PropertyInfo m_btnWasPressedProp;
	private static PropertyInfo m_btnWasReleasedProp;

	private static object CurrentKeyboard => m_currentKeyboard ?? (m_currentKeyboard = m_kbCurrentProp.GetValue(null, null));
	private static object m_currentKeyboard;
	private static PropertyInfo m_kbCurrentProp;
	private static PropertyInfo m_kbIndexer;

	private static object CurrentMouse => m_currentMouse ?? (m_currentMouse = m_mouseCurrentProp.GetValue(null, null));
	private static object m_currentMouse;
	private static PropertyInfo m_mouseCurrentProp;

	private static object LeftMouseButton => m_lmb ?? (m_lmb = m_leftButtonProp.GetValue(CurrentMouse, null));
	private static object m_lmb;
	private static PropertyInfo m_leftButtonProp;

	private static object RightMouseButton => m_rmb ?? (m_rmb = m_rightButtonProp.GetValue(CurrentMouse, null));
	private static object m_rmb;
	private static PropertyInfo m_rightButtonProp;

	private static object MiddleMouseButton => m_mmb ?? (m_mmb = m_middleButtonProp.GetValue(CurrentMouse, null));
	private static object m_mmb;
	private static PropertyInfo m_middleButtonProp;

	private static object MouseScrollInfo => m_scroll ?? (m_scroll = m_scrollProp.GetValue(CurrentMouse, null));
	private static object m_scroll;
	private static PropertyInfo m_scrollProp;

	private static object MousePositionInfo => m_pos ?? (m_pos = m_positionProp.GetValue(CurrentMouse, null));
	private static object m_pos;
	private static PropertyInfo m_positionProp;
	private static MethodInfo m_readVector2InputMethod;

	internal static Dictionary<KeyCode, object> ActualKeyDict = new Dictionary<KeyCode, object>();
	internal static Dictionary<string, string> enumNameFixes = new Dictionary<string, string>
	{
		{ "Control", "Ctrl" },
		{ "Return", "Enter" },
		{ "Alpha", "Digit" },
		{ "Keypad", "Numpad" },
		{ "Numlock", "NumLock" },
		{ "Print", "PrintScreen" },
		{ "BackQuote", "Backquote" }
	};

	internal object GetActualKey(KeyCode key)
	{
		if (!ActualKeyDict.ContainsKey(key))
		{
			var s = key.ToString();
			try
			{
				if (enumNameFixes.First(it => s.Contains(it.Key)) is KeyValuePair<string, string> entry)
					s = s.Replace(entry.Key, entry.Value);
			}
			catch { }

			var parsed = Enum.Parse(TKey, s);
			var actualKey = m_kbIndexer.GetValue(CurrentKeyboard, new object[] { parsed });

			ActualKeyDict.Add(key, actualKey);
		}

		return ActualKeyDict[key];
	}

	public bool GetKeyDown(KeyCode key) => (bool)m_btnWasPressedProp.GetValue(GetActualKey(key), null);

	public bool GetKey(KeyCode key) => (bool)m_btnIsPressedProp.GetValue(GetActualKey(key), null);

	public bool GetKeyUp(KeyCode key) => (bool)m_btnWasReleasedProp.GetValue(GetActualKey(key), null);

	public bool GetMouseButtonDown(int btn)
	{
		switch (btn)
		{
			case 0: return (bool)m_btnWasPressedProp.GetValue(LeftMouseButton, null);
			case 1: return (bool)m_btnWasPressedProp.GetValue(RightMouseButton, null);
			case 2: return (bool)m_btnWasPressedProp.GetValue(MiddleMouseButton, null);
			default: throw new NotImplementedException();
		}
	}

	public bool GetMouseButton(int btn)
	{
		switch (btn)
		{
			case 0: return (bool)m_btnIsPressedProp.GetValue(LeftMouseButton, null);
			case 1: return (bool)m_btnIsPressedProp.GetValue(RightMouseButton, null);
			case 2: return (bool)m_btnIsPressedProp.GetValue(MiddleMouseButton, null);
			default: throw new NotImplementedException();
		}
	}

	public bool GetMouseButtonUp(int btn)
	{
		switch (btn)
		{
			case 0: return (bool)m_btnWasReleasedProp.GetValue(LeftMouseButton, null);
			case 1: return (bool)m_btnWasReleasedProp.GetValue(RightMouseButton, null);
			case 2: return (bool)m_btnWasReleasedProp.GetValue(MiddleMouseButton, null);
			default: throw new NotImplementedException();
		}
	}

	public Vector2 MousePosition
	{
		get
		{
			try
			{
				return (Vector2)m_readVector2InputMethod.Invoke(MousePositionInfo, new object[0]);
			}
			catch
			{
				return Vector2.zero;
			}
		}
	}

	public Vector2 MouseScrollDelta
	{
		get
		{
			try
			{
				return (Vector2)m_readVector2InputMethod.Invoke(MouseScrollInfo, new object[0]);
			}
			catch
			{
				return Vector2.zero;
			}
		}
	}
}
