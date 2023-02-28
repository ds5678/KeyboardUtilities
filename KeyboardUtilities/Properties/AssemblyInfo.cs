using MelonLoader;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle(KeyboardUtilities.BuildInfo.Name)]
[assembly: AssemblyDescription(KeyboardUtilities.BuildInfo.Description)]
[assembly: AssemblyCompany(KeyboardUtilities.BuildInfo.Company)]
[assembly: AssemblyProduct(KeyboardUtilities.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + KeyboardUtilities.BuildInfo.Author)]
[assembly: AssemblyTrademark(KeyboardUtilities.BuildInfo.Company)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("31e7efcb-d77e-40ba-8609-ea5f7d63e20b")]

[assembly: AssemblyVersion(KeyboardUtilities.BuildInfo.Version)]
[assembly: AssemblyFileVersion(KeyboardUtilities.BuildInfo.Version)]
[assembly: MelonInfo(typeof(KeyboardUtilities.Implementation), KeyboardUtilities.BuildInfo.Name, KeyboardUtilities.BuildInfo.Version, KeyboardUtilities.BuildInfo.Author, KeyboardUtilities.BuildInfo.DownloadLink)]
[assembly: MelonGame(null, null)]