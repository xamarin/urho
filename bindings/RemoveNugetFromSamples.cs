using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

/*
	Changes nuget references to ProjectReferences in FeatureSamples.
	Nuget doesn't support ItemGroups with conditions (it will overwrite them on the next package update) so that's why this script exists
	By default, this utility removes urho.targets imports with native libs, use "-refsonly" arg to replace only <Reference>s
*/

class P
{
	static XmlNamespaceManager nsManager;
	static XNamespace ns;
	static bool onlyRefs;

	static void Main(string[] args)
	{
		if (args.Length > 0 && args[0] == "-refsonly")
			onlyRefs = true;

		var nsStr = "http://schemas.microsoft.com/developer/msbuild/2003";
		nsManager = new XmlNamespaceManager(new NameTable());
		nsManager.AddNamespace("p", nsStr);
		ns = nsStr;

		//FeatureSamples.Core
		var coreCsproj = @"Samples/FeatureSamples/Core/Urho.Samples.csproj";
		var coreDoc = XDocument.Load(coreCsproj);
		ReplaceRef(coreDoc, "Urho", "{641886db-2c6c-4d33-88da-97bec0ec5f86}", @"..\..\..\bindings\Urho.csproj", "Urho");
		coreDoc.Save(coreCsproj);

		//FeatureSamples.Droid
		var droidCsproj = @"Samples/FeatureSamples/Android/Urho.Samples.Droid.csproj";
		var droidDoc = XDocument.Load(droidCsproj);
		ReplaceRef(droidDoc, "Urho", "{641886db-2c6c-4d33-88da-97bec0ec5f86}", @"..\..\..\bindings\Urho.csproj", "Urho");
		ReplaceRef(droidDoc, "Urho.Droid", "{f0c1189b-75f7-4bd8-b394-a23d5b03eff6}", @"..\..\..\Bindings\Android\Urho.Droid.csproj", "Urho.Droid");
		ReplaceRef(droidDoc, "Urho.Droid.SdlBindings", "{9438f1bb-e800-48c0-95ce-f158a60abd36}", @"..\..\..\Bindings\Android\Urho.Android.SdlBindings\Urho.Droid.SdlBindings.csproj", "Urho.Droid.SdlBindings");
		RemoveTargets(droidDoc);
		droidDoc.Save(droidCsproj);

		//FeatureSamples.iOS
		var iosCsproj = @"Samples/FeatureSamples/iOS/Urho.Samples.iOS.csproj";
		var iosDoc = XDocument.Load(iosCsproj);
		ReplaceRef(iosDoc, "Urho", "{641886db-2c6c-4d33-88da-97bec0ec5f86}", @"..\..\..\bindings\Urho.csproj", "Urho");
		ReplaceRef(iosDoc, "Urho.iOS", "{9ae80bd9-e1e2-41da-bb6f-712b35028bd9}", @"..\..\..\Bindings\iOS\Urho.iOS.csproj", "Urho.iOS");
		RemoveTargets(iosDoc);
		iosDoc.Save(iosCsproj);

		//FeatureSamples.Desktop
		var desktopCsproj = @"Samples/FeatureSamples/Desktop/Urho.Samples.Desktop.csproj";
		var desktopDoc = XDocument.Load(desktopCsproj);
		ReplaceRef(desktopDoc, "Urho", "{641886db-2c6c-4d33-88da-97bec0ec5f86}", @"..\..\..\bindings\Urho.csproj", "Urho");
		ReplaceRef(desktopDoc, "Urho.Desktop", "{F0359D5E-D6D4-47D3-A9F0-5A97C31DC476}", @"..\..\..\Bindings\Desktop\Urho.Desktop.csproj", "Urho.Desktop");
		RemoveTargets(desktopDoc);
		desktopDoc.Save(desktopCsproj);
	}

	static void ReplaceRef(XDocument doc, string refName, string id, string path, string name)
	{
		var node = doc.XPathSelectElement($"//p:Reference[@Include='{refName}']", nsManager);
		if (node == null)//means it's already converted to ProjectReference
			return;
		node.Parent.Add(new XElement(ns + "ProjectReference", new XAttribute("Include", path),
			new XElement(ns + "Project", id),
			new XElement(ns + "Name", name)));
		node.Remove();
	}

	static void RemoveTargets(XDocument doc)
	{
		if (onlyRefs) return;
		doc.XPathSelectElement(@"//p:Import[contains(@Project, 'packages\UrhoSharp')]", nsManager)?.Remove();
		doc.XPathSelectElement(@"//p:Error[contains(@Condition, 'packages\UrhoSharp')]", nsManager)?.Remove();
	}
}
