using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BF = System.Reflection.BindingFlags;

namespace KeyboardUtilities
{
	internal static class ReflectionHelpers
	{
		public static BF CommonFlags = BF.Public | BF.Instance | BF.NonPublic | BF.Static;

		public static Type GetTypeByName(string fullName)
		{
			foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (var type in asm.TryGetTypes())
				{
					if (type.FullName == fullName)
					{
						return type;
					}
				}
			}

			return null;
		}

		public static IEnumerable<Type> TryGetTypes(this Assembly asm)
		{
			try
			{
				return asm.GetTypes();
			}
			catch (ReflectionTypeLoadException e)
			{
				try
				{
					return asm.GetExportedTypes();
				}
				catch
				{
					return e.Types.Where(t => t != null);
				}
			}
			catch
			{
				return Enumerable.Empty<Type>();
			}
		}

		internal static void TryLoadGameModules()
		{
			LoadModule("Assembly-CSharp");
			LoadModule("Assembly-CSharp-firstpass");
		}

		public static bool LoadModule(string module)
		{

			var path = $@"MelonLoader\Managed\{module}.dll";

			return LoadModuleInternal(path);
		}

		internal static bool LoadModuleInternal(string fullPath)
		{
			if (!File.Exists(fullPath))
				return false;

			try
			{
				Assembly.Load(File.ReadAllBytes(fullPath));
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.GetType() + ", " + e.Message);
			}

			return false;
		}
	}
}
