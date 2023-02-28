using MelonLoader;
using System.Reflection;

[assembly: AssemblyTitle(SpacebarTestMod.BuildInfo.Name)]
[assembly: AssemblyDescription(SpacebarTestMod.BuildInfo.Description)]
[assembly: AssemblyCompany(SpacebarTestMod.BuildInfo.Company)]
[assembly: AssemblyProduct(SpacebarTestMod.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + SpacebarTestMod.BuildInfo.Author)]
[assembly: AssemblyTrademark(SpacebarTestMod.BuildInfo.Company)]
[assembly: AssemblyCulture("")]

[assembly: AssemblyVersion(SpacebarTestMod.BuildInfo.Version)]
[assembly: AssemblyFileVersion(SpacebarTestMod.BuildInfo.Version)]
[assembly: MelonInfo(typeof(SpacebarTestMod.Implementation), SpacebarTestMod.BuildInfo.Name, SpacebarTestMod.BuildInfo.Version, SpacebarTestMod.BuildInfo.Author, SpacebarTestMod.BuildInfo.DownloadLink)]
[assembly: MelonGame(null, null)]