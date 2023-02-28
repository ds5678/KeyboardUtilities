using MelonLoader;
using System.Reflection;

[assembly: AssemblyTitle(KeyboardUtilities.BuildInfo.Name)]
[assembly: AssemblyDescription(KeyboardUtilities.BuildInfo.Description)]
[assembly: AssemblyCompany(KeyboardUtilities.BuildInfo.Company)]
[assembly: AssemblyProduct(KeyboardUtilities.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + KeyboardUtilities.BuildInfo.Author)]
[assembly: AssemblyTrademark(KeyboardUtilities.BuildInfo.Company)]
[assembly: AssemblyCulture("")]

[assembly: AssemblyVersion(KeyboardUtilities.BuildInfo.Version)]
[assembly: AssemblyFileVersion(KeyboardUtilities.BuildInfo.Version)]
[assembly: MelonInfo(typeof(KeyboardUtilities.Implementation), KeyboardUtilities.BuildInfo.Name, KeyboardUtilities.BuildInfo.Version, KeyboardUtilities.BuildInfo.Author, KeyboardUtilities.BuildInfo.DownloadLink)]
[assembly: MelonGame(null, null)]