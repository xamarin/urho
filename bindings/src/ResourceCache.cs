//
// ResourceCache C# sugar
//
// Authors:
//   Miguel de Icaza (miguel@xamarin.com)
//
// Copyrigh 2015 Xamarin INc
//
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Urho {

	public partial class ResourceCache {
		static Dictionary<System.Type,ConstructorInfo> ctorMap;
		ConstructorInfo GetCtor (System.Type t)
		{
			if (ctorMap == null)
				ctorMap = new Dictionary<System.Type,ConstructorInfo> ();
			ConstructorInfo ctor;
			if (ctorMap.TryGetValue (t, out ctor))
				return ctor;
			ctor = t.GetConstructor (new System.Type [] {typeof(IntPtr)});
			ctorMap [t] = ctor;
			return ctor;
		}

#region Convenience methods to get resources
		T GetResource<T> (StringHash type, string name, bool sendEventOnFailure) where T:UrhoObject
		{
			var ptr = ResourceCache_GetResource (handle, type, name, sendEventOnFailure);
			var ctor = GetCtor (typeof (T));
			return (T) ctor.Invoke (null, new object [] { ptr });
		}

		public Sound GetSound (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Sound> (Sound.TypeStatic, name, sendEventOnFailure);
		}
		
		public Animation GetAnimation (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Animation> (Animation.TypeStatic, name, sendEventOnFailure);
		}
		
		public Material GetMaterial (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Material> (Material.TypeStatic, name, sendEventOnFailure);
		}
		
		public Model GetModel (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Model> (Model.TypeStatic, name, sendEventOnFailure);
		}
		
		public ParticleEffect GetParticleEffect (string name, bool sendEventOnFailure = true)
		{
			return GetResource<ParticleEffect> (ParticleEffect.TypeStatic, name, sendEventOnFailure);
		}
		
		public Shader GetShader (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Shader> (Shader.TypeStatic, name, sendEventOnFailure);
		}
		
		public Technique GetTechnique (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Technique> (Technique.TypeStatic, name, sendEventOnFailure);
		}
		
		public Image GetImage (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Image> (Image.TypeStatic, name, sendEventOnFailure);
		}
		public PListFile GetPListFile (string name, bool sendEventOnFailure = true)
		{
			return GetResource<PListFile> (PListFile.TypeStatic, name, sendEventOnFailure);
		}
		
		public XMLFile GetXMLFile (string name, bool sendEventOnFailure = true)
		{
			return GetResource<XMLFile> (XMLFile.TypeStatic, name, sendEventOnFailure);
		}
		
		public ObjectAnimation GetObjectAnimation (string name, bool sendEventOnFailure = true)
		{
			return GetResource<ObjectAnimation> (ObjectAnimation.TypeStatic, name, sendEventOnFailure);
		}
		
		public ValueAnimation GetValueAnimation (string name, bool sendEventOnFailure = true)
		{
			return GetResource<ValueAnimation> (ValueAnimation.TypeStatic, name, sendEventOnFailure);
		}
		
#if false
		public LuaFile GetLuaFile (string name, bool sendEventOnFailure = true)
		{
			return GetResource<LuaFile> (LuaFile.TypeStatic, name, sendEventOnFailure);
		}
		
		public JSONFile GetJSONFile (string name, bool sendEventOnFailure = true)
		{
			return GetResource<JSONFile> (JSONFile.TypeStatic, name, sendEventOnFailure);
		}
		
		public ScriptFile GetScriptFile (string name, bool sendEventOnFailure = true)
		{
			return GetResource<ScriptFile> (ScriptFile.TypeStatic, name, sendEventOnFailure);
		}
#endif
		
		public Font GetFont (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Font> (Font.TypeStatic, name, sendEventOnFailure);
		}
		
		public AnimationSet2D GetAnimationSet2D (string name, bool sendEventOnFailure = true)
		{
			return GetResource<AnimationSet2D> (AnimationSet2D.TypeStatic, name, sendEventOnFailure);
		}
		
		public ParticleEffect2D GetParticleEffect2D (string name, bool sendEventOnFailure = true)
		{
			return GetResource<ParticleEffect2D> (ParticleEffect2D.TypeStatic, name, sendEventOnFailure);
		}
		
		public Sprite2D GetSprite2D (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Sprite2D> (Sprite2D.TypeStatic, name, sendEventOnFailure);
		}
		
		public SpriteSheet2D GetSpriteSheet2D (string name, bool sendEventOnFailure = true)
		{
			return GetResource<SpriteSheet2D> (SpriteSheet2D.TypeStatic, name, sendEventOnFailure);
		}
		
		public TmxFile2D GetTmxFile2D (string name, bool sendEventOnFailure = true)
		{
			return GetResource<TmxFile2D> (TmxFile2D.TypeStatic, name, sendEventOnFailure);
		}
#endregion
	}
}
