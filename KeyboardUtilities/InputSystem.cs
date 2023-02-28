using System.Reflection;
using UnityEngine;

namespace KeyboardUtilities;

internal sealed class InputSystem : IHandleInput
{
	public InputSystem()
	{
		Implementation.Log("Initializing new InputSystem support...");

		TKeyboard = ReflectionHelpers.GetTypeByName("UnityEngine.InputSystem.Keyboard") ?? throw new NullReferenceException(nameof(TKeyboard));
		TMouse = ReflectionHelpers.GetTypeByName("UnityEngine.InputSystem.Mouse") ?? throw new NullReferenceException(nameof(TMouse));
		TKey = ReflectionHelpers.GetTypeByName("UnityEngine.InputSystem.Key") ?? throw new NullReferenceException(nameof(TKey));

		m_kbCurrentProp = TKeyboard.GetProperty("current") ?? throw new NullReferenceException(nameof(m_kbCurrentProp));
		CurrentKeyboard = m_kbCurrentProp.GetValue(null, null) ?? throw new NullReferenceException(nameof(CurrentKeyboard));
		m_kbIndexer = TKeyboard.GetProperty("Item", new Type[] { TKey }) ?? throw new NullReferenceException(nameof(m_kbIndexer));

		Type btnControl = ReflectionHelpers.GetTypeByName("UnityEngine.InputSystem.Controls.ButtonControl") ?? throw new NullReferenceException(nameof(btnControl));
		m_btnIsPressedProp = btnControl.GetProperty("isPressed") ?? throw new NullReferenceException(nameof(m_btnIsPressedProp));
		m_btnWasPressedProp = btnControl.GetProperty("wasPressedThisFrame") ?? throw new NullReferenceException(nameof(m_btnWasPressedProp));
		m_btnWasReleasedProp = btnControl.GetProperty("wasReleasedThisFrame") ?? throw new NullReferenceException(nameof(m_btnWasReleasedProp));

		PropertyInfo m_mouseCurrentProp = TMouse.GetProperty("current") ?? throw new NullReferenceException(nameof(m_mouseCurrentProp));
		CurrentMouse = m_mouseCurrentProp.GetValue(null, null) ?? throw new NullReferenceException(nameof(CurrentMouse));

		PropertyInfo m_leftButtonProp = TMouse.GetProperty("leftButton") ?? throw new NullReferenceException(nameof(m_leftButtonProp));
		LeftMouseButton = m_leftButtonProp.GetValue(CurrentMouse, null) ?? throw new NullReferenceException(nameof(LeftMouseButton));

		PropertyInfo m_rightButtonProp = TMouse.GetProperty("rightButton") ?? throw new NullReferenceException(nameof(m_rightButtonProp));
		RightMouseButton = m_rightButtonProp.GetValue(CurrentMouse, null) ?? throw new NullReferenceException(nameof(RightMouseButton));

		PropertyInfo m_middleButtonProp = TMouse.GetProperty("middleButton") ?? throw new NullReferenceException(nameof(m_middleButtonProp));
		MiddleMouseButton = m_middleButtonProp.GetValue(CurrentMouse, null) ?? throw new NullReferenceException(nameof(MiddleMouseButton));

		PropertyInfo m_scrollProp = TMouse.GetProperty("scroll") ?? throw new NullReferenceException(nameof(m_kbCurrentProp));
		MouseScrollInfo = m_scrollProp.GetValue(CurrentMouse, null) ?? throw new NullReferenceException(nameof(MouseScrollInfo));

		m_positionProp = ReflectionHelpers.GetTypeByName("UnityEngine.InputSystem.Pointer")?
			.GetProperty("position")
			?? throw new NullReferenceException(nameof(m_kbCurrentProp));
		MousePositionInfo = m_positionProp.GetValue(CurrentMouse, null) ?? throw new NullReferenceException(nameof(MousePositionInfo));

		m_readVector2InputMethod = ReflectionHelpers.GetTypeByName("UnityEngine.InputSystem.InputControl`1")?
			.MakeGenericType(typeof(Vector2))
			.GetMethod("ReadValue")
			?? throw new NullReferenceException(nameof(m_kbCurrentProp));
	}

