using System;
using System.Runtime.InteropServices;
using Urho.Physics;
using Urho.Navigation;
using Urho.Network;
using Urho.Urho2D;
using Urho.Gui;
using Urho.Actions;
using Urho.Audio;
using Urho.Resources;
using Urho.IO;
#pragma warning disable CS0618, CS0649
namespace Urho {

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]	public delegate void ObjectCallbackSignature (IntPtr data, int stringhash, IntPtr variantMap);
}

namespace Urho {
        public partial struct SoundFinishedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public SoundSource SoundSource => UrhoMap.get_SoundSource (handle, unchecked((int)368456554) /* SoundSource (P_SOUNDSOURCE) */);
            public Sound Sound => UrhoMap.get_Sound (handle, unchecked((int)3920519599) /* Sound (P_SOUND) */);
        } /* struct SoundFinishedEventArgs */

} /* namespace */

namespace Urho {
        public partial struct FrameStartedEventArgs {
            internal IntPtr handle;
            public uint FrameNumber => UrhoMap.get_uint (handle, unchecked((int)1441088918) /* FrameNumber (P_FRAMENUMBER) */);
            public float TimeStep => UrhoMap.get_float (handle, unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct FrameStartedEventArgs */

        public partial class Time {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.FrameStarted += ...' instead.")]
             public Subscription SubscribeToFrameStarted (Action<FrameStartedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FrameStartedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)529677540) /* BeginFrame (E_BEGINFRAME) */);
                  return s;
             }

