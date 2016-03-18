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
using Urho.Audio;
using Urho.Resources;
using Urho.Gui;
using Urho.Urho2D;

namespace Urho.Resources {
	
	public partial class ResourceCache {
#region Convenience methods to get resources
		T GetResource<T> (StringHash type, string name, bool sendEventOnFailure) where T:UrhoObject
		{
			var ptr = ResourceCache_GetResource (handle, type.Code, name, sendEventOnFailure);
			if (ptr == IntPtr.Zero)
				return null;
			return Runtime.LookupObject<T> (ptr);
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

		public Texture2D GetTexture2D (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Texture2D> (Texture2D.TypeStatic, name, sendEventOnFailure);
		}

		public Texture3D GetTexture3D(string name, bool sendEventOnFailure = true)
		{
			return GetResource<Texture3D>(Texture3D.TypeStatic, name, sendEventOnFailure);
		}

		public Image GetImage (string name, bool sendEventOnFailure = true)
		{
			return GetResource<Image> (Image.TypeStatic, name, sendEventOnFailure);
		}
		public PListFile GetPListFile (string name, bool sendEventOnFailure = true)
		{
			return GetResource<PListFile> (PListFile.TypeStatic, name, sendEventOnFailure);
		}
		
		public XmlFile GetXmlFile (string name, bool sendEventOnFailure = true)
		{
			return GetResource<XmlFile> (XmlFile.TypeStatic, name, sendEventOnFailure);
		}
		
		public ObjectAnimation GetObjectAnimation (string name, bool sendEventOnFailure = true)
		{
			return GetResource<ObjectAnimation> (ObjectAnimation.TypeStatic, name, sendEventOnFailure);
		}
		
		public ValueAnimation GetValueAnimation (string name, bool sendEventOnFailure = true)
		{
			return GetResource<ValueAnimation> (ValueAnimation.TypeStatic, name, sendEventOnFailure);
		}

		public JsonFile GetJsonFile(string name, bool sendEventOnFailure = true)
		{
			return GetResource<JsonFile>(JsonFile.TypeStatic, name, sendEventOnFailure);
		}

#if false
		public LuaFile GetLuaFile (string name, bool sendEventOnFailure = true)
		{
			return GetResource<LuaFile> (LuaFile.TypeStatic, name, sendEventOnFailure);
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