	public Type TKeyboard { get; }

	public Type TMouse { get; }

	public Type TKey { get; }

	private readonly PropertyInfo m_btnIsPressedProp;
	private readonly PropertyInfo m_btnWasPressedProp;
	private readonly PropertyInfo m_btnWasReleasedProp;

	private object CurrentKeyboard { get; }

	private readonly PropertyInfo m_kbCurrentProp;
	private readonly PropertyInfo m_kbIndexer;

	private object CurrentMouse { get; }

	private object LeftMouseButton { get; }

	private object RightMouseButton { get; }

	private object MiddleMouseButton { get; }

	private object MouseScrollInfo { get; }

	private object MousePositionInfo { get; }

	private readonly PropertyInfo m_positionProp;
	private readonly MethodInfo m_readVector2InputMethod;

	internal Dictionary<KeyCode, object> ActualKeyDict { get; } = new();
	internal Dictionary<string, string> enumNameFixes = new()
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
			string s = key.ToString();
			try
			{
				if (enumNameFixes.First(it => s.Contains(it.Key)) is KeyValuePair<string, string> entry)
				{
					s = s.Replace(entry.Key, entry.Value);
				}
			}
			catch { }

			object parsed = Enum.Parse(TKey, s);
			object actualKey = m_kbIndexer.GetValue(CurrentKeyboard, new object[] { parsed }) ?? throw new NullReferenceException();

			ActualKeyDict.Add(key, actualKey);
		}

		return ActualKeyDict[key];
	}

	public bool GetKeyDown(KeyCode key) => (bool?)m_btnWasPressedProp.GetValue(GetActualKey(key), null) ?? throw new NullReferenceException();

	public bool GetKey(KeyCode key) => (bool?)m_btnIsPressedProp.GetValue(GetActualKey(key), null) ?? throw new NullReferenceException();

	public bool GetKeyUp(KeyCode key) => (bool?)m_btnWasReleasedProp.GetValue(GetActualKey(key), null) ?? throw new NullReferenceException();

	public bool GetMouseButtonDown(int btn)
	{
		return btn switch
		{
			0 => (bool?)m_btnWasPressedProp.GetValue(LeftMouseButton, null),
			1 => (bool?)m_btnWasPressedProp.GetValue(RightMouseButton, null),
			2 => (bool?)m_btnWasPressedProp.GetValue(MiddleMouseButton, null),
			_ => throw new NotImplementedException(),
		} ?? throw new NullReferenceException();
	}

	public bool GetMouseButton(int btn)
	{
		return btn switch
		{
			0 => (bool?)m_btnIsPressedProp.GetValue(LeftMouseButton, null),
			1 => (bool?)m_btnIsPressedProp.GetValue(RightMouseButton, null),
			2 => (bool?)m_btnIsPressedProp.GetValue(MiddleMouseButton, null),
			_ => throw new NotImplementedException(),
		} ?? throw new NullReferenceException();
	}

	public bool GetMouseButtonUp(int btn)
	{
		return btn switch
		{
			0 => (bool?)m_btnWasReleasedProp.GetValue(LeftMouseButton, null),
			1 => (bool?)m_btnWasReleasedProp.GetValue(RightMouseButton, null),
			2 => (bool?)m_btnWasReleasedProp.GetValue(MiddleMouseButton, null),
			_ => throw new NotImplementedException(),
		} ?? throw new NullReferenceException();
	}

	public Vector2 MousePosition
	{
		get
		{
			try
			{
				return (Vector2?)m_readVector2InputMethod.Invoke(MousePositionInfo, Array.Empty<object>()) ?? throw new NullReferenceException();
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
				return (Vector2?)m_readVector2InputMethod.Invoke(MouseScrollInfo, Array.Empty<object>()) ?? throw new NullReferenceException();
			}
			catch
			{
				return Vector2.zero;
			}
		}
	}
}