             static UrhoEventAdapter<FrameStartedEventArgs> eventAdapterForFrameStarted;
             public event Action<FrameStartedEventArgs> FrameStarted
             {
                 add
                 {
                      if (eventAdapterForFrameStarted == null)
                          eventAdapterForFrameStarted = new UrhoEventAdapter<FrameStartedEventArgs>(typeof(Time));
                      eventAdapterForFrameStarted.AddManagedSubscriber(handle, value, SubscribeToFrameStarted);
                 }
                 remove { eventAdapterForFrameStarted.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Time */ 

} /* namespace */

namespace Urho {
        public partial struct UpdateEventArgs {
            internal IntPtr handle;
            public float TimeStep => UrhoMap.get_float (handle, unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct UpdateEventArgs */

        public partial class Engine {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.Update += ...' instead.")]
             internal Subscription SubscribeToUpdate (Action<UpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)915638697) /* Update (E_UPDATE) */);
                  return s;
             }

             static UrhoEventAdapter<UpdateEventArgs> eventAdapterForUpdate;
             public event Action<UpdateEventArgs> Update
             {
                 add
                 {
                      if (eventAdapterForUpdate == null)
                          eventAdapterForUpdate = new UrhoEventAdapter<UpdateEventArgs>(typeof(Engine));
                      eventAdapterForUpdate.AddManagedSubscriber(handle, value, SubscribeToUpdate);
                 }
                 remove { eventAdapterForUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Engine */ 

} /* namespace */

namespace Urho {
        public partial struct PostUpdateEventArgs {
            internal IntPtr handle;
            public float TimeStep => UrhoMap.get_float (handle, unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct PostUpdateEventArgs */

        public partial class Engine {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PostUpdate += ...' instead.")]
             public Subscription SubscribeToPostUpdate (Action<PostUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PostUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2702758633) /* PostUpdate (E_POSTUPDATE) */);
                  return s;
             }

             static UrhoEventAdapter<PostUpdateEventArgs> eventAdapterForPostUpdate;
             public event Action<PostUpdateEventArgs> PostUpdate
             {
                 add
                 {
                      if (eventAdapterForPostUpdate == null)
                          eventAdapterForPostUpdate = new UrhoEventAdapter<PostUpdateEventArgs>(typeof(Engine));
                      eventAdapterForPostUpdate.AddManagedSubscriber(handle, value, SubscribeToPostUpdate);
                 }
                 remove { eventAdapterForPostUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Engine */ 

} /* namespace */

namespace Urho {
        public partial struct RenderUpdateEventArgs {
            internal IntPtr handle;
            public float TimeStep => UrhoMap.get_float (handle, unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct RenderUpdateEventArgs */

        public partial class Engine {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.RenderUpdate += ...' instead.")]
             public Subscription SubscribeToRenderUpdate (Action<RenderUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new RenderUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3851615199) /* RenderUpdate (E_RENDERUPDATE) */);
                  return s;
             }

             static UrhoEventAdapter<RenderUpdateEventArgs> eventAdapterForRenderUpdate;
             public event Action<RenderUpdateEventArgs> RenderUpdate
             {
                 add
                 {
                      if (eventAdapterForRenderUpdate == null)
                          eventAdapterForRenderUpdate = new UrhoEventAdapter<RenderUpdateEventArgs>(typeof(Engine));
                      eventAdapterForRenderUpdate.AddManagedSubscriber(handle, value, SubscribeToRenderUpdate);
                 }
                 remove { eventAdapterForRenderUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Engine */ 

} /* namespace */

namespace Urho {
        public partial struct PostRenderUpdateEventArgs {
            internal IntPtr handle;
            public float TimeStep => UrhoMap.get_float (handle, unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct PostRenderUpdateEventArgs */

        public partial class Engine {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PostRenderUpdate += ...' instead.")]
             public Subscription SubscribeToPostRenderUpdate (Action<PostRenderUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PostRenderUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)22439199) /* PostRenderUpdate (E_POSTRENDERUPDATE) */);
                  return s;
             }

             static UrhoEventAdapter<PostRenderUpdateEventArgs> eventAdapterForPostRenderUpdate;
             public event Action<PostRenderUpdateEventArgs> PostRenderUpdate
             {
                 add
                 {
                      if (eventAdapterForPostRenderUpdate == null)
                          eventAdapterForPostRenderUpdate = new UrhoEventAdapter<PostRenderUpdateEventArgs>(typeof(Engine));
                      eventAdapterForPostRenderUpdate.AddManagedSubscriber(handle, value, SubscribeToPostRenderUpdate);
                 }
                 remove { eventAdapterForPostRenderUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Engine */ 

} /* namespace */

namespace Urho {
        public partial struct FrameEndedEventArgs {
            internal IntPtr handle;
        } /* struct FrameEndedEventArgs */

        public partial class Time {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.FrameEnded += ...' instead.")]
             public Subscription SubscribeToFrameEnded (Action<FrameEndedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FrameEndedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)4264256242) /* EndFrame (E_ENDFRAME) */);
                  return s;
             }

             static UrhoEventAdapter<FrameEndedEventArgs> eventAdapterForFrameEnded;
             public event Action<FrameEndedEventArgs> FrameEnded
             {
                 add
                 {
                      if (eventAdapterForFrameEnded == null)
                          eventAdapterForFrameEnded = new UrhoEventAdapter<FrameEndedEventArgs>(typeof(Time));
                      eventAdapterForFrameEnded.AddManagedSubscriber(handle, value, SubscribeToFrameEnded);
                 }
                 remove { eventAdapterForFrameEnded.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Time */ 

} /* namespace */

namespace Urho {
        public partial struct WorkItemCompletedEventArgs {
            internal IntPtr handle;
            public WorkItem Item => UrhoMap.get_WorkItem (handle, unchecked((int)1322237459) /* Item (P_ITEM) */);
        } /* struct WorkItemCompletedEventArgs */

        public partial class WorkQueue {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.WorkItemCompleted += ...' instead.")]
             public Subscription SubscribeToWorkItemCompleted (Action<WorkItemCompletedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new WorkItemCompletedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1507705319) /* WorkItemCompleted (E_WORKITEMCOMPLETED) */);
                  return s;
             }

             static UrhoEventAdapter<WorkItemCompletedEventArgs> eventAdapterForWorkItemCompleted;
             public event Action<WorkItemCompletedEventArgs> WorkItemCompleted
             {
                 add
                 {
                      if (eventAdapterForWorkItemCompleted == null)
                          eventAdapterForWorkItemCompleted = new UrhoEventAdapter<WorkItemCompletedEventArgs>(typeof(WorkQueue));
                      eventAdapterForWorkItemCompleted.AddManagedSubscriber(handle, value, SubscribeToWorkItemCompleted);
                 }
                 remove { eventAdapterForWorkItemCompleted.RemoveManagedSubscriber(handle, value); }
             }
        } /* class WorkQueue */ 

} /* namespace */

namespace Urho {
        public partial struct ConsoleCommandEventArgs {
            internal IntPtr handle;
            public String Command => UrhoMap.get_String (handle, unchecked((int)1528149579) /* Command (P_COMMAND) */);
            public String Id => UrhoMap.get_String (handle, unchecked((int)6887995) /* Id (P_ID) */);
        } /* struct ConsoleCommandEventArgs */

        public partial class UrhoConsole {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ConsoleCommand += ...' instead.")]
             public Subscription SubscribeToConsoleCommand (Action<ConsoleCommandEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ConsoleCommandEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1402653748) /* ConsoleCommand (E_CONSOLECOMMAND) */);
                  return s;
             }

             static UrhoEventAdapter<ConsoleCommandEventArgs> eventAdapterForConsoleCommand;
             public event Action<ConsoleCommandEventArgs> ConsoleCommand
             {
                 add
                 {
                      if (eventAdapterForConsoleCommand == null)
                          eventAdapterForConsoleCommand = new UrhoEventAdapter<ConsoleCommandEventArgs>(typeof(UrhoConsole));
                      eventAdapterForConsoleCommand.AddManagedSubscriber(handle, value, SubscribeToConsoleCommand);
                 }
                 remove { eventAdapterForConsoleCommand.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UrhoConsole */ 

} /* namespace */

namespace Urho {
        public partial struct BoneHierarchyCreatedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
        } /* struct BoneHierarchyCreatedEventArgs */

        public partial class Node {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.BoneHierarchyCreated += ...' instead.")]
             public Subscription SubscribeToBoneHierarchyCreated (Action<BoneHierarchyCreatedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new BoneHierarchyCreatedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1724904343) /* BoneHierarchyCreated (E_BONEHIERARCHYCREATED) */);
                  return s;
             }

             static UrhoEventAdapter<BoneHierarchyCreatedEventArgs> eventAdapterForBoneHierarchyCreated;
             public event Action<BoneHierarchyCreatedEventArgs> BoneHierarchyCreated
             {
                 add
                 {
                      if (eventAdapterForBoneHierarchyCreated == null)
                          eventAdapterForBoneHierarchyCreated = new UrhoEventAdapter<BoneHierarchyCreatedEventArgs>(typeof(Node));
                      eventAdapterForBoneHierarchyCreated.AddManagedSubscriber(handle, value, SubscribeToBoneHierarchyCreated);
                 }
                 remove { eventAdapterForBoneHierarchyCreated.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Node */ 

} /* namespace */

namespace Urho {
        public partial struct AnimationTriggerEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public Animation Animation => UrhoMap.get_Animation (handle, unchecked((int)1554425540) /* Animation (P_ANIMATION) */);
            public String Name => UrhoMap.get_String (handle, unchecked((int)773762347) /* Name (P_NAME) */);
            public float Time => UrhoMap.get_float (handle, unchecked((int)1228410285) /* Time (P_TIME) */);
            public IntPtr Data => UrhoMap.get_IntPtr (handle, unchecked((int)1558284138) /* Data (P_DATA) */);
        } /* struct AnimationTriggerEventArgs */

        public partial class Node {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.AnimationTrigger += ...' instead.")]
             public Subscription SubscribeToAnimationTrigger (Action<AnimationTriggerEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AnimationTriggerEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3945634612) /* AnimationTrigger (E_ANIMATIONTRIGGER) */);
                  return s;
             }

             static UrhoEventAdapter<AnimationTriggerEventArgs> eventAdapterForAnimationTrigger;
             public event Action<AnimationTriggerEventArgs> AnimationTrigger
             {
                 add
                 {
                      if (eventAdapterForAnimationTrigger == null)
                          eventAdapterForAnimationTrigger = new UrhoEventAdapter<AnimationTriggerEventArgs>(typeof(Node));
                      eventAdapterForAnimationTrigger.AddManagedSubscriber(handle, value, SubscribeToAnimationTrigger);
                 }
                 remove { eventAdapterForAnimationTrigger.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Node */ 

} /* namespace */

namespace Urho {
        public partial struct AnimationFinishedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public Animation Animation => UrhoMap.get_Animation (handle, unchecked((int)1554425540) /* Animation (P_ANIMATION) */);
            public String Name => UrhoMap.get_String (handle, unchecked((int)773762347) /* Name (P_NAME) */);
            public bool Looped => UrhoMap.get_bool (handle, unchecked((int)842439811) /* Looped (P_LOOPED) */);
        } /* struct AnimationFinishedEventArgs */

} /* namespace */

namespace Urho {
        public partial struct ParticleEffectFinishedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public ParticleEffect Effect => UrhoMap.get_ParticleEffect (handle, unchecked((int)2340854545) /* Effect (P_EFFECT) */);
        } /* struct ParticleEffectFinishedEventArgs */

} /* namespace */

namespace Urho {
        public partial struct TerrainCreatedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
        } /* struct TerrainCreatedEventArgs */

        public partial class Terrain {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TerrainCreated += ...' instead.")]
             public Subscription SubscribeToTerrainCreated (Action<TerrainCreatedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TerrainCreatedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1280797747) /* TerrainCreated (E_TERRAINCREATED) */);
                  return s;
             }

             static UrhoEventAdapter<TerrainCreatedEventArgs> eventAdapterForTerrainCreated;
             public event Action<TerrainCreatedEventArgs> TerrainCreated
             {
                 add
                 {
                      if (eventAdapterForTerrainCreated == null)
                          eventAdapterForTerrainCreated = new UrhoEventAdapter<TerrainCreatedEventArgs>(typeof(Terrain));
                      eventAdapterForTerrainCreated.AddManagedSubscriber(handle, value, SubscribeToTerrainCreated);
                 }
                 remove { eventAdapterForTerrainCreated.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Terrain */ 

} /* namespace */

namespace Urho {
        public partial struct ScreenModeEventArgs {
            internal IntPtr handle;
            public int Width => UrhoMap.get_int (handle, unchecked((int)3655201574) /* Width (P_WIDTH) */);
            public int Height => UrhoMap.get_int (handle, unchecked((int)380957255) /* Height (P_HEIGHT) */);
            public bool Fullscreen => UrhoMap.get_bool (handle, unchecked((int)1835757435) /* Fullscreen (P_FULLSCREEN) */);
            public bool Borderless => UrhoMap.get_bool (handle, unchecked((int)2212104069) /* Borderless (P_BORDERLESS) */);
            public bool Resizable => UrhoMap.get_bool (handle, unchecked((int)3579260107) /* Resizable (P_RESIZABLE) */);
            public bool HighDPI => UrhoMap.get_bool (handle, unchecked((int)2251421851) /* HighDPI (P_HIGHDPI) */);
        } /* struct ScreenModeEventArgs */

} /* namespace */

namespace Urho {
        public partial struct WindowPosEventArgs {
            internal IntPtr handle;
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
        } /* struct WindowPosEventArgs */

} /* namespace */

namespace Urho {
        public partial struct RenderSurfaceUpdateEventArgs {
            internal IntPtr handle;
        } /* struct RenderSurfaceUpdateEventArgs */

        public partial class Renderer {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.RenderSurfaceUpdate += ...' instead.")]
             public Subscription SubscribeToRenderSurfaceUpdate (Action<RenderSurfaceUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new RenderSurfaceUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)530312032) /* RenderSurfaceUpdate (E_RENDERSURFACEUPDATE) */);
                  return s;
             }

             static UrhoEventAdapter<RenderSurfaceUpdateEventArgs> eventAdapterForRenderSurfaceUpdate;
             public event Action<RenderSurfaceUpdateEventArgs> RenderSurfaceUpdate
             {
                 add
                 {
                      if (eventAdapterForRenderSurfaceUpdate == null)
                          eventAdapterForRenderSurfaceUpdate = new UrhoEventAdapter<RenderSurfaceUpdateEventArgs>(typeof(Renderer));
                      eventAdapterForRenderSurfaceUpdate.AddManagedSubscriber(handle, value, SubscribeToRenderSurfaceUpdate);
                 }
                 remove { eventAdapterForRenderSurfaceUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Renderer */ 

} /* namespace */

namespace Urho {
        public partial struct BeginRenderingEventArgs {
            internal IntPtr handle;
        } /* struct BeginRenderingEventArgs */

} /* namespace */

namespace Urho {
        public partial struct EndRenderingEventArgs {
            internal IntPtr handle;
        } /* struct EndRenderingEventArgs */

} /* namespace */

namespace Urho {
        public partial struct BeginViewUpdateEventArgs {
            internal IntPtr handle;
            public View View => UrhoMap.get_View (handle, unchecked((int)2789059909) /* View (P_VIEW) */);
            public Texture Texture => UrhoMap.get_Texture (handle, unchecked((int)4041785787) /* Texture (P_TEXTURE) */);
            public RenderSurface Surface => UrhoMap.get_RenderSurface (handle, unchecked((int)1353844973) /* Surface (P_SURFACE) */);
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Camera Camera => UrhoMap.get_Camera (handle, unchecked((int)1364112997) /* Camera (P_CAMERA) */);
        } /* struct BeginViewUpdateEventArgs */

        public partial class View {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.BeginViewUpdate += ...' instead.")]
             public Subscription SubscribeToBeginViewUpdate (Action<BeginViewUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new BeginViewUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1204361687) /* BeginViewUpdate (E_BEGINVIEWUPDATE) */);
                  return s;
             }

             static UrhoEventAdapter<BeginViewUpdateEventArgs> eventAdapterForBeginViewUpdate;
             public event Action<BeginViewUpdateEventArgs> BeginViewUpdate
             {
                 add
                 {
                      if (eventAdapterForBeginViewUpdate == null)
                          eventAdapterForBeginViewUpdate = new UrhoEventAdapter<BeginViewUpdateEventArgs>(typeof(View));
                      eventAdapterForBeginViewUpdate.AddManagedSubscriber(handle, value, SubscribeToBeginViewUpdate);
                 }
                 remove { eventAdapterForBeginViewUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class View */ 

} /* namespace */

namespace Urho {
        public partial struct EndViewUpdateEventArgs {
            internal IntPtr handle;
            public View View => UrhoMap.get_View (handle, unchecked((int)2789059909) /* View (P_VIEW) */);
            public Texture Texture => UrhoMap.get_Texture (handle, unchecked((int)4041785787) /* Texture (P_TEXTURE) */);
            public RenderSurface Surface => UrhoMap.get_RenderSurface (handle, unchecked((int)1353844973) /* Surface (P_SURFACE) */);
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Camera Camera => UrhoMap.get_Camera (handle, unchecked((int)1364112997) /* Camera (P_CAMERA) */);
        } /* struct EndViewUpdateEventArgs */

        public partial class View {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.EndViewUpdate += ...' instead.")]
             public Subscription SubscribeToEndViewUpdate (Action<EndViewUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new EndViewUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3578578249) /* EndViewUpdate (E_ENDVIEWUPDATE) */);
                  return s;
             }

             static UrhoEventAdapter<EndViewUpdateEventArgs> eventAdapterForEndViewUpdate;
             public event Action<EndViewUpdateEventArgs> EndViewUpdate
             {
                 add
                 {
                      if (eventAdapterForEndViewUpdate == null)
                          eventAdapterForEndViewUpdate = new UrhoEventAdapter<EndViewUpdateEventArgs>(typeof(View));
                      eventAdapterForEndViewUpdate.AddManagedSubscriber(handle, value, SubscribeToEndViewUpdate);
                 }
                 remove { eventAdapterForEndViewUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class View */ 

} /* namespace */

namespace Urho {
        public partial struct BeginViewRenderEventArgs {
            internal IntPtr handle;
            public View View => UrhoMap.get_View (handle, unchecked((int)2789059909) /* View (P_VIEW) */);
            public Texture Texture => UrhoMap.get_Texture (handle, unchecked((int)4041785787) /* Texture (P_TEXTURE) */);
            public RenderSurface Surface => UrhoMap.get_RenderSurface (handle, unchecked((int)1353844973) /* Surface (P_SURFACE) */);
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Camera Camera => UrhoMap.get_Camera (handle, unchecked((int)1364112997) /* Camera (P_CAMERA) */);
        } /* struct BeginViewRenderEventArgs */

        public partial class Renderer {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.BeginViewRender += ...' instead.")]
             public Subscription SubscribeToBeginViewRender (Action<BeginViewRenderEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new BeginViewRenderEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3972739940) /* BeginViewRender (E_BEGINVIEWRENDER) */);
                  return s;
             }

             static UrhoEventAdapter<BeginViewRenderEventArgs> eventAdapterForBeginViewRender;
             public event Action<BeginViewRenderEventArgs> BeginViewRender
             {
                 add
                 {
                      if (eventAdapterForBeginViewRender == null)
                          eventAdapterForBeginViewRender = new UrhoEventAdapter<BeginViewRenderEventArgs>(typeof(Renderer));
                      eventAdapterForBeginViewRender.AddManagedSubscriber(handle, value, SubscribeToBeginViewRender);
                 }
                 remove { eventAdapterForBeginViewRender.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Renderer */ 

} /* namespace */

namespace Urho {
        public partial struct ViewBuffersReadyEventArgs {
            internal IntPtr handle;
            public View View => UrhoMap.get_View (handle, unchecked((int)2789059909) /* View (P_VIEW) */);
            public Texture Texture => UrhoMap.get_Texture (handle, unchecked((int)4041785787) /* Texture (P_TEXTURE) */);
            public RenderSurface Surface => UrhoMap.get_RenderSurface (handle, unchecked((int)1353844973) /* Surface (P_SURFACE) */);
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Camera Camera => UrhoMap.get_Camera (handle, unchecked((int)1364112997) /* Camera (P_CAMERA) */);
        } /* struct ViewBuffersReadyEventArgs */

} /* namespace */

namespace Urho {
        public partial struct ViewGlobalShaderParametersEventArgs {
            internal IntPtr handle;
            public View View => UrhoMap.get_View (handle, unchecked((int)2789059909) /* View (P_VIEW) */);
            public Texture Texture => UrhoMap.get_Texture (handle, unchecked((int)4041785787) /* Texture (P_TEXTURE) */);
            public RenderSurface Surface => UrhoMap.get_RenderSurface (handle, unchecked((int)1353844973) /* Surface (P_SURFACE) */);
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Camera Camera => UrhoMap.get_Camera (handle, unchecked((int)1364112997) /* Camera (P_CAMERA) */);
        } /* struct ViewGlobalShaderParametersEventArgs */

} /* namespace */

namespace Urho {
        public partial struct EndViewRenderEventArgs {
            internal IntPtr handle;
            public View View => UrhoMap.get_View (handle, unchecked((int)2789059909) /* View (P_VIEW) */);
            public Texture Texture => UrhoMap.get_Texture (handle, unchecked((int)4041785787) /* Texture (P_TEXTURE) */);
            public RenderSurface Surface => UrhoMap.get_RenderSurface (handle, unchecked((int)1353844973) /* Surface (P_SURFACE) */);
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Camera Camera => UrhoMap.get_Camera (handle, unchecked((int)1364112997) /* Camera (P_CAMERA) */);
        } /* struct EndViewRenderEventArgs */

        public partial class Renderer {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.EndViewRender += ...' instead.")]
             public Subscription SubscribeToEndViewRender (Action<EndViewRenderEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new EndViewRenderEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2051989206) /* EndViewRender (E_ENDVIEWRENDER) */);
                  return s;
             }

             static UrhoEventAdapter<EndViewRenderEventArgs> eventAdapterForEndViewRender;
             public event Action<EndViewRenderEventArgs> EndViewRender
             {
                 add
                 {
                      if (eventAdapterForEndViewRender == null)
                          eventAdapterForEndViewRender = new UrhoEventAdapter<EndViewRenderEventArgs>(typeof(Renderer));
                      eventAdapterForEndViewRender.AddManagedSubscriber(handle, value, SubscribeToEndViewRender);
                 }
                 remove { eventAdapterForEndViewRender.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Renderer */ 

} /* namespace */

namespace Urho {
        public partial struct RenderPathEventEventArgs {
            internal IntPtr handle;
            public String Name => UrhoMap.get_String (handle, unchecked((int)773762347) /* Name (P_NAME) */);
        } /* struct RenderPathEventEventArgs */

} /* namespace */

namespace Urho {
        public partial struct DeviceLostEventArgs {
            internal IntPtr handle;
        } /* struct DeviceLostEventArgs */

} /* namespace */

namespace Urho {
        public partial struct DeviceResetEventArgs {
            internal IntPtr handle;
        } /* struct DeviceResetEventArgs */

} /* namespace */

namespace Urho.IO {
        public partial struct LogMessageEventArgs {
            internal IntPtr handle;
            public String Message => UrhoMap.get_String (handle, unchecked((int)2310231975) /* Message (P_MESSAGE) */);
            public int Level => UrhoMap.get_int (handle, unchecked((int)1030270596) /* Level (P_LEVEL) */);
        } /* struct LogMessageEventArgs */

        public partial class Log {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.LogMessage += ...' instead.")]
             public Subscription SubscribeToLogMessage (Action<LogMessageEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new LogMessageEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2123706499) /* LogMessage (E_LOGMESSAGE) */);
                  return s;
             }

             static UrhoEventAdapter<LogMessageEventArgs> eventAdapterForLogMessage;
             public event Action<LogMessageEventArgs> LogMessage
             {
                 add
                 {
                      if (eventAdapterForLogMessage == null)
                          eventAdapterForLogMessage = new UrhoEventAdapter<LogMessageEventArgs>(typeof(Log));
                      eventAdapterForLogMessage.AddManagedSubscriber(handle, value, SubscribeToLogMessage);
                 }
                 remove { eventAdapterForLogMessage.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Log */ 

} /* namespace */

namespace Urho.IO {
        public partial struct AsyncExecFinishedEventArgs {
            internal IntPtr handle;
            public uint RequestID => UrhoMap.get_uint (handle, unchecked((int)4010202986) /* RequestID (P_REQUESTID) */);
            public int ExitCode => UrhoMap.get_int (handle, unchecked((int)3466160107) /* ExitCode (P_EXITCODE) */);
        } /* struct AsyncExecFinishedEventArgs */

        public partial class FileSystem {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.AsyncExecFinished += ...' instead.")]
             public Subscription SubscribeToAsyncExecFinished (Action<AsyncExecFinishedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AsyncExecFinishedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1250019487) /* AsyncExecFinished (E_ASYNCEXECFINISHED) */);
                  return s;
             }

             static UrhoEventAdapter<AsyncExecFinishedEventArgs> eventAdapterForAsyncExecFinished;
             public event Action<AsyncExecFinishedEventArgs> AsyncExecFinished
             {
                 add
                 {
                      if (eventAdapterForAsyncExecFinished == null)
                          eventAdapterForAsyncExecFinished = new UrhoEventAdapter<AsyncExecFinishedEventArgs>(typeof(FileSystem));
                      eventAdapterForAsyncExecFinished.AddManagedSubscriber(handle, value, SubscribeToAsyncExecFinished);
                 }
                 remove { eventAdapterForAsyncExecFinished.RemoveManagedSubscriber(handle, value); }
             }
        } /* class FileSystem */ 

} /* namespace */

namespace Urho {
        public partial struct MouseButtonDownEventArgs {
            internal IntPtr handle;
            public int Button => UrhoMap.get_int (handle, unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => UrhoMap.get_int (handle, unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct MouseButtonDownEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MouseButtonDown += ...' instead.")]
             public Subscription SubscribeToMouseButtonDown (Action<MouseButtonDownEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseButtonDownEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1619012089) /* MouseButtonDown (E_MOUSEBUTTONDOWN) */);
                  return s;
             }

             static UrhoEventAdapter<MouseButtonDownEventArgs> eventAdapterForMouseButtonDown;
             public event Action<MouseButtonDownEventArgs> MouseButtonDown
             {
                 add
                 {
                      if (eventAdapterForMouseButtonDown == null)
                          eventAdapterForMouseButtonDown = new UrhoEventAdapter<MouseButtonDownEventArgs>(typeof(Input));
                      eventAdapterForMouseButtonDown.AddManagedSubscriber(handle, value, SubscribeToMouseButtonDown);
                 }
                 remove { eventAdapterForMouseButtonDown.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct MouseButtonUpEventArgs {
            internal IntPtr handle;
            public int Button => UrhoMap.get_int (handle, unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => UrhoMap.get_int (handle, unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct MouseButtonUpEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MouseButtonUp += ...' instead.")]
             public Subscription SubscribeToMouseButtonUp (Action<MouseButtonUpEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseButtonUpEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)546345330) /* MouseButtonUp (E_MOUSEBUTTONUP) */);
                  return s;
             }

             static UrhoEventAdapter<MouseButtonUpEventArgs> eventAdapterForMouseButtonUp;
             public event Action<MouseButtonUpEventArgs> MouseButtonUp
             {
                 add
                 {
                      if (eventAdapterForMouseButtonUp == null)
                          eventAdapterForMouseButtonUp = new UrhoEventAdapter<MouseButtonUpEventArgs>(typeof(Input));
                      eventAdapterForMouseButtonUp.AddManagedSubscriber(handle, value, SubscribeToMouseButtonUp);
                 }
                 remove { eventAdapterForMouseButtonUp.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct MouseMovedEventArgs {
            internal IntPtr handle;
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
            public int DX => UrhoMap.get_int (handle, unchecked((int)6560020) /* DX (P_DX) */);
            public int DY => UrhoMap.get_int (handle, unchecked((int)6560021) /* DY (P_DY) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => UrhoMap.get_int (handle, unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct MouseMovedEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MouseMoved += ...' instead.")]
             public Subscription SubscribeToMouseMoved (Action<MouseMovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseMovedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1089985430) /* MouseMove (E_MOUSEMOVE) */);
                  return s;
             }

             static UrhoEventAdapter<MouseMovedEventArgs> eventAdapterForMouseMoved;
             public event Action<MouseMovedEventArgs> MouseMoved
             {
                 add
                 {
                      if (eventAdapterForMouseMoved == null)
                          eventAdapterForMouseMoved = new UrhoEventAdapter<MouseMovedEventArgs>(typeof(Input));
                      eventAdapterForMouseMoved.AddManagedSubscriber(handle, value, SubscribeToMouseMoved);
                 }
                 remove { eventAdapterForMouseMoved.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct MouseWheelEventArgs {
            internal IntPtr handle;
            public int Wheel => UrhoMap.get_int (handle, unchecked((int)2881891899) /* Wheel (P_WHEEL) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => UrhoMap.get_int (handle, unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct MouseWheelEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MouseWheel += ...' instead.")]
             public Subscription SubscribeToMouseWheel (Action<MouseWheelEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseWheelEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)834798486) /* MouseWheel (E_MOUSEWHEEL) */);
                  return s;
             }

             static UrhoEventAdapter<MouseWheelEventArgs> eventAdapterForMouseWheel;
             public event Action<MouseWheelEventArgs> MouseWheel
             {
                 add
                 {
                      if (eventAdapterForMouseWheel == null)
                          eventAdapterForMouseWheel = new UrhoEventAdapter<MouseWheelEventArgs>(typeof(Input));
                      eventAdapterForMouseWheel.AddManagedSubscriber(handle, value, SubscribeToMouseWheel);
                 }
                 remove { eventAdapterForMouseWheel.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct KeyDownEventArgs {
            internal IntPtr handle;
            public Key Key =>(Key) UrhoMap.get_int (handle, unchecked((int)890606655) /* Key (P_KEY) */);
            public int Scancode => UrhoMap.get_int (handle, unchecked((int)3743304650) /* Scancode (P_SCANCODE) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => UrhoMap.get_int (handle, unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
            public bool Repeat => UrhoMap.get_bool (handle, unchecked((int)958223163) /* Repeat (P_REPEAT) */);
        } /* struct KeyDownEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.KeyDown += ...' instead.")]
             public Subscription SubscribeToKeyDown (Action<KeyDownEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new KeyDownEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3254146689) /* KeyDown (E_KEYDOWN) */);
                  return s;
             }

             static UrhoEventAdapter<KeyDownEventArgs> eventAdapterForKeyDown;
             public event Action<KeyDownEventArgs> KeyDown
             {
                 add
                 {
                      if (eventAdapterForKeyDown == null)
                          eventAdapterForKeyDown = new UrhoEventAdapter<KeyDownEventArgs>(typeof(Input));
                      eventAdapterForKeyDown.AddManagedSubscriber(handle, value, SubscribeToKeyDown);
                 }
                 remove { eventAdapterForKeyDown.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct KeyUpEventArgs {
            internal IntPtr handle;
            public Key Key =>(Key) UrhoMap.get_int (handle, unchecked((int)890606655) /* Key (P_KEY) */);
            public int Scancode => UrhoMap.get_int (handle, unchecked((int)3743304650) /* Scancode (P_SCANCODE) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => UrhoMap.get_int (handle, unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct KeyUpEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.KeyUp += ...' instead.")]
             public Subscription SubscribeToKeyUp (Action<KeyUpEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new KeyUpEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)4211507706) /* KeyUp (E_KEYUP) */);
                  return s;
             }

             static UrhoEventAdapter<KeyUpEventArgs> eventAdapterForKeyUp;
             public event Action<KeyUpEventArgs> KeyUp
             {
                 add
                 {
                      if (eventAdapterForKeyUp == null)
                          eventAdapterForKeyUp = new UrhoEventAdapter<KeyUpEventArgs>(typeof(Input));
                      eventAdapterForKeyUp.AddManagedSubscriber(handle, value, SubscribeToKeyUp);
                 }
                 remove { eventAdapterForKeyUp.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct TextInputEventArgs {
            internal IntPtr handle;
            public String Text => UrhoMap.get_String (handle, unchecked((int)1196085869) /* Text (P_TEXT) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => UrhoMap.get_int (handle, unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct TextInputEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TextInput += ...' instead.")]
             public Subscription SubscribeToTextInput (Action<TextInputEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TextInputEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2136843517) /* TextInput (E_TEXTINPUT) */);
                  return s;
             }

             static UrhoEventAdapter<TextInputEventArgs> eventAdapterForTextInput;
             public event Action<TextInputEventArgs> TextInput
             {
                 add
                 {
                      if (eventAdapterForTextInput == null)
                          eventAdapterForTextInput = new UrhoEventAdapter<TextInputEventArgs>(typeof(Input));
                      eventAdapterForTextInput.AddManagedSubscriber(handle, value, SubscribeToTextInput);
                 }
                 remove { eventAdapterForTextInput.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct JoystickConnectedEventArgs {
            internal IntPtr handle;
            public int JoystickID => UrhoMap.get_int (handle, unchecked((int)1510428343) /* JoystickID (P_JOYSTICKID) */);
        } /* struct JoystickConnectedEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.JoystickConnected += ...' instead.")]
             public Subscription SubscribeToJoystickConnected (Action<JoystickConnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickConnectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2560363053) /* JoystickConnected (E_JOYSTICKCONNECTED) */);
                  return s;
             }

             static UrhoEventAdapter<JoystickConnectedEventArgs> eventAdapterForJoystickConnected;
             public event Action<JoystickConnectedEventArgs> JoystickConnected
             {
                 add
                 {
                      if (eventAdapterForJoystickConnected == null)
                          eventAdapterForJoystickConnected = new UrhoEventAdapter<JoystickConnectedEventArgs>(typeof(Input));
                      eventAdapterForJoystickConnected.AddManagedSubscriber(handle, value, SubscribeToJoystickConnected);
                 }
                 remove { eventAdapterForJoystickConnected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct JoystickDisconnectedEventArgs {
            internal IntPtr handle;
            public int JoystickID => UrhoMap.get_int (handle, unchecked((int)1510428343) /* JoystickID (P_JOYSTICKID) */);
        } /* struct JoystickDisconnectedEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.JoystickDisconnected += ...' instead.")]
             public Subscription SubscribeToJoystickDisconnected (Action<JoystickDisconnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickDisconnectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)819446519) /* JoystickDisconnected (E_JOYSTICKDISCONNECTED) */);
                  return s;
             }

             static UrhoEventAdapter<JoystickDisconnectedEventArgs> eventAdapterForJoystickDisconnected;
             public event Action<JoystickDisconnectedEventArgs> JoystickDisconnected
             {
                 add
                 {
                      if (eventAdapterForJoystickDisconnected == null)
                          eventAdapterForJoystickDisconnected = new UrhoEventAdapter<JoystickDisconnectedEventArgs>(typeof(Input));
                      eventAdapterForJoystickDisconnected.AddManagedSubscriber(handle, value, SubscribeToJoystickDisconnected);
                 }
                 remove { eventAdapterForJoystickDisconnected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct JoystickButtonDownEventArgs {
            internal IntPtr handle;
            public int JoystickID => UrhoMap.get_int (handle, unchecked((int)1510428343) /* JoystickID (P_JOYSTICKID) */);
            public int Button => UrhoMap.get_int (handle, unchecked((int)3601423954) /* Button (P_BUTTON) */);
        } /* struct JoystickButtonDownEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.JoystickButtonDown += ...' instead.")]
             public Subscription SubscribeToJoystickButtonDown (Action<JoystickButtonDownEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickButtonDownEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2528733712) /* JoystickButtonDown (E_JOYSTICKBUTTONDOWN) */);
                  return s;
             }

             static UrhoEventAdapter<JoystickButtonDownEventArgs> eventAdapterForJoystickButtonDown;
             public event Action<JoystickButtonDownEventArgs> JoystickButtonDown
             {
                 add
                 {
                      if (eventAdapterForJoystickButtonDown == null)
                          eventAdapterForJoystickButtonDown = new UrhoEventAdapter<JoystickButtonDownEventArgs>(typeof(Input));
                      eventAdapterForJoystickButtonDown.AddManagedSubscriber(handle, value, SubscribeToJoystickButtonDown);
                 }
                 remove { eventAdapterForJoystickButtonDown.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct JoystickButtonUpEventArgs {
            internal IntPtr handle;
            public int JoystickID => UrhoMap.get_int (handle, unchecked((int)1510428343) /* JoystickID (P_JOYSTICKID) */);
            public int Button => UrhoMap.get_int (handle, unchecked((int)3601423954) /* Button (P_BUTTON) */);
        } /* struct JoystickButtonUpEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.JoystickButtonUp += ...' instead.")]
             public Subscription SubscribeToJoystickButtonUp (Action<JoystickButtonUpEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickButtonUpEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)109849865) /* JoystickButtonUp (E_JOYSTICKBUTTONUP) */);
                  return s;
             }

             static UrhoEventAdapter<JoystickButtonUpEventArgs> eventAdapterForJoystickButtonUp;
             public event Action<JoystickButtonUpEventArgs> JoystickButtonUp
             {
                 add
                 {
                      if (eventAdapterForJoystickButtonUp == null)
                          eventAdapterForJoystickButtonUp = new UrhoEventAdapter<JoystickButtonUpEventArgs>(typeof(Input));
                      eventAdapterForJoystickButtonUp.AddManagedSubscriber(handle, value, SubscribeToJoystickButtonUp);
                 }
                 remove { eventAdapterForJoystickButtonUp.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct JoystickAxisMoveEventArgs {
            internal IntPtr handle;
            public int JoystickID => UrhoMap.get_int (handle, unchecked((int)1510428343) /* JoystickID (P_JOYSTICKID) */);
            public int Button => UrhoMap.get_int (handle, unchecked((int)3601423954) /* Button (P_AXIS) */);
            public float Position => UrhoMap.get_float (handle, unchecked((int)1333256809) /* Position (P_POSITION) */);
        } /* struct JoystickAxisMoveEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.JoystickAxisMove += ...' instead.")]
             public Subscription SubscribeToJoystickAxisMove (Action<JoystickAxisMoveEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickAxisMoveEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1368926286) /* JoystickAxisMove (E_JOYSTICKAXISMOVE) */);
                  return s;
             }

             static UrhoEventAdapter<JoystickAxisMoveEventArgs> eventAdapterForJoystickAxisMove;
             public event Action<JoystickAxisMoveEventArgs> JoystickAxisMove
             {
                 add
                 {
                      if (eventAdapterForJoystickAxisMove == null)
                          eventAdapterForJoystickAxisMove = new UrhoEventAdapter<JoystickAxisMoveEventArgs>(typeof(Input));
                      eventAdapterForJoystickAxisMove.AddManagedSubscriber(handle, value, SubscribeToJoystickAxisMove);
                 }
                 remove { eventAdapterForJoystickAxisMove.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct JoystickHatMoveEventArgs {
            internal IntPtr handle;
            public int JoystickID => UrhoMap.get_int (handle, unchecked((int)1510428343) /* JoystickID (P_JOYSTICKID) */);
            public int Button => UrhoMap.get_int (handle, unchecked((int)3601423954) /* Button (P_HAT) */);
            public int Position => UrhoMap.get_int (handle, unchecked((int)1333256809) /* Position (P_POSITION) */);
        } /* struct JoystickHatMoveEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.JoystickHatMove += ...' instead.")]
             public Subscription SubscribeToJoystickHatMove (Action<JoystickHatMoveEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickHatMoveEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3808716784) /* JoystickHatMove (E_JOYSTICKHATMOVE) */);
                  return s;
             }

             static UrhoEventAdapter<JoystickHatMoveEventArgs> eventAdapterForJoystickHatMove;
             public event Action<JoystickHatMoveEventArgs> JoystickHatMove
             {
                 add
                 {
                      if (eventAdapterForJoystickHatMove == null)
                          eventAdapterForJoystickHatMove = new UrhoEventAdapter<JoystickHatMoveEventArgs>(typeof(Input));
                      eventAdapterForJoystickHatMove.AddManagedSubscriber(handle, value, SubscribeToJoystickHatMove);
                 }
                 remove { eventAdapterForJoystickHatMove.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct TouchBeginEventArgs {
            internal IntPtr handle;
            public int TouchID => UrhoMap.get_int (handle, unchecked((int)3850094778) /* TouchID (P_TOUCHID) */);
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
            public float Pressure => UrhoMap.get_float (handle, unchecked((int)439090309) /* Pressure (P_PRESSURE) */);
        } /* struct TouchBeginEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TouchBegin += ...' instead.")]
             public Subscription SubscribeToTouchBegin (Action<TouchBeginEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TouchBeginEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3456070058) /* TouchBegin (E_TOUCHBEGIN) */);
                  return s;
             }

             static UrhoEventAdapter<TouchBeginEventArgs> eventAdapterForTouchBegin;
             public event Action<TouchBeginEventArgs> TouchBegin
             {
                 add
                 {
                      if (eventAdapterForTouchBegin == null)
                          eventAdapterForTouchBegin = new UrhoEventAdapter<TouchBeginEventArgs>(typeof(Input));
                      eventAdapterForTouchBegin.AddManagedSubscriber(handle, value, SubscribeToTouchBegin);
                 }
                 remove { eventAdapterForTouchBegin.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct TouchEndEventArgs {
            internal IntPtr handle;
            public int TouchID => UrhoMap.get_int (handle, unchecked((int)3850094778) /* TouchID (P_TOUCHID) */);
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
        } /* struct TouchEndEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TouchEnd += ...' instead.")]
             public Subscription SubscribeToTouchEnd (Action<TouchEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TouchEndEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1078078108) /* TouchEnd (E_TOUCHEND) */);
                  return s;
             }

             static UrhoEventAdapter<TouchEndEventArgs> eventAdapterForTouchEnd;
             public event Action<TouchEndEventArgs> TouchEnd
             {
                 add
                 {
                      if (eventAdapterForTouchEnd == null)
                          eventAdapterForTouchEnd = new UrhoEventAdapter<TouchEndEventArgs>(typeof(Input));
                      eventAdapterForTouchEnd.AddManagedSubscriber(handle, value, SubscribeToTouchEnd);
                 }
                 remove { eventAdapterForTouchEnd.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct TouchMoveEventArgs {
            internal IntPtr handle;
            public int TouchID => UrhoMap.get_int (handle, unchecked((int)3850094778) /* TouchID (P_TOUCHID) */);
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
            public int DX => UrhoMap.get_int (handle, unchecked((int)6560020) /* DX (P_DX) */);
            public int DY => UrhoMap.get_int (handle, unchecked((int)6560021) /* DY (P_DY) */);
            public float Pressure => UrhoMap.get_float (handle, unchecked((int)439090309) /* Pressure (P_PRESSURE) */);
        } /* struct TouchMoveEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TouchMove += ...' instead.")]
             public Subscription SubscribeToTouchMove (Action<TouchMoveEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TouchMoveEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1873483440) /* TouchMove (E_TOUCHMOVE) */);
                  return s;
             }

             static UrhoEventAdapter<TouchMoveEventArgs> eventAdapterForTouchMove;
             public event Action<TouchMoveEventArgs> TouchMove
             {
                 add
                 {
                      if (eventAdapterForTouchMove == null)
                          eventAdapterForTouchMove = new UrhoEventAdapter<TouchMoveEventArgs>(typeof(Input));
                      eventAdapterForTouchMove.AddManagedSubscriber(handle, value, SubscribeToTouchMove);
                 }
                 remove { eventAdapterForTouchMove.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct GestureRecordedEventArgs {
            internal IntPtr handle;
            public uint GestureID => UrhoMap.get_uint (handle, unchecked((int)2079416292) /* GestureID (P_GESTUREID) */);
        } /* struct GestureRecordedEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.GestureRecorded += ...' instead.")]
             public Subscription SubscribeToGestureRecorded (Action<GestureRecordedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new GestureRecordedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2974572953) /* GestureRecorded (E_GESTURERECORDED) */);
                  return s;
             }

             static UrhoEventAdapter<GestureRecordedEventArgs> eventAdapterForGestureRecorded;
             public event Action<GestureRecordedEventArgs> GestureRecorded
             {
                 add
                 {
                      if (eventAdapterForGestureRecorded == null)
                          eventAdapterForGestureRecorded = new UrhoEventAdapter<GestureRecordedEventArgs>(typeof(Input));
                      eventAdapterForGestureRecorded.AddManagedSubscriber(handle, value, SubscribeToGestureRecorded);
                 }
                 remove { eventAdapterForGestureRecorded.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct GestureInputEventArgs {
            internal IntPtr handle;
            public uint GestureID => UrhoMap.get_uint (handle, unchecked((int)2079416292) /* GestureID (P_GESTUREID) */);
            public int CenterX => UrhoMap.get_int (handle, unchecked((int)150093091) /* CenterX (P_CENTERX) */);
            public int CenterY => UrhoMap.get_int (handle, unchecked((int)150093092) /* CenterY (P_CENTERY) */);
            public int NumFingers => UrhoMap.get_int (handle, unchecked((int)2749362116) /* NumFingers (P_NUMFINGERS) */);
            public float Error => UrhoMap.get_float (handle, unchecked((int)3168564136) /* Error (P_ERROR) */);
        } /* struct GestureInputEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.GestureInput += ...' instead.")]
             public Subscription SubscribeToGestureInput (Action<GestureInputEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new GestureInputEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3375880257) /* GestureInput (E_GESTUREINPUT) */);
                  return s;
             }

             static UrhoEventAdapter<GestureInputEventArgs> eventAdapterForGestureInput;
             public event Action<GestureInputEventArgs> GestureInput
             {
                 add
                 {
                      if (eventAdapterForGestureInput == null)
                          eventAdapterForGestureInput = new UrhoEventAdapter<GestureInputEventArgs>(typeof(Input));
                      eventAdapterForGestureInput.AddManagedSubscriber(handle, value, SubscribeToGestureInput);
                 }
                 remove { eventAdapterForGestureInput.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct MultiGestureEventArgs {
            internal IntPtr handle;
            public int CenterX => UrhoMap.get_int (handle, unchecked((int)150093091) /* CenterX (P_CENTERX) */);
            public int CenterY => UrhoMap.get_int (handle, unchecked((int)150093092) /* CenterY (P_CENTERY) */);
            public int NumFingers => UrhoMap.get_int (handle, unchecked((int)2749362116) /* NumFingers (P_NUMFINGERS) */);
            public float DTheta => UrhoMap.get_float (handle, unchecked((int)2305167738) /* DTheta (P_DTHETA) */);
            public float DDist => UrhoMap.get_float (handle, unchecked((int)3911589802) /* DDist (P_DDIST) */);
        } /* struct MultiGestureEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MultiGesture += ...' instead.")]
             public Subscription SubscribeToMultiGesture (Action<MultiGestureEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MultiGestureEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2419467216) /* MultiGesture (E_MULTIGESTURE) */);
                  return s;
             }

             static UrhoEventAdapter<MultiGestureEventArgs> eventAdapterForMultiGesture;
             public event Action<MultiGestureEventArgs> MultiGesture
             {
                 add
                 {
                      if (eventAdapterForMultiGesture == null)
                          eventAdapterForMultiGesture = new UrhoEventAdapter<MultiGestureEventArgs>(typeof(Input));
                      eventAdapterForMultiGesture.AddManagedSubscriber(handle, value, SubscribeToMultiGesture);
                 }
                 remove { eventAdapterForMultiGesture.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct DropFileEventArgs {
            internal IntPtr handle;
            public String FileName => UrhoMap.get_String (handle, unchecked((int)633459751) /* FileName (P_FILENAME) */);
        } /* struct DropFileEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.DropFile += ...' instead.")]
             public Subscription SubscribeToDropFile (Action<DropFileEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DropFileEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)612827595) /* DropFile (E_DROPFILE) */);
                  return s;
             }

             static UrhoEventAdapter<DropFileEventArgs> eventAdapterForDropFile;
             public event Action<DropFileEventArgs> DropFile
             {
                 add
                 {
                      if (eventAdapterForDropFile == null)
                          eventAdapterForDropFile = new UrhoEventAdapter<DropFileEventArgs>(typeof(Input));
                      eventAdapterForDropFile.AddManagedSubscriber(handle, value, SubscribeToDropFile);
                 }
                 remove { eventAdapterForDropFile.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct InputFocusEventArgs {
            internal IntPtr handle;
            public bool Focus => UrhoMap.get_bool (handle, unchecked((int)1842837848) /* Focus (P_FOCUS) */);
            public bool Minimized => UrhoMap.get_bool (handle, unchecked((int)541506182) /* Minimized (P_MINIMIZED) */);
        } /* struct InputFocusEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.InputFocus += ...' instead.")]
             public Subscription SubscribeToInputFocus (Action<InputFocusEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new InputFocusEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)620076718) /* InputFocus (E_INPUTFOCUS) */);
                  return s;
             }

             static UrhoEventAdapter<InputFocusEventArgs> eventAdapterForInputFocus;
             public event Action<InputFocusEventArgs> InputFocus
             {
                 add
                 {
                      if (eventAdapterForInputFocus == null)
                          eventAdapterForInputFocus = new UrhoEventAdapter<InputFocusEventArgs>(typeof(Input));
                      eventAdapterForInputFocus.AddManagedSubscriber(handle, value, SubscribeToInputFocus);
                 }
                 remove { eventAdapterForInputFocus.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct MouseVisibleChangedEventArgs {
            internal IntPtr handle;
            public bool Visible => UrhoMap.get_bool (handle, unchecked((int)2569414770) /* Visible (P_VISIBLE) */);
        } /* struct MouseVisibleChangedEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MouseVisibleChanged += ...' instead.")]
             public Subscription SubscribeToMouseVisibleChanged (Action<MouseVisibleChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseVisibleChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)360201095) /* MouseVisibleChanged (E_MOUSEVISIBLECHANGED) */);
                  return s;
             }

             static UrhoEventAdapter<MouseVisibleChangedEventArgs> eventAdapterForMouseVisibleChanged;
             public event Action<MouseVisibleChangedEventArgs> MouseVisibleChanged
             {
                 add
                 {
                      if (eventAdapterForMouseVisibleChanged == null)
                          eventAdapterForMouseVisibleChanged = new UrhoEventAdapter<MouseVisibleChangedEventArgs>(typeof(Input));
                      eventAdapterForMouseVisibleChanged.AddManagedSubscriber(handle, value, SubscribeToMouseVisibleChanged);
                 }
                 remove { eventAdapterForMouseVisibleChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct MouseModeChangedEventArgs {
            internal IntPtr handle;
            public MouseMode Mode => UrhoMap.get_MouseMode (handle, unchecked((int)108245827) /* Mode (P_MODE) */);
            public bool MouseLocked => UrhoMap.get_bool (handle, unchecked((int)3485665135) /* MouseLocked (P_MOUSELOCKED) */);
        } /* struct MouseModeChangedEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MouseModeChanged += ...' instead.")]
             public Subscription SubscribeToMouseModeChanged (Action<MouseModeChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseModeChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3642946540) /* MouseModeChanged (E_MOUSEMODECHANGED) */);
                  return s;
             }

             static UrhoEventAdapter<MouseModeChangedEventArgs> eventAdapterForMouseModeChanged;
             public event Action<MouseModeChangedEventArgs> MouseModeChanged
             {
                 add
                 {
                      if (eventAdapterForMouseModeChanged == null)
                          eventAdapterForMouseModeChanged = new UrhoEventAdapter<MouseModeChangedEventArgs>(typeof(Input));
                      eventAdapterForMouseModeChanged.AddManagedSubscriber(handle, value, SubscribeToMouseModeChanged);
                 }
                 remove { eventAdapterForMouseModeChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct ExitRequestedEventArgs {
            internal IntPtr handle;
        } /* struct ExitRequestedEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ExitRequested += ...' instead.")]
             public Subscription SubscribeToExitRequested (Action<ExitRequestedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ExitRequestedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)899637200) /* ExitRequested (E_EXITREQUESTED) */);
                  return s;
             }

             static UrhoEventAdapter<ExitRequestedEventArgs> eventAdapterForExitRequested;
             public event Action<ExitRequestedEventArgs> ExitRequested
             {
                 add
                 {
                      if (eventAdapterForExitRequested == null)
                          eventAdapterForExitRequested = new UrhoEventAdapter<ExitRequestedEventArgs>(typeof(Input));
                      eventAdapterForExitRequested.AddManagedSubscriber(handle, value, SubscribeToExitRequested);
                 }
                 remove { eventAdapterForExitRequested.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct SDLRawInputEventArgs {
            internal IntPtr handle;
            public IntPtr SDLEvent => UrhoMap.get_IntPtr (handle, unchecked((int)3036739231) /* SDLEvent (P_SDLEVENT) */);
            public bool Consumed => UrhoMap.get_bool (handle, unchecked((int)1885648840) /* Consumed (P_CONSUMED) */);
        } /* struct SDLRawInputEventArgs */

} /* namespace */

namespace Urho {
        public partial struct InputBeginEventArgs {
            internal IntPtr handle;
        } /* struct InputBeginEventArgs */

} /* namespace */

namespace Urho {
        public partial struct InputEndEventArgs {
            internal IntPtr handle;
        } /* struct InputEndEventArgs */

} /* namespace */

namespace Urho.Navigation {
        public partial struct NavigationMeshRebuiltEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public NavigationMesh Mesh => UrhoMap.get_NavigationMesh (handle, unchecked((int)26614765) /* Mesh (P_MESH) */);
        } /* struct NavigationMeshRebuiltEventArgs */

        public partial class NavigationMesh {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NavigationMeshRebuilt += ...' instead.")]
             public Subscription SubscribeToNavigationMeshRebuilt (Action<NavigationMeshRebuiltEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NavigationMeshRebuiltEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1184056682) /* NavigationMeshRebuilt (E_NAVIGATION_MESH_REBUILT) */);
                  return s;
             }

             static UrhoEventAdapter<NavigationMeshRebuiltEventArgs> eventAdapterForNavigationMeshRebuilt;
             public event Action<NavigationMeshRebuiltEventArgs> NavigationMeshRebuilt
             {
                 add
                 {
                      if (eventAdapterForNavigationMeshRebuilt == null)
                          eventAdapterForNavigationMeshRebuilt = new UrhoEventAdapter<NavigationMeshRebuiltEventArgs>(typeof(NavigationMesh));
                      eventAdapterForNavigationMeshRebuilt.AddManagedSubscriber(handle, value, SubscribeToNavigationMeshRebuilt);
                 }
                 remove { eventAdapterForNavigationMeshRebuilt.RemoveManagedSubscriber(handle, value); }
             }
        } /* class NavigationMesh */ 

} /* namespace */

namespace Urho.Navigation {
        public partial struct NavigationAreaRebuiltEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public NavigationMesh Mesh => UrhoMap.get_NavigationMesh (handle, unchecked((int)26614765) /* Mesh (P_MESH) */);
            public Vector3 BoundsMin => UrhoMap.get_Vector3 (handle, unchecked((int)2452762269) /* BoundsMin (P_BOUNDSMIN) */);
            public Vector3 BoundsMax => UrhoMap.get_Vector3 (handle, unchecked((int)2452237487) /* BoundsMax (P_BOUNDSMAX) */);
        } /* struct NavigationAreaRebuiltEventArgs */

        public partial class NavigationMesh {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NavigationAreaRebuilt += ...' instead.")]
             public Subscription SubscribeToNavigationAreaRebuilt (Action<NavigationAreaRebuiltEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NavigationAreaRebuiltEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2012037194) /* NavigationAreaRebuilt (E_NAVIGATION_AREA_REBUILT) */);
                  return s;
             }

             static UrhoEventAdapter<NavigationAreaRebuiltEventArgs> eventAdapterForNavigationAreaRebuilt;
             public event Action<NavigationAreaRebuiltEventArgs> NavigationAreaRebuilt
             {
                 add
                 {
                      if (eventAdapterForNavigationAreaRebuilt == null)
                          eventAdapterForNavigationAreaRebuilt = new UrhoEventAdapter<NavigationAreaRebuiltEventArgs>(typeof(NavigationMesh));
                      eventAdapterForNavigationAreaRebuilt.AddManagedSubscriber(handle, value, SubscribeToNavigationAreaRebuilt);
                 }
                 remove { eventAdapterForNavigationAreaRebuilt.RemoveManagedSubscriber(handle, value); }
             }
        } /* class NavigationMesh */ 

} /* namespace */

namespace Urho.Navigation {
        public partial struct CrowdAgentFormationEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public CrowdAgent CrowdAgent => UrhoMap.get_CrowdAgent (handle, unchecked((int)687004888) /* CrowdAgent (P_CROWD_AGENT) */);
            public uint Index => UrhoMap.get_uint (handle, unchecked((int)193188146) /* Index (P_INDEX) */);
            public uint Size => UrhoMap.get_uint (handle, unchecked((int)448675873) /* Size (P_SIZE) */);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, unchecked((int)1333256809) /* Position (P_POSITION) */);
        } /* struct CrowdAgentFormationEventArgs */

} /* namespace */

namespace Urho {
        public partial struct CrowdAgentNodeFormationEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public CrowdAgent CrowdAgent => UrhoMap.get_CrowdAgent (handle, unchecked((int)687004888) /* CrowdAgent (P_CROWD_AGENT) */);
            public uint Index => UrhoMap.get_uint (handle, unchecked((int)193188146) /* Index (P_INDEX) */);
            public uint Size => UrhoMap.get_uint (handle, unchecked((int)448675873) /* Size (P_SIZE) */);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, unchecked((int)1333256809) /* Position (P_POSITION) */);
        } /* struct CrowdAgentNodeFormationEventArgs */

} /* namespace */

namespace Urho.Navigation {
        public partial struct CrowdAgentRepositionEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public CrowdAgent CrowdAgent => UrhoMap.get_CrowdAgent (handle, unchecked((int)687004888) /* CrowdAgent (P_CROWD_AGENT) */);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, unchecked((int)1333256809) /* Position (P_POSITION) */);
            public Vector3 Velocity => UrhoMap.get_Vector3 (handle, unchecked((int)2845405629) /* Velocity (P_VELOCITY) */);
            public bool Arrived => UrhoMap.get_bool (handle, unchecked((int)2501236845) /* Arrived (P_ARRIVED) */);
            public float TimeStep => UrhoMap.get_float (handle, unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct CrowdAgentRepositionEventArgs */

        public partial class CrowdManager {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.CrowdAgentReposition += ...' instead.")]
             public Subscription SubscribeToCrowdAgentReposition (Action<CrowdAgentRepositionEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new CrowdAgentRepositionEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3736902068) /* CrowdAgentReposition (E_CROWD_AGENT_REPOSITION) */);
                  return s;
             }

             static UrhoEventAdapter<CrowdAgentRepositionEventArgs> eventAdapterForCrowdAgentReposition;
             public event Action<CrowdAgentRepositionEventArgs> CrowdAgentReposition
             {
                 add
                 {
                      if (eventAdapterForCrowdAgentReposition == null)
                          eventAdapterForCrowdAgentReposition = new UrhoEventAdapter<CrowdAgentRepositionEventArgs>(typeof(CrowdManager));
                      eventAdapterForCrowdAgentReposition.AddManagedSubscriber(handle, value, SubscribeToCrowdAgentReposition);
                 }
                 remove { eventAdapterForCrowdAgentReposition.RemoveManagedSubscriber(handle, value); }
             }
        } /* class CrowdManager */ 

} /* namespace */

namespace Urho {
        public partial struct CrowdAgentNodeRepositionEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public CrowdAgent CrowdAgent => UrhoMap.get_CrowdAgent (handle, unchecked((int)687004888) /* CrowdAgent (P_CROWD_AGENT) */);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, unchecked((int)1333256809) /* Position (P_POSITION) */);
            public Vector3 Velocity => UrhoMap.get_Vector3 (handle, unchecked((int)2845405629) /* Velocity (P_VELOCITY) */);
            public bool Arrived => UrhoMap.get_bool (handle, unchecked((int)2501236845) /* Arrived (P_ARRIVED) */);
            public float TimeStep => UrhoMap.get_float (handle, unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct CrowdAgentNodeRepositionEventArgs */

} /* namespace */

namespace Urho.Navigation {
        public partial struct CrowdAgentFailureEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public CrowdAgent CrowdAgent => UrhoMap.get_CrowdAgent (handle, unchecked((int)687004888) /* CrowdAgent (P_CROWD_AGENT) */);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, unchecked((int)1333256809) /* Position (P_POSITION) */);
            public Vector3 Velocity => UrhoMap.get_Vector3 (handle, unchecked((int)2845405629) /* Velocity (P_VELOCITY) */);
            public int CrowdAgentState => UrhoMap.get_int (handle, unchecked((int)1729065465) /* CrowdAgentState (P_CROWD_AGENT_STATE) */);
            public int CrowdTargetState => UrhoMap.get_int (handle, unchecked((int)928574867) /* CrowdTargetState (P_CROWD_TARGET_STATE) */);
        } /* struct CrowdAgentFailureEventArgs */

        public partial class CrowdManager {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.CrowdAgentFailure += ...' instead.")]
             public Subscription SubscribeToCrowdAgentFailure (Action<CrowdAgentFailureEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new CrowdAgentFailureEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)487208914) /* CrowdAgentFailure (E_CROWD_AGENT_FAILURE) */);
                  return s;
             }

             static UrhoEventAdapter<CrowdAgentFailureEventArgs> eventAdapterForCrowdAgentFailure;
             public event Action<CrowdAgentFailureEventArgs> CrowdAgentFailure
             {
                 add
                 {
                      if (eventAdapterForCrowdAgentFailure == null)
                          eventAdapterForCrowdAgentFailure = new UrhoEventAdapter<CrowdAgentFailureEventArgs>(typeof(CrowdManager));
                      eventAdapterForCrowdAgentFailure.AddManagedSubscriber(handle, value, SubscribeToCrowdAgentFailure);
                 }
                 remove { eventAdapterForCrowdAgentFailure.RemoveManagedSubscriber(handle, value); }
             }
        } /* class CrowdManager */ 

} /* namespace */

namespace Urho {
        public partial struct CrowdAgentNodeFailureEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public CrowdAgent CrowdAgent => UrhoMap.get_CrowdAgent (handle, unchecked((int)687004888) /* CrowdAgent (P_CROWD_AGENT) */);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, unchecked((int)1333256809) /* Position (P_POSITION) */);
            public Vector3 Velocity => UrhoMap.get_Vector3 (handle, unchecked((int)2845405629) /* Velocity (P_VELOCITY) */);
            public int CrowdAgentState => UrhoMap.get_int (handle, unchecked((int)1729065465) /* CrowdAgentState (P_CROWD_AGENT_STATE) */);
            public int CrowdTargetState => UrhoMap.get_int (handle, unchecked((int)928574867) /* CrowdTargetState (P_CROWD_TARGET_STATE) */);
        } /* struct CrowdAgentNodeFailureEventArgs */

} /* namespace */

namespace Urho.Navigation {
        public partial struct CrowdAgentStateChangedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public CrowdAgent CrowdAgent => UrhoMap.get_CrowdAgent (handle, unchecked((int)687004888) /* CrowdAgent (P_CROWD_AGENT) */);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, unchecked((int)1333256809) /* Position (P_POSITION) */);
            public Vector3 Velocity => UrhoMap.get_Vector3 (handle, unchecked((int)2845405629) /* Velocity (P_VELOCITY) */);
            public int CrowdAgentState => UrhoMap.get_int (handle, unchecked((int)1729065465) /* CrowdAgentState (P_CROWD_AGENT_STATE) */);
            public int CrowdTargetState => UrhoMap.get_int (handle, unchecked((int)928574867) /* CrowdTargetState (P_CROWD_TARGET_STATE) */);
        } /* struct CrowdAgentStateChangedEventArgs */

        public partial class CrowdManager {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.CrowdAgentStateChanged += ...' instead.")]
             public Subscription SubscribeToCrowdAgentStateChanged (Action<CrowdAgentStateChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new CrowdAgentStateChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1897461467) /* CrowdAgentStateChanged (E_CROWD_AGENT_STATE_CHANGED) */);
                  return s;
             }

             static UrhoEventAdapter<CrowdAgentStateChangedEventArgs> eventAdapterForCrowdAgentStateChanged;
             public event Action<CrowdAgentStateChangedEventArgs> CrowdAgentStateChanged
             {
                 add
                 {
                      if (eventAdapterForCrowdAgentStateChanged == null)
                          eventAdapterForCrowdAgentStateChanged = new UrhoEventAdapter<CrowdAgentStateChangedEventArgs>(typeof(CrowdManager));
                      eventAdapterForCrowdAgentStateChanged.AddManagedSubscriber(handle, value, SubscribeToCrowdAgentStateChanged);
                 }
                 remove { eventAdapterForCrowdAgentStateChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class CrowdManager */ 

} /* namespace */

namespace Urho {
        public partial struct CrowdAgentNodeStateChangedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public CrowdAgent CrowdAgent => UrhoMap.get_CrowdAgent (handle, unchecked((int)687004888) /* CrowdAgent (P_CROWD_AGENT) */);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, unchecked((int)1333256809) /* Position (P_POSITION) */);
            public Vector3 Velocity => UrhoMap.get_Vector3 (handle, unchecked((int)2845405629) /* Velocity (P_VELOCITY) */);
            public int CrowdAgentState => UrhoMap.get_int (handle, unchecked((int)1729065465) /* CrowdAgentState (P_CROWD_AGENT_STATE) */);
            public int CrowdTargetState => UrhoMap.get_int (handle, unchecked((int)928574867) /* CrowdTargetState (P_CROWD_TARGET_STATE) */);
        } /* struct CrowdAgentNodeStateChangedEventArgs */

} /* namespace */

namespace Urho.Navigation {
        public partial struct NavigationObstacleAddedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public Obstacle Obstacle => UrhoMap.get_Obstacle (handle, unchecked((int)1080511791) /* Obstacle (P_OBSTACLE) */);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, unchecked((int)1333256809) /* Position (P_POSITION) */);
            public float Radius => UrhoMap.get_float (handle, unchecked((int)4247146802) /* Radius (P_RADIUS) */);
            public float Height => UrhoMap.get_float (handle, unchecked((int)380957255) /* Height (P_HEIGHT) */);
        } /* struct NavigationObstacleAddedEventArgs */

        public partial class DynamicNavigationMesh {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NavigationObstacleAdded += ...' instead.")]
             public Subscription SubscribeToNavigationObstacleAdded (Action<NavigationObstacleAddedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NavigationObstacleAddedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)842705885) /* NavigationObstacleAdded (E_NAVIGATION_OBSTACLE_ADDED) */);
                  return s;
             }

             static UrhoEventAdapter<NavigationObstacleAddedEventArgs> eventAdapterForNavigationObstacleAdded;
             public event Action<NavigationObstacleAddedEventArgs> NavigationObstacleAdded
             {
                 add
                 {
                      if (eventAdapterForNavigationObstacleAdded == null)
                          eventAdapterForNavigationObstacleAdded = new UrhoEventAdapter<NavigationObstacleAddedEventArgs>(typeof(DynamicNavigationMesh));
                      eventAdapterForNavigationObstacleAdded.AddManagedSubscriber(handle, value, SubscribeToNavigationObstacleAdded);
                 }
                 remove { eventAdapterForNavigationObstacleAdded.RemoveManagedSubscriber(handle, value); }
             }
        } /* class DynamicNavigationMesh */ 

} /* namespace */

