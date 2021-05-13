using UnityEngine;

namespace KeyboardUtilities
{
	internal interface IHandleInput
	{
		bool GetKeyDown(KeyCode key);
		bool GetKey(KeyCode key);
		bool GetKeyUp(KeyCode key);

		bool GetMouseButtonDown(int btn);
		bool GetMouseButton(int btn);
		bool GetMouseButtonUp(int btn);

		Vector2 MousePosition { get; }
		Vector2 MouseScrollDelta { get; }
	}
}
