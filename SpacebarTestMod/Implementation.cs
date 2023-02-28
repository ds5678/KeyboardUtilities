using KeyboardUtilities;
using MelonLoader;
using UnityEngine;

namespace SpacebarTestMod;
public static class BuildInfo
{
	public const string Name = "SpacebarTestMod"; // Name of the Mod.  (MUST BE SET)
	public const string Description = "A test mod for KeyboardUtilities."; // Description for the Mod.  (Set as null if none)
	public const string Author = "ds5678"; // Author of the Mod.  (MUST BE SET)
	public const string Company = null; // Company that made the Mod.  (Set as null if none)
	public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
	public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
}
internal class Implementation : MelonMod
{
	public override void OnUpdate()
	{
		if (InputManager.GetKeyDown(KeyCode.Space))
		{
			LoggerInstance.Msg("Pressed");
		}
		else if (InputManager.GetKeyUp(KeyCode.Space))
		{
			LoggerInstance.Msg("Released");
		}
		else if (InputManager.GetKey(KeyCode.Space))
		{
			LoggerInstance.Msg("Held");
		}
	}
}