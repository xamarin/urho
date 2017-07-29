using System;
using System.Collections.Generic;
using System.Linq;
using Clang.Ast;
using ICSharpCode.NRefactory.CSharp;
using Sharpie.Bind;

namespace SharpieBinder
{
	/// <summary>
	/// Finds a few types that we use later to make decisions, and scans for methods for get/set patterns
	/// </summary>
	class ScanBaseTypes : AstVisitor
	{
		// 
		// These are the types that we have to lookup earlier, before we run the scan in CxxBinder
		//
		static public CXXRecordDecl UrhoRefCounted, EventHandlerType, UrhoObjectType;

		// Provides a way of mapping names to declarations, we load this as we process
		// and use this information later in CxxBinder
		public static Dictionary<string, CXXRecordDecl> nameToDecl = new Dictionary<string, CXXRecordDecl>();

		public override void VisitCXXRecordDecl(CXXRecordDecl decl, VisitKind visitKind)
		{
			if (visitKind != VisitKind.Enter || !decl.IsCompleteDefinition || decl.Name == null)
				return;

			nameToDecl[decl.QualifiedName] = decl;
			switch (decl.QualifiedName) {
				case "Urho3D::RefCounted":
					UrhoRefCounted = decl;
					break;
				case "Urho3D::Object":
					UrhoObjectType = decl;
					break;
				case "Urho3D::EventHandler":
					EventHandlerType = decl;
					break;
			}
		}

		public class GetterSetter
		{
			public CXXMethodDecl Getter, Setter;
			public TypeDeclaration HostType;
			public AstType MethodReturn;
			public bool SetMethodPublic;
			public string Name;
		}

		// typeName to propertyName to returnType to GetterSetter pairs
		public static Dictionary<string, Dictionary<string, Dictionary<QualType, GetterSetter>>> allProperties =
			new Dictionary<string, Dictionary<string, Dictionary<QualType, GetterSetter>>>();

		public override void VisitCXXMethodDecl(CXXMethodDecl decl, VisitKind visitKind)
		{
			if (visitKind != VisitKind.Enter)
				return;

			var isConstructor = decl is CXXConstructorDecl;
			if (decl is CXXDestructorDecl || isConstructor)
				return;

			if (decl.IsCopyAssignmentOperator || decl.IsMoveAssignmentOperator)
				return;

			if (decl.Parent == null)
				return;
			if (!decl.Parent.QualifiedName.StartsWith("Urho3D::"))
				return;

			// Only get methods prefixed with Get with no parameters
			// and Set methods that return void and take a single parameter
			var name = decl.Name;

			// Handle Get methods that are not really getters
			// This is a get method that does not get anything

			bool legacySetMethod = false;
			QualType type;
			if (name.StartsWith("Get") || name.StartsWith("Is")) {
				if (decl.Parameters.Count() != 0)
					return;
				if (decl.ReturnQualType.ToString() == "void")
					return;
				if (name == "IsElementEventSender" ||
					name == "IsOpen" ||
					name == "IsPressed")
					return;

				type = decl.ReturnQualType;
			} else if (name.StartsWith("Set")) {
				if (decl.Parameters.Count() != 1)
					return;
				if ((name == "SetTypeName" || name == "SetType") && decl.Parent.Name == "UnknownComponent")
					return;
				if (decl.Access != AccessSpecifier.Public)
					return;
				if (!(decl.ReturnQualType.Bind() is Sharpie.Bind.Types.VoidType))
				{
					legacySetMethod = true;
				}
				type = decl.Parameters.FirstOrDefault().QualType;
			} else
				return;

			Dictionary<string, Dictionary<QualType, GetterSetter>> typeProperties;
			if (!allProperties.TryGetValue(decl.Parent.Name, out typeProperties)) {
				typeProperties = new Dictionary<string, Dictionary<QualType, GetterSetter>>();
				allProperties[decl.Parent.Name] = typeProperties;
			}

			var propName = name.Substring(name.StartsWith("Is") ? 2 : 3);

			Dictionary<QualType, GetterSetter> property;

			if (!typeProperties.TryGetValue(propName, out property)) {
				property = new Dictionary<QualType, GetterSetter>();
				typeProperties[propName] = property;
			}
			GetterSetter gs;
			if (!property.TryGetValue(type, out gs)) {
				gs = new GetterSetter() { Name = propName };
			}
			if (legacySetMethod)
				gs.SetMethodPublic = legacySetMethod;

			if (name.StartsWith("Get") || name.StartsWith("Is")) {
				if (propName != decl.Parent.Name || propName == "Text")
					// do not generate Getter if propertyName equals to typename (Text type already has a workaround for this case)
					gs.Getter = decl;
			} else {
				gs.Setter = decl;
			}
			property[type] = gs;
		}

		// Contains a list of all methods that will be part of a property
		static Dictionary<CXXMethodDecl, GetterSetter> allPropertyMethods = new Dictionary<CXXMethodDecl, GetterSetter>();

		public static GetterSetter GetPropertyInfo(CXXMethodDecl decl)
		{
			GetterSetter gs;
			if (allPropertyMethods.TryGetValue(decl, out gs))
				return gs;
			return null;
		}

		//
		// After we collected the information, remove pairs that only had a setter, but no getter
		//
		public void PrepareProperties()
		{
			var typeRemovals = new List<string>();
			foreach (var typeKV in allProperties) {
				var propertyRemovals = new List<string>();
				foreach (var propNameKV in typeKV.Value) {
					var qualTypeRemoval = new List<QualType>();
					foreach (var propTypeKV in propNameKV.Value) {
						if (propTypeKV.Value.Getter == null)
							qualTypeRemoval.Add(propTypeKV.Key);
					}
					foreach (var qualType in qualTypeRemoval)
						propNameKV.Value.Remove(qualType);
					if (propNameKV.Value.Count == 0)
						propertyRemovals.Add(propNameKV.Key);
				}
				foreach (var property in propertyRemovals)
					typeKV.Value.Remove(property);

				if (typeKV.Value.Count == 0)
					typeRemovals.Add(typeKV.Key);
			}
			foreach (var type in typeRemovals)
				allProperties.Remove(type);

			foreach (var typeKV in allProperties) {
				foreach (var propNameKV in typeKV.Value) {
					foreach (var gs in propNameKV.Value.Values) {
						allPropertyMethods[gs.Getter] = gs;
						if (gs.Setter != null)
							allPropertyMethods[gs.Setter] = gs;
					}
				}
			}
		}
	}
}