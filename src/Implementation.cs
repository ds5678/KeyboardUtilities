using MelonLoader;
using UnityEngine;

namespace KeyboardUtilities
{
    public class Implementation : MelonMod
    {
        public override void OnApplicationStart()
        {
            Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
            ReflectionHelpers.TryLoadGameModules();
            InputManager.Init();
        }


        internal static void Log(string message)
        {
            MelonLogger.Log(message);
        }

        internal static void Log(string message, params object[] parameters)
        {
            string preformattedMessage = string.Format(message, parameters);
            Log(preformattedMessage);
        }

        internal static void LogWarning(string message)
        {
            MelonLogger.LogWarning(message);
        }
    }
}
