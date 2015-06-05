The class hierarchy for the objects in Urho is the following:

= ValueTypes =

* Vector2 
* IntVector2
* Vector3
* Vector4
* Skeleton
* StringHash
* Rect
* IntRect
* Sphere
* Octant
* Ray
* Polyhedron
* Quaternion
* Matrix3
* Matrix3x4
* Matrix4
* Plane
* Frustum
* Color
* BoundingBox
* Spline
* String
* WString
* Controls
* Mutex
* MutexLock
* UIBatch
* Variant
* Condition
* JSONValue
* XMLElement
* XPathResultSet
* XPathQuery
* SceneResolver
* CScriptDictValue
* HashBase
* ListBase
* VectorBase
* Timer
* HiresTimer
* AreaAllocator

= Classes = 

* RefCounted
  * Object : public RefCounted
    * Audio : public Object
    * Profiler : public Object
    * Time : public Object
    * WorkQueue : public Object
    * Application : public Object
    * Console : public Object
    * DebugHud : public Object
    * Engine : public Object
    * Geometry : public Object
    * OcclusionBuffer : public Object
    * Renderer : public Object
    * ShaderPrecache : public Object
    * View : public Object
    * Viewport : public Object
    * File : public Object, public Deserializer, public Serializer
    * FileSystem : public Object
    * FileWatcher : public Object, public Thread
    * Log : public Object
    * PackageFile : public Object
    * Input : public Object
    * LuaScript : public Object, public LuaScriptEventListener
    * Connection : public Object
    * Network : public Object, public kNet::IMessageHandler, public kNet::INetworkServerListener
    * Resource : public Object
      * Sound : public Resource
      * Animation : public Resource
      * Material : public Resource
      * Model : public Resource
      * ParticleEffect : public Resource
      * Shader : public Resource
      * Technique : public Resource
      * LuaFile : public Resource
      * Image : public Resource
      * JSONFile : public Resource
      * PListFile : public Resource
      * XMLFile : public Resource
      * ObjectAnimation : public Resource
      * ValueAnimation : public Resource
      * ScriptFile : public Resource, public ScriptEventListener
      * Font : public Resource
      * AnimationSet2D : public Resource
      * ParticleEffect2D : public Resource
      * Sprite2D : public Resource
      * SpriteSheet2D : public Resource
      * TmxFile2D : public Resource
    * ResourceRouter : public Object
    * ResourceCache : public Object
    * Serializable : public Object
      * Animatable : public Serializable
        * Component : public Animatable
          * LogicComponent : public Component
          * SmoothedTransform : public Component
          * SplinePath : public Component
          * UnknownComponent : public Component
          * ScriptInstance : public Component, public ScriptEventListener
          * CollisionShape2D : public Component
            * CollisionBox2D : public CollisionShape2D
            * CollisionChain2D : public CollisionShape2D
            * CollisionCircle2D : public CollisionShape2D
            * CollisionEdge2D : public CollisionShape2D
            * CollisionPolygon2D : public CollisionShape2D
          * Constraint2D : public Component
            * ConstraintDistance2D : public Constraint2D
            * ConstraintFriction2D : public Constraint2D
            * ConstraintGear2D : public Constraint2D
            * ConstraintMotor2D : public Constraint2D
            * ConstraintMouse2D : public Constraint2D
            * ConstraintPrismatic2D : public Constraint2D
            * ConstraintPulley2D : public Constraint2D
            * ConstraintRevolute2D : public Constraint2D
            * ConstraintRope2D : public Constraint2D
            * ConstraintWeld2D : public Constraint2D
            * ConstraintWheel2D : public Constraint2D
            
          * PhysicsWorld2D : public Component, public b2ContactListener, public b2Draw
          * RigidBody2D : public Component
          * TileMap2D : public Component
          * TileMapLayer2D : public Component
          * SoundListener : public Component
          * SoundSource : public Component
            * SoundSource3D : public SoundSource
          * AnimationController : public Component
          * Camera : public Component
          * DebugRenderer : public Component
          * Drawable : public Component
            * BillboardSet : public Drawable
              * ParticleEmitter : public BillboardSet
            * CustomGeometry : public Drawable
            * DecalSet : public Drawable
            * Light : public Drawable
            * StaticModel : public Drawable
              * AnimatedModel : public StaticModel
              * Skybox : public StaticModel
              * StaticModelGroup : public StaticModel
            * TerrainPatch : public Drawable
            * Zone : public Drawable
            * Text3D : public Drawable
            * Drawable2D : public Drawable
              * ParticleEmitter2D : public Drawable2D
              * StaticSprite2D : public Drawable2D
                * AnimatedSprite2D : public StaticSprite2D
            * Renderer2D : public Drawable
          * Octree : public Component, public Octant
          * Terrain : public Component
          * LuaScriptInstance : public Component, public LuaScriptEventListener
          * CrowdAgent : public Component
          * DetourCrowdManager : public Component
          * NavArea : public Component
          * Navigable : public Component
          * NavigationMesh : public Component
            * DynamicNavigationMesh : public NavigationMesh
          * Obstacle : public Component
          * OffMeshConnection : public Component
          * NetworkPriority : public Component
          * CollisionShape : public Component
          * Constraint : public Component
          * PhysicsWorld : public Component, public btIDebugDraw
          * RigidBody : public Component, public btMotionState
        * Node : public Animatable
          * Scene : public Node
        * UIElement : public Animatable
          * BorderImage : public UIElement
            * Button : public BorderImage
              * Menu : public Button
                * DropDownList : public Menu
            * CheckBox : public BorderImage
            * Cursor : public BorderImage
            * LineEdit : public BorderImage
            * Slider : public BorderImage
            * Window : public BorderImage
              * View3D : public Window
          * ScrollBar : public UIElement
          * ScrollView : public UIElement
            * ListView : public ScrollView
          * Sprite : public UIElement
          * Text : public UIElement
          * ToolTip : public UIElement
    * Script : public Object
    * ScriptEventInvoker : public Object
    * FileSelector : public Object
    * MessageBox : public Object
    * UI : public Object
  * Context : public RefCounted
  * SoundStream : public RefCounted
    * BufferedSoundStream : public SoundStream
    * OggVorbisSoundStream : public SoundStream
  * AttributeAccessor : public RefCounted
  * ObjectFactory : public RefCounted
  * AnimationState : public RefCounted
  * RenderPath : public RefCounted
  * Pass : public RefCounted
  * LuaFunction : public RefCounted
  * FontFace : public RefCounted
    * FontFaceBitmap : public FontFace
    * FontFaceFreeType : public FontFace
  * Animation2D : public RefCounted
  * PropertySet2D : public RefCounted
  * Tile2D : public RefCounted
  * TileMapObject2D : public RefCounted
  * RenderPath : public RefCounted
  * Pass : public RefCounted
  * LuaFunction : public RefCounted
  * FontFace : public RefCounted
  * Animation2D : public RefCounted
  * PropertySet2D : public RefCounted
  * Tile2D : public RefCounted
  * TileMapObject2D : public RefCounted
* EventHandler : public LinkedListNode
* Deserializer
  * MemoryBuffer : public Deserializer, public Serializer
  * VectorBuffer : public Deserializer, public Serializer
* Serializer
* OctreeQuery
  * PointOctreeQuery : public OctreeQuery
  * SphereOctreeQuery : public OctreeQuery
  * BoxOctreeQuery : public OctreeQuery
  * FrustumOctreeQuery : public OctreeQuery
* AutoProfileBlock
* ProfilerBlock
* Thread
* PListValue
* CScriptArray
* CScriptDictionary
* ScriptEventListener
* LuaScriptEventListener
* RayOctreeQuery

