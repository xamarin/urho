using System.Collections.Generic;

namespace SharpieBinder
{
	static class NamespaceRegistry
	{
		static readonly Dictionary<string, string> TypeNamespaces = new Dictionary<string, string>
		{
			// The Urho.UI ones
			{ "BorderImage", "Urho.Gui" },
			{ "Button", "Urho.Gui" },
			{ "CheckBox", "Urho.Gui" },
			{ "Cursor", "Urho.Gui" },
			{ "DropDownList", "Urho.Gui" },
			{ "FileSelector", "Urho.Gui" },
			{ "Font", "Urho.Gui" },
			{ "FontFace", "Urho.Gui" },
			{ "FontFaceBitmap", "Urho.Gui" },
			{ "FontFaceFreeType", "Urho.Gui" },
			{ "LineEdit", "Urho.Gui" },
			{ "ListView", "Urho.Gui" },
			{ "Menu", "Urho.Gui" },
			{ "MessageBox", "Urho.Gui" },
			{ "ScrollBar", "Urho.Gui" },
			{ "ScrollView", "Urho.Gui" },
			{ "Slider", "Urho.Gui" },
			{ "Sprite", "Urho.Gui" },
			{ "Text", "Urho.Gui" },
			{ "Text3D", "Urho.Gui" },
			{ "ToolTip", "Urho.Gui" },
			{ "UI", "Urho.Gui" },
			{ "UIBatch", "Urho.Gui" },
			{ "UIElement", "Urho.Gui" },
			{ "View3D", "Urho.Gui" },
			{ "Window", "Urho.Gui" },
			// Enums
			{ "CursorShape", "Urho.Gui" },
			//{ "FONT_TYPE", "Urho.Gui" },
			{ "HighlightMode", "Urho.Gui" },
			{ "TextEffect", "Urho.Gui" },
			{ "HorizontalAlignment", "Urho.Gui" },
			{ "VerticalAlignment", "Urho.Gui" },
			{ "Corner", "Urho.Gui" },
			{ "Orientation", "Urho.Gui" },
			{ "FocusMode", "Urho.Gui" },
			{ "LayoutMode", "Urho.Gui" },
			{ "TraversalMode", "Urho.Gui" },
			{ "WindowDragMode", "Urho.Gui" },


			// Audio
			{ "Audio", "Urho.Audio" },
			{ "BufferedSoundStream", "Urho.Audio" },
			{ "OggVorbisSoundStream", "Urho.Audio" },
			{ "Sound", "Urho.Audio" },
			{ "SoundListener", "Urho.Audio" },
			{ "SoundSource", "Urho.Audio" },
			{ "SoundSource3D", "Urho.Audio" },
			{ "SoundStream", "Urho.Audio" }, 

			// Physics
			{ "CollisionShape", "Urho.Physics" },
			{ "Constraint", "Urho.Physics" },
			{ "PhysicsWorld", "Urho.Physics" },
			{ "RigidBody", "Urho.Physics" }, 
			// enums
			{ "ShapeType", "Urho.Physics" },
			{ "ConstraintType", "Urho.Physics" },
			{ "CollisionEventMode", "Urho.Physics" },

			// Network
			{ "Connection", "Urho.Network" },
			{ "HttpRequest", "Urho.Network" },
			{ "Network", "Urho.Network" },
			{ "NetworkPriority", "Urho.Network" }, 
			// enums
			{ "ObserverPositionSendMode", "Urho.Network" },
			{ "HttpRequestState", "Urho.Network" },

			// Navigation
			{ "CrowdAgent", "Urho.Navigation" },
			{ "CrowdManager", "Urho.Navigation" },
			{ "DynamicNavigationMesh", "Urho.Navigation" },
			{ "NavArea", "Urho.Navigation" },
			{ "NavBuildData", "Urho.Navigation" },
			{ "Navigable", "Urho.Navigation" },
			{ "NavigationMesh", "Urho.Navigation" },
			{ "Obstacle", "Urho.Navigation" },
			{ "OffMeshConnection", "Urho.Navigation" }, 
			// enums
			{ "CrowdAgentRequestedTarget", "Urho.Navigation" },
			{ "CrowdAgentTargetState", "Urho.Navigation" },
			{ "CrowdAgentState", "Urho.Navigation" },
			{ "NavigationQuality", "Urho.Navigation" },
			{ "NavigationPushiness", "Urho.Navigation" },
			{ "NavmeshPartitionType", "Urho.Navigation" },

			// IO
			{ "Compression", "Urho.IO" },
			{ "Deserializer", "Urho.IO" },
			{ "File", "Urho.IO" },
			{ "FileSystem", "Urho.IO" },
			{ "FileWatcher", "Urho.IO" },
			{ "Log", "Urho.IO" },
			{ "MemoryBuffer", "Urho.IO" },
			{ "PackageFile", "Urho.IO" },
			{ "Serializer", "Urho.IO" },
			{ "VectorBuffer", "Urho.IO" }, 
			// enums
			{ "FileMode", "Urho.IO" },

			// Resources
			{ "BackgroundLoader", "Urho.Resources" },
			{ "Decompress", "Urho.Resources" },
			{ "Image", "Urho.Resources" },
			{ "JsonFile", "Urho.Resources" },
			{ "JsonValue", "Urho.Resources" },
			{ "Localization", "Urho.Resources" },
			{ "PListFile", "Urho.Resources" },
			{ "Resource", "Urho.Resources" },
			{ "ResourceCache", "Urho.Resources" },
			{ "XmlElement", "Urho.Resources" },
			{ "XmlFile", "Urho.Resources" }, 
			// enum
			{ "CompressedFormat", "Urho.Resources" },
			{ "JsonValueType", "Urho.Resources" },
			{ "JsonNumberType", "Urho.Resources" },
			{ "PListValueType", "Urho.Resources" },
			{ "AsyncLoadState", "Urho.Resources" },
			{ "ResourceRequest", "Urho.Resources" },

			// Urho2D
			{ "AnimatedSprite2D", "Urho.Urho2D" },
			{ "AnimationSet2D", "Urho.Urho2D" },
			{ "CollisionBox2D", "Urho.Urho2D" },
			{ "CollisionChain2D", "Urho.Urho2D" },
			{ "CollisionCircle2D", "Urho.Urho2D" },
			{ "CollisionEdge2D", "Urho.Urho2D" },
			{ "CollisionPolygon2D", "Urho.Urho2D" },
			{ "CollisionShape2D", "Urho.Urho2D" },
			{ "Constraint2D", "Urho.Urho2D" },
			{ "ConstraintDistance2D", "Urho.Urho2D" },
			{ "ConstraintFriction2D", "Urho.Urho2D" },
			{ "ConstraintGear2D", "Urho.Urho2D" },
			{ "ConstraintMotor2D", "Urho.Urho2D" },
			{ "ConstraintMouse2D", "Urho.Urho2D" },
			{ "ConstraintPrismatic2D", "Urho.Urho2D" },
			{ "ConstraintPulley2D", "Urho.Urho2D" },
			{ "ConstraintRevolute2D", "Urho.Urho2D" },
			{ "ConstraintRope2D", "Urho.Urho2D" },
			{ "ConstraintWeld2D", "Urho.Urho2D" },
			{ "ConstraintWheel2D", "Urho.Urho2D" },
			{ "Drawable2D", "Urho.Urho2D" },
			{ "ParticleEffect2D", "Urho.Urho2D" },
			{ "ParticleEmitter2D", "Urho.Urho2D" },
			{ "PhysicsWorld2D", "Urho.Urho2D" },
			{ "Renderer2D", "Urho.Urho2D" },
			{ "RigidBody2D", "Urho.Urho2D" },
			{ "Sprite2D", "Urho.Urho2D" },
			{ "SpriteSheet2D", "Urho.Urho2D" },
			{ "SpriterData2D", "Urho.Urho2D" },
			{ "SpriterInstance2D", "Urho.Urho2D" },
			{ "StaticSprite2D", "Urho.Urho2D" },
			{ "TileMap2D", "Urho.Urho2D" },
			{ "TileMapDefs2D", "Urho.Urho2D" },
			{ "TileMapLayer2D", "Urho.Urho2D" },
			{ "TmxFile2D", "Urho.Urho2D" },
			{ "Urho2D", "Urho.Urho2D" },
			{ "PropertySet2D", "Urho.Urho2D" },
			{ "Texture2D", "Urho.Urho2D" },
			{ "Tile2D", "Urho.Urho2D" },
			{ "TileMapObject2D", "Urho.Urho2D" },
			{ "TmxImageLayer2D", "Urho.Urho2D" },
			{ "TmxLayer2D", "Urho.Urho2D" },
			{ "TmxObjectGroup2D", "Urho.Urho2D" },
			{ "TmxTileLayer2D", "Urho.Urho2D" },
			{ "TileMapInfo2D", "Urho.Urho2D" },


			// enum
			{ "LoopMode2D", "Urho.Urho2D" },
			{ "EmitterType2D", "Urho.Urho2D" },
			{ "BodyType2D", "Urho.Urho2D" },
			{ "ObjectType", "Urho.Urho2D" },
			{ "CurveType", "Urho.Urho2D" },
			{ "LoopMode", "Urho.Urho2D" },
			{ "Orientation2D", "Urho.Urho2D" },
			{ "TileMapLayerType2D", "Urho.Urho2D" },
			{ "TileMapObjectType2D", "Urho.Urho2D" },

		};

		public static string DetermineNamespace(string typename)
		{
			string ns;

			if (TypeNamespaces.TryGetValue(typename, out ns))
				return ns;
			return "Urho";
		}

		public static string RemapTypeToNamespace(string typename)
		{
			return DetermineNamespace(typename) + "." + typename;
		}
	}
}