namespace Urho.Navigation {
        public partial struct NavigationObstacleRemovedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public Obstacle Obstacle => UrhoMap.get_Obstacle (handle, unchecked((int)1080511791) /* Obstacle (P_OBSTACLE) */);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, unchecked((int)1333256809) /* Position (P_POSITION) */);
            public float Radius => UrhoMap.get_float (handle, unchecked((int)4247146802) /* Radius (P_RADIUS) */);
            public float Height => UrhoMap.get_float (handle, unchecked((int)380957255) /* Height (P_HEIGHT) */);
        } /* struct NavigationObstacleRemovedEventArgs */

        public partial class DynamicNavigationMesh {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NavigationObstacleRemoved += ...' instead.")]
             public Subscription SubscribeToNavigationObstacleRemoved (Action<NavigationObstacleRemovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NavigationObstacleRemovedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3812914173) /* NavigationObstacleRemoved (E_NAVIGATION_OBSTACLE_REMOVED) */);
                  return s;
             }

             static UrhoEventAdapter<NavigationObstacleRemovedEventArgs> eventAdapterForNavigationObstacleRemoved;
             public event Action<NavigationObstacleRemovedEventArgs> NavigationObstacleRemoved
             {
                 add
                 {
                      if (eventAdapterForNavigationObstacleRemoved == null)
                          eventAdapterForNavigationObstacleRemoved = new UrhoEventAdapter<NavigationObstacleRemovedEventArgs>(typeof(DynamicNavigationMesh));
                      eventAdapterForNavigationObstacleRemoved.AddManagedSubscriber(handle, value, SubscribeToNavigationObstacleRemoved);
                 }
                 remove { eventAdapterForNavigationObstacleRemoved.RemoveManagedSubscriber(handle, value); }
             }
        } /* class DynamicNavigationMesh */ 

} /* namespace */

