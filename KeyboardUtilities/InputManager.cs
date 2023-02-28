using UnityEngine;

namespace KeyboardUtilities
{
	public enum InputType
	{
		InputSystem,
		Legacy,
		None
	}

	public static class InputManager
	{
		public static InputType CurrentType { get; private set; }

		private static IHandleInput m_inputModule;

		/// <summary>
		/// Get the status of a key.
		/// </summary>
		/// <returns>True if the key was pressed during that frame. False otherwise.</returns>
		public static bool GetKeyDown(KeyCode key) => m_inputModule.GetKeyDown(key);
		/// <summary>
		/// Get the status of a key.
		/// </summary>
		/// <returns>True if the key is currently pressed. False otherwise.</returns>
		public static bool GetKey(KeyCode key) => m_inputModule.GetKey(key);
		/// <summary>
		/// Get the status of a key.
		/// </summary>
		/// <returns>True if the key was released during that frame. False otherwise.</returns>
		public static bool GetKeyUp(KeyCode key) => m_inputModule.GetKeyUp(key);

		/// <summary>
		/// Get the status of a mouse button.
		/// </summary>
		/// <param name="btn">0 : Left<br/>1 : Right<br/>2 : Middle</param>
		/// <returns>True if the button was pressed during that frame. False otherwise.</returns>
		public static bool GetMouseButtonDown(int btn) => m_inputModule.GetMouseButtonDown(btn);
		/// <summary>
		/// Get the status of a mouse button.
		/// </summary>
		/// <param name="btn">0 : Left<br/>1 : Right<br/>2 : Middle</param>
		/// <returns>True if the button is currently pressed. False otherwise.</returns>
		public static bool GetMouseButton(int btn) => m_inputModule.GetMouseButton(btn);
		/// <summary>
		/// Get the status of a mouse button.
		/// </summary>
		/// <param name="btn">0 : Left<br/>1 : Right<br/>2 : Middle</param>
		/// <returns>True if the button was released during that frame. False otherwise.</returns>
		public static bool GetMouseButtonUp(int btn) => m_inputModule.GetMouseButtonUp(btn);

		/// <summary>
		/// Gets the current position of the mouse cursor on the screen.
		/// </summary>
		/// <returns>A 2D vector ( horizontal , vertical )</returns>
		public static Vector2 GetMousePosition() => m_inputModule.MousePosition;
		/// <summary>
		/// Gets the integer change in the mouse wheel position during that frame.
		/// </summary>
		/// <returns>A 2D vector ( horizontal , vertical )<br/>Most mice only have vertical scrolling.</returns>
		public static Vector2 GetMouseScrollDelta() => m_inputModule.MouseScrollDelta;


		internal static void Init()
		{
			if (InputSystem.TKeyboard != null || (ReflectionHelpers.LoadModule("Unity.InputSystem") && InputSystem.TKeyboard != null))
			{
				m_inputModule = new InputSystem();
				CurrentType = InputType.InputSystem;
			}
			else if (LegacyInput.TInput != null || (ReflectionHelpers.LoadModule("UnityEngine.InputLegacyModule") && LegacyInput.TInput != null))
			{
				m_inputModule = new LegacyInput();
				CurrentType = InputType.Legacy;
			}

			if (m_inputModule == null)
			{
				Implementation.LogWarning("Could not find any Input module!");
				m_inputModule = new NoInput();
				CurrentType = InputType.None;
			}
		}
	}
}
