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
		static Dictionary<Type,ConstructorInfo> ctorMap;
		ConstructorInfo GetCtor (Type t)
		{
			if (ctorMap == null)
				ctorMap = new Dictionary<Type,ConstructorInfo> ();
			if (ctorMap.TryGetValue (t, out ctor))
				return ctor;
			ctor = t.GetConstructor (new Type {typeof(IntPtr)});
			ctorMap [t] = ctor;
			return ctor;
		}

#region Convenience methods to get resources
		T GetResource<T> (StringHash type, string name) where T:Resource
		{
			var ptr = ResourceCache_GetResource (type, name, sendEventOnFailure);
			var ctor = GetCtor (typeof (T));
			return (T) ctor.Invoke (null, new object [] { ptr });
		}

		public Sound GetSound (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Sound);
		}
		
		public Animation GetAnimation (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Animation> (Animation.GetTypeStatic(), sendEventOnFailure);
		}
		
		public Sound GetSound (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Sound> (Sound.GetTypeStatic (), sendEventOnFailure);
		}
		
		public Animation GetAnimation (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Animation> (Animation.GetTypeStatic (), sendEventOnFailure);
		}
		
		public Material GetMaterial (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Material> (Material.GetTypeStatic (), sendEventOnFailure);
		}
		
		public Model GetModel (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Model> (Model.GetTypeStatic (), sendEventOnFailure);
		}
		
		public ParticleEffect GetParticleEffect (string name, bool sendEventOnFailure = true)
		{
			return GetResource<ParticleEffect> (ParticleEffect.GetTypeStatic (), sendEventOnFailure);
		}
		
		public Shader GetShader (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Shader> (Shader.GetTypeStatic (), sendEventOnFailure);
		}
		
		public Technique GetTechnique (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Technique> (Technique.GetTypeStatic (), sendEventOnFailure);
		}
		
		public LuaFile GetLuaFile (string name, bool sendEventOnFailure = true)
		{
			return GetResource<LuaFile> (LuaFile.GetTypeStatic (), sendEventOnFailure);
		}
		
		public Image GetImage (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Image> (Image.GetTypeStatic (), sendEventOnFailure);
		}
		
		public JSONFile GetJSONFile (string name, bool sendEventOnFailure = true)
		{
			return GetResource<JSONFile> (JSONFile.GetTypeStatic (), sendEventOnFailure);
		}
		
		public PListFile GetPListFile (string name, bool sendEventOnFailure = true)
		{
			return GetResource<PListFile> (PListFile.GetTypeStatic (), sendEventOnFailure);
		}
		
		public XMLFile GetXMLFile (string name, bool sendEventOnFailure = true)
		{
			return GetResource<XMLFile> (XMLFile.GetTypeStatic (), sendEventOnFailure);
		}
		
		public ObjectAnimation GetObjectAnimation (string name, bool sendEventOnFailure = true)
		{
			return GetResource<ObjectAnimation> (ObjectAnimation.GetTypeStatic (), sendEventOnFailure);
		}
		
		public ValueAnimation GetValueAnimation (string name, bool sendEventOnFailure = true)
		{
			return GetResource<ValueAnimation> (ValueAnimation.GetTypeStatic (), sendEventOnFailure);
		}
		
		public ScriptFile GetScriptFile (string name, bool sendEventOnFailure = true)
		{
			return GetResource<ScriptFile> (ScriptFile.GetTypeStatic (), sendEventOnFailure);
		}
		
		public Font GetFont (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Font> (Font.GetTypeStatic (), sendEventOnFailure);
		}
		
		public AnimationSet2D GetAnimationSet2D (string name, bool sendEventOnFailure = true)
		{
			return GetResource<AnimationSet2D> (AnimationSet2D.GetTypeStatic (), sendEventOnFailure);
		}
		
		public ParticleEffect2D GetParticleEffect2D (string name, bool sendEventOnFailure = true)
		{
			return GetResource<ParticleEffect2D> (ParticleEffect2D.GetTypeStatic (), sendEventOnFailure);
		}
		
		public Sprite2D GetSprite2D (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Sprite2D> (Sprite2D.GetTypeStatic (), sendEventOnFailure);
		}
		
		public SpriteSheet2D GetSpriteSheet2D (string name, bool sendEventOnFailure = true)
		{
			return GetResource<SpriteSheet2D> (SpriteSheet2D.GetTypeStatic (), sendEventOnFailure);
		}
		
		public TmxFile2D GetTmxFile2D (string name, bool sendEventOnFailure = true)
		{
			return GetResource<TmxFile2D> (TmxFile2D.GetTypeStatic (), sendEventOnFailure);
		}
#endregion
	}
}