namespace Urho.Network {
        public partial struct ServerConnectedEventArgs {
            internal IntPtr handle;
        } /* struct ServerConnectedEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ServerConnected += ...' instead.")]
             public Subscription SubscribeToServerConnected (Action<ServerConnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ServerConnectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)4052463078) /* ServerConnected (E_SERVERCONNECTED) */);
                  return s;
             }

             static UrhoEventAdapter<ServerConnectedEventArgs> eventAdapterForServerConnected;
             public event Action<ServerConnectedEventArgs> ServerConnected
             {
                 add
                 {
                      if (eventAdapterForServerConnected == null)
                          eventAdapterForServerConnected = new UrhoEventAdapter<ServerConnectedEventArgs>(typeof(Network));
                      eventAdapterForServerConnected.AddManagedSubscriber(handle, value, SubscribeToServerConnected);
                 }
                 remove { eventAdapterForServerConnected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Network */ 

} /* namespace */

namespace Urho.Network {
        public partial struct ServerDisconnectedEventArgs {
            internal IntPtr handle;
        } /* struct ServerDisconnectedEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ServerDisconnected += ...' instead.")]
             public Subscription SubscribeToServerDisconnected (Action<ServerDisconnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ServerDisconnectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)830421502) /* ServerDisconnected (E_SERVERDISCONNECTED) */);
                  return s;
             }

             static UrhoEventAdapter<ServerDisconnectedEventArgs> eventAdapterForServerDisconnected;
             public event Action<ServerDisconnectedEventArgs> ServerDisconnected
             {
                 add
                 {
                      if (eventAdapterForServerDisconnected == null)
                          eventAdapterForServerDisconnected = new UrhoEventAdapter<ServerDisconnectedEventArgs>(typeof(Network));
                      eventAdapterForServerDisconnected.AddManagedSubscriber(handle, value, SubscribeToServerDisconnected);
                 }
                 remove { eventAdapterForServerDisconnected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Network */ 

} /* namespace */

namespace Urho.Network {
        public partial struct ConnectFailedEventArgs {
            internal IntPtr handle;
        } /* struct ConnectFailedEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ConnectFailed += ...' instead.")]
             public Subscription SubscribeToConnectFailed (Action<ConnectFailedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ConnectFailedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1377395431) /* ConnectFailed (E_CONNECTFAILED) */);
                  return s;
             }

             static UrhoEventAdapter<ConnectFailedEventArgs> eventAdapterForConnectFailed;
             public event Action<ConnectFailedEventArgs> ConnectFailed
             {
                 add
                 {
                      if (eventAdapterForConnectFailed == null)
                          eventAdapterForConnectFailed = new UrhoEventAdapter<ConnectFailedEventArgs>(typeof(Network));
                      eventAdapterForConnectFailed.AddManagedSubscriber(handle, value, SubscribeToConnectFailed);
                 }
                 remove { eventAdapterForConnectFailed.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Network */ 

} /* namespace */

namespace Urho.Network {
        public partial struct ClientConnectedEventArgs {
            internal IntPtr handle;
            public Connection Connection => UrhoMap.get_Connection (handle, unchecked((int)2179499454) /* Connection (P_CONNECTION) */);
        } /* struct ClientConnectedEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ClientConnected += ...' instead.")]
             public Subscription SubscribeToClientConnected (Action<ClientConnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ClientConnectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)4110472926) /* ClientConnected (E_CLIENTCONNECTED) */);
                  return s;
             }

             static UrhoEventAdapter<ClientConnectedEventArgs> eventAdapterForClientConnected;
             public event Action<ClientConnectedEventArgs> ClientConnected
             {
                 add
                 {
                      if (eventAdapterForClientConnected == null)
                          eventAdapterForClientConnected = new UrhoEventAdapter<ClientConnectedEventArgs>(typeof(Network));
                      eventAdapterForClientConnected.AddManagedSubscriber(handle, value, SubscribeToClientConnected);
                 }
                 remove { eventAdapterForClientConnected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Network */ 

} /* namespace */

namespace Urho.Network {
        public partial struct ClientDisconnectedEventArgs {
            internal IntPtr handle;
            public Connection Connection => UrhoMap.get_Connection (handle, unchecked((int)2179499454) /* Connection (P_CONNECTION) */);
        } /* struct ClientDisconnectedEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ClientDisconnected += ...' instead.")]
             public Subscription SubscribeToClientDisconnected (Action<ClientDisconnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ClientDisconnectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)4177677062) /* ClientDisconnected (E_CLIENTDISCONNECTED) */);
                  return s;
             }

             static UrhoEventAdapter<ClientDisconnectedEventArgs> eventAdapterForClientDisconnected;
             public event Action<ClientDisconnectedEventArgs> ClientDisconnected
             {
                 add
                 {
                      if (eventAdapterForClientDisconnected == null)
                          eventAdapterForClientDisconnected = new UrhoEventAdapter<ClientDisconnectedEventArgs>(typeof(Network));
                      eventAdapterForClientDisconnected.AddManagedSubscriber(handle, value, SubscribeToClientDisconnected);
                 }
                 remove { eventAdapterForClientDisconnected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Network */ 

} /* namespace */

namespace Urho.Network {
        public partial struct ClientIdentityEventArgs {
            internal IntPtr handle;
            public Connection Connection => UrhoMap.get_Connection (handle, unchecked((int)2179499454) /* Connection (P_CONNECTION) */);
            public bool Allow => UrhoMap.get_bool (handle, unchecked((int)2467149353) /* Allow (P_ALLOW) */);
        } /* struct ClientIdentityEventArgs */

        public partial class Connection {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ClientIdentity += ...' instead.")]
             public Subscription SubscribeToClientIdentity (Action<ClientIdentityEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ClientIdentityEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)165479177) /* ClientIdentity (E_CLIENTIDENTITY) */);
                  return s;
             }

             static UrhoEventAdapter<ClientIdentityEventArgs> eventAdapterForClientIdentity;
             public event Action<ClientIdentityEventArgs> ClientIdentity
             {
                 add
                 {
                      if (eventAdapterForClientIdentity == null)
                          eventAdapterForClientIdentity = new UrhoEventAdapter<ClientIdentityEventArgs>(typeof(Connection));
                      eventAdapterForClientIdentity.AddManagedSubscriber(handle, value, SubscribeToClientIdentity);
                 }
                 remove { eventAdapterForClientIdentity.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Connection */ 

} /* namespace */

namespace Urho.Network {
        public partial struct ClientSceneLoadedEventArgs {
            internal IntPtr handle;
            public Connection Connection => UrhoMap.get_Connection (handle, unchecked((int)2179499454) /* Connection (P_CONNECTION) */);
        } /* struct ClientSceneLoadedEventArgs */

        public partial class Connection {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ClientSceneLoaded += ...' instead.")]
             public Subscription SubscribeToClientSceneLoaded (Action<ClientSceneLoadedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ClientSceneLoadedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2870394214) /* ClientSceneLoaded (E_CLIENTSCENELOADED) */);
                  return s;
             }

             static UrhoEventAdapter<ClientSceneLoadedEventArgs> eventAdapterForClientSceneLoaded;
             public event Action<ClientSceneLoadedEventArgs> ClientSceneLoaded
             {
                 add
                 {
                      if (eventAdapterForClientSceneLoaded == null)
                          eventAdapterForClientSceneLoaded = new UrhoEventAdapter<ClientSceneLoadedEventArgs>(typeof(Connection));
                      eventAdapterForClientSceneLoaded.AddManagedSubscriber(handle, value, SubscribeToClientSceneLoaded);
                 }
                 remove { eventAdapterForClientSceneLoaded.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Connection */ 

} /* namespace */

namespace Urho.Network {
        public partial struct NetworkMessageEventArgs {
            internal IntPtr handle;
            public Connection Connection => UrhoMap.get_Connection (handle, unchecked((int)2179499454) /* Connection (P_CONNECTION) */);
            public int MessageID => UrhoMap.get_int (handle, unchecked((int)169676386) /* MessageID (P_MESSAGEID) */);
            public byte [] Data => UrhoMap.get_Buffer (handle, unchecked((int)1558284138) /* Data (P_DATA) */);
        } /* struct NetworkMessageEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NetworkMessage += ...' instead.")]
             public Subscription SubscribeToNetworkMessage (Action<NetworkMessageEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NetworkMessageEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)511054905) /* NetworkMessage (E_NETWORKMESSAGE) */);
                  return s;
             }

             static UrhoEventAdapter<NetworkMessageEventArgs> eventAdapterForNetworkMessage;
             public event Action<NetworkMessageEventArgs> NetworkMessage
             {
                 add
                 {
                      if (eventAdapterForNetworkMessage == null)
                          eventAdapterForNetworkMessage = new UrhoEventAdapter<NetworkMessageEventArgs>(typeof(Network));
                      eventAdapterForNetworkMessage.AddManagedSubscriber(handle, value, SubscribeToNetworkMessage);
                 }
                 remove { eventAdapterForNetworkMessage.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Network */ 

} /* namespace */

namespace Urho.Network {
        public partial struct NetworkUpdateEventArgs {
            internal IntPtr handle;
        } /* struct NetworkUpdateEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NetworkUpdate += ...' instead.")]
             public Subscription SubscribeToNetworkUpdate (Action<NetworkUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NetworkUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3682502807) /* NetworkUpdate (E_NETWORKUPDATE) */);
                  return s;
             }

             static UrhoEventAdapter<NetworkUpdateEventArgs> eventAdapterForNetworkUpdate;
             public event Action<NetworkUpdateEventArgs> NetworkUpdate
             {
                 add
                 {
                      if (eventAdapterForNetworkUpdate == null)
                          eventAdapterForNetworkUpdate = new UrhoEventAdapter<NetworkUpdateEventArgs>(typeof(Network));
                      eventAdapterForNetworkUpdate.AddManagedSubscriber(handle, value, SubscribeToNetworkUpdate);
                 }
                 remove { eventAdapterForNetworkUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Network */ 

} /* namespace */

namespace Urho.Network {
        public partial struct NetworkUpdateSentEventArgs {
            internal IntPtr handle;
        } /* struct NetworkUpdateSentEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NetworkUpdateSent += ...' instead.")]
             public Subscription SubscribeToNetworkUpdateSent (Action<NetworkUpdateSentEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NetworkUpdateSentEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1495044303) /* NetworkUpdateSent (E_NETWORKUPDATESENT) */);
                  return s;
             }

             static UrhoEventAdapter<NetworkUpdateSentEventArgs> eventAdapterForNetworkUpdateSent;
             public event Action<NetworkUpdateSentEventArgs> NetworkUpdateSent
             {
                 add
                 {
                      if (eventAdapterForNetworkUpdateSent == null)
                          eventAdapterForNetworkUpdateSent = new UrhoEventAdapter<NetworkUpdateSentEventArgs>(typeof(Network));
                      eventAdapterForNetworkUpdateSent.AddManagedSubscriber(handle, value, SubscribeToNetworkUpdateSent);
                 }
                 remove { eventAdapterForNetworkUpdateSent.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Network */ 

} /* namespace */

namespace Urho.Network {
        public partial struct NetworkSceneLoadFailedEventArgs {
            internal IntPtr handle;
            public Connection Connection => UrhoMap.get_Connection (handle, unchecked((int)2179499454) /* Connection (P_CONNECTION) */);
        } /* struct NetworkSceneLoadFailedEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NetworkSceneLoadFailed += ...' instead.")]
             public Subscription SubscribeToNetworkSceneLoadFailed (Action<NetworkSceneLoadFailedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NetworkSceneLoadFailedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3832128641) /* NetworkSceneLoadFailed (E_NETWORKSCENELOADFAILED) */);
                  return s;
             }

             static UrhoEventAdapter<NetworkSceneLoadFailedEventArgs> eventAdapterForNetworkSceneLoadFailed;
             public event Action<NetworkSceneLoadFailedEventArgs> NetworkSceneLoadFailed
             {
                 add
                 {
                      if (eventAdapterForNetworkSceneLoadFailed == null)
                          eventAdapterForNetworkSceneLoadFailed = new UrhoEventAdapter<NetworkSceneLoadFailedEventArgs>(typeof(Network));
                      eventAdapterForNetworkSceneLoadFailed.AddManagedSubscriber(handle, value, SubscribeToNetworkSceneLoadFailed);
                 }
                 remove { eventAdapterForNetworkSceneLoadFailed.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Network */ 

} /* namespace */

namespace Urho.Network {
        public partial struct RemoteEventDataEventArgs {
            internal IntPtr handle;
            public Connection Connection => UrhoMap.get_Connection (handle, unchecked((int)2179499454) /* Connection (P_CONNECTION) */);
        } /* struct RemoteEventDataEventArgs */

} /* namespace */

namespace Urho.Physics {
        public partial struct PhysicsPreStepEventArgs {
            internal IntPtr handle;
            public PhysicsWorld World => UrhoMap.get_PhysicsWorld (handle, unchecked((int)4158893746) /* World (P_WORLD) */);
            public float TimeStep => UrhoMap.get_float (handle, unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct PhysicsPreStepEventArgs */

        public partial class PhysicsWorld {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PhysicsPreStep += ...' instead.")]
             public Subscription SubscribeToPhysicsPreStep (Action<PhysicsPreStepEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsPreStepEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2540038056) /* PhysicsPreStep (E_PHYSICSPRESTEP) */);
                  return s;
             }

             static UrhoEventAdapter<PhysicsPreStepEventArgs> eventAdapterForPhysicsPreStep;
             public event Action<PhysicsPreStepEventArgs> PhysicsPreStep
             {
                 add
                 {
                      if (eventAdapterForPhysicsPreStep == null)
                          eventAdapterForPhysicsPreStep = new UrhoEventAdapter<PhysicsPreStepEventArgs>(typeof(PhysicsWorld));
                      eventAdapterForPhysicsPreStep.AddManagedSubscriber(handle, value, SubscribeToPhysicsPreStep);
                 }
                 remove { eventAdapterForPhysicsPreStep.RemoveManagedSubscriber(handle, value); }
             }
        } /* class PhysicsWorld */ 

} /* namespace */

namespace Urho.Physics {
        public partial struct PhysicsPostStepEventArgs {
            internal IntPtr handle;
            public PhysicsWorld World => UrhoMap.get_PhysicsWorld (handle, unchecked((int)4158893746) /* World (P_WORLD) */);
            public float TimeStep => UrhoMap.get_float (handle, unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct PhysicsPostStepEventArgs */

        public partial class PhysicsWorld {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PhysicsPostStep += ...' instead.")]
             public Subscription SubscribeToPhysicsPostStep (Action<PhysicsPostStepEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsPostStepEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)4200987859) /* PhysicsPostStep (E_PHYSICSPOSTSTEP) */);
                  return s;
             }

             static UrhoEventAdapter<PhysicsPostStepEventArgs> eventAdapterForPhysicsPostStep;
             public event Action<PhysicsPostStepEventArgs> PhysicsPostStep
             {
                 add
                 {
                      if (eventAdapterForPhysicsPostStep == null)
                          eventAdapterForPhysicsPostStep = new UrhoEventAdapter<PhysicsPostStepEventArgs>(typeof(PhysicsWorld));
                      eventAdapterForPhysicsPostStep.AddManagedSubscriber(handle, value, SubscribeToPhysicsPostStep);
                 }
                 remove { eventAdapterForPhysicsPostStep.RemoveManagedSubscriber(handle, value); }
             }
        } /* class PhysicsWorld */ 

} /* namespace */

namespace Urho.Physics {
        public partial struct PhysicsCollisionStartEventArgs {
            internal IntPtr handle;
            public PhysicsWorld World => UrhoMap.get_PhysicsWorld (handle, unchecked((int)4158893746) /* World (P_WORLD) */);
            public Node NodeA => UrhoMap.get_Node (handle, unchecked((int)2376629471) /* NodeA (P_NODEA) */);
            public Node NodeB => UrhoMap.get_Node (handle, unchecked((int)2376629472) /* NodeB (P_NODEB) */);
            public RigidBody BodyA => UrhoMap.get_RigidBody (handle, unchecked((int)1588071871) /* BodyA (P_BODYA) */);
            public RigidBody BodyB => UrhoMap.get_RigidBody (handle, unchecked((int)1588071872) /* BodyB (P_BODYB) */);
            public bool Trigger => UrhoMap.get_bool (handle, unchecked((int)2995104504) /* Trigger (P_TRIGGER) */);
            public CollisionData [] Contacts => UrhoMap.get_CollisionData (handle, unchecked((int)216739987) /* Contacts (P_CONTACTS) */);
        } /* struct PhysicsCollisionStartEventArgs */

        public partial class PhysicsWorld {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PhysicsCollisionStart += ...' instead.")]
             public Subscription SubscribeToPhysicsCollisionStart (Action<PhysicsCollisionStartEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsCollisionStartEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3207652439) /* PhysicsCollisionStart (E_PHYSICSCOLLISIONSTART) */);
                  return s;
             }

             static UrhoEventAdapter<PhysicsCollisionStartEventArgs> eventAdapterForPhysicsCollisionStart;
             public event Action<PhysicsCollisionStartEventArgs> PhysicsCollisionStart
             {
                 add
                 {
                      if (eventAdapterForPhysicsCollisionStart == null)
                          eventAdapterForPhysicsCollisionStart = new UrhoEventAdapter<PhysicsCollisionStartEventArgs>(typeof(PhysicsWorld));
                      eventAdapterForPhysicsCollisionStart.AddManagedSubscriber(handle, value, SubscribeToPhysicsCollisionStart);
                 }
                 remove { eventAdapterForPhysicsCollisionStart.RemoveManagedSubscriber(handle, value); }
             }
        } /* class PhysicsWorld */ 

} /* namespace */

namespace Urho.Physics {
        public partial struct PhysicsCollisionEventArgs {
            internal IntPtr handle;
            public PhysicsWorld World => UrhoMap.get_PhysicsWorld (handle, unchecked((int)4158893746) /* World (P_WORLD) */);
            public Node NodeA => UrhoMap.get_Node (handle, unchecked((int)2376629471) /* NodeA (P_NODEA) */);
            public Node NodeB => UrhoMap.get_Node (handle, unchecked((int)2376629472) /* NodeB (P_NODEB) */);
            public RigidBody BodyA => UrhoMap.get_RigidBody (handle, unchecked((int)1588071871) /* BodyA (P_BODYA) */);
            public RigidBody BodyB => UrhoMap.get_RigidBody (handle, unchecked((int)1588071872) /* BodyB (P_BODYB) */);
            public bool Trigger => UrhoMap.get_bool (handle, unchecked((int)2995104504) /* Trigger (P_TRIGGER) */);
            public CollisionData [] Contacts => UrhoMap.get_CollisionData (handle, unchecked((int)216739987) /* Contacts (P_CONTACTS) */);
        } /* struct PhysicsCollisionEventArgs */

        public partial class PhysicsWorld {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PhysicsCollision += ...' instead.")]
             public Subscription SubscribeToPhysicsCollision (Action<PhysicsCollisionEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsCollisionEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2202188235) /* PhysicsCollision (E_PHYSICSCOLLISION) */);
                  return s;
             }

             static UrhoEventAdapter<PhysicsCollisionEventArgs> eventAdapterForPhysicsCollision;
             public event Action<PhysicsCollisionEventArgs> PhysicsCollision
             {
                 add
                 {
                      if (eventAdapterForPhysicsCollision == null)
                          eventAdapterForPhysicsCollision = new UrhoEventAdapter<PhysicsCollisionEventArgs>(typeof(PhysicsWorld));
                      eventAdapterForPhysicsCollision.AddManagedSubscriber(handle, value, SubscribeToPhysicsCollision);
                 }
                 remove { eventAdapterForPhysicsCollision.RemoveManagedSubscriber(handle, value); }
             }
        } /* class PhysicsWorld */ 

} /* namespace */

namespace Urho.Physics {
        public partial struct PhysicsCollisionEndEventArgs {
            internal IntPtr handle;
            public PhysicsWorld World => UrhoMap.get_PhysicsWorld (handle, unchecked((int)4158893746) /* World (P_WORLD) */);
            public Node NodeA => UrhoMap.get_Node (handle, unchecked((int)2376629471) /* NodeA (P_NODEA) */);
            public Node NodeB => UrhoMap.get_Node (handle, unchecked((int)2376629472) /* NodeB (P_NODEB) */);
            public RigidBody BodyA => UrhoMap.get_RigidBody (handle, unchecked((int)1588071871) /* BodyA (P_BODYA) */);
            public RigidBody BodyB => UrhoMap.get_RigidBody (handle, unchecked((int)1588071872) /* BodyB (P_BODYB) */);
            public bool Trigger => UrhoMap.get_bool (handle, unchecked((int)2995104504) /* Trigger (P_TRIGGER) */);
        } /* struct PhysicsCollisionEndEventArgs */

        public partial class PhysicsWorld {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PhysicsCollisionEnd += ...' instead.")]
             public Subscription SubscribeToPhysicsCollisionEnd (Action<PhysicsCollisionEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsCollisionEndEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)304728016) /* PhysicsCollisionEnd (E_PHYSICSCOLLISIONEND) */);
                  return s;
             }

             static UrhoEventAdapter<PhysicsCollisionEndEventArgs> eventAdapterForPhysicsCollisionEnd;
             public event Action<PhysicsCollisionEndEventArgs> PhysicsCollisionEnd
             {
                 add
                 {
                      if (eventAdapterForPhysicsCollisionEnd == null)
                          eventAdapterForPhysicsCollisionEnd = new UrhoEventAdapter<PhysicsCollisionEndEventArgs>(typeof(PhysicsWorld));
                      eventAdapterForPhysicsCollisionEnd.AddManagedSubscriber(handle, value, SubscribeToPhysicsCollisionEnd);
                 }
                 remove { eventAdapterForPhysicsCollisionEnd.RemoveManagedSubscriber(handle, value); }
             }
        } /* class PhysicsWorld */ 

} /* namespace */

