using MelonLoader;

namespace KeyboardUtilities;

internal static class BuildInfo
{
	public const string Name = "KeyboardUtilities"; // Name of the Mod.  (MUST BE SET)
	public const string Description = "A input system mod."; // Description for the Mod.  (Set as null if none)
	public const string Author = "Sinai, ds5678"; // Author of the Mod.  (MUST BE SET)
	public const string Company = null; // Company that made the Mod.  (Set as null if none)
	public const string Version = "2.0.0"; // Version of the Mod.  (MUST BE SET)
	public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
}
internal class Implementation : MelonMod
{
	public override void OnInitializeMelon()
	{
		ReflectionHelpers.TryLoadGameModules();
		InputManager.Init();
	}

	internal static void Log(string message) => MelonLogger.Log(message);
	internal static void LogWarning(string message) => MelonLogger.LogWarning(message);
}
