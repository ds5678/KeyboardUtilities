using MelonLoader;

namespace KeyboardUtilities;

public static class BuildInfo
{
	public const string Name = "KeyboardUtilities"; // Name of the Mod.  (MUST BE SET)
	public const string Description = "A input system mod."; // Description for the Mod.  (Set as null if none)
	public const string Author = "Sinai, ds5678"; // Author of the Mod.  (MUST BE SET)
	public const string Company = null; // Company that made the Mod.  (Set as null if none)
	public const string Version = "1.3.0"; // Version of the Mod.  (MUST BE SET)
	public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
}
internal class Implementation : MelonMod
{
	public override void OnApplicationStart()
	{
		ReflectionHelpers.TryLoadGameModules();
		InputManager.Init();
	}

	internal static void Log(string message, params object[] parameters) => MelonLogger.Log(message, parameters);
	internal static void LogWarning(string message, params object[] parameters) => MelonLogger.LogWarning(message, parameters);
	internal static void LogError(string message, params object[] parameters) => MelonLogger.LogError(message, parameters);
}