namespace Urho {
        public partial struct NodeCollisionStartEventArgs {
            internal IntPtr handle;
            public RigidBody Body => UrhoMap.get_RigidBody (handle, unchecked((int)111721250) /* Body (P_BODY) */);
            public Node OtherNode => UrhoMap.get_Node (handle, unchecked((int)2707292594) /* OtherNode (P_OTHERNODE) */);
            public RigidBody OtherBody => UrhoMap.get_RigidBody (handle, unchecked((int)1930180818) /* OtherBody (P_OTHERBODY) */);
            public bool Trigger => UrhoMap.get_bool (handle, unchecked((int)2995104504) /* Trigger (P_TRIGGER) */);
            public CollisionData [] Contacts => UrhoMap.get_CollisionData (handle, unchecked((int)216739987) /* Contacts (P_CONTACTS) */);
        } /* struct NodeCollisionStartEventArgs */

        public partial class Node {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NodeCollisionStart += ...' instead.")]
             public Subscription SubscribeToNodeCollisionStart (Action<NodeCollisionStartEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeCollisionStartEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2797145554) /* NodeCollisionStart (E_NODECOLLISIONSTART) */);
                  return s;
             }

             static UrhoEventAdapter<NodeCollisionStartEventArgs> eventAdapterForNodeCollisionStart;
             public event Action<NodeCollisionStartEventArgs> NodeCollisionStart
             {
                 add
                 {
                      if (eventAdapterForNodeCollisionStart == null)
                          eventAdapterForNodeCollisionStart = new UrhoEventAdapter<NodeCollisionStartEventArgs>(typeof(Node));
                      eventAdapterForNodeCollisionStart.AddManagedSubscriber(handle, value, SubscribeToNodeCollisionStart);
                 }
                 remove { eventAdapterForNodeCollisionStart.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Node */ 

} /* namespace */

namespace Urho {
        public partial struct NodeCollisionEventArgs {
            internal IntPtr handle;
            public RigidBody Body => UrhoMap.get_RigidBody (handle, unchecked((int)111721250) /* Body (P_BODY) */);
            public Node OtherNode => UrhoMap.get_Node (handle, unchecked((int)2707292594) /* OtherNode (P_OTHERNODE) */);
            public RigidBody OtherBody => UrhoMap.get_RigidBody (handle, unchecked((int)1930180818) /* OtherBody (P_OTHERBODY) */);
            public bool Trigger => UrhoMap.get_bool (handle, unchecked((int)2995104504) /* Trigger (P_TRIGGER) */);
            public CollisionData [] Contacts => UrhoMap.get_CollisionData (handle, unchecked((int)216739987) /* Contacts (P_CONTACTS) */);
        } /* struct NodeCollisionEventArgs */

        public partial class Node {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NodeCollision += ...' instead.")]
             public Subscription SubscribeToNodeCollision (Action<NodeCollisionEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeCollisionEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2675467920) /* NodeCollision (E_NODECOLLISION) */);
                  return s;
             }

             static UrhoEventAdapter<NodeCollisionEventArgs> eventAdapterForNodeCollision;
             public event Action<NodeCollisionEventArgs> NodeCollision
             {
                 add
                 {
                      if (eventAdapterForNodeCollision == null)
                          eventAdapterForNodeCollision = new UrhoEventAdapter<NodeCollisionEventArgs>(typeof(Node));
                      eventAdapterForNodeCollision.AddManagedSubscriber(handle, value, SubscribeToNodeCollision);
                 }
                 remove { eventAdapterForNodeCollision.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Node */ 

} /* namespace */

namespace Urho {
        public partial struct NodeCollisionEndEventArgs {
            internal IntPtr handle;
            public RigidBody Body => UrhoMap.get_RigidBody (handle, unchecked((int)111721250) /* Body (P_BODY) */);
            public Node OtherNode => UrhoMap.get_Node (handle, unchecked((int)2707292594) /* OtherNode (P_OTHERNODE) */);
            public RigidBody OtherBody => UrhoMap.get_RigidBody (handle, unchecked((int)1930180818) /* OtherBody (P_OTHERBODY) */);
            public bool Trigger => UrhoMap.get_bool (handle, unchecked((int)2995104504) /* Trigger (P_TRIGGER) */);
        } /* struct NodeCollisionEndEventArgs */

        public partial class Node {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NodeCollisionEnd += ...' instead.")]
             public Subscription SubscribeToNodeCollisionEnd (Action<NodeCollisionEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeCollisionEndEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2410921675) /* NodeCollisionEnd (E_NODECOLLISIONEND) */);
                  return s;
             }

             static UrhoEventAdapter<NodeCollisionEndEventArgs> eventAdapterForNodeCollisionEnd;
             public event Action<NodeCollisionEndEventArgs> NodeCollisionEnd
             {
                 add
                 {
                      if (eventAdapterForNodeCollisionEnd == null)
                          eventAdapterForNodeCollisionEnd = new UrhoEventAdapter<NodeCollisionEndEventArgs>(typeof(Node));
                      eventAdapterForNodeCollisionEnd.AddManagedSubscriber(handle, value, SubscribeToNodeCollisionEnd);
                 }
                 remove { eventAdapterForNodeCollisionEnd.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Node */ 

} /* namespace */

namespace Urho.Resources {
        public partial struct ReloadStartedEventArgs {
            internal IntPtr handle;
        } /* struct ReloadStartedEventArgs */

        public partial class Resource {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ReloadStarted += ...' instead.")]
             public Subscription SubscribeToReloadStarted (Action<ReloadStartedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ReloadStartedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)213872936) /* ReloadStarted (E_RELOADSTARTED) */);
                  return s;
             }

             static UrhoEventAdapter<ReloadStartedEventArgs> eventAdapterForReloadStarted;
             public event Action<ReloadStartedEventArgs> ReloadStarted
             {
                 add
                 {
                      if (eventAdapterForReloadStarted == null)
                          eventAdapterForReloadStarted = new UrhoEventAdapter<ReloadStartedEventArgs>(typeof(Resource));
                      eventAdapterForReloadStarted.AddManagedSubscriber(handle, value, SubscribeToReloadStarted);
                 }
                 remove { eventAdapterForReloadStarted.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Resource */ 

} /* namespace */

namespace Urho.Resources {
        public partial struct ReloadFinishedEventArgs {
            internal IntPtr handle;
        } /* struct ReloadFinishedEventArgs */

        public partial class Resource {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ReloadFinished += ...' instead.")]
             public Subscription SubscribeToReloadFinished (Action<ReloadFinishedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ReloadFinishedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2825685547) /* ReloadFinished (E_RELOADFINISHED) */);
                  return s;
             }

             static UrhoEventAdapter<ReloadFinishedEventArgs> eventAdapterForReloadFinished;
             public event Action<ReloadFinishedEventArgs> ReloadFinished
             {
                 add
                 {
                      if (eventAdapterForReloadFinished == null)
                          eventAdapterForReloadFinished = new UrhoEventAdapter<ReloadFinishedEventArgs>(typeof(Resource));
                      eventAdapterForReloadFinished.AddManagedSubscriber(handle, value, SubscribeToReloadFinished);
                 }
                 remove { eventAdapterForReloadFinished.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Resource */ 

} /* namespace */

namespace Urho.Resources {
        public partial struct ReloadFailedEventArgs {
            internal IntPtr handle;
        } /* struct ReloadFailedEventArgs */

        public partial class Resource {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ReloadFailed += ...' instead.")]
             public Subscription SubscribeToReloadFailed (Action<ReloadFailedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ReloadFailedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)4176168502) /* ReloadFailed (E_RELOADFAILED) */);
                  return s;
             }

             static UrhoEventAdapter<ReloadFailedEventArgs> eventAdapterForReloadFailed;
             public event Action<ReloadFailedEventArgs> ReloadFailed
             {
                 add
                 {
                      if (eventAdapterForReloadFailed == null)
                          eventAdapterForReloadFailed = new UrhoEventAdapter<ReloadFailedEventArgs>(typeof(Resource));
                      eventAdapterForReloadFailed.AddManagedSubscriber(handle, value, SubscribeToReloadFailed);
                 }
                 remove { eventAdapterForReloadFailed.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Resource */ 

} /* namespace */

namespace Urho.Resources {
        public partial struct FileChangedEventArgs {
            internal IntPtr handle;
            public String FileName => UrhoMap.get_String (handle, unchecked((int)633459751) /* FileName (P_FILENAME) */);
            public String ResourceName => UrhoMap.get_String (handle, unchecked((int)4134618969) /* ResourceName (P_RESOURCENAME) */);
        } /* struct FileChangedEventArgs */

        public partial class ResourceCache {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.FileChanged += ...' instead.")]
             public Subscription SubscribeToFileChanged (Action<FileChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FileChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3297483544) /* FileChanged (E_FILECHANGED) */);
                  return s;
             }

             static UrhoEventAdapter<FileChangedEventArgs> eventAdapterForFileChanged;
             public event Action<FileChangedEventArgs> FileChanged
             {
                 add
                 {
                      if (eventAdapterForFileChanged == null)
                          eventAdapterForFileChanged = new UrhoEventAdapter<FileChangedEventArgs>(typeof(ResourceCache));
                      eventAdapterForFileChanged.AddManagedSubscriber(handle, value, SubscribeToFileChanged);
                 }
                 remove { eventAdapterForFileChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ResourceCache */ 

} /* namespace */

namespace Urho.Resources {
        public partial struct LoadFailedEventArgs {
            internal IntPtr handle;
            public String ResourceName => UrhoMap.get_String (handle, unchecked((int)4134618969) /* ResourceName (P_RESOURCENAME) */);
        } /* struct LoadFailedEventArgs */

        public partial class ResourceCache {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.LoadFailed += ...' instead.")]
             public Subscription SubscribeToLoadFailed (Action<LoadFailedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new LoadFailedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2824526147) /* LoadFailed (E_LOADFAILED) */);
                  return s;
             }

             static UrhoEventAdapter<LoadFailedEventArgs> eventAdapterForLoadFailed;
             public event Action<LoadFailedEventArgs> LoadFailed
             {
                 add
                 {
                      if (eventAdapterForLoadFailed == null)
                          eventAdapterForLoadFailed = new UrhoEventAdapter<LoadFailedEventArgs>(typeof(ResourceCache));
                      eventAdapterForLoadFailed.AddManagedSubscriber(handle, value, SubscribeToLoadFailed);
                 }
                 remove { eventAdapterForLoadFailed.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ResourceCache */ 

} /* namespace */

namespace Urho.Resources {
        public partial struct ResourceNotFoundEventArgs {
            internal IntPtr handle;
            public String ResourceName => UrhoMap.get_String (handle, unchecked((int)4134618969) /* ResourceName (P_RESOURCENAME) */);
        } /* struct ResourceNotFoundEventArgs */

        public partial class ResourceCache {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ResourceNotFound += ...' instead.")]
             public Subscription SubscribeToResourceNotFound (Action<ResourceNotFoundEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ResourceNotFoundEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)217257309) /* ResourceNotFound (E_RESOURCENOTFOUND) */);
                  return s;
             }

             static UrhoEventAdapter<ResourceNotFoundEventArgs> eventAdapterForResourceNotFound;
             public event Action<ResourceNotFoundEventArgs> ResourceNotFound
             {
                 add
                 {
                      if (eventAdapterForResourceNotFound == null)
                          eventAdapterForResourceNotFound = new UrhoEventAdapter<ResourceNotFoundEventArgs>(typeof(ResourceCache));
                      eventAdapterForResourceNotFound.AddManagedSubscriber(handle, value, SubscribeToResourceNotFound);
                 }
                 remove { eventAdapterForResourceNotFound.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ResourceCache */ 

} /* namespace */

namespace Urho.Resources {
        public partial struct UnknownResourceTypeEventArgs {
            internal IntPtr handle;
            public StringHash ResourceType => UrhoMap.get_StringHash (handle, unchecked((int)426680488) /* ResourceType (P_RESOURCETYPE) */);
        } /* struct UnknownResourceTypeEventArgs */

        public partial class ResourceCache {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.UnknownResourceType += ...' instead.")]
             public Subscription SubscribeToUnknownResourceType (Action<UnknownResourceTypeEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UnknownResourceTypeEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2352310994) /* UnknownResourceType (E_UNKNOWNRESOURCETYPE) */);
                  return s;
             }

             static UrhoEventAdapter<UnknownResourceTypeEventArgs> eventAdapterForUnknownResourceType;
             public event Action<UnknownResourceTypeEventArgs> UnknownResourceType
             {
                 add
                 {
                      if (eventAdapterForUnknownResourceType == null)
                          eventAdapterForUnknownResourceType = new UrhoEventAdapter<UnknownResourceTypeEventArgs>(typeof(ResourceCache));
                      eventAdapterForUnknownResourceType.AddManagedSubscriber(handle, value, SubscribeToUnknownResourceType);
                 }
                 remove { eventAdapterForUnknownResourceType.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ResourceCache */ 

} /* namespace */

namespace Urho.Resources {
        public partial struct ResourceBackgroundLoadedEventArgs {
            internal IntPtr handle;
            public String ResourceName => UrhoMap.get_String (handle, unchecked((int)4134618969) /* ResourceName (P_RESOURCENAME) */);
            public bool Success => UrhoMap.get_bool (handle, unchecked((int)3427551139) /* Success (P_SUCCESS) */);
            public Resource Resource => UrhoMap.get_Resource (handle, unchecked((int)39946286) /* Resource (P_RESOURCE) */);
        } /* struct ResourceBackgroundLoadedEventArgs */

        public partial class ResourceCache {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ResourceBackgroundLoaded += ...' instead.")]
             public Subscription SubscribeToResourceBackgroundLoaded (Action<ResourceBackgroundLoadedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ResourceBackgroundLoadedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)4263239041) /* ResourceBackgroundLoaded (E_RESOURCEBACKGROUNDLOADED) */);
                  return s;
             }

             static UrhoEventAdapter<ResourceBackgroundLoadedEventArgs> eventAdapterForResourceBackgroundLoaded;
             public event Action<ResourceBackgroundLoadedEventArgs> ResourceBackgroundLoaded
             {
                 add
                 {
                      if (eventAdapterForResourceBackgroundLoaded == null)
                          eventAdapterForResourceBackgroundLoaded = new UrhoEventAdapter<ResourceBackgroundLoadedEventArgs>(typeof(ResourceCache));
                      eventAdapterForResourceBackgroundLoaded.AddManagedSubscriber(handle, value, SubscribeToResourceBackgroundLoaded);
                 }
                 remove { eventAdapterForResourceBackgroundLoaded.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ResourceCache */ 

} /* namespace */

namespace Urho.Resources {
        public partial struct ChangeLanguageEventArgs {
            internal IntPtr handle;
        } /* struct ChangeLanguageEventArgs */

        public partial class Localization {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ChangeLanguage += ...' instead.")]
             public Subscription SubscribeToChangeLanguage (Action<ChangeLanguageEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ChangeLanguageEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1337208360) /* ChangeLanguage (E_CHANGELANGUAGE) */);
                  return s;
             }

             static UrhoEventAdapter<ChangeLanguageEventArgs> eventAdapterForChangeLanguage;
             public event Action<ChangeLanguageEventArgs> ChangeLanguage
             {
                 add
                 {
                      if (eventAdapterForChangeLanguage == null)
                          eventAdapterForChangeLanguage = new UrhoEventAdapter<ChangeLanguageEventArgs>(typeof(Localization));
                      eventAdapterForChangeLanguage.AddManagedSubscriber(handle, value, SubscribeToChangeLanguage);
                 }
                 remove { eventAdapterForChangeLanguage.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Localization */ 

} /* namespace */

namespace Urho {
        public partial struct SceneUpdateEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public float TimeStep => UrhoMap.get_float (handle, unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct SceneUpdateEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.SceneUpdate += ...' instead.")]
             public Subscription SubscribeToSceneUpdate (Action<SceneUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SceneUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3145164885) /* SceneUpdate (E_SCENEUPDATE) */);
                  return s;
             }

             static UrhoEventAdapter<SceneUpdateEventArgs> eventAdapterForSceneUpdate;
             public event Action<SceneUpdateEventArgs> SceneUpdate
             {
                 add
                 {
                      if (eventAdapterForSceneUpdate == null)
                          eventAdapterForSceneUpdate = new UrhoEventAdapter<SceneUpdateEventArgs>(typeof(Scene));
                      eventAdapterForSceneUpdate.AddManagedSubscriber(handle, value, SubscribeToSceneUpdate);
                 }
                 remove { eventAdapterForSceneUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct SceneSubsystemUpdateEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public float TimeStep => UrhoMap.get_float (handle, unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct SceneSubsystemUpdateEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.SceneSubsystemUpdate += ...' instead.")]
             public Subscription SubscribeToSceneSubsystemUpdate (Action<SceneSubsystemUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SceneSubsystemUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1997371372) /* SceneSubsystemUpdate (E_SCENESUBSYSTEMUPDATE) */);
                  return s;
             }

             static UrhoEventAdapter<SceneSubsystemUpdateEventArgs> eventAdapterForSceneSubsystemUpdate;
             public event Action<SceneSubsystemUpdateEventArgs> SceneSubsystemUpdate
             {
                 add
                 {
                      if (eventAdapterForSceneSubsystemUpdate == null)
                          eventAdapterForSceneSubsystemUpdate = new UrhoEventAdapter<SceneSubsystemUpdateEventArgs>(typeof(Scene));
                      eventAdapterForSceneSubsystemUpdate.AddManagedSubscriber(handle, value, SubscribeToSceneSubsystemUpdate);
                 }
                 remove { eventAdapterForSceneSubsystemUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct UpdateSmoothingEventArgs {
            internal IntPtr handle;
            public float Constant => UrhoMap.get_float (handle, unchecked((int)1006513988) /* Constant (P_CONSTANT) */);
            public float SquaredSnapThreshold => UrhoMap.get_float (handle, unchecked((int)4276457658) /* SquaredSnapThreshold (P_SQUAREDSNAPTHRESHOLD) */);
        } /* struct UpdateSmoothingEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.UpdateSmoothing += ...' instead.")]
             public Subscription SubscribeToUpdateSmoothing (Action<UpdateSmoothingEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UpdateSmoothingEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)4083015947) /* UpdateSmoothing (E_UPDATESMOOTHING) */);
                  return s;
             }

             static UrhoEventAdapter<UpdateSmoothingEventArgs> eventAdapterForUpdateSmoothing;
             public event Action<UpdateSmoothingEventArgs> UpdateSmoothing
             {
                 add
                 {
                      if (eventAdapterForUpdateSmoothing == null)
                          eventAdapterForUpdateSmoothing = new UrhoEventAdapter<UpdateSmoothingEventArgs>(typeof(Scene));
                      eventAdapterForUpdateSmoothing.AddManagedSubscriber(handle, value, SubscribeToUpdateSmoothing);
                 }
                 remove { eventAdapterForUpdateSmoothing.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct SceneDrawableUpdateFinishedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public float TimeStep => UrhoMap.get_float (handle, unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct SceneDrawableUpdateFinishedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.SceneDrawableUpdateFinished += ...' instead.")]
             public Subscription SubscribeToSceneDrawableUpdateFinished (Action<SceneDrawableUpdateFinishedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SceneDrawableUpdateFinishedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)228472677) /* SceneDrawableUpdateFinished (E_SCENEDRAWABLEUPDATEFINISHED) */);
                  return s;
             }

             static UrhoEventAdapter<SceneDrawableUpdateFinishedEventArgs> eventAdapterForSceneDrawableUpdateFinished;
             public event Action<SceneDrawableUpdateFinishedEventArgs> SceneDrawableUpdateFinished
             {
                 add
                 {
                      if (eventAdapterForSceneDrawableUpdateFinished == null)
                          eventAdapterForSceneDrawableUpdateFinished = new UrhoEventAdapter<SceneDrawableUpdateFinishedEventArgs>(typeof(Scene));
                      eventAdapterForSceneDrawableUpdateFinished.AddManagedSubscriber(handle, value, SubscribeToSceneDrawableUpdateFinished);
                 }
                 remove { eventAdapterForSceneDrawableUpdateFinished.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct TargetPositionChangedEventArgs {
            internal IntPtr handle;
        } /* struct TargetPositionChangedEventArgs */

        public partial class SmoothedTransform {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TargetPositionChanged += ...' instead.")]
             public Subscription SubscribeToTargetPositionChanged (Action<TargetPositionChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TargetPositionChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3850327802) /* TargetPositionChanged (E_TARGETPOSITION) */);
                  return s;
             }

             static UrhoEventAdapter<TargetPositionChangedEventArgs> eventAdapterForTargetPositionChanged;
             public event Action<TargetPositionChangedEventArgs> TargetPositionChanged
             {
                 add
                 {
                      if (eventAdapterForTargetPositionChanged == null)
                          eventAdapterForTargetPositionChanged = new UrhoEventAdapter<TargetPositionChangedEventArgs>(typeof(SmoothedTransform));
                      eventAdapterForTargetPositionChanged.AddManagedSubscriber(handle, value, SubscribeToTargetPositionChanged);
                 }
                 remove { eventAdapterForTargetPositionChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class SmoothedTransform */ 

} /* namespace */

namespace Urho {
        public partial struct TargetRotationChangedEventArgs {
            internal IntPtr handle;
        } /* struct TargetRotationChangedEventArgs */

        public partial class SmoothedTransform {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TargetRotationChanged += ...' instead.")]
             public Subscription SubscribeToTargetRotationChanged (Action<TargetRotationChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TargetRotationChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3938072325) /* TargetRotationChanged (E_TARGETROTATION) */);
                  return s;
             }

             static UrhoEventAdapter<TargetRotationChangedEventArgs> eventAdapterForTargetRotationChanged;
             public event Action<TargetRotationChangedEventArgs> TargetRotationChanged
             {
                 add
                 {
                      if (eventAdapterForTargetRotationChanged == null)
                          eventAdapterForTargetRotationChanged = new UrhoEventAdapter<TargetRotationChangedEventArgs>(typeof(SmoothedTransform));
                      eventAdapterForTargetRotationChanged.AddManagedSubscriber(handle, value, SubscribeToTargetRotationChanged);
                 }
                 remove { eventAdapterForTargetRotationChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class SmoothedTransform */ 

} /* namespace */

namespace Urho {
        public partial struct AttributeAnimationUpdateEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public float TimeStep => UrhoMap.get_float (handle, unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct AttributeAnimationUpdateEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.AttributeAnimationUpdate += ...' instead.")]
             public Subscription SubscribeToAttributeAnimationUpdate (Action<AttributeAnimationUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AttributeAnimationUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1013401233) /* AttributeAnimationUpdate (E_ATTRIBUTEANIMATIONUPDATE) */);
                  return s;
             }

             static UrhoEventAdapter<AttributeAnimationUpdateEventArgs> eventAdapterForAttributeAnimationUpdate;
             public event Action<AttributeAnimationUpdateEventArgs> AttributeAnimationUpdate
             {
                 add
                 {
                      if (eventAdapterForAttributeAnimationUpdate == null)
                          eventAdapterForAttributeAnimationUpdate = new UrhoEventAdapter<AttributeAnimationUpdateEventArgs>(typeof(Scene));
                      eventAdapterForAttributeAnimationUpdate.AddManagedSubscriber(handle, value, SubscribeToAttributeAnimationUpdate);
                 }
                 remove { eventAdapterForAttributeAnimationUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct AttributeAnimationAddedEventArgs {
            internal IntPtr handle;
            public Object ObjectAnimation => UrhoMap.get_Object (handle, unchecked((int)485250565) /* ObjectAnimation (P_OBJECTANIMATION) */);
            public String AttributeAnimationName => UrhoMap.get_String (handle, unchecked((int)4253834771) /* AttributeAnimationName (P_ATTRIBUTEANIMATIONNAME) */);
        } /* struct AttributeAnimationAddedEventArgs */

        public partial class ObjectAnimation {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.AttributeAnimationAdded += ...' instead.")]
             public Subscription SubscribeToAttributeAnimationAdded (Action<AttributeAnimationAddedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AttributeAnimationAddedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3619895992) /* AttributeAnimationAdded (E_ATTRIBUTEANIMATIONADDED) */);
                  return s;
             }

             static UrhoEventAdapter<AttributeAnimationAddedEventArgs> eventAdapterForAttributeAnimationAdded;
             public event Action<AttributeAnimationAddedEventArgs> AttributeAnimationAdded
             {
                 add
                 {
                      if (eventAdapterForAttributeAnimationAdded == null)
                          eventAdapterForAttributeAnimationAdded = new UrhoEventAdapter<AttributeAnimationAddedEventArgs>(typeof(ObjectAnimation));
                      eventAdapterForAttributeAnimationAdded.AddManagedSubscriber(handle, value, SubscribeToAttributeAnimationAdded);
                 }
                 remove { eventAdapterForAttributeAnimationAdded.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ObjectAnimation */ 

} /* namespace */

namespace Urho {
        public partial struct AttributeAnimationRemovedEventArgs {
            internal IntPtr handle;
            public Object ObjectAnimation => UrhoMap.get_Object (handle, unchecked((int)485250565) /* ObjectAnimation (P_OBJECTANIMATION) */);
            public String AttributeAnimationName => UrhoMap.get_String (handle, unchecked((int)4253834771) /* AttributeAnimationName (P_ATTRIBUTEANIMATIONNAME) */);
        } /* struct AttributeAnimationRemovedEventArgs */

        public partial class ObjectAnimation {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.AttributeAnimationRemoved += ...' instead.")]
             public Subscription SubscribeToAttributeAnimationRemoved (Action<AttributeAnimationRemovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AttributeAnimationRemovedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2638038360) /* AttributeAnimationRemoved (E_ATTRIBUTEANIMATIONREMOVED) */);
                  return s;
             }

             static UrhoEventAdapter<AttributeAnimationRemovedEventArgs> eventAdapterForAttributeAnimationRemoved;
             public event Action<AttributeAnimationRemovedEventArgs> AttributeAnimationRemoved
             {
                 add
                 {
                      if (eventAdapterForAttributeAnimationRemoved == null)
                          eventAdapterForAttributeAnimationRemoved = new UrhoEventAdapter<AttributeAnimationRemovedEventArgs>(typeof(ObjectAnimation));
                      eventAdapterForAttributeAnimationRemoved.AddManagedSubscriber(handle, value, SubscribeToAttributeAnimationRemoved);
                 }
                 remove { eventAdapterForAttributeAnimationRemoved.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ObjectAnimation */ 

} /* namespace */

namespace Urho {
        public partial struct ScenePostUpdateEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public float TimeStep => UrhoMap.get_float (handle, unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct ScenePostUpdateEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ScenePostUpdate += ...' instead.")]
             public Subscription SubscribeToScenePostUpdate (Action<ScenePostUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ScenePostUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3048853) /* ScenePostUpdate (E_SCENEPOSTUPDATE) */);
                  return s;
             }

             static UrhoEventAdapter<ScenePostUpdateEventArgs> eventAdapterForScenePostUpdate;
             public event Action<ScenePostUpdateEventArgs> ScenePostUpdate
             {
                 add
                 {
                      if (eventAdapterForScenePostUpdate == null)
                          eventAdapterForScenePostUpdate = new UrhoEventAdapter<ScenePostUpdateEventArgs>(typeof(Scene));
                      eventAdapterForScenePostUpdate.AddManagedSubscriber(handle, value, SubscribeToScenePostUpdate);
                 }
                 remove { eventAdapterForScenePostUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct AsyncLoadProgressEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public float Progress => UrhoMap.get_float (handle, unchecked((int)2456587373) /* Progress (P_PROGRESS) */);
            public int LoadedNodes => UrhoMap.get_int (handle, unchecked((int)2460871468) /* LoadedNodes (P_LOADEDNODES) */);
            public int TotalNodes => UrhoMap.get_int (handle, unchecked((int)3592672237) /* TotalNodes (P_TOTALNODES) */);
            public int LoadedResources => UrhoMap.get_int (handle, unchecked((int)914347776) /* LoadedResources (P_LOADEDRESOURCES) */);
            public int TotalResources => UrhoMap.get_int (handle, unchecked((int)1346461377) /* TotalResources (P_TOTALRESOURCES) */);
        } /* struct AsyncLoadProgressEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.AsyncLoadProgress += ...' instead.")]
             public Subscription SubscribeToAsyncLoadProgress (Action<AsyncLoadProgressEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AsyncLoadProgressEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1563837135) /* AsyncLoadProgress (E_ASYNCLOADPROGRESS) */);
                  return s;
             }

             static UrhoEventAdapter<AsyncLoadProgressEventArgs> eventAdapterForAsyncLoadProgress;
             public event Action<AsyncLoadProgressEventArgs> AsyncLoadProgress
             {
                 add
                 {
                      if (eventAdapterForAsyncLoadProgress == null)
                          eventAdapterForAsyncLoadProgress = new UrhoEventAdapter<AsyncLoadProgressEventArgs>(typeof(Scene));
                      eventAdapterForAsyncLoadProgress.AddManagedSubscriber(handle, value, SubscribeToAsyncLoadProgress);
                 }
                 remove { eventAdapterForAsyncLoadProgress.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct AsyncLoadFinishedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
        } /* struct AsyncLoadFinishedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.AsyncLoadFinished += ...' instead.")]
             public Subscription SubscribeToAsyncLoadFinished (Action<AsyncLoadFinishedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AsyncLoadFinishedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)413288276) /* AsyncLoadFinished (E_ASYNCLOADFINISHED) */);
                  return s;
             }

             static UrhoEventAdapter<AsyncLoadFinishedEventArgs> eventAdapterForAsyncLoadFinished;
             public event Action<AsyncLoadFinishedEventArgs> AsyncLoadFinished
             {
                 add
                 {
                      if (eventAdapterForAsyncLoadFinished == null)
                          eventAdapterForAsyncLoadFinished = new UrhoEventAdapter<AsyncLoadFinishedEventArgs>(typeof(Scene));
                      eventAdapterForAsyncLoadFinished.AddManagedSubscriber(handle, value, SubscribeToAsyncLoadFinished);
                 }
                 remove { eventAdapterForAsyncLoadFinished.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct NodeAddedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Parent => UrhoMap.get_Node (handle, unchecked((int)1512946026) /* Parent (P_PARENT) */);
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
        } /* struct NodeAddedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NodeAdded += ...' instead.")]
             public Subscription SubscribeToNodeAdded (Action<NodeAddedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeAddedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3974098974) /* NodeAdded (E_NODEADDED) */);
                  return s;
             }

             static UrhoEventAdapter<NodeAddedEventArgs> eventAdapterForNodeAdded;
             public event Action<NodeAddedEventArgs> NodeAdded
             {
                 add
                 {
                      if (eventAdapterForNodeAdded == null)
                          eventAdapterForNodeAdded = new UrhoEventAdapter<NodeAddedEventArgs>(typeof(Scene));
                      eventAdapterForNodeAdded.AddManagedSubscriber(handle, value, SubscribeToNodeAdded);
                 }
                 remove { eventAdapterForNodeAdded.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct NodeRemovedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Parent => UrhoMap.get_Node (handle, unchecked((int)1512946026) /* Parent (P_PARENT) */);
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
        } /* struct NodeRemovedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NodeRemoved += ...' instead.")]
             public Subscription SubscribeToNodeRemoved (Action<NodeRemovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeRemovedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)931768254) /* NodeRemoved (E_NODEREMOVED) */);
                  return s;
             }

             static UrhoEventAdapter<NodeRemovedEventArgs> eventAdapterForNodeRemoved;
             public event Action<NodeRemovedEventArgs> NodeRemoved
             {
                 add
                 {
                      if (eventAdapterForNodeRemoved == null)
                          eventAdapterForNodeRemoved = new UrhoEventAdapter<NodeRemovedEventArgs>(typeof(Scene));
                      eventAdapterForNodeRemoved.AddManagedSubscriber(handle, value, SubscribeToNodeRemoved);
                 }
                 remove { eventAdapterForNodeRemoved.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct ComponentAddedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public Component Component => UrhoMap.get_Component (handle, unchecked((int)3739730333) /* Component (P_COMPONENT) */);
        } /* struct ComponentAddedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ComponentAdded += ...' instead.")]
             public Subscription SubscribeToComponentAdded (Action<ComponentAddedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ComponentAddedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2336085059) /* ComponentAdded (E_COMPONENTADDED) */);
                  return s;
             }

             static UrhoEventAdapter<ComponentAddedEventArgs> eventAdapterForComponentAdded;
             public event Action<ComponentAddedEventArgs> ComponentAdded
             {
                 add
                 {
                      if (eventAdapterForComponentAdded == null)
                          eventAdapterForComponentAdded = new UrhoEventAdapter<ComponentAddedEventArgs>(typeof(Scene));
                      eventAdapterForComponentAdded.AddManagedSubscriber(handle, value, SubscribeToComponentAdded);
                 }
                 remove { eventAdapterForComponentAdded.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct ComponentRemovedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public Component Component => UrhoMap.get_Component (handle, unchecked((int)3739730333) /* Component (P_COMPONENT) */);
        } /* struct ComponentRemovedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ComponentRemoved += ...' instead.")]
             public Subscription SubscribeToComponentRemoved (Action<ComponentRemovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ComponentRemovedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3480078691) /* ComponentRemoved (E_COMPONENTREMOVED) */);
                  return s;
             }

             static UrhoEventAdapter<ComponentRemovedEventArgs> eventAdapterForComponentRemoved;
             public event Action<ComponentRemovedEventArgs> ComponentRemoved
             {
                 add
                 {
                      if (eventAdapterForComponentRemoved == null)
                          eventAdapterForComponentRemoved = new UrhoEventAdapter<ComponentRemovedEventArgs>(typeof(Scene));
                      eventAdapterForComponentRemoved.AddManagedSubscriber(handle, value, SubscribeToComponentRemoved);
                 }
                 remove { eventAdapterForComponentRemoved.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct NodeNameChangedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
        } /* struct NodeNameChangedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NodeNameChanged += ...' instead.")]
             public Subscription SubscribeToNodeNameChanged (Action<NodeNameChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeNameChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2921157799) /* NodeNameChanged (E_NODENAMECHANGED) */);
                  return s;
             }

             static UrhoEventAdapter<NodeNameChangedEventArgs> eventAdapterForNodeNameChanged;
             public event Action<NodeNameChangedEventArgs> NodeNameChanged
             {
                 add
                 {
                      if (eventAdapterForNodeNameChanged == null)
                          eventAdapterForNodeNameChanged = new UrhoEventAdapter<NodeNameChangedEventArgs>(typeof(Scene));
                      eventAdapterForNodeNameChanged.AddManagedSubscriber(handle, value, SubscribeToNodeNameChanged);
                 }
                 remove { eventAdapterForNodeNameChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct NodeEnabledChangedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
        } /* struct NodeEnabledChangedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NodeEnabledChanged += ...' instead.")]
             public Subscription SubscribeToNodeEnabledChanged (Action<NodeEnabledChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeEnabledChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)476028341) /* NodeEnabledChanged (E_NODEENABLEDCHANGED) */);
                  return s;
             }

             static UrhoEventAdapter<NodeEnabledChangedEventArgs> eventAdapterForNodeEnabledChanged;
             public event Action<NodeEnabledChangedEventArgs> NodeEnabledChanged
             {
                 add
                 {
                      if (eventAdapterForNodeEnabledChanged == null)
                          eventAdapterForNodeEnabledChanged = new UrhoEventAdapter<NodeEnabledChangedEventArgs>(typeof(Scene));
                      eventAdapterForNodeEnabledChanged.AddManagedSubscriber(handle, value, SubscribeToNodeEnabledChanged);
                 }
                 remove { eventAdapterForNodeEnabledChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct NodeTagAddedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public String Tag => UrhoMap.get_String (handle, unchecked((int)964697786) /* Tag (P_TAG) */);
        } /* struct NodeTagAddedEventArgs */

} /* namespace */

namespace Urho {
        public partial struct NodeTagRemovedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public String Tag => UrhoMap.get_String (handle, unchecked((int)964697786) /* Tag (P_TAG) */);
        } /* struct NodeTagRemovedEventArgs */

} /* namespace */

namespace Urho {
        public partial struct ComponentEnabledChangedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public Component Component => UrhoMap.get_Component (handle, unchecked((int)3739730333) /* Component (P_COMPONENT) */);
        } /* struct ComponentEnabledChangedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ComponentEnabledChanged += ...' instead.")]
             public Subscription SubscribeToComponentEnabledChanged (Action<ComponentEnabledChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ComponentEnabledChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1976356048) /* ComponentEnabledChanged (E_COMPONENTENABLEDCHANGED) */);
                  return s;
             }

             static UrhoEventAdapter<ComponentEnabledChangedEventArgs> eventAdapterForComponentEnabledChanged;
             public event Action<ComponentEnabledChangedEventArgs> ComponentEnabledChanged
             {
                 add
                 {
                      if (eventAdapterForComponentEnabledChanged == null)
                          eventAdapterForComponentEnabledChanged = new UrhoEventAdapter<ComponentEnabledChangedEventArgs>(typeof(Scene));
                      eventAdapterForComponentEnabledChanged.AddManagedSubscriber(handle, value, SubscribeToComponentEnabledChanged);
                 }
                 remove { eventAdapterForComponentEnabledChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct TemporaryChangedEventArgs {
            internal IntPtr handle;
            public Serializable Serializable => UrhoMap.get_Serializable (handle, unchecked((int)1481290239) /* Serializable (P_SERIALIZABLE) */);
        } /* struct TemporaryChangedEventArgs */

        public partial class Serializable {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TemporaryChanged += ...' instead.")]
             public Subscription SubscribeToTemporaryChanged (Action<TemporaryChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TemporaryChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2975291043) /* TemporaryChanged (E_TEMPORARYCHANGED) */);
                  return s;
             }

             static UrhoEventAdapter<TemporaryChangedEventArgs> eventAdapterForTemporaryChanged;
             public event Action<TemporaryChangedEventArgs> TemporaryChanged
             {
                 add
                 {
                      if (eventAdapterForTemporaryChanged == null)
                          eventAdapterForTemporaryChanged = new UrhoEventAdapter<TemporaryChangedEventArgs>(typeof(Serializable));
                      eventAdapterForTemporaryChanged.AddManagedSubscriber(handle, value, SubscribeToTemporaryChanged);
                 }
                 remove { eventAdapterForTemporaryChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Serializable */ 

} /* namespace */

namespace Urho {
        public partial struct NodeClonedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Node => UrhoMap.get_Node (handle, unchecked((int)888833026) /* Node (P_NODE) */);
            public Node CloneNode => UrhoMap.get_Node (handle, unchecked((int)3545672031) /* CloneNode (P_CLONENODE) */);
        } /* struct NodeClonedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NodeCloned += ...' instead.")]
             public Subscription SubscribeToNodeCloned (Action<NodeClonedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeClonedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2335200841) /* NodeCloned (E_NODECLONED) */);
                  return s;
             }

             static UrhoEventAdapter<NodeClonedEventArgs> eventAdapterForNodeCloned;
             public event Action<NodeClonedEventArgs> NodeCloned
             {
                 add
                 {
                      if (eventAdapterForNodeCloned == null)
                          eventAdapterForNodeCloned = new UrhoEventAdapter<NodeClonedEventArgs>(typeof(Scene));
                      eventAdapterForNodeCloned.AddManagedSubscriber(handle, value, SubscribeToNodeCloned);
                 }
                 remove { eventAdapterForNodeCloned.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct ComponentClonedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Component Component => UrhoMap.get_Component (handle, unchecked((int)3739730333) /* Component (P_COMPONENT) */);
            public Component CloneComponent => UrhoMap.get_Component (handle, unchecked((int)733556864) /* CloneComponent (P_CLONECOMPONENT) */);
        } /* struct ComponentClonedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ComponentCloned += ...' instead.")]
             public Subscription SubscribeToComponentCloned (Action<ComponentClonedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ComponentClonedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1752202084) /* ComponentCloned (E_COMPONENTCLONED) */);
                  return s;
             }

             static UrhoEventAdapter<ComponentClonedEventArgs> eventAdapterForComponentCloned;
             public event Action<ComponentClonedEventArgs> ComponentCloned
             {
                 add
                 {
                      if (eventAdapterForComponentCloned == null)
                          eventAdapterForComponentCloned = new UrhoEventAdapter<ComponentClonedEventArgs>(typeof(Scene));
                      eventAdapterForComponentCloned.AddManagedSubscriber(handle, value, SubscribeToComponentCloned);
                 }
                 remove { eventAdapterForComponentCloned.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct InterceptNetworkUpdateEventArgs {
            internal IntPtr handle;
            public Serializable Serializable => UrhoMap.get_Serializable (handle, unchecked((int)1481290239) /* Serializable (P_SERIALIZABLE) */);
            public uint TimeStamp => UrhoMap.get_uint (handle, unchecked((int)1110190518) /* TimeStamp (P_TIMESTAMP) */);
            public uint Index => UrhoMap.get_uint (handle, unchecked((int)193188146) /* Index (P_INDEX) */);
            public String Name => UrhoMap.get_String (handle, unchecked((int)773762347) /* Name (P_NAME) */);
            public Variant Value => UrhoMap.get_Variant (handle, unchecked((int)632064625) /* Value (P_VALUE) */);
        } /* struct InterceptNetworkUpdateEventArgs */

        public partial class Serializable {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.InterceptNetworkUpdate += ...' instead.")]
             public Subscription SubscribeToInterceptNetworkUpdate (Action<InterceptNetworkUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new InterceptNetworkUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)4264627733) /* InterceptNetworkUpdate (E_INTERCEPTNETWORKUPDATE) */);
                  return s;
             }

             static UrhoEventAdapter<InterceptNetworkUpdateEventArgs> eventAdapterForInterceptNetworkUpdate;
             public event Action<InterceptNetworkUpdateEventArgs> InterceptNetworkUpdate
             {
                 add
                 {
                      if (eventAdapterForInterceptNetworkUpdate == null)
                          eventAdapterForInterceptNetworkUpdate = new UrhoEventAdapter<InterceptNetworkUpdateEventArgs>(typeof(Serializable));
                      eventAdapterForInterceptNetworkUpdate.AddManagedSubscriber(handle, value, SubscribeToInterceptNetworkUpdate);
                 }
                 remove { eventAdapterForInterceptNetworkUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Serializable */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct UIMouseClickEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
            public int Button => UrhoMap.get_int (handle, unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => UrhoMap.get_int (handle, unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct UIMouseClickEventArgs */

        public partial class UI {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.UIMouseClick += ...' instead.")]
             public Subscription SubscribeToUIMouseClick (Action<UIMouseClickEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UIMouseClickEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2980213559) /* UIMouseClick (E_UIMOUSECLICK) */);
                  return s;
             }

             static UrhoEventAdapter<UIMouseClickEventArgs> eventAdapterForUIMouseClick;
             public event Action<UIMouseClickEventArgs> UIMouseClick
             {
                 add
                 {
                      if (eventAdapterForUIMouseClick == null)
                          eventAdapterForUIMouseClick = new UrhoEventAdapter<UIMouseClickEventArgs>(typeof(UI));
                      eventAdapterForUIMouseClick.AddManagedSubscriber(handle, value, SubscribeToUIMouseClick);
                 }
                 remove { eventAdapterForUIMouseClick.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UI */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct UIMouseClickEndEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public UIElement BeginElement => UrhoMap.get_UIElement (handle, unchecked((int)660892787) /* BeginElement (P_BEGINELEMENT) */);
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
            public int Button => UrhoMap.get_int (handle, unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => UrhoMap.get_int (handle, unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct UIMouseClickEndEventArgs */

        public partial class UI {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.UIMouseClickEnd += ...' instead.")]
             public Subscription SubscribeToUIMouseClickEnd (Action<UIMouseClickEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UIMouseClickEndEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3905216356) /* UIMouseClickEnd (E_UIMOUSECLICKEND) */);
                  return s;
             }

             static UrhoEventAdapter<UIMouseClickEndEventArgs> eventAdapterForUIMouseClickEnd;
             public event Action<UIMouseClickEndEventArgs> UIMouseClickEnd
             {
                 add
                 {
                      if (eventAdapterForUIMouseClickEnd == null)
                          eventAdapterForUIMouseClickEnd = new UrhoEventAdapter<UIMouseClickEndEventArgs>(typeof(UI));
                      eventAdapterForUIMouseClickEnd.AddManagedSubscriber(handle, value, SubscribeToUIMouseClickEnd);
                 }
                 remove { eventAdapterForUIMouseClickEnd.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UI */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct UIMouseDoubleClickEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
            public int Button => UrhoMap.get_int (handle, unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => UrhoMap.get_int (handle, unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct UIMouseDoubleClickEventArgs */

} /* namespace */

namespace Urho {
        public partial struct ClickEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
            public int Button => UrhoMap.get_int (handle, unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => UrhoMap.get_int (handle, unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct ClickEventArgs */

} /* namespace */

namespace Urho {
        public partial struct ClickEndEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public UIElement BeginElement => UrhoMap.get_UIElement (handle, unchecked((int)660892787) /* BeginElement (P_BEGINELEMENT) */);
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
            public int Button => UrhoMap.get_int (handle, unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => UrhoMap.get_int (handle, unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct ClickEndEventArgs */

} /* namespace */

namespace Urho {
        public partial struct DoubleClickEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
            public int Button => UrhoMap.get_int (handle, unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => UrhoMap.get_int (handle, unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct DoubleClickEventArgs */

} /* namespace */

namespace Urho.Gui {
        public partial struct DragDropTestEventArgs {
            internal IntPtr handle;
            public UIElement Source => UrhoMap.get_UIElement (handle, unchecked((int)3851438139) /* Source (P_SOURCE) */);
            public UIElement Target => UrhoMap.get_UIElement (handle, unchecked((int)3016907569) /* Target (P_TARGET) */);
            public bool Accept => UrhoMap.get_bool (handle, unchecked((int)3683158536) /* Accept (P_ACCEPT) */);
        } /* struct DragDropTestEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.DragDropTest += ...' instead.")]
             public Subscription SubscribeToDragDropTest (Action<DragDropTestEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragDropTestEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2650870357) /* DragDropTest (E_DRAGDROPTEST) */);
                  return s;
             }

             static UrhoEventAdapter<DragDropTestEventArgs> eventAdapterForDragDropTest;
             public event Action<DragDropTestEventArgs> DragDropTest
             {
                 add
                 {
                      if (eventAdapterForDragDropTest == null)
                          eventAdapterForDragDropTest = new UrhoEventAdapter<DragDropTestEventArgs>(typeof(UIElement));
                      eventAdapterForDragDropTest.AddManagedSubscriber(handle, value, SubscribeToDragDropTest);
                 }
                 remove { eventAdapterForDragDropTest.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct DragDropFinishEventArgs {
            internal IntPtr handle;
            public UIElement Source => UrhoMap.get_UIElement (handle, unchecked((int)3851438139) /* Source (P_SOURCE) */);
            public UIElement Target => UrhoMap.get_UIElement (handle, unchecked((int)3016907569) /* Target (P_TARGET) */);
            public bool Accept => UrhoMap.get_bool (handle, unchecked((int)3683158536) /* Accept (P_ACCEPT) */);
        } /* struct DragDropFinishEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.DragDropFinish += ...' instead.")]
             public Subscription SubscribeToDragDropFinish (Action<DragDropFinishEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragDropFinishEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2686304470) /* DragDropFinish (E_DRAGDROPFINISH) */);
                  return s;
             }

             static UrhoEventAdapter<DragDropFinishEventArgs> eventAdapterForDragDropFinish;
             public event Action<DragDropFinishEventArgs> DragDropFinish
             {
                 add
                 {
                      if (eventAdapterForDragDropFinish == null)
                          eventAdapterForDragDropFinish = new UrhoEventAdapter<DragDropFinishEventArgs>(typeof(UIElement));
                      eventAdapterForDragDropFinish.AddManagedSubscriber(handle, value, SubscribeToDragDropFinish);
                 }
                 remove { eventAdapterForDragDropFinish.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct FocusChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public UIElement ClickedElement => UrhoMap.get_UIElement (handle, unchecked((int)1630589429) /* ClickedElement (P_CLICKEDELEMENT) */);
        } /* struct FocusChangedEventArgs */

        public partial class UI {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.FocusChanged += ...' instead.")]
             public Subscription SubscribeToFocusChanged (Action<FocusChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FocusChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3351127484) /* FocusChanged (E_FOCUSCHANGED) */);
                  return s;
             }

             static UrhoEventAdapter<FocusChangedEventArgs> eventAdapterForFocusChanged;
             public event Action<FocusChangedEventArgs> FocusChanged
             {
                 add
                 {
                      if (eventAdapterForFocusChanged == null)
                          eventAdapterForFocusChanged = new UrhoEventAdapter<FocusChangedEventArgs>(typeof(UI));
                      eventAdapterForFocusChanged.AddManagedSubscriber(handle, value, SubscribeToFocusChanged);
                 }
                 remove { eventAdapterForFocusChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UI */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct NameChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct NameChangedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NameChanged += ...' instead.")]
             public Subscription SubscribeToNameChanged (Action<NameChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NameChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1102426921) /* NameChanged (E_NAMECHANGED) */);
                  return s;
             }

             static UrhoEventAdapter<NameChangedEventArgs> eventAdapterForNameChanged;
             public event Action<NameChangedEventArgs> NameChanged
             {
                 add
                 {
                      if (eventAdapterForNameChanged == null)
                          eventAdapterForNameChanged = new UrhoEventAdapter<NameChangedEventArgs>(typeof(UIElement));
                      eventAdapterForNameChanged.AddManagedSubscriber(handle, value, SubscribeToNameChanged);
                 }
                 remove { eventAdapterForNameChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ResizedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int Width => UrhoMap.get_int (handle, unchecked((int)3655201574) /* Width (P_WIDTH) */);
            public int Height => UrhoMap.get_int (handle, unchecked((int)380957255) /* Height (P_HEIGHT) */);
            public int DX => UrhoMap.get_int (handle, unchecked((int)6560020) /* DX (P_DX) */);
            public int DY => UrhoMap.get_int (handle, unchecked((int)6560021) /* DY (P_DY) */);
        } /* struct ResizedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.Resized += ...' instead.")]
             public Subscription SubscribeToResized (Action<ResizedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ResizedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)4086520784) /* Resized (E_RESIZED) */);
                  return s;
             }

             static UrhoEventAdapter<ResizedEventArgs> eventAdapterForResized;
             public event Action<ResizedEventArgs> Resized
             {
                 add
                 {
                      if (eventAdapterForResized == null)
                          eventAdapterForResized = new UrhoEventAdapter<ResizedEventArgs>(typeof(UIElement));
                      eventAdapterForResized.AddManagedSubscriber(handle, value, SubscribeToResized);
                 }
                 remove { eventAdapterForResized.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct PositionedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
        } /* struct PositionedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.Positioned += ...' instead.")]
             public Subscription SubscribeToPositioned (Action<PositionedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PositionedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2176866088) /* Positioned (E_POSITIONED) */);
                  return s;
             }

             static UrhoEventAdapter<PositionedEventArgs> eventAdapterForPositioned;
             public event Action<PositionedEventArgs> Positioned
             {
                 add
                 {
                      if (eventAdapterForPositioned == null)
                          eventAdapterForPositioned = new UrhoEventAdapter<PositionedEventArgs>(typeof(UIElement));
                      eventAdapterForPositioned.AddManagedSubscriber(handle, value, SubscribeToPositioned);
                 }
                 remove { eventAdapterForPositioned.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct VisibleChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public bool Visible => UrhoMap.get_bool (handle, unchecked((int)2569414770) /* Visible (P_VISIBLE) */);
        } /* struct VisibleChangedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.VisibleChanged += ...' instead.")]
             public Subscription SubscribeToVisibleChanged (Action<VisibleChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new VisibleChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2906506274) /* VisibleChanged (E_VISIBLECHANGED) */);
                  return s;
             }

             static UrhoEventAdapter<VisibleChangedEventArgs> eventAdapterForVisibleChanged;
             public event Action<VisibleChangedEventArgs> VisibleChanged
             {
                 add
                 {
                      if (eventAdapterForVisibleChanged == null)
                          eventAdapterForVisibleChanged = new UrhoEventAdapter<VisibleChangedEventArgs>(typeof(UIElement));
                      eventAdapterForVisibleChanged.AddManagedSubscriber(handle, value, SubscribeToVisibleChanged);
                 }
                 remove { eventAdapterForVisibleChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct FocusedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public bool ByKey => UrhoMap.get_bool (handle, unchecked((int)860527848) /* ByKey (P_BYKEY) */);
        } /* struct FocusedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.Focused += ...' instead.")]
             public Subscription SubscribeToFocused (Action<FocusedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FocusedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3009767063) /* Focused (E_FOCUSED) */);
                  return s;
             }

             static UrhoEventAdapter<FocusedEventArgs> eventAdapterForFocused;
             public event Action<FocusedEventArgs> Focused
             {
                 add
                 {
                      if (eventAdapterForFocused == null)
                          eventAdapterForFocused = new UrhoEventAdapter<FocusedEventArgs>(typeof(UIElement));
                      eventAdapterForFocused.AddManagedSubscriber(handle, value, SubscribeToFocused);
                 }
                 remove { eventAdapterForFocused.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct DefocusedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct DefocusedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.Defocused += ...' instead.")]
             public Subscription SubscribeToDefocused (Action<DefocusedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DefocusedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2137964374) /* Defocused (E_DEFOCUSED) */);
                  return s;
             }

             static UrhoEventAdapter<DefocusedEventArgs> eventAdapterForDefocused;
             public event Action<DefocusedEventArgs> Defocused
             {
                 add
                 {
                      if (eventAdapterForDefocused == null)
                          eventAdapterForDefocused = new UrhoEventAdapter<DefocusedEventArgs>(typeof(UIElement));
                      eventAdapterForDefocused.AddManagedSubscriber(handle, value, SubscribeToDefocused);
                 }
                 remove { eventAdapterForDefocused.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct LayoutUpdatedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct LayoutUpdatedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.LayoutUpdated += ...' instead.")]
             public Subscription SubscribeToLayoutUpdated (Action<LayoutUpdatedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new LayoutUpdatedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)4202066961) /* LayoutUpdated (E_LAYOUTUPDATED) */);
                  return s;
             }

             static UrhoEventAdapter<LayoutUpdatedEventArgs> eventAdapterForLayoutUpdated;
             public event Action<LayoutUpdatedEventArgs> LayoutUpdated
             {
                 add
                 {
                      if (eventAdapterForLayoutUpdated == null)
                          eventAdapterForLayoutUpdated = new UrhoEventAdapter<LayoutUpdatedEventArgs>(typeof(UIElement));
                      eventAdapterForLayoutUpdated.AddManagedSubscriber(handle, value, SubscribeToLayoutUpdated);
                 }
                 remove { eventAdapterForLayoutUpdated.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct PressedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct PressedEventArgs */

        public partial class Button {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.Pressed += ...' instead.")]
             public Subscription SubscribeToPressed (Action<PressedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PressedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2120415202) /* Pressed (E_PRESSED) */);
                  return s;
             }

             static UrhoEventAdapter<PressedEventArgs> eventAdapterForPressed;
             public event Action<PressedEventArgs> Pressed
             {
                 add
                 {
                      if (eventAdapterForPressed == null)
                          eventAdapterForPressed = new UrhoEventAdapter<PressedEventArgs>(typeof(Button));
                      eventAdapterForPressed.AddManagedSubscriber(handle, value, SubscribeToPressed);
                 }
                 remove { eventAdapterForPressed.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Button */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ReleasedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct ReleasedEventArgs */

        public partial class Button {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.Released += ...' instead.")]
             public Subscription SubscribeToReleased (Action<ReleasedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ReleasedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)501672573) /* Released (E_RELEASED) */);
                  return s;
             }

             static UrhoEventAdapter<ReleasedEventArgs> eventAdapterForReleased;
             public event Action<ReleasedEventArgs> Released
             {
                 add
                 {
                      if (eventAdapterForReleased == null)
                          eventAdapterForReleased = new UrhoEventAdapter<ReleasedEventArgs>(typeof(Button));
                      eventAdapterForReleased.AddManagedSubscriber(handle, value, SubscribeToReleased);
                 }
                 remove { eventAdapterForReleased.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Button */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ToggledEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public bool State => UrhoMap.get_bool (handle, unchecked((int)3363651793) /* State (P_STATE) */);
        } /* struct ToggledEventArgs */

        public partial class CheckBox {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.Toggled += ...' instead.")]
             public Subscription SubscribeToToggled (Action<ToggledEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ToggledEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3936229040) /* Toggled (E_TOGGLED) */);
                  return s;
             }

             static UrhoEventAdapter<ToggledEventArgs> eventAdapterForToggled;
             public event Action<ToggledEventArgs> Toggled
             {
                 add
                 {
                      if (eventAdapterForToggled == null)
                          eventAdapterForToggled = new UrhoEventAdapter<ToggledEventArgs>(typeof(CheckBox));
                      eventAdapterForToggled.AddManagedSubscriber(handle, value, SubscribeToToggled);
                 }
                 remove { eventAdapterForToggled.RemoveManagedSubscriber(handle, value); }
             }
        } /* class CheckBox */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct SliderChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public float Value => UrhoMap.get_float (handle, unchecked((int)632064625) /* Value (P_VALUE) */);
        } /* struct SliderChangedEventArgs */

        public partial class Slider {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.SliderChanged += ...' instead.")]
             public Subscription SubscribeToSliderChanged (Action<SliderChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SliderChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1822227731) /* SliderChanged (E_SLIDERCHANGED) */);
                  return s;
             }

             static UrhoEventAdapter<SliderChangedEventArgs> eventAdapterForSliderChanged;
             public event Action<SliderChangedEventArgs> SliderChanged
             {
                 add
                 {
                      if (eventAdapterForSliderChanged == null)
                          eventAdapterForSliderChanged = new UrhoEventAdapter<SliderChangedEventArgs>(typeof(Slider));
                      eventAdapterForSliderChanged.AddManagedSubscriber(handle, value, SubscribeToSliderChanged);
                 }
                 remove { eventAdapterForSliderChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Slider */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct SliderPagedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int Offset => UrhoMap.get_int (handle, unchecked((int)1881751827) /* Offset (P_OFFSET) */);
            public bool Pressed => UrhoMap.get_bool (handle, unchecked((int)2120415202) /* Pressed (P_PRESSED) */);
        } /* struct SliderPagedEventArgs */

        public partial class Slider {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.SliderPaged += ...' instead.")]
             public Subscription SubscribeToSliderPaged (Action<SliderPagedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SliderPagedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)436602484) /* SliderPaged (E_SLIDERPAGED) */);
                  return s;
             }

             static UrhoEventAdapter<SliderPagedEventArgs> eventAdapterForSliderPaged;
             public event Action<SliderPagedEventArgs> SliderPaged
             {
                 add
                 {
                      if (eventAdapterForSliderPaged == null)
                          eventAdapterForSliderPaged = new UrhoEventAdapter<SliderPagedEventArgs>(typeof(Slider));
                      eventAdapterForSliderPaged.AddManagedSubscriber(handle, value, SubscribeToSliderPaged);
                 }
                 remove { eventAdapterForSliderPaged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Slider */ 

} /* namespace */

namespace Urho {
        public partial struct ProgressBarChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public float Value => UrhoMap.get_float (handle, unchecked((int)632064625) /* Value (P_VALUE) */);
        } /* struct ProgressBarChangedEventArgs */

} /* namespace */

namespace Urho.Gui {
        public partial struct ScrollBarChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public float Value => UrhoMap.get_float (handle, unchecked((int)632064625) /* Value (P_VALUE) */);
        } /* struct ScrollBarChangedEventArgs */

        public partial class ScrollBar {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ScrollBarChanged += ...' instead.")]
             public Subscription SubscribeToScrollBarChanged (Action<ScrollBarChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ScrollBarChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2589580238) /* ScrollBarChanged (E_SCROLLBARCHANGED) */);
                  return s;
             }

             static UrhoEventAdapter<ScrollBarChangedEventArgs> eventAdapterForScrollBarChanged;
             public event Action<ScrollBarChangedEventArgs> ScrollBarChanged
             {
                 add
                 {
                      if (eventAdapterForScrollBarChanged == null)
                          eventAdapterForScrollBarChanged = new UrhoEventAdapter<ScrollBarChangedEventArgs>(typeof(ScrollBar));
                      eventAdapterForScrollBarChanged.AddManagedSubscriber(handle, value, SubscribeToScrollBarChanged);
                 }
                 remove { eventAdapterForScrollBarChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ScrollBar */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ViewChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
        } /* struct ViewChangedEventArgs */

        public partial class ScrollView {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ViewChanged += ...' instead.")]
             public Subscription SubscribeToViewChanged (Action<ViewChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ViewChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3751985295) /* ViewChanged (E_VIEWCHANGED) */);
                  return s;
             }

             static UrhoEventAdapter<ViewChangedEventArgs> eventAdapterForViewChanged;
             public event Action<ViewChangedEventArgs> ViewChanged
             {
                 add
                 {
                      if (eventAdapterForViewChanged == null)
                          eventAdapterForViewChanged = new UrhoEventAdapter<ViewChangedEventArgs>(typeof(ScrollView));
                      eventAdapterForViewChanged.AddManagedSubscriber(handle, value, SubscribeToViewChanged);
                 }
                 remove { eventAdapterForViewChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ScrollView */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ModalChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public bool Modal => UrhoMap.get_bool (handle, unchecked((int)1236802797) /* Modal (P_MODAL) */);
        } /* struct ModalChangedEventArgs */

        public partial class Window {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ModalChanged += ...' instead.")]
             public Subscription SubscribeToModalChanged (Action<ModalChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ModalChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3780589287) /* ModalChanged (E_MODALCHANGED) */);
                  return s;
             }

             static UrhoEventAdapter<ModalChangedEventArgs> eventAdapterForModalChanged;
             public event Action<ModalChangedEventArgs> ModalChanged
             {
                 add
                 {
                      if (eventAdapterForModalChanged == null)
                          eventAdapterForModalChanged = new UrhoEventAdapter<ModalChangedEventArgs>(typeof(Window));
                      eventAdapterForModalChanged.AddManagedSubscriber(handle, value, SubscribeToModalChanged);
                 }
                 remove { eventAdapterForModalChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Window */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct CharEntryEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public String Text => UrhoMap.get_String (handle, unchecked((int)1196085869) /* Text (P_TEXT) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => UrhoMap.get_int (handle, unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct CharEntryEventArgs */

        public partial class LineEdit {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.CharEntry += ...' instead.")]
             public Subscription SubscribeToCharEntry (Action<CharEntryEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new CharEntryEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3958156636) /* CharEntry (E_TEXTENTRY) */);
                  return s;
             }

             static UrhoEventAdapter<CharEntryEventArgs> eventAdapterForCharEntry;
             public event Action<CharEntryEventArgs> CharEntry
             {
                 add
                 {
                      if (eventAdapterForCharEntry == null)
                          eventAdapterForCharEntry = new UrhoEventAdapter<CharEntryEventArgs>(typeof(LineEdit));
                      eventAdapterForCharEntry.AddManagedSubscriber(handle, value, SubscribeToCharEntry);
                 }
                 remove { eventAdapterForCharEntry.RemoveManagedSubscriber(handle, value); }
             }
        } /* class LineEdit */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct TextChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public String Text => UrhoMap.get_String (handle, unchecked((int)1196085869) /* Text (P_TEXT) */);
        } /* struct TextChangedEventArgs */

        public partial class LineEdit {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TextChanged += ...' instead.")]
             public Subscription SubscribeToTextChanged (Action<TextChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TextChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)248127847) /* TextChanged (E_TEXTCHANGED) */);
                  return s;
             }

             static UrhoEventAdapter<TextChangedEventArgs> eventAdapterForTextChanged;
             public event Action<TextChangedEventArgs> TextChanged
             {
                 add
                 {
                      if (eventAdapterForTextChanged == null)
                          eventAdapterForTextChanged = new UrhoEventAdapter<TextChangedEventArgs>(typeof(LineEdit));
                      eventAdapterForTextChanged.AddManagedSubscriber(handle, value, SubscribeToTextChanged);
                 }
                 remove { eventAdapterForTextChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class LineEdit */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct TextFinishedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public String Text => UrhoMap.get_String (handle, unchecked((int)1196085869) /* Text (P_TEXT) */);
            public float Value => UrhoMap.get_float (handle, unchecked((int)632064625) /* Value (P_VALUE) */);
        } /* struct TextFinishedEventArgs */

        public partial class LineEdit {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TextFinished += ...' instead.")]
             public Subscription SubscribeToTextFinished (Action<TextFinishedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TextFinishedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2163558751) /* TextFinished (E_TEXTFINISHED) */);
                  return s;
             }

             static UrhoEventAdapter<TextFinishedEventArgs> eventAdapterForTextFinished;
             public event Action<TextFinishedEventArgs> TextFinished
             {
                 add
                 {
                      if (eventAdapterForTextFinished == null)
                          eventAdapterForTextFinished = new UrhoEventAdapter<TextFinishedEventArgs>(typeof(LineEdit));
                      eventAdapterForTextFinished.AddManagedSubscriber(handle, value, SubscribeToTextFinished);
                 }
                 remove { eventAdapterForTextFinished.RemoveManagedSubscriber(handle, value); }
             }
        } /* class LineEdit */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct MenuSelectedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct MenuSelectedEventArgs */

        public partial class Menu {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MenuSelected += ...' instead.")]
             public Subscription SubscribeToMenuSelected (Action<MenuSelectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MenuSelectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3684051450) /* MenuSelected (E_MENUSELECTED) */);
                  return s;
             }

             static UrhoEventAdapter<MenuSelectedEventArgs> eventAdapterForMenuSelected;
             public event Action<MenuSelectedEventArgs> MenuSelected
             {
                 add
                 {
                      if (eventAdapterForMenuSelected == null)
                          eventAdapterForMenuSelected = new UrhoEventAdapter<MenuSelectedEventArgs>(typeof(Menu));
                      eventAdapterForMenuSelected.AddManagedSubscriber(handle, value, SubscribeToMenuSelected);
                 }
                 remove { eventAdapterForMenuSelected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Menu */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ItemSelectedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int Selection => UrhoMap.get_int (handle, unchecked((int)3519890092) /* Selection (P_SELECTION) */);
        } /* struct ItemSelectedEventArgs */

        public partial class DropDownList {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ItemSelected += ...' instead.")]
             public Subscription SubscribeToItemSelected (Action<ItemSelectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ItemSelectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3577816398) /* ItemSelected (E_ITEMSELECTED) */);
                  return s;
             }

             static UrhoEventAdapter<ItemSelectedEventArgs> eventAdapterForItemSelected;
             public event Action<ItemSelectedEventArgs> ItemSelected
             {
                 add
                 {
                      if (eventAdapterForItemSelected == null)
                          eventAdapterForItemSelected = new UrhoEventAdapter<ItemSelectedEventArgs>(typeof(DropDownList));
                      eventAdapterForItemSelected.AddManagedSubscriber(handle, value, SubscribeToItemSelected);
                 }
                 remove { eventAdapterForItemSelected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class DropDownList */ 

        public partial class ListView {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ItemSelected += ...' instead.")]
             public Subscription SubscribeToItemSelected (Action<ItemSelectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ItemSelectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3577816398) /* ItemSelected (E_ITEMSELECTED) */);
                  return s;
             }

             static UrhoEventAdapter<ItemSelectedEventArgs> eventAdapterForItemSelected;
             public event Action<ItemSelectedEventArgs> ItemSelected
             {
                 add
                 {
                      if (eventAdapterForItemSelected == null)
                          eventAdapterForItemSelected = new UrhoEventAdapter<ItemSelectedEventArgs>(typeof(ListView));
                      eventAdapterForItemSelected.AddManagedSubscriber(handle, value, SubscribeToItemSelected);
                 }
                 remove { eventAdapterForItemSelected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ListView */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ItemDeselectedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int Selection => UrhoMap.get_int (handle, unchecked((int)3519890092) /* Selection (P_SELECTION) */);
        } /* struct ItemDeselectedEventArgs */

        public partial class ListView {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ItemDeselected += ...' instead.")]
             public Subscription SubscribeToItemDeselected (Action<ItemDeselectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ItemDeselectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)750527183) /* ItemDeselected (E_ITEMDESELECTED) */);
                  return s;
             }

             static UrhoEventAdapter<ItemDeselectedEventArgs> eventAdapterForItemDeselected;
             public event Action<ItemDeselectedEventArgs> ItemDeselected
             {
                 add
                 {
                      if (eventAdapterForItemDeselected == null)
                          eventAdapterForItemDeselected = new UrhoEventAdapter<ItemDeselectedEventArgs>(typeof(ListView));
                      eventAdapterForItemDeselected.AddManagedSubscriber(handle, value, SubscribeToItemDeselected);
                 }
                 remove { eventAdapterForItemDeselected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ListView */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct SelectionChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct SelectionChangedEventArgs */

        public partial class ListView {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.SelectionChanged += ...' instead.")]
             public Subscription SubscribeToSelectionChanged (Action<SelectionChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SelectionChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)652636008) /* SelectionChanged (E_SELECTIONCHANGED) */);
                  return s;
             }

             static UrhoEventAdapter<SelectionChangedEventArgs> eventAdapterForSelectionChanged;
             public event Action<SelectionChangedEventArgs> SelectionChanged
             {
                 add
                 {
                      if (eventAdapterForSelectionChanged == null)
                          eventAdapterForSelectionChanged = new UrhoEventAdapter<SelectionChangedEventArgs>(typeof(ListView));
                      eventAdapterForSelectionChanged.AddManagedSubscriber(handle, value, SubscribeToSelectionChanged);
                 }
                 remove { eventAdapterForSelectionChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ListView */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ItemClickedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public UIElement Item => UrhoMap.get_UIElement (handle, unchecked((int)1322237459) /* Item (P_ITEM) */);
            public int Selection => UrhoMap.get_int (handle, unchecked((int)3519890092) /* Selection (P_SELECTION) */);
            public int Button => UrhoMap.get_int (handle, unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => UrhoMap.get_int (handle, unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct ItemClickedEventArgs */

        public partial class ListView {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ItemClicked += ...' instead.")]
             public Subscription SubscribeToItemClicked (Action<ItemClickedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ItemClickedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1680571156) /* ItemClicked (E_ITEMCLICKED) */);
                  return s;
             }

             static UrhoEventAdapter<ItemClickedEventArgs> eventAdapterForItemClicked;
             public event Action<ItemClickedEventArgs> ItemClicked
             {
                 add
                 {
                      if (eventAdapterForItemClicked == null)
                          eventAdapterForItemClicked = new UrhoEventAdapter<ItemClickedEventArgs>(typeof(ListView));
                      eventAdapterForItemClicked.AddManagedSubscriber(handle, value, SubscribeToItemClicked);
                 }
                 remove { eventAdapterForItemClicked.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ListView */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ItemDoubleClickedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public UIElement Item => UrhoMap.get_UIElement (handle, unchecked((int)1322237459) /* Item (P_ITEM) */);
            public int Selection => UrhoMap.get_int (handle, unchecked((int)3519890092) /* Selection (P_SELECTION) */);
            public int Button => UrhoMap.get_int (handle, unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => UrhoMap.get_int (handle, unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct ItemDoubleClickedEventArgs */

        public partial class ListView {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ItemDoubleClicked += ...' instead.")]
             public Subscription SubscribeToItemDoubleClicked (Action<ItemDoubleClickedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ItemDoubleClickedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1209953699) /* ItemDoubleClicked (E_ITEMDOUBLECLICKED) */);
                  return s;
             }

             static UrhoEventAdapter<ItemDoubleClickedEventArgs> eventAdapterForItemDoubleClicked;
             public event Action<ItemDoubleClickedEventArgs> ItemDoubleClicked
             {
                 add
                 {
                      if (eventAdapterForItemDoubleClicked == null)
                          eventAdapterForItemDoubleClicked = new UrhoEventAdapter<ItemDoubleClickedEventArgs>(typeof(ListView));
                      eventAdapterForItemDoubleClicked.AddManagedSubscriber(handle, value, SubscribeToItemDoubleClicked);
                 }
                 remove { eventAdapterForItemDoubleClicked.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ListView */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct UnhandledKeyEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public Key Key =>(Key) UrhoMap.get_int (handle, unchecked((int)890606655) /* Key (P_KEY) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => UrhoMap.get_int (handle, unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct UnhandledKeyEventArgs */

        public partial class LineEdit {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.UnhandledKey += ...' instead.")]
             public Subscription SubscribeToUnhandledKey (Action<UnhandledKeyEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UnhandledKeyEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1583051260) /* UnhandledKey (E_UNHANDLEDKEY) */);
                  return s;
             }

             static UrhoEventAdapter<UnhandledKeyEventArgs> eventAdapterForUnhandledKey;
             public event Action<UnhandledKeyEventArgs> UnhandledKey
             {
                 add
                 {
                      if (eventAdapterForUnhandledKey == null)
                          eventAdapterForUnhandledKey = new UrhoEventAdapter<UnhandledKeyEventArgs>(typeof(LineEdit));
                      eventAdapterForUnhandledKey.AddManagedSubscriber(handle, value, SubscribeToUnhandledKey);
                 }
                 remove { eventAdapterForUnhandledKey.RemoveManagedSubscriber(handle, value); }
             }
        } /* class LineEdit */ 

        public partial class ListView {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.UnhandledKey += ...' instead.")]
             public Subscription SubscribeToUnhandledKey (Action<UnhandledKeyEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UnhandledKeyEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1583051260) /* UnhandledKey (E_UNHANDLEDKEY) */);
                  return s;
             }

             static UrhoEventAdapter<UnhandledKeyEventArgs> eventAdapterForUnhandledKey;
             public event Action<UnhandledKeyEventArgs> UnhandledKey
             {
                 add
                 {
                      if (eventAdapterForUnhandledKey == null)
                          eventAdapterForUnhandledKey = new UrhoEventAdapter<UnhandledKeyEventArgs>(typeof(ListView));
                      eventAdapterForUnhandledKey.AddManagedSubscriber(handle, value, SubscribeToUnhandledKey);
                 }
                 remove { eventAdapterForUnhandledKey.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ListView */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct FileSelectedEventArgs {
            internal IntPtr handle;
            public String FileName => UrhoMap.get_String (handle, unchecked((int)633459751) /* FileName (P_FILENAME) */);
            public String Filter => UrhoMap.get_String (handle, unchecked((int)2349197016) /* Filter (P_FILTER) */);
            public bool Ok => UrhoMap.get_bool (handle, unchecked((int)7281596) /* Ok (P_OK) */);
        } /* struct FileSelectedEventArgs */

        public partial class FileSelector {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.FileSelected += ...' instead.")]
             public Subscription SubscribeToFileSelected (Action<FileSelectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FileSelectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2247030839) /* FileSelected (E_FILESELECTED) */);
                  return s;
             }

             static UrhoEventAdapter<FileSelectedEventArgs> eventAdapterForFileSelected;
             public event Action<FileSelectedEventArgs> FileSelected
             {
                 add
                 {
                      if (eventAdapterForFileSelected == null)
                          eventAdapterForFileSelected = new UrhoEventAdapter<FileSelectedEventArgs>(typeof(FileSelector));
                      eventAdapterForFileSelected.AddManagedSubscriber(handle, value, SubscribeToFileSelected);
                 }
                 remove { eventAdapterForFileSelected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class FileSelector */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct MessageACKEventArgs {
            internal IntPtr handle;
            public bool Ok => UrhoMap.get_bool (handle, unchecked((int)7281596) /* Ok (P_OK) */);
        } /* struct MessageACKEventArgs */

        public partial class MessageBox {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MessageACK += ...' instead.")]
             public Subscription SubscribeToMessageACK (Action<MessageACKEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MessageACKEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2274823746) /* MessageACK (E_MESSAGEACK) */);
                  return s;
             }

             static UrhoEventAdapter<MessageACKEventArgs> eventAdapterForMessageACK;
             public event Action<MessageACKEventArgs> MessageACK
             {
                 add
                 {
                      if (eventAdapterForMessageACK == null)
                          eventAdapterForMessageACK = new UrhoEventAdapter<MessageACKEventArgs>(typeof(MessageBox));
                      eventAdapterForMessageACK.AddManagedSubscriber(handle, value, SubscribeToMessageACK);
                 }
                 remove { eventAdapterForMessageACK.RemoveManagedSubscriber(handle, value); }
             }
        } /* class MessageBox */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ElementAddedEventArgs {
            internal IntPtr handle;
            public UIElement Root => UrhoMap.get_UIElement (handle, unchecked((int)4011903426) /* Root (P_ROOT) */);
            public UIElement Parent => UrhoMap.get_UIElement (handle, unchecked((int)1512946026) /* Parent (P_PARENT) */);
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct ElementAddedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ElementAdded += ...' instead.")]
             public Subscription SubscribeToElementAdded (Action<ElementAddedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ElementAddedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3505291012) /* ElementAdded (E_ELEMENTADDED) */);
                  return s;
             }

             static UrhoEventAdapter<ElementAddedEventArgs> eventAdapterForElementAdded;
             public event Action<ElementAddedEventArgs> ElementAdded
             {
                 add
                 {
                      if (eventAdapterForElementAdded == null)
                          eventAdapterForElementAdded = new UrhoEventAdapter<ElementAddedEventArgs>(typeof(UIElement));
                      eventAdapterForElementAdded.AddManagedSubscriber(handle, value, SubscribeToElementAdded);
                 }
                 remove { eventAdapterForElementAdded.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ElementRemovedEventArgs {
            internal IntPtr handle;
            public UIElement Root => UrhoMap.get_UIElement (handle, unchecked((int)4011903426) /* Root (P_ROOT) */);
            public UIElement Parent => UrhoMap.get_UIElement (handle, unchecked((int)1512946026) /* Parent (P_PARENT) */);
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct ElementRemovedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ElementRemoved += ...' instead.")]
             public Subscription SubscribeToElementRemoved (Action<ElementRemovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ElementRemovedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1383277476) /* ElementRemoved (E_ELEMENTREMOVED) */);
                  return s;
             }

             static UrhoEventAdapter<ElementRemovedEventArgs> eventAdapterForElementRemoved;
             public event Action<ElementRemovedEventArgs> ElementRemoved
             {
                 add
                 {
                      if (eventAdapterForElementRemoved == null)
                          eventAdapterForElementRemoved = new UrhoEventAdapter<ElementRemovedEventArgs>(typeof(UIElement));
                      eventAdapterForElementRemoved.AddManagedSubscriber(handle, value, SubscribeToElementRemoved);
                 }
                 remove { eventAdapterForElementRemoved.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct HoverBeginEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
            public int ElementX => UrhoMap.get_int (handle, unchecked((int)3977097692) /* ElementX (P_ELEMENTX) */);
            public int ElementY => UrhoMap.get_int (handle, unchecked((int)3977097693) /* ElementY (P_ELEMENTY) */);
        } /* struct HoverBeginEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.HoverBegin += ...' instead.")]
             public Subscription SubscribeToHoverBegin (Action<HoverBeginEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new HoverBeginEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)2870063597) /* HoverBegin (E_HOVERBEGIN) */);
                  return s;
             }

             static UrhoEventAdapter<HoverBeginEventArgs> eventAdapterForHoverBegin;
             public event Action<HoverBeginEventArgs> HoverBegin
             {
                 add
                 {
                      if (eventAdapterForHoverBegin == null)
                          eventAdapterForHoverBegin = new UrhoEventAdapter<HoverBeginEventArgs>(typeof(UIElement));
                      eventAdapterForHoverBegin.AddManagedSubscriber(handle, value, SubscribeToHoverBegin);
                 }
                 remove { eventAdapterForHoverBegin.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct HoverEndEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct HoverEndEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.HoverEnd += ...' instead.")]
             public Subscription SubscribeToHoverEnd (Action<HoverEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new HoverEndEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)179772511) /* HoverEnd (E_HOVEREND) */);
                  return s;
             }

             static UrhoEventAdapter<HoverEndEventArgs> eventAdapterForHoverEnd;
             public event Action<HoverEndEventArgs> HoverEnd
             {
                 add
                 {
                      if (eventAdapterForHoverEnd == null)
                          eventAdapterForHoverEnd = new UrhoEventAdapter<HoverEndEventArgs>(typeof(UIElement));
                      eventAdapterForHoverEnd.AddManagedSubscriber(handle, value, SubscribeToHoverEnd);
                 }
                 remove { eventAdapterForHoverEnd.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct DragBeginEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
            public int ElementX => UrhoMap.get_int (handle, unchecked((int)3977097692) /* ElementX (P_ELEMENTX) */);
            public int ElementY => UrhoMap.get_int (handle, unchecked((int)3977097693) /* ElementY (P_ELEMENTY) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int NumButtons => UrhoMap.get_int (handle, unchecked((int)1318335099) /* NumButtons (P_NUMBUTTONS) */);
        } /* struct DragBeginEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.DragBegin += ...' instead.")]
             public Subscription SubscribeToDragBegin (Action<DragBeginEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragBeginEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3034378133) /* DragBegin (E_DRAGBEGIN) */);
                  return s;
             }

             static UrhoEventAdapter<DragBeginEventArgs> eventAdapterForDragBegin;
             public event Action<DragBeginEventArgs> DragBegin
             {
                 add
                 {
                      if (eventAdapterForDragBegin == null)
                          eventAdapterForDragBegin = new UrhoEventAdapter<DragBeginEventArgs>(typeof(UIElement));
                      eventAdapterForDragBegin.AddManagedSubscriber(handle, value, SubscribeToDragBegin);
                 }
                 remove { eventAdapterForDragBegin.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct DragMoveEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
            public int DX => UrhoMap.get_int (handle, unchecked((int)6560020) /* DX (P_DX) */);
            public int DY => UrhoMap.get_int (handle, unchecked((int)6560021) /* DY (P_DY) */);
            public int ElementX => UrhoMap.get_int (handle, unchecked((int)3977097692) /* ElementX (P_ELEMENTX) */);
            public int ElementY => UrhoMap.get_int (handle, unchecked((int)3977097693) /* ElementY (P_ELEMENTY) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int NumButtons => UrhoMap.get_int (handle, unchecked((int)1318335099) /* NumButtons (P_NUMBUTTONS) */);
        } /* struct DragMoveEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.DragMove += ...' instead.")]
             public Subscription SubscribeToDragMove (Action<DragMoveEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragMoveEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3547885061) /* DragMove (E_DRAGMOVE) */);
                  return s;
             }

             static UrhoEventAdapter<DragMoveEventArgs> eventAdapterForDragMove;
             public event Action<DragMoveEventArgs> DragMove
             {
                 add
                 {
                      if (eventAdapterForDragMove == null)
                          eventAdapterForDragMove = new UrhoEventAdapter<DragMoveEventArgs>(typeof(UIElement));
                      eventAdapterForDragMove.AddManagedSubscriber(handle, value, SubscribeToDragMove);
                 }
                 remove { eventAdapterForDragMove.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct DragEndEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
            public int ElementX => UrhoMap.get_int (handle, unchecked((int)3977097692) /* ElementX (P_ELEMENTX) */);
            public int ElementY => UrhoMap.get_int (handle, unchecked((int)3977097693) /* ElementY (P_ELEMENTY) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int NumButtons => UrhoMap.get_int (handle, unchecked((int)1318335099) /* NumButtons (P_NUMBUTTONS) */);
        } /* struct DragEndEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.DragEnd += ...' instead.")]
             public Subscription SubscribeToDragEnd (Action<DragEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragEndEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)796962311) /* DragEnd (E_DRAGEND) */);
                  return s;
             }

             static UrhoEventAdapter<DragEndEventArgs> eventAdapterForDragEnd;
             public event Action<DragEndEventArgs> DragEnd
             {
                 add
                 {
                      if (eventAdapterForDragEnd == null)
                          eventAdapterForDragEnd = new UrhoEventAdapter<DragEndEventArgs>(typeof(UIElement));
                      eventAdapterForDragEnd.AddManagedSubscriber(handle, value, SubscribeToDragEnd);
                 }
                 remove { eventAdapterForDragEnd.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct DragCancelEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
            public int ElementX => UrhoMap.get_int (handle, unchecked((int)3977097692) /* ElementX (P_ELEMENTX) */);
            public int ElementY => UrhoMap.get_int (handle, unchecked((int)3977097693) /* ElementY (P_ELEMENTY) */);
            public int Buttons => UrhoMap.get_int (handle, unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int NumButtons => UrhoMap.get_int (handle, unchecked((int)1318335099) /* NumButtons (P_NUMBUTTONS) */);
        } /* struct DragCancelEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.DragCancel += ...' instead.")]
             public Subscription SubscribeToDragCancel (Action<DragCancelEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragCancelEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3139514702) /* DragCancel (E_DRAGCANCEL) */);
                  return s;
             }

             static UrhoEventAdapter<DragCancelEventArgs> eventAdapterForDragCancel;
             public event Action<DragCancelEventArgs> DragCancel
             {
                 add
                 {
                      if (eventAdapterForDragCancel == null)
                          eventAdapterForDragCancel = new UrhoEventAdapter<DragCancelEventArgs>(typeof(UIElement));
                      eventAdapterForDragCancel.AddManagedSubscriber(handle, value, SubscribeToDragCancel);
                 }
                 remove { eventAdapterForDragCancel.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct UIDropFileEventArgs {
            internal IntPtr handle;
            public String FileName => UrhoMap.get_String (handle, unchecked((int)633459751) /* FileName (P_FILENAME) */);
            public UIElement Element => UrhoMap.get_UIElement (handle, unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => UrhoMap.get_int (handle, unchecked((int)120) /* X (P_X) */);
            public int Y => UrhoMap.get_int (handle, unchecked((int)121) /* Y (P_Y) */);
            public int ElementX => UrhoMap.get_int (handle, unchecked((int)3977097692) /* ElementX (P_ELEMENTX) */);
            public int ElementY => UrhoMap.get_int (handle, unchecked((int)3977097693) /* ElementY (P_ELEMENTY) */);
        } /* struct UIDropFileEventArgs */

        public partial class UI {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.UIDropFile += ...' instead.")]
             public Subscription SubscribeToUIDropFile (Action<UIDropFileEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UIDropFileEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3334256383) /* UIDropFile (E_UIDROPFILE) */);
                  return s;
             }

             static UrhoEventAdapter<UIDropFileEventArgs> eventAdapterForUIDropFile;
             public event Action<UIDropFileEventArgs> UIDropFile
             {
                 add
                 {
                      if (eventAdapterForUIDropFile == null)
                          eventAdapterForUIDropFile = new UrhoEventAdapter<UIDropFileEventArgs>(typeof(UI));
                      eventAdapterForUIDropFile.AddManagedSubscriber(handle, value, SubscribeToUIDropFile);
                 }
                 remove { eventAdapterForUIDropFile.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UI */ 

} /* namespace */

namespace Urho.Urho2D {
        public partial struct PhysicsBeginContact2DEventArgs {
            internal IntPtr handle;
            public PhysicsWorld2D World => UrhoMap.get_PhysicsWorld2D (handle, unchecked((int)4158893746) /* World (P_WORLD) */);
            public RigidBody2D BodyA => UrhoMap.get_RigidBody2D (handle, unchecked((int)1588071871) /* BodyA (P_BODYA) */);
            public RigidBody2D BodyB => UrhoMap.get_RigidBody2D (handle, unchecked((int)1588071872) /* BodyB (P_BODYB) */);
            public Node NodeA => UrhoMap.get_Node (handle, unchecked((int)2376629471) /* NodeA (P_NODEA) */);
            public Node NodeB => UrhoMap.get_Node (handle, unchecked((int)2376629472) /* NodeB (P_NODEB) */);
            public IntPtr Contact => UrhoMap.get_IntPtr (handle, unchecked((int)3836135392) /* Contact (P_CONTACT) */);
        } /* struct PhysicsBeginContact2DEventArgs */

        public partial class PhysicsWorld2D {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PhysicsBeginContact2D += ...' instead.")]
             public Subscription SubscribeToPhysicsBeginContact2D (Action<PhysicsBeginContact2DEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsBeginContact2DEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3421721456) /* PhysicsBeginContact2D (E_PHYSICSBEGINCONTACT2D) */);
                  return s;
             }

             static UrhoEventAdapter<PhysicsBeginContact2DEventArgs> eventAdapterForPhysicsBeginContact2D;
             public event Action<PhysicsBeginContact2DEventArgs> PhysicsBeginContact2D
             {
                 add
                 {
                      if (eventAdapterForPhysicsBeginContact2D == null)
                          eventAdapterForPhysicsBeginContact2D = new UrhoEventAdapter<PhysicsBeginContact2DEventArgs>(typeof(PhysicsWorld2D));
                      eventAdapterForPhysicsBeginContact2D.AddManagedSubscriber(handle, value, SubscribeToPhysicsBeginContact2D);
                 }
                 remove { eventAdapterForPhysicsBeginContact2D.RemoveManagedSubscriber(handle, value); }
             }
        } /* class PhysicsWorld2D */ 

} /* namespace */

namespace Urho.Urho2D {
        public partial struct PhysicsEndContact2DEventArgs {
            internal IntPtr handle;
            public PhysicsWorld2D World => UrhoMap.get_PhysicsWorld2D (handle, unchecked((int)4158893746) /* World (P_WORLD) */);
            public RigidBody2D BodyA => UrhoMap.get_RigidBody2D (handle, unchecked((int)1588071871) /* BodyA (P_BODYA) */);
            public RigidBody2D BodyB => UrhoMap.get_RigidBody2D (handle, unchecked((int)1588071872) /* BodyB (P_BODYB) */);
            public Node NodeA => UrhoMap.get_Node (handle, unchecked((int)2376629471) /* NodeA (P_NODEA) */);
            public Node NodeB => UrhoMap.get_Node (handle, unchecked((int)2376629472) /* NodeB (P_NODEB) */);
            public IntPtr Contact => UrhoMap.get_IntPtr (handle, unchecked((int)3836135392) /* Contact (P_CONTACT) */);
        } /* struct PhysicsEndContact2DEventArgs */

        public partial class PhysicsWorld2D {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PhysicsEndContact2D += ...' instead.")]
             public Subscription SubscribeToPhysicsEndContact2D (Action<PhysicsEndContact2DEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsEndContact2DEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)3071590142) /* PhysicsEndContact2D (E_PHYSICSENDCONTACT2D) */);
                  return s;
             }

             static UrhoEventAdapter<PhysicsEndContact2DEventArgs> eventAdapterForPhysicsEndContact2D;
             public event Action<PhysicsEndContact2DEventArgs> PhysicsEndContact2D
             {
                 add
                 {
                      if (eventAdapterForPhysicsEndContact2D == null)
                          eventAdapterForPhysicsEndContact2D = new UrhoEventAdapter<PhysicsEndContact2DEventArgs>(typeof(PhysicsWorld2D));
                      eventAdapterForPhysicsEndContact2D.AddManagedSubscriber(handle, value, SubscribeToPhysicsEndContact2D);
                 }
                 remove { eventAdapterForPhysicsEndContact2D.RemoveManagedSubscriber(handle, value); }
             }
        } /* class PhysicsWorld2D */ 

} /* namespace */

namespace Urho {
        public partial struct NodeBeginContact2DEventArgs {
            internal IntPtr handle;
            public RigidBody2D Body => UrhoMap.get_RigidBody2D (handle, unchecked((int)111721250) /* Body (P_BODY) */);
            public Node OtherNode => UrhoMap.get_Node (handle, unchecked((int)2707292594) /* OtherNode (P_OTHERNODE) */);
            public RigidBody2D OtherBody => UrhoMap.get_RigidBody2D (handle, unchecked((int)1930180818) /* OtherBody (P_OTHERBODY) */);
            public IntPtr Contact => UrhoMap.get_IntPtr (handle, unchecked((int)3836135392) /* Contact (P_CONTACT) */);
        } /* struct NodeBeginContact2DEventArgs */

} /* namespace */

namespace Urho {
        public partial struct NodeEndContact2DEventArgs {
            internal IntPtr handle;
            public RigidBody2D Body => UrhoMap.get_RigidBody2D (handle, unchecked((int)111721250) /* Body (P_BODY) */);
            public Node OtherNode => UrhoMap.get_Node (handle, unchecked((int)2707292594) /* OtherNode (P_OTHERNODE) */);
            public RigidBody2D OtherBody => UrhoMap.get_RigidBody2D (handle, unchecked((int)1930180818) /* OtherBody (P_OTHERBODY) */);
            public IntPtr Contact => UrhoMap.get_IntPtr (handle, unchecked((int)3836135392) /* Contact (P_CONTACT) */);
        } /* struct NodeEndContact2DEventArgs */

} /* namespace */

#pragma warning restore CS0618, CS0649