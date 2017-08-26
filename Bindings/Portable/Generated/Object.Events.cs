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
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public SoundSource SoundSource => EventData.get_SoundSource (unchecked((int)368456554) /* SoundSource (P_SOUNDSOURCE) */);
            public Sound Sound => EventData.get_Sound (unchecked((int)3920519599) /* Sound (P_SOUND) */);
        } /* struct SoundFinishedEventArgs */

} /* namespace */

namespace Urho {
        public partial struct FrameStartedEventArgs {
            public EventDataContainer EventData;
            public uint FrameNumber => EventData.get_uint (unchecked((int)1441088918) /* FrameNumber (P_FRAMENUMBER) */);
            public float TimeStep => EventData.get_float (unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct FrameStartedEventArgs */

        public partial class Time {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.FrameStarted += ...' instead.")]
             public Subscription SubscribeToFrameStarted (Action<FrameStartedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FrameStartedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public float TimeStep => EventData.get_float (unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct UpdateEventArgs */

        public partial class Engine {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.Update += ...' instead.")]
             internal Subscription SubscribeToUpdate (Action<UpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UpdateEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public float TimeStep => EventData.get_float (unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct PostUpdateEventArgs */

        public partial class Engine {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PostUpdate += ...' instead.")]
             public Subscription SubscribeToPostUpdate (Action<PostUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PostUpdateEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public float TimeStep => EventData.get_float (unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct RenderUpdateEventArgs */

        public partial class Engine {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.RenderUpdate += ...' instead.")]
             public Subscription SubscribeToRenderUpdate (Action<RenderUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new RenderUpdateEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public float TimeStep => EventData.get_float (unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct PostRenderUpdateEventArgs */

        public partial class Engine {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PostRenderUpdate += ...' instead.")]
             public Subscription SubscribeToPostRenderUpdate (Action<PostRenderUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PostRenderUpdateEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
        } /* struct FrameEndedEventArgs */

        public partial class Time {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.FrameEnded += ...' instead.")]
             public Subscription SubscribeToFrameEnded (Action<FrameEndedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FrameEndedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public WorkItem Item => EventData.get_WorkItem (unchecked((int)1322237459) /* Item (P_ITEM) */);
        } /* struct WorkItemCompletedEventArgs */

        public partial class WorkQueue {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.WorkItemCompleted += ...' instead.")]
             public Subscription SubscribeToWorkItemCompleted (Action<WorkItemCompletedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new WorkItemCompletedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public String Command => EventData.get_String (unchecked((int)1528149579) /* Command (P_COMMAND) */);
            public String Id => EventData.get_String (unchecked((int)6887995) /* Id (P_ID) */);
        } /* struct ConsoleCommandEventArgs */

        public partial class UrhoConsole {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ConsoleCommand += ...' instead.")]
             public Subscription SubscribeToConsoleCommand (Action<ConsoleCommandEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ConsoleCommandEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
        } /* struct BoneHierarchyCreatedEventArgs */

        public partial class Node {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.BoneHierarchyCreated += ...' instead.")]
             public Subscription SubscribeToBoneHierarchyCreated (Action<BoneHierarchyCreatedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new BoneHierarchyCreatedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public Animation Animation => EventData.get_Animation (unchecked((int)1554425540) /* Animation (P_ANIMATION) */);
            public String Name => EventData.get_String (unchecked((int)773762347) /* Name (P_NAME) */);
            public float Time => EventData.get_float (unchecked((int)1228410285) /* Time (P_TIME) */);
            public IntPtr Data => EventData.get_IntPtr (unchecked((int)1558284138) /* Data (P_DATA) */);
        } /* struct AnimationTriggerEventArgs */

        public partial class Node {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.AnimationTrigger += ...' instead.")]
             public Subscription SubscribeToAnimationTrigger (Action<AnimationTriggerEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AnimationTriggerEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public Animation Animation => EventData.get_Animation (unchecked((int)1554425540) /* Animation (P_ANIMATION) */);
            public String Name => EventData.get_String (unchecked((int)773762347) /* Name (P_NAME) */);
            public bool Looped => EventData.get_bool (unchecked((int)842439811) /* Looped (P_LOOPED) */);
        } /* struct AnimationFinishedEventArgs */

} /* namespace */

namespace Urho {
        public partial struct ParticleEffectFinishedEventArgs {
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public ParticleEffect Effect => EventData.get_ParticleEffect (unchecked((int)2340854545) /* Effect (P_EFFECT) */);
        } /* struct ParticleEffectFinishedEventArgs */

} /* namespace */

namespace Urho {
        public partial struct TerrainCreatedEventArgs {
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
        } /* struct TerrainCreatedEventArgs */

        public partial class Terrain {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TerrainCreated += ...' instead.")]
             public Subscription SubscribeToTerrainCreated (Action<TerrainCreatedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TerrainCreatedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public int Width => EventData.get_int (unchecked((int)3655201574) /* Width (P_WIDTH) */);
            public int Height => EventData.get_int (unchecked((int)380957255) /* Height (P_HEIGHT) */);
            public bool Fullscreen => EventData.get_bool (unchecked((int)1835757435) /* Fullscreen (P_FULLSCREEN) */);
            public bool Borderless => EventData.get_bool (unchecked((int)2212104069) /* Borderless (P_BORDERLESS) */);
            public bool Resizable => EventData.get_bool (unchecked((int)3579260107) /* Resizable (P_RESIZABLE) */);
            public bool HighDPI => EventData.get_bool (unchecked((int)2251421851) /* HighDPI (P_HIGHDPI) */);
            public int Monitor => EventData.get_int (unchecked((int)1274038842) /* Monitor (P_MONITOR) */);
            public int RefreshRate => EventData.get_int (unchecked((int)1452299323) /* RefreshRate (P_REFRESHRATE) */);
        } /* struct ScreenModeEventArgs */

} /* namespace */

namespace Urho {
        public partial struct WindowPosEventArgs {
            public EventDataContainer EventData;
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
        } /* struct WindowPosEventArgs */

} /* namespace */

namespace Urho {
        public partial struct RenderSurfaceUpdateEventArgs {
            public EventDataContainer EventData;
        } /* struct RenderSurfaceUpdateEventArgs */

        public partial class Renderer {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.RenderSurfaceUpdate += ...' instead.")]
             public Subscription SubscribeToRenderSurfaceUpdate (Action<RenderSurfaceUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new RenderSurfaceUpdateEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
        } /* struct BeginRenderingEventArgs */

} /* namespace */

namespace Urho {
        public partial struct EndRenderingEventArgs {
            public EventDataContainer EventData;
        } /* struct EndRenderingEventArgs */

} /* namespace */

namespace Urho {
        public partial struct BeginViewUpdateEventArgs {
            public EventDataContainer EventData;
            public View View => EventData.get_View (unchecked((int)2789059909) /* View (P_VIEW) */);
            public Texture Texture => EventData.get_Texture (unchecked((int)4041785787) /* Texture (P_TEXTURE) */);
            public RenderSurface Surface => EventData.get_RenderSurface (unchecked((int)1353844973) /* Surface (P_SURFACE) */);
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Camera Camera => EventData.get_Camera (unchecked((int)1364112997) /* Camera (P_CAMERA) */);
        } /* struct BeginViewUpdateEventArgs */

        public partial class View {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.BeginViewUpdate += ...' instead.")]
             public Subscription SubscribeToBeginViewUpdate (Action<BeginViewUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new BeginViewUpdateEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public View View => EventData.get_View (unchecked((int)2789059909) /* View (P_VIEW) */);
            public Texture Texture => EventData.get_Texture (unchecked((int)4041785787) /* Texture (P_TEXTURE) */);
            public RenderSurface Surface => EventData.get_RenderSurface (unchecked((int)1353844973) /* Surface (P_SURFACE) */);
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Camera Camera => EventData.get_Camera (unchecked((int)1364112997) /* Camera (P_CAMERA) */);
        } /* struct EndViewUpdateEventArgs */

        public partial class View {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.EndViewUpdate += ...' instead.")]
             public Subscription SubscribeToEndViewUpdate (Action<EndViewUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new EndViewUpdateEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public View View => EventData.get_View (unchecked((int)2789059909) /* View (P_VIEW) */);
            public Texture Texture => EventData.get_Texture (unchecked((int)4041785787) /* Texture (P_TEXTURE) */);
            public RenderSurface Surface => EventData.get_RenderSurface (unchecked((int)1353844973) /* Surface (P_SURFACE) */);
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Camera Camera => EventData.get_Camera (unchecked((int)1364112997) /* Camera (P_CAMERA) */);
        } /* struct BeginViewRenderEventArgs */

        public partial class Renderer {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.BeginViewRender += ...' instead.")]
             public Subscription SubscribeToBeginViewRender (Action<BeginViewRenderEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new BeginViewRenderEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public View View => EventData.get_View (unchecked((int)2789059909) /* View (P_VIEW) */);
            public Texture Texture => EventData.get_Texture (unchecked((int)4041785787) /* Texture (P_TEXTURE) */);
            public RenderSurface Surface => EventData.get_RenderSurface (unchecked((int)1353844973) /* Surface (P_SURFACE) */);
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Camera Camera => EventData.get_Camera (unchecked((int)1364112997) /* Camera (P_CAMERA) */);
        } /* struct ViewBuffersReadyEventArgs */

} /* namespace */

namespace Urho {
        public partial struct ViewGlobalShaderParametersEventArgs {
            public EventDataContainer EventData;
            public View View => EventData.get_View (unchecked((int)2789059909) /* View (P_VIEW) */);
            public Texture Texture => EventData.get_Texture (unchecked((int)4041785787) /* Texture (P_TEXTURE) */);
            public RenderSurface Surface => EventData.get_RenderSurface (unchecked((int)1353844973) /* Surface (P_SURFACE) */);
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Camera Camera => EventData.get_Camera (unchecked((int)1364112997) /* Camera (P_CAMERA) */);
        } /* struct ViewGlobalShaderParametersEventArgs */

} /* namespace */

namespace Urho {
        public partial struct EndViewRenderEventArgs {
            public EventDataContainer EventData;
            public View View => EventData.get_View (unchecked((int)2789059909) /* View (P_VIEW) */);
            public Texture Texture => EventData.get_Texture (unchecked((int)4041785787) /* Texture (P_TEXTURE) */);
            public RenderSurface Surface => EventData.get_RenderSurface (unchecked((int)1353844973) /* Surface (P_SURFACE) */);
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Camera Camera => EventData.get_Camera (unchecked((int)1364112997) /* Camera (P_CAMERA) */);
        } /* struct EndViewRenderEventArgs */

        public partial class Renderer {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.EndViewRender += ...' instead.")]
             public Subscription SubscribeToEndViewRender (Action<EndViewRenderEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new EndViewRenderEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
        public partial struct EndAllViewsRenderEventArgs {
            public EventDataContainer EventData;
        } /* struct EndAllViewsRenderEventArgs */

} /* namespace */

namespace Urho {
        public partial struct RenderPathEventEventArgs {
            public EventDataContainer EventData;
            public String Name => EventData.get_String (unchecked((int)773762347) /* Name (P_NAME) */);
        } /* struct RenderPathEventEventArgs */

} /* namespace */

namespace Urho {
        public partial struct DeviceLostEventArgs {
            public EventDataContainer EventData;
        } /* struct DeviceLostEventArgs */

} /* namespace */

namespace Urho {
        public partial struct DeviceResetEventArgs {
            public EventDataContainer EventData;
        } /* struct DeviceResetEventArgs */

} /* namespace */

namespace Urho {
        public partial struct IKEffectorTargetChangedEventArgs {
            public EventDataContainer EventData;
            public Node EffectorNode => EventData.get_Node (unchecked((int)725806678) /* EffectorNode (P_EFFECTORNODE) */);
            public Node TargetNode => EventData.get_Node (unchecked((int)3704622643) /* TargetNode (P_TARGETNODE) */);
        } /* struct IKEffectorTargetChangedEventArgs */

} /* namespace */

namespace Urho.IO {
        public partial struct LogMessageEventArgs {
            public EventDataContainer EventData;
            public String Message => EventData.get_String (unchecked((int)2310231975) /* Message (P_MESSAGE) */);
            public int Level => EventData.get_int (unchecked((int)1030270596) /* Level (P_LEVEL) */);
        } /* struct LogMessageEventArgs */

        public partial class Log {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.LogMessage += ...' instead.")]
             public Subscription SubscribeToLogMessage (Action<LogMessageEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new LogMessageEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public uint RequestID => EventData.get_uint (unchecked((int)4010202986) /* RequestID (P_REQUESTID) */);
            public int ExitCode => EventData.get_int (unchecked((int)3466160107) /* ExitCode (P_EXITCODE) */);
        } /* struct AsyncExecFinishedEventArgs */

        public partial class FileSystem {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.AsyncExecFinished += ...' instead.")]
             public Subscription SubscribeToAsyncExecFinished (Action<AsyncExecFinishedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AsyncExecFinishedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public int Button => EventData.get_int (unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => EventData.get_int (unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct MouseButtonDownEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MouseButtonDown += ...' instead.")]
             public Subscription SubscribeToMouseButtonDown (Action<MouseButtonDownEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseButtonDownEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public int Button => EventData.get_int (unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => EventData.get_int (unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct MouseButtonUpEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MouseButtonUp += ...' instead.")]
             public Subscription SubscribeToMouseButtonUp (Action<MouseButtonUpEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseButtonUpEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
            public int DX => EventData.get_int (unchecked((int)6560020) /* DX (P_DX) */);
            public int DY => EventData.get_int (unchecked((int)6560021) /* DY (P_DY) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => EventData.get_int (unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct MouseMovedEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MouseMoved += ...' instead.")]
             public Subscription SubscribeToMouseMoved (Action<MouseMovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseMovedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public int Wheel => EventData.get_int (unchecked((int)2881891899) /* Wheel (P_WHEEL) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => EventData.get_int (unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct MouseWheelEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MouseWheel += ...' instead.")]
             public Subscription SubscribeToMouseWheel (Action<MouseWheelEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseWheelEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Key Key =>(Key) EventData.get_int (unchecked((int)890606655) /* Key (P_KEY) */);
            public int Scancode => EventData.get_int (unchecked((int)3743304650) /* Scancode (P_SCANCODE) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => EventData.get_int (unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
            public bool Repeat => EventData.get_bool (unchecked((int)958223163) /* Repeat (P_REPEAT) */);
        } /* struct KeyDownEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.KeyDown += ...' instead.")]
             public Subscription SubscribeToKeyDown (Action<KeyDownEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new KeyDownEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Key Key =>(Key) EventData.get_int (unchecked((int)890606655) /* Key (P_KEY) */);
            public int Scancode => EventData.get_int (unchecked((int)3743304650) /* Scancode (P_SCANCODE) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => EventData.get_int (unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct KeyUpEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.KeyUp += ...' instead.")]
             public Subscription SubscribeToKeyUp (Action<KeyUpEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new KeyUpEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public String Text => EventData.get_String (unchecked((int)1196085869) /* Text (P_TEXT) */);
        } /* struct TextInputEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TextInput += ...' instead.")]
             public Subscription SubscribeToTextInput (Action<TextInputEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TextInputEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
        public partial struct TextEditingEventArgs {
            public EventDataContainer EventData;
            public String Composition => EventData.get_String (unchecked((int)3273882378) /* Composition (P_COMPOSITION) */);
            public int Cursor => EventData.get_int (unchecked((int)2403995286) /* Cursor (P_CURSOR) */);
            public int SelectionLength => EventData.get_int (unchecked((int)2629242514) /* SelectionLength (P_SELECTION_LENGTH) */);
        } /* struct TextEditingEventArgs */

} /* namespace */

namespace Urho {
        public partial struct JoystickConnectedEventArgs {
            public EventDataContainer EventData;
            public int JoystickID => EventData.get_int (unchecked((int)1510428343) /* JoystickID (P_JOYSTICKID) */);
        } /* struct JoystickConnectedEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.JoystickConnected += ...' instead.")]
             public Subscription SubscribeToJoystickConnected (Action<JoystickConnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickConnectedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public int JoystickID => EventData.get_int (unchecked((int)1510428343) /* JoystickID (P_JOYSTICKID) */);
        } /* struct JoystickDisconnectedEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.JoystickDisconnected += ...' instead.")]
             public Subscription SubscribeToJoystickDisconnected (Action<JoystickDisconnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickDisconnectedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public int JoystickID => EventData.get_int (unchecked((int)1510428343) /* JoystickID (P_JOYSTICKID) */);
            public int Button => EventData.get_int (unchecked((int)3601423954) /* Button (P_BUTTON) */);
        } /* struct JoystickButtonDownEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.JoystickButtonDown += ...' instead.")]
             public Subscription SubscribeToJoystickButtonDown (Action<JoystickButtonDownEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickButtonDownEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public int JoystickID => EventData.get_int (unchecked((int)1510428343) /* JoystickID (P_JOYSTICKID) */);
            public int Button => EventData.get_int (unchecked((int)3601423954) /* Button (P_BUTTON) */);
        } /* struct JoystickButtonUpEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.JoystickButtonUp += ...' instead.")]
             public Subscription SubscribeToJoystickButtonUp (Action<JoystickButtonUpEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickButtonUpEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public int JoystickID => EventData.get_int (unchecked((int)1510428343) /* JoystickID (P_JOYSTICKID) */);
            public int Button => EventData.get_int (unchecked((int)3601423954) /* Button (P_AXIS) */);
            public float Position => EventData.get_float (unchecked((int)1333256809) /* Position (P_POSITION) */);
        } /* struct JoystickAxisMoveEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.JoystickAxisMove += ...' instead.")]
             public Subscription SubscribeToJoystickAxisMove (Action<JoystickAxisMoveEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickAxisMoveEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public int JoystickID => EventData.get_int (unchecked((int)1510428343) /* JoystickID (P_JOYSTICKID) */);
            public int Button => EventData.get_int (unchecked((int)3601423954) /* Button (P_HAT) */);
            public int Position => EventData.get_int (unchecked((int)1333256809) /* Position (P_POSITION) */);
        } /* struct JoystickHatMoveEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.JoystickHatMove += ...' instead.")]
             public Subscription SubscribeToJoystickHatMove (Action<JoystickHatMoveEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickHatMoveEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public int TouchID => EventData.get_int (unchecked((int)3850094778) /* TouchID (P_TOUCHID) */);
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
            public float Pressure => EventData.get_float (unchecked((int)439090309) /* Pressure (P_PRESSURE) */);
        } /* struct TouchBeginEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TouchBegin += ...' instead.")]
             public Subscription SubscribeToTouchBegin (Action<TouchBeginEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TouchBeginEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public int TouchID => EventData.get_int (unchecked((int)3850094778) /* TouchID (P_TOUCHID) */);
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
        } /* struct TouchEndEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TouchEnd += ...' instead.")]
             public Subscription SubscribeToTouchEnd (Action<TouchEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TouchEndEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public int TouchID => EventData.get_int (unchecked((int)3850094778) /* TouchID (P_TOUCHID) */);
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
            public int DX => EventData.get_int (unchecked((int)6560020) /* DX (P_DX) */);
            public int DY => EventData.get_int (unchecked((int)6560021) /* DY (P_DY) */);
            public float Pressure => EventData.get_float (unchecked((int)439090309) /* Pressure (P_PRESSURE) */);
        } /* struct TouchMoveEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TouchMove += ...' instead.")]
             public Subscription SubscribeToTouchMove (Action<TouchMoveEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TouchMoveEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public uint GestureID => EventData.get_uint (unchecked((int)2079416292) /* GestureID (P_GESTUREID) */);
        } /* struct GestureRecordedEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.GestureRecorded += ...' instead.")]
             public Subscription SubscribeToGestureRecorded (Action<GestureRecordedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new GestureRecordedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public uint GestureID => EventData.get_uint (unchecked((int)2079416292) /* GestureID (P_GESTUREID) */);
            public int CenterX => EventData.get_int (unchecked((int)150093091) /* CenterX (P_CENTERX) */);
            public int CenterY => EventData.get_int (unchecked((int)150093092) /* CenterY (P_CENTERY) */);
            public int NumFingers => EventData.get_int (unchecked((int)2749362116) /* NumFingers (P_NUMFINGERS) */);
            public float Error => EventData.get_float (unchecked((int)3168564136) /* Error (P_ERROR) */);
        } /* struct GestureInputEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.GestureInput += ...' instead.")]
             public Subscription SubscribeToGestureInput (Action<GestureInputEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new GestureInputEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public int CenterX => EventData.get_int (unchecked((int)150093091) /* CenterX (P_CENTERX) */);
            public int CenterY => EventData.get_int (unchecked((int)150093092) /* CenterY (P_CENTERY) */);
            public int NumFingers => EventData.get_int (unchecked((int)2749362116) /* NumFingers (P_NUMFINGERS) */);
            public float DTheta => EventData.get_float (unchecked((int)2305167738) /* DTheta (P_DTHETA) */);
            public float DDist => EventData.get_float (unchecked((int)3911589802) /* DDist (P_DDIST) */);
        } /* struct MultiGestureEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MultiGesture += ...' instead.")]
             public Subscription SubscribeToMultiGesture (Action<MultiGestureEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MultiGestureEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public String FileName => EventData.get_String (unchecked((int)633459751) /* FileName (P_FILENAME) */);
        } /* struct DropFileEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.DropFile += ...' instead.")]
             public Subscription SubscribeToDropFile (Action<DropFileEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DropFileEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public bool Focus => EventData.get_bool (unchecked((int)1842837848) /* Focus (P_FOCUS) */);
            public bool Minimized => EventData.get_bool (unchecked((int)541506182) /* Minimized (P_MINIMIZED) */);
        } /* struct InputFocusEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.InputFocus += ...' instead.")]
             public Subscription SubscribeToInputFocus (Action<InputFocusEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new InputFocusEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public bool Visible => EventData.get_bool (unchecked((int)2569414770) /* Visible (P_VISIBLE) */);
        } /* struct MouseVisibleChangedEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MouseVisibleChanged += ...' instead.")]
             public Subscription SubscribeToMouseVisibleChanged (Action<MouseVisibleChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseVisibleChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public MouseMode Mode => EventData.get_MouseMode (unchecked((int)108245827) /* Mode (P_MODE) */);
            public bool MouseLocked => EventData.get_bool (unchecked((int)3485665135) /* MouseLocked (P_MOUSELOCKED) */);
        } /* struct MouseModeChangedEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MouseModeChanged += ...' instead.")]
             public Subscription SubscribeToMouseModeChanged (Action<MouseModeChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseModeChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
        } /* struct ExitRequestedEventArgs */

        public partial class Input {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ExitRequested += ...' instead.")]
             public Subscription SubscribeToExitRequested (Action<ExitRequestedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ExitRequestedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public IntPtr SDLEvent => EventData.get_IntPtr (unchecked((int)3036739231) /* SDLEvent (P_SDLEVENT) */);
            public bool Consumed => EventData.get_bool (unchecked((int)1885648840) /* Consumed (P_CONSUMED) */);
        } /* struct SDLRawInputEventArgs */

} /* namespace */

namespace Urho {
        public partial struct InputBeginEventArgs {
            public EventDataContainer EventData;
        } /* struct InputBeginEventArgs */

} /* namespace */

namespace Urho {
        public partial struct InputEndEventArgs {
            public EventDataContainer EventData;
        } /* struct InputEndEventArgs */

} /* namespace */

namespace Urho.Navigation {
        public partial struct NavigationMeshRebuiltEventArgs {
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public NavigationMesh Mesh => EventData.get_NavigationMesh (unchecked((int)26614765) /* Mesh (P_MESH) */);
        } /* struct NavigationMeshRebuiltEventArgs */

        public partial class NavigationMesh {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NavigationMeshRebuilt += ...' instead.")]
             public Subscription SubscribeToNavigationMeshRebuilt (Action<NavigationMeshRebuiltEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NavigationMeshRebuiltEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public NavigationMesh Mesh => EventData.get_NavigationMesh (unchecked((int)26614765) /* Mesh (P_MESH) */);
            public Vector3 BoundsMin => EventData.get_Vector3 (unchecked((int)2452762269) /* BoundsMin (P_BOUNDSMIN) */);
            public Vector3 BoundsMax => EventData.get_Vector3 (unchecked((int)2452237487) /* BoundsMax (P_BOUNDSMAX) */);
        } /* struct NavigationAreaRebuiltEventArgs */

        public partial class NavigationMesh {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NavigationAreaRebuilt += ...' instead.")]
             public Subscription SubscribeToNavigationAreaRebuilt (Action<NavigationAreaRebuiltEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NavigationAreaRebuiltEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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

namespace Urho {
        public partial struct NavigationTileAddedEventArgs {
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public NavigationMesh Mesh => EventData.get_NavigationMesh (unchecked((int)26614765) /* Mesh (P_MESH) */);
            public IntVector2 Tile => EventData.get_IntVector2 (unchecked((int)1228344686) /* Tile (P_TILE) */);
        } /* struct NavigationTileAddedEventArgs */

} /* namespace */

namespace Urho {
        public partial struct NavigationTileRemovedEventArgs {
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public NavigationMesh Mesh => EventData.get_NavigationMesh (unchecked((int)26614765) /* Mesh (P_MESH) */);
            public IntVector2 Tile => EventData.get_IntVector2 (unchecked((int)1228344686) /* Tile (P_TILE) */);
        } /* struct NavigationTileRemovedEventArgs */

} /* namespace */

namespace Urho {
        public partial struct NavigationAllTilesRemovedEventArgs {
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public NavigationMesh Mesh => EventData.get_NavigationMesh (unchecked((int)26614765) /* Mesh (P_MESH) */);
        } /* struct NavigationAllTilesRemovedEventArgs */

} /* namespace */

namespace Urho.Navigation {
        public partial struct CrowdAgentFormationEventArgs {
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public CrowdAgent CrowdAgent => EventData.get_CrowdAgent (unchecked((int)687004888) /* CrowdAgent (P_CROWD_AGENT) */);
            public uint Index => EventData.get_uint (unchecked((int)193188146) /* Index (P_INDEX) */);
            public uint Size => EventData.get_uint (unchecked((int)448675873) /* Size (P_SIZE) */);
            public Vector3 Position => EventData.get_Vector3 (unchecked((int)1333256809) /* Position (P_POSITION) */);
        } /* struct CrowdAgentFormationEventArgs */

} /* namespace */

namespace Urho {
        public partial struct CrowdAgentNodeFormationEventArgs {
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public CrowdAgent CrowdAgent => EventData.get_CrowdAgent (unchecked((int)687004888) /* CrowdAgent (P_CROWD_AGENT) */);
            public uint Index => EventData.get_uint (unchecked((int)193188146) /* Index (P_INDEX) */);
            public uint Size => EventData.get_uint (unchecked((int)448675873) /* Size (P_SIZE) */);
            public Vector3 Position => EventData.get_Vector3 (unchecked((int)1333256809) /* Position (P_POSITION) */);
        } /* struct CrowdAgentNodeFormationEventArgs */

} /* namespace */

namespace Urho.Navigation {
        public partial struct CrowdAgentRepositionEventArgs {
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public CrowdAgent CrowdAgent => EventData.get_CrowdAgent (unchecked((int)687004888) /* CrowdAgent (P_CROWD_AGENT) */);
            public Vector3 Position => EventData.get_Vector3 (unchecked((int)1333256809) /* Position (P_POSITION) */);
            public Vector3 Velocity => EventData.get_Vector3 (unchecked((int)2845405629) /* Velocity (P_VELOCITY) */);
            public bool Arrived => EventData.get_bool (unchecked((int)2501236845) /* Arrived (P_ARRIVED) */);
            public float TimeStep => EventData.get_float (unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct CrowdAgentRepositionEventArgs */

        public partial class CrowdManager {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.CrowdAgentReposition += ...' instead.")]
             public Subscription SubscribeToCrowdAgentReposition (Action<CrowdAgentRepositionEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new CrowdAgentRepositionEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public CrowdAgent CrowdAgent => EventData.get_CrowdAgent (unchecked((int)687004888) /* CrowdAgent (P_CROWD_AGENT) */);
            public Vector3 Position => EventData.get_Vector3 (unchecked((int)1333256809) /* Position (P_POSITION) */);
            public Vector3 Velocity => EventData.get_Vector3 (unchecked((int)2845405629) /* Velocity (P_VELOCITY) */);
            public bool Arrived => EventData.get_bool (unchecked((int)2501236845) /* Arrived (P_ARRIVED) */);
            public float TimeStep => EventData.get_float (unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct CrowdAgentNodeRepositionEventArgs */

} /* namespace */

namespace Urho.Navigation {
        public partial struct CrowdAgentFailureEventArgs {
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public CrowdAgent CrowdAgent => EventData.get_CrowdAgent (unchecked((int)687004888) /* CrowdAgent (P_CROWD_AGENT) */);
            public Vector3 Position => EventData.get_Vector3 (unchecked((int)1333256809) /* Position (P_POSITION) */);
            public Vector3 Velocity => EventData.get_Vector3 (unchecked((int)2845405629) /* Velocity (P_VELOCITY) */);
            public int CrowdAgentState => EventData.get_int (unchecked((int)1729065465) /* CrowdAgentState (P_CROWD_AGENT_STATE) */);
            public int CrowdTargetState => EventData.get_int (unchecked((int)928574867) /* CrowdTargetState (P_CROWD_TARGET_STATE) */);
        } /* struct CrowdAgentFailureEventArgs */

        public partial class CrowdManager {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.CrowdAgentFailure += ...' instead.")]
             public Subscription SubscribeToCrowdAgentFailure (Action<CrowdAgentFailureEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new CrowdAgentFailureEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public CrowdAgent CrowdAgent => EventData.get_CrowdAgent (unchecked((int)687004888) /* CrowdAgent (P_CROWD_AGENT) */);
            public Vector3 Position => EventData.get_Vector3 (unchecked((int)1333256809) /* Position (P_POSITION) */);
            public Vector3 Velocity => EventData.get_Vector3 (unchecked((int)2845405629) /* Velocity (P_VELOCITY) */);
            public int CrowdAgentState => EventData.get_int (unchecked((int)1729065465) /* CrowdAgentState (P_CROWD_AGENT_STATE) */);
            public int CrowdTargetState => EventData.get_int (unchecked((int)928574867) /* CrowdTargetState (P_CROWD_TARGET_STATE) */);
        } /* struct CrowdAgentNodeFailureEventArgs */

} /* namespace */

namespace Urho.Navigation {
        public partial struct CrowdAgentStateChangedEventArgs {
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public CrowdAgent CrowdAgent => EventData.get_CrowdAgent (unchecked((int)687004888) /* CrowdAgent (P_CROWD_AGENT) */);
            public Vector3 Position => EventData.get_Vector3 (unchecked((int)1333256809) /* Position (P_POSITION) */);
            public Vector3 Velocity => EventData.get_Vector3 (unchecked((int)2845405629) /* Velocity (P_VELOCITY) */);
            public int CrowdAgentState => EventData.get_int (unchecked((int)1729065465) /* CrowdAgentState (P_CROWD_AGENT_STATE) */);
            public int CrowdTargetState => EventData.get_int (unchecked((int)928574867) /* CrowdTargetState (P_CROWD_TARGET_STATE) */);
        } /* struct CrowdAgentStateChangedEventArgs */

        public partial class CrowdManager {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.CrowdAgentStateChanged += ...' instead.")]
             public Subscription SubscribeToCrowdAgentStateChanged (Action<CrowdAgentStateChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new CrowdAgentStateChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public CrowdAgent CrowdAgent => EventData.get_CrowdAgent (unchecked((int)687004888) /* CrowdAgent (P_CROWD_AGENT) */);
            public Vector3 Position => EventData.get_Vector3 (unchecked((int)1333256809) /* Position (P_POSITION) */);
            public Vector3 Velocity => EventData.get_Vector3 (unchecked((int)2845405629) /* Velocity (P_VELOCITY) */);
            public int CrowdAgentState => EventData.get_int (unchecked((int)1729065465) /* CrowdAgentState (P_CROWD_AGENT_STATE) */);
            public int CrowdTargetState => EventData.get_int (unchecked((int)928574867) /* CrowdTargetState (P_CROWD_TARGET_STATE) */);
        } /* struct CrowdAgentNodeStateChangedEventArgs */

} /* namespace */

namespace Urho.Navigation {
        public partial struct NavigationObstacleAddedEventArgs {
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public Obstacle Obstacle => EventData.get_Obstacle (unchecked((int)1080511791) /* Obstacle (P_OBSTACLE) */);
            public Vector3 Position => EventData.get_Vector3 (unchecked((int)1333256809) /* Position (P_POSITION) */);
            public float Radius => EventData.get_float (unchecked((int)4247146802) /* Radius (P_RADIUS) */);
            public float Height => EventData.get_float (unchecked((int)380957255) /* Height (P_HEIGHT) */);
        } /* struct NavigationObstacleAddedEventArgs */

        public partial class DynamicNavigationMesh {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NavigationObstacleAdded += ...' instead.")]
             public Subscription SubscribeToNavigationObstacleAdded (Action<NavigationObstacleAddedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NavigationObstacleAddedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public Obstacle Obstacle => EventData.get_Obstacle (unchecked((int)1080511791) /* Obstacle (P_OBSTACLE) */);
            public Vector3 Position => EventData.get_Vector3 (unchecked((int)1333256809) /* Position (P_POSITION) */);
            public float Radius => EventData.get_float (unchecked((int)4247146802) /* Radius (P_RADIUS) */);
            public float Height => EventData.get_float (unchecked((int)380957255) /* Height (P_HEIGHT) */);
        } /* struct NavigationObstacleRemovedEventArgs */

        public partial class DynamicNavigationMesh {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NavigationObstacleRemoved += ...' instead.")]
             public Subscription SubscribeToNavigationObstacleRemoved (Action<NavigationObstacleRemovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NavigationObstacleRemovedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
        } /* struct ServerConnectedEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ServerConnected += ...' instead.")]
             public Subscription SubscribeToServerConnected (Action<ServerConnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ServerConnectedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
        } /* struct ServerDisconnectedEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ServerDisconnected += ...' instead.")]
             public Subscription SubscribeToServerDisconnected (Action<ServerDisconnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ServerDisconnectedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
        } /* struct ConnectFailedEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ConnectFailed += ...' instead.")]
             public Subscription SubscribeToConnectFailed (Action<ConnectFailedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ConnectFailedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Connection Connection => EventData.get_Connection (unchecked((int)2179499454) /* Connection (P_CONNECTION) */);
        } /* struct ClientConnectedEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ClientConnected += ...' instead.")]
             public Subscription SubscribeToClientConnected (Action<ClientConnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ClientConnectedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Connection Connection => EventData.get_Connection (unchecked((int)2179499454) /* Connection (P_CONNECTION) */);
        } /* struct ClientDisconnectedEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ClientDisconnected += ...' instead.")]
             public Subscription SubscribeToClientDisconnected (Action<ClientDisconnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ClientDisconnectedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Connection Connection => EventData.get_Connection (unchecked((int)2179499454) /* Connection (P_CONNECTION) */);
            public bool Allow => EventData.get_bool (unchecked((int)2467149353) /* Allow (P_ALLOW) */);
        } /* struct ClientIdentityEventArgs */

        public partial class Connection {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ClientIdentity += ...' instead.")]
             public Subscription SubscribeToClientIdentity (Action<ClientIdentityEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ClientIdentityEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Connection Connection => EventData.get_Connection (unchecked((int)2179499454) /* Connection (P_CONNECTION) */);
        } /* struct ClientSceneLoadedEventArgs */

        public partial class Connection {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ClientSceneLoaded += ...' instead.")]
             public Subscription SubscribeToClientSceneLoaded (Action<ClientSceneLoadedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ClientSceneLoadedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Connection Connection => EventData.get_Connection (unchecked((int)2179499454) /* Connection (P_CONNECTION) */);
            public int MessageID => EventData.get_int (unchecked((int)169676386) /* MessageID (P_MESSAGEID) */);
            public byte [] Data => EventData.get_Buffer (unchecked((int)1558284138) /* Data (P_DATA) */);
        } /* struct NetworkMessageEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NetworkMessage += ...' instead.")]
             public Subscription SubscribeToNetworkMessage (Action<NetworkMessageEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NetworkMessageEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
        } /* struct NetworkUpdateEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NetworkUpdate += ...' instead.")]
             public Subscription SubscribeToNetworkUpdate (Action<NetworkUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NetworkUpdateEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
        } /* struct NetworkUpdateSentEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NetworkUpdateSent += ...' instead.")]
             public Subscription SubscribeToNetworkUpdateSent (Action<NetworkUpdateSentEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NetworkUpdateSentEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Connection Connection => EventData.get_Connection (unchecked((int)2179499454) /* Connection (P_CONNECTION) */);
        } /* struct NetworkSceneLoadFailedEventArgs */

        public partial class Network {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NetworkSceneLoadFailed += ...' instead.")]
             public Subscription SubscribeToNetworkSceneLoadFailed (Action<NetworkSceneLoadFailedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NetworkSceneLoadFailedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Connection Connection => EventData.get_Connection (unchecked((int)2179499454) /* Connection (P_CONNECTION) */);
        } /* struct RemoteEventDataEventArgs */

} /* namespace */

namespace Urho.Physics {
        public partial struct PhysicsPreStepEventArgs {
            public EventDataContainer EventData;
            public PhysicsWorld World => EventData.get_PhysicsWorld (unchecked((int)4158893746) /* World (P_WORLD) */);
            public float TimeStep => EventData.get_float (unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct PhysicsPreStepEventArgs */

        public partial class PhysicsWorld {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PhysicsPreStep += ...' instead.")]
             public Subscription SubscribeToPhysicsPreStep (Action<PhysicsPreStepEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsPreStepEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public PhysicsWorld World => EventData.get_PhysicsWorld (unchecked((int)4158893746) /* World (P_WORLD) */);
            public float TimeStep => EventData.get_float (unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct PhysicsPostStepEventArgs */

        public partial class PhysicsWorld {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PhysicsPostStep += ...' instead.")]
             public Subscription SubscribeToPhysicsPostStep (Action<PhysicsPostStepEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsPostStepEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public PhysicsWorld World => EventData.get_PhysicsWorld (unchecked((int)4158893746) /* World (P_WORLD) */);
            public Node NodeA => EventData.get_Node (unchecked((int)2376629471) /* NodeA (P_NODEA) */);
            public Node NodeB => EventData.get_Node (unchecked((int)2376629472) /* NodeB (P_NODEB) */);
            public RigidBody BodyA => EventData.get_RigidBody (unchecked((int)1588071871) /* BodyA (P_BODYA) */);
            public RigidBody BodyB => EventData.get_RigidBody (unchecked((int)1588071872) /* BodyB (P_BODYB) */);
            public bool Trigger => EventData.get_bool (unchecked((int)2995104504) /* Trigger (P_TRIGGER) */);
            public CollisionData [] Contacts => EventData.get_CollisionData (unchecked((int)216739987) /* Contacts (P_CONTACTS) */);
        } /* struct PhysicsCollisionStartEventArgs */

        public partial class PhysicsWorld {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PhysicsCollisionStart += ...' instead.")]
             public Subscription SubscribeToPhysicsCollisionStart (Action<PhysicsCollisionStartEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsCollisionStartEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public PhysicsWorld World => EventData.get_PhysicsWorld (unchecked((int)4158893746) /* World (P_WORLD) */);
            public Node NodeA => EventData.get_Node (unchecked((int)2376629471) /* NodeA (P_NODEA) */);
            public Node NodeB => EventData.get_Node (unchecked((int)2376629472) /* NodeB (P_NODEB) */);
            public RigidBody BodyA => EventData.get_RigidBody (unchecked((int)1588071871) /* BodyA (P_BODYA) */);
            public RigidBody BodyB => EventData.get_RigidBody (unchecked((int)1588071872) /* BodyB (P_BODYB) */);
            public bool Trigger => EventData.get_bool (unchecked((int)2995104504) /* Trigger (P_TRIGGER) */);
            public CollisionData [] Contacts => EventData.get_CollisionData (unchecked((int)216739987) /* Contacts (P_CONTACTS) */);
        } /* struct PhysicsCollisionEventArgs */

        public partial class PhysicsWorld {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PhysicsCollision += ...' instead.")]
             public Subscription SubscribeToPhysicsCollision (Action<PhysicsCollisionEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsCollisionEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public PhysicsWorld World => EventData.get_PhysicsWorld (unchecked((int)4158893746) /* World (P_WORLD) */);
            public Node NodeA => EventData.get_Node (unchecked((int)2376629471) /* NodeA (P_NODEA) */);
            public Node NodeB => EventData.get_Node (unchecked((int)2376629472) /* NodeB (P_NODEB) */);
            public RigidBody BodyA => EventData.get_RigidBody (unchecked((int)1588071871) /* BodyA (P_BODYA) */);
            public RigidBody BodyB => EventData.get_RigidBody (unchecked((int)1588071872) /* BodyB (P_BODYB) */);
            public bool Trigger => EventData.get_bool (unchecked((int)2995104504) /* Trigger (P_TRIGGER) */);
        } /* struct PhysicsCollisionEndEventArgs */

        public partial class PhysicsWorld {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PhysicsCollisionEnd += ...' instead.")]
             public Subscription SubscribeToPhysicsCollisionEnd (Action<PhysicsCollisionEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsCollisionEndEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public RigidBody Body => EventData.get_RigidBody (unchecked((int)111721250) /* Body (P_BODY) */);
            public Node OtherNode => EventData.get_Node (unchecked((int)2707292594) /* OtherNode (P_OTHERNODE) */);
            public RigidBody OtherBody => EventData.get_RigidBody (unchecked((int)1930180818) /* OtherBody (P_OTHERBODY) */);
            public bool Trigger => EventData.get_bool (unchecked((int)2995104504) /* Trigger (P_TRIGGER) */);
            public CollisionData [] Contacts => EventData.get_CollisionData (unchecked((int)216739987) /* Contacts (P_CONTACTS) */);
        } /* struct NodeCollisionStartEventArgs */

        public partial class Node {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NodeCollisionStart += ...' instead.")]
             public Subscription SubscribeToNodeCollisionStart (Action<NodeCollisionStartEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeCollisionStartEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public RigidBody Body => EventData.get_RigidBody (unchecked((int)111721250) /* Body (P_BODY) */);
            public Node OtherNode => EventData.get_Node (unchecked((int)2707292594) /* OtherNode (P_OTHERNODE) */);
            public RigidBody OtherBody => EventData.get_RigidBody (unchecked((int)1930180818) /* OtherBody (P_OTHERBODY) */);
            public bool Trigger => EventData.get_bool (unchecked((int)2995104504) /* Trigger (P_TRIGGER) */);
            public CollisionData [] Contacts => EventData.get_CollisionData (unchecked((int)216739987) /* Contacts (P_CONTACTS) */);
        } /* struct NodeCollisionEventArgs */

        public partial class Node {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NodeCollision += ...' instead.")]
             public Subscription SubscribeToNodeCollision (Action<NodeCollisionEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeCollisionEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public RigidBody Body => EventData.get_RigidBody (unchecked((int)111721250) /* Body (P_BODY) */);
            public Node OtherNode => EventData.get_Node (unchecked((int)2707292594) /* OtherNode (P_OTHERNODE) */);
            public RigidBody OtherBody => EventData.get_RigidBody (unchecked((int)1930180818) /* OtherBody (P_OTHERBODY) */);
            public bool Trigger => EventData.get_bool (unchecked((int)2995104504) /* Trigger (P_TRIGGER) */);
        } /* struct NodeCollisionEndEventArgs */

        public partial class Node {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NodeCollisionEnd += ...' instead.")]
             public Subscription SubscribeToNodeCollisionEnd (Action<NodeCollisionEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeCollisionEndEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
        } /* struct ReloadStartedEventArgs */

        public partial class Resource {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ReloadStarted += ...' instead.")]
             public Subscription SubscribeToReloadStarted (Action<ReloadStartedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ReloadStartedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
        } /* struct ReloadFinishedEventArgs */

        public partial class Resource {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ReloadFinished += ...' instead.")]
             public Subscription SubscribeToReloadFinished (Action<ReloadFinishedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ReloadFinishedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
        } /* struct ReloadFailedEventArgs */

        public partial class Resource {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ReloadFailed += ...' instead.")]
             public Subscription SubscribeToReloadFailed (Action<ReloadFailedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ReloadFailedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public String FileName => EventData.get_String (unchecked((int)633459751) /* FileName (P_FILENAME) */);
            public String ResourceName => EventData.get_String (unchecked((int)4134618969) /* ResourceName (P_RESOURCENAME) */);
        } /* struct FileChangedEventArgs */

        public partial class ResourceCache {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.FileChanged += ...' instead.")]
             public Subscription SubscribeToFileChanged (Action<FileChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FileChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public String ResourceName => EventData.get_String (unchecked((int)4134618969) /* ResourceName (P_RESOURCENAME) */);
        } /* struct LoadFailedEventArgs */

        public partial class ResourceCache {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.LoadFailed += ...' instead.")]
             public Subscription SubscribeToLoadFailed (Action<LoadFailedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new LoadFailedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public String ResourceName => EventData.get_String (unchecked((int)4134618969) /* ResourceName (P_RESOURCENAME) */);
        } /* struct ResourceNotFoundEventArgs */

        public partial class ResourceCache {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ResourceNotFound += ...' instead.")]
             public Subscription SubscribeToResourceNotFound (Action<ResourceNotFoundEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ResourceNotFoundEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public StringHash ResourceType => EventData.get_StringHash (unchecked((int)426680488) /* ResourceType (P_RESOURCETYPE) */);
        } /* struct UnknownResourceTypeEventArgs */

        public partial class ResourceCache {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.UnknownResourceType += ...' instead.")]
             public Subscription SubscribeToUnknownResourceType (Action<UnknownResourceTypeEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UnknownResourceTypeEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public String ResourceName => EventData.get_String (unchecked((int)4134618969) /* ResourceName (P_RESOURCENAME) */);
            public bool Success => EventData.get_bool (unchecked((int)3427551139) /* Success (P_SUCCESS) */);
            public Resource Resource => EventData.get_Resource (unchecked((int)39946286) /* Resource (P_RESOURCE) */);
        } /* struct ResourceBackgroundLoadedEventArgs */

        public partial class ResourceCache {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ResourceBackgroundLoaded += ...' instead.")]
             public Subscription SubscribeToResourceBackgroundLoaded (Action<ResourceBackgroundLoadedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ResourceBackgroundLoadedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
        } /* struct ChangeLanguageEventArgs */

        public partial class Localization {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ChangeLanguage += ...' instead.")]
             public Subscription SubscribeToChangeLanguage (Action<ChangeLanguageEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ChangeLanguageEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public float TimeStep => EventData.get_float (unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct SceneUpdateEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.SceneUpdate += ...' instead.")]
             public Subscription SubscribeToSceneUpdate (Action<SceneUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SceneUpdateEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public float TimeStep => EventData.get_float (unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct SceneSubsystemUpdateEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.SceneSubsystemUpdate += ...' instead.")]
             public Subscription SubscribeToSceneSubsystemUpdate (Action<SceneSubsystemUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SceneSubsystemUpdateEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public float Constant => EventData.get_float (unchecked((int)1006513988) /* Constant (P_CONSTANT) */);
            public float SquaredSnapThreshold => EventData.get_float (unchecked((int)4276457658) /* SquaredSnapThreshold (P_SQUAREDSNAPTHRESHOLD) */);
        } /* struct UpdateSmoothingEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.UpdateSmoothing += ...' instead.")]
             public Subscription SubscribeToUpdateSmoothing (Action<UpdateSmoothingEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UpdateSmoothingEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public float TimeStep => EventData.get_float (unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct SceneDrawableUpdateFinishedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.SceneDrawableUpdateFinished += ...' instead.")]
             public Subscription SubscribeToSceneDrawableUpdateFinished (Action<SceneDrawableUpdateFinishedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SceneDrawableUpdateFinishedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
        } /* struct TargetPositionChangedEventArgs */

        public partial class SmoothedTransform {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TargetPositionChanged += ...' instead.")]
             public Subscription SubscribeToTargetPositionChanged (Action<TargetPositionChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TargetPositionChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
        } /* struct TargetRotationChangedEventArgs */

        public partial class SmoothedTransform {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TargetRotationChanged += ...' instead.")]
             public Subscription SubscribeToTargetRotationChanged (Action<TargetRotationChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TargetRotationChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public float TimeStep => EventData.get_float (unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct AttributeAnimationUpdateEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.AttributeAnimationUpdate += ...' instead.")]
             public Subscription SubscribeToAttributeAnimationUpdate (Action<AttributeAnimationUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AttributeAnimationUpdateEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Object ObjectAnimation => EventData.get_Object (unchecked((int)485250565) /* ObjectAnimation (P_OBJECTANIMATION) */);
            public String AttributeAnimationName => EventData.get_String (unchecked((int)4253834771) /* AttributeAnimationName (P_ATTRIBUTEANIMATIONNAME) */);
        } /* struct AttributeAnimationAddedEventArgs */

        public partial class ObjectAnimation {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.AttributeAnimationAdded += ...' instead.")]
             public Subscription SubscribeToAttributeAnimationAdded (Action<AttributeAnimationAddedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AttributeAnimationAddedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Object ObjectAnimation => EventData.get_Object (unchecked((int)485250565) /* ObjectAnimation (P_OBJECTANIMATION) */);
            public String AttributeAnimationName => EventData.get_String (unchecked((int)4253834771) /* AttributeAnimationName (P_ATTRIBUTEANIMATIONNAME) */);
        } /* struct AttributeAnimationRemovedEventArgs */

        public partial class ObjectAnimation {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.AttributeAnimationRemoved += ...' instead.")]
             public Subscription SubscribeToAttributeAnimationRemoved (Action<AttributeAnimationRemovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AttributeAnimationRemovedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public float TimeStep => EventData.get_float (unchecked((int)417015353) /* TimeStep (P_TIMESTEP) */);
        } /* struct ScenePostUpdateEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ScenePostUpdate += ...' instead.")]
             public Subscription SubscribeToScenePostUpdate (Action<ScenePostUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ScenePostUpdateEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public float Progress => EventData.get_float (unchecked((int)2456587373) /* Progress (P_PROGRESS) */);
            public int LoadedNodes => EventData.get_int (unchecked((int)2460871468) /* LoadedNodes (P_LOADEDNODES) */);
            public int TotalNodes => EventData.get_int (unchecked((int)3592672237) /* TotalNodes (P_TOTALNODES) */);
            public int LoadedResources => EventData.get_int (unchecked((int)914347776) /* LoadedResources (P_LOADEDRESOURCES) */);
            public int TotalResources => EventData.get_int (unchecked((int)1346461377) /* TotalResources (P_TOTALRESOURCES) */);
        } /* struct AsyncLoadProgressEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.AsyncLoadProgress += ...' instead.")]
             public Subscription SubscribeToAsyncLoadProgress (Action<AsyncLoadProgressEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AsyncLoadProgressEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
        } /* struct AsyncLoadFinishedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.AsyncLoadFinished += ...' instead.")]
             public Subscription SubscribeToAsyncLoadFinished (Action<AsyncLoadFinishedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AsyncLoadFinishedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Parent => EventData.get_Node (unchecked((int)1512946026) /* Parent (P_PARENT) */);
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
        } /* struct NodeAddedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NodeAdded += ...' instead.")]
             public Subscription SubscribeToNodeAdded (Action<NodeAddedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeAddedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Parent => EventData.get_Node (unchecked((int)1512946026) /* Parent (P_PARENT) */);
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
        } /* struct NodeRemovedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NodeRemoved += ...' instead.")]
             public Subscription SubscribeToNodeRemoved (Action<NodeRemovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeRemovedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public Component Component => EventData.get_Component (unchecked((int)3739730333) /* Component (P_COMPONENT) */);
        } /* struct ComponentAddedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ComponentAdded += ...' instead.")]
             public Subscription SubscribeToComponentAdded (Action<ComponentAddedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ComponentAddedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public Component Component => EventData.get_Component (unchecked((int)3739730333) /* Component (P_COMPONENT) */);
        } /* struct ComponentRemovedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ComponentRemoved += ...' instead.")]
             public Subscription SubscribeToComponentRemoved (Action<ComponentRemovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ComponentRemovedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
        } /* struct NodeNameChangedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NodeNameChanged += ...' instead.")]
             public Subscription SubscribeToNodeNameChanged (Action<NodeNameChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeNameChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
        } /* struct NodeEnabledChangedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NodeEnabledChanged += ...' instead.")]
             public Subscription SubscribeToNodeEnabledChanged (Action<NodeEnabledChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeEnabledChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public String Tag => EventData.get_String (unchecked((int)964697786) /* Tag (P_TAG) */);
        } /* struct NodeTagAddedEventArgs */

} /* namespace */

namespace Urho {
        public partial struct NodeTagRemovedEventArgs {
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public String Tag => EventData.get_String (unchecked((int)964697786) /* Tag (P_TAG) */);
        } /* struct NodeTagRemovedEventArgs */

} /* namespace */

namespace Urho {
        public partial struct ComponentEnabledChangedEventArgs {
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public Component Component => EventData.get_Component (unchecked((int)3739730333) /* Component (P_COMPONENT) */);
        } /* struct ComponentEnabledChangedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ComponentEnabledChanged += ...' instead.")]
             public Subscription SubscribeToComponentEnabledChanged (Action<ComponentEnabledChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ComponentEnabledChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Serializable Serializable => EventData.get_Serializable (unchecked((int)1481290239) /* Serializable (P_SERIALIZABLE) */);
        } /* struct TemporaryChangedEventArgs */

        public partial class Serializable {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TemporaryChanged += ...' instead.")]
             public Subscription SubscribeToTemporaryChanged (Action<TemporaryChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TemporaryChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public Node CloneNode => EventData.get_Node (unchecked((int)3545672031) /* CloneNode (P_CLONENODE) */);
        } /* struct NodeClonedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NodeCloned += ...' instead.")]
             public Subscription SubscribeToNodeCloned (Action<NodeClonedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeClonedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Scene Scene => EventData.get_Scene (unchecked((int)3011223724) /* Scene (P_SCENE) */);
            public Component Component => EventData.get_Component (unchecked((int)3739730333) /* Component (P_COMPONENT) */);
            public Component CloneComponent => EventData.get_Component (unchecked((int)733556864) /* CloneComponent (P_CLONECOMPONENT) */);
        } /* struct ComponentClonedEventArgs */

        public partial class Scene {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ComponentCloned += ...' instead.")]
             public Subscription SubscribeToComponentCloned (Action<ComponentClonedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ComponentClonedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public Serializable Serializable => EventData.get_Serializable (unchecked((int)1481290239) /* Serializable (P_SERIALIZABLE) */);
            public uint TimeStamp => EventData.get_uint (unchecked((int)1110190518) /* TimeStamp (P_TIMESTAMP) */);
            public uint Index => EventData.get_uint (unchecked((int)193188146) /* Index (P_INDEX) */);
            public String Name => EventData.get_String (unchecked((int)773762347) /* Name (P_NAME) */);
            public Variant Value => EventData.get_Variant (unchecked((int)632064625) /* Value (P_VALUE) */);
        } /* struct InterceptNetworkUpdateEventArgs */

        public partial class Serializable {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.InterceptNetworkUpdate += ...' instead.")]
             public Subscription SubscribeToInterceptNetworkUpdate (Action<InterceptNetworkUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new InterceptNetworkUpdateEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
            public int Button => EventData.get_int (unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => EventData.get_int (unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct UIMouseClickEventArgs */

        public partial class UI {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.UIMouseClick += ...' instead.")]
             public Subscription SubscribeToUIMouseClick (Action<UIMouseClickEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UIMouseClickEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public UIElement BeginElement => EventData.get_UIElement (unchecked((int)660892787) /* BeginElement (P_BEGINELEMENT) */);
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
            public int Button => EventData.get_int (unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => EventData.get_int (unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct UIMouseClickEndEventArgs */

        public partial class UI {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.UIMouseClickEnd += ...' instead.")]
             public Subscription SubscribeToUIMouseClickEnd (Action<UIMouseClickEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UIMouseClickEndEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
            public int Button => EventData.get_int (unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => EventData.get_int (unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct UIMouseDoubleClickEventArgs */

} /* namespace */

namespace Urho {
        public partial struct ClickEventArgs {
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
            public int Button => EventData.get_int (unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => EventData.get_int (unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct ClickEventArgs */

} /* namespace */

namespace Urho {
        public partial struct ClickEndEventArgs {
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public UIElement BeginElement => EventData.get_UIElement (unchecked((int)660892787) /* BeginElement (P_BEGINELEMENT) */);
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
            public int Button => EventData.get_int (unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => EventData.get_int (unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct ClickEndEventArgs */

} /* namespace */

namespace Urho {
        public partial struct DoubleClickEventArgs {
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
            public int Button => EventData.get_int (unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => EventData.get_int (unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct DoubleClickEventArgs */

} /* namespace */

namespace Urho.Gui {
        public partial struct DragDropTestEventArgs {
            public EventDataContainer EventData;
            public UIElement Source => EventData.get_UIElement (unchecked((int)3851438139) /* Source (P_SOURCE) */);
            public UIElement Target => EventData.get_UIElement (unchecked((int)3016907569) /* Target (P_TARGET) */);
            public bool Accept => EventData.get_bool (unchecked((int)3683158536) /* Accept (P_ACCEPT) */);
        } /* struct DragDropTestEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.DragDropTest += ...' instead.")]
             public Subscription SubscribeToDragDropTest (Action<DragDropTestEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragDropTestEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Source => EventData.get_UIElement (unchecked((int)3851438139) /* Source (P_SOURCE) */);
            public UIElement Target => EventData.get_UIElement (unchecked((int)3016907569) /* Target (P_TARGET) */);
            public bool Accept => EventData.get_bool (unchecked((int)3683158536) /* Accept (P_ACCEPT) */);
        } /* struct DragDropFinishEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.DragDropFinish += ...' instead.")]
             public Subscription SubscribeToDragDropFinish (Action<DragDropFinishEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragDropFinishEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public UIElement ClickedElement => EventData.get_UIElement (unchecked((int)1630589429) /* ClickedElement (P_CLICKEDELEMENT) */);
        } /* struct FocusChangedEventArgs */

        public partial class UI {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.FocusChanged += ...' instead.")]
             public Subscription SubscribeToFocusChanged (Action<FocusChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FocusChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct NameChangedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.NameChanged += ...' instead.")]
             public Subscription SubscribeToNameChanged (Action<NameChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NameChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int Width => EventData.get_int (unchecked((int)3655201574) /* Width (P_WIDTH) */);
            public int Height => EventData.get_int (unchecked((int)380957255) /* Height (P_HEIGHT) */);
            public int DX => EventData.get_int (unchecked((int)6560020) /* DX (P_DX) */);
            public int DY => EventData.get_int (unchecked((int)6560021) /* DY (P_DY) */);
        } /* struct ResizedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.Resized += ...' instead.")]
             public Subscription SubscribeToResized (Action<ResizedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ResizedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
        } /* struct PositionedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.Positioned += ...' instead.")]
             public Subscription SubscribeToPositioned (Action<PositionedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PositionedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public bool Visible => EventData.get_bool (unchecked((int)2569414770) /* Visible (P_VISIBLE) */);
        } /* struct VisibleChangedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.VisibleChanged += ...' instead.")]
             public Subscription SubscribeToVisibleChanged (Action<VisibleChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new VisibleChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public bool ByKey => EventData.get_bool (unchecked((int)860527848) /* ByKey (P_BYKEY) */);
        } /* struct FocusedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.Focused += ...' instead.")]
             public Subscription SubscribeToFocused (Action<FocusedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FocusedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct DefocusedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.Defocused += ...' instead.")]
             public Subscription SubscribeToDefocused (Action<DefocusedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DefocusedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct LayoutUpdatedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.LayoutUpdated += ...' instead.")]
             public Subscription SubscribeToLayoutUpdated (Action<LayoutUpdatedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new LayoutUpdatedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct PressedEventArgs */

        public partial class Button {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.Pressed += ...' instead.")]
             public Subscription SubscribeToPressed (Action<PressedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PressedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct ReleasedEventArgs */

        public partial class Button {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.Released += ...' instead.")]
             public Subscription SubscribeToReleased (Action<ReleasedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ReleasedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public bool State => EventData.get_bool (unchecked((int)3363651793) /* State (P_STATE) */);
        } /* struct ToggledEventArgs */

        public partial class CheckBox {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.Toggled += ...' instead.")]
             public Subscription SubscribeToToggled (Action<ToggledEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ToggledEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public float Value => EventData.get_float (unchecked((int)632064625) /* Value (P_VALUE) */);
        } /* struct SliderChangedEventArgs */

        public partial class Slider {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.SliderChanged += ...' instead.")]
             public Subscription SubscribeToSliderChanged (Action<SliderChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SliderChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int Offset => EventData.get_int (unchecked((int)1881751827) /* Offset (P_OFFSET) */);
            public bool Pressed => EventData.get_bool (unchecked((int)2120415202) /* Pressed (P_PRESSED) */);
        } /* struct SliderPagedEventArgs */

        public partial class Slider {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.SliderPaged += ...' instead.")]
             public Subscription SubscribeToSliderPaged (Action<SliderPagedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SliderPagedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public float Value => EventData.get_float (unchecked((int)632064625) /* Value (P_VALUE) */);
        } /* struct ProgressBarChangedEventArgs */

} /* namespace */

namespace Urho.Gui {
        public partial struct ScrollBarChangedEventArgs {
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public float Value => EventData.get_float (unchecked((int)632064625) /* Value (P_VALUE) */);
        } /* struct ScrollBarChangedEventArgs */

        public partial class ScrollBar {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ScrollBarChanged += ...' instead.")]
             public Subscription SubscribeToScrollBarChanged (Action<ScrollBarChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ScrollBarChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
        } /* struct ViewChangedEventArgs */

        public partial class ScrollView {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ViewChanged += ...' instead.")]
             public Subscription SubscribeToViewChanged (Action<ViewChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ViewChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public bool Modal => EventData.get_bool (unchecked((int)1236802797) /* Modal (P_MODAL) */);
        } /* struct ModalChangedEventArgs */

        public partial class Window {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ModalChanged += ...' instead.")]
             public Subscription SubscribeToModalChanged (Action<ModalChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ModalChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
        public partial struct TextEntryEventArgs {
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public String Text => EventData.get_String (unchecked((int)1196085869) /* Text (P_TEXT) */);
        } /* struct TextEntryEventArgs */

        public partial class LineEdit {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TextEntry += ...' instead.")]
             public Subscription SubscribeToTextEntry (Action<TextEntryEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TextEntryEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
                  var s = new Subscription (proxy);
                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)1906402885) /* TextEntry (E_TEXTENTRY) */);
                  return s;
             }

             static UrhoEventAdapter<TextEntryEventArgs> eventAdapterForTextEntry;
             public event Action<TextEntryEventArgs> TextEntry
             {
                 add
                 {
                      if (eventAdapterForTextEntry == null)
                          eventAdapterForTextEntry = new UrhoEventAdapter<TextEntryEventArgs>(typeof(LineEdit));
                      eventAdapterForTextEntry.AddManagedSubscriber(handle, value, SubscribeToTextEntry);
                 }
                 remove { eventAdapterForTextEntry.RemoveManagedSubscriber(handle, value); }
             }
        } /* class LineEdit */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct TextChangedEventArgs {
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public String Text => EventData.get_String (unchecked((int)1196085869) /* Text (P_TEXT) */);
        } /* struct TextChangedEventArgs */

        public partial class LineEdit {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TextChanged += ...' instead.")]
             public Subscription SubscribeToTextChanged (Action<TextChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TextChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public String Text => EventData.get_String (unchecked((int)1196085869) /* Text (P_TEXT) */);
            public float Value => EventData.get_float (unchecked((int)632064625) /* Value (P_VALUE) */);
        } /* struct TextFinishedEventArgs */

        public partial class LineEdit {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.TextFinished += ...' instead.")]
             public Subscription SubscribeToTextFinished (Action<TextFinishedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TextFinishedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct MenuSelectedEventArgs */

        public partial class Menu {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MenuSelected += ...' instead.")]
             public Subscription SubscribeToMenuSelected (Action<MenuSelectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MenuSelectedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int Selection => EventData.get_int (unchecked((int)3519890092) /* Selection (P_SELECTION) */);
        } /* struct ItemSelectedEventArgs */

        public partial class DropDownList {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ItemSelected += ...' instead.")]
             public Subscription SubscribeToItemSelected (Action<ItemSelectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ItemSelectedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
                  Action<IntPtr> proxy = (x)=> { var d = new ItemSelectedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int Selection => EventData.get_int (unchecked((int)3519890092) /* Selection (P_SELECTION) */);
        } /* struct ItemDeselectedEventArgs */

        public partial class ListView {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ItemDeselected += ...' instead.")]
             public Subscription SubscribeToItemDeselected (Action<ItemDeselectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ItemDeselectedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct SelectionChangedEventArgs */

        public partial class ListView {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.SelectionChanged += ...' instead.")]
             public Subscription SubscribeToSelectionChanged (Action<SelectionChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SelectionChangedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public UIElement Item => EventData.get_UIElement (unchecked((int)1322237459) /* Item (P_ITEM) */);
            public int Selection => EventData.get_int (unchecked((int)3519890092) /* Selection (P_SELECTION) */);
            public int Button => EventData.get_int (unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => EventData.get_int (unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct ItemClickedEventArgs */

        public partial class ListView {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ItemClicked += ...' instead.")]
             public Subscription SubscribeToItemClicked (Action<ItemClickedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ItemClickedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public UIElement Item => EventData.get_UIElement (unchecked((int)1322237459) /* Item (P_ITEM) */);
            public int Selection => EventData.get_int (unchecked((int)3519890092) /* Selection (P_SELECTION) */);
            public int Button => EventData.get_int (unchecked((int)3601423954) /* Button (P_BUTTON) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => EventData.get_int (unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct ItemDoubleClickedEventArgs */

        public partial class ListView {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ItemDoubleClicked += ...' instead.")]
             public Subscription SubscribeToItemDoubleClicked (Action<ItemDoubleClickedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ItemDoubleClickedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public Key Key =>(Key) EventData.get_int (unchecked((int)890606655) /* Key (P_KEY) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int Qualifiers => EventData.get_int (unchecked((int)1438392841) /* Qualifiers (P_QUALIFIERS) */);
        } /* struct UnhandledKeyEventArgs */

        public partial class LineEdit {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.UnhandledKey += ...' instead.")]
             public Subscription SubscribeToUnhandledKey (Action<UnhandledKeyEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UnhandledKeyEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
                  Action<IntPtr> proxy = (x)=> { var d = new UnhandledKeyEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public String FileName => EventData.get_String (unchecked((int)633459751) /* FileName (P_FILENAME) */);
            public String Filter => EventData.get_String (unchecked((int)2349197016) /* Filter (P_FILTER) */);
            public bool Ok => EventData.get_bool (unchecked((int)7281596) /* Ok (P_OK) */);
        } /* struct FileSelectedEventArgs */

        public partial class FileSelector {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.FileSelected += ...' instead.")]
             public Subscription SubscribeToFileSelected (Action<FileSelectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FileSelectedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public bool Ok => EventData.get_bool (unchecked((int)7281596) /* Ok (P_OK) */);
        } /* struct MessageACKEventArgs */

        public partial class MessageBox {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.MessageACK += ...' instead.")]
             public Subscription SubscribeToMessageACK (Action<MessageACKEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MessageACKEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Root => EventData.get_UIElement (unchecked((int)4011903426) /* Root (P_ROOT) */);
            public UIElement Parent => EventData.get_UIElement (unchecked((int)1512946026) /* Parent (P_PARENT) */);
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct ElementAddedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ElementAdded += ...' instead.")]
             public Subscription SubscribeToElementAdded (Action<ElementAddedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ElementAddedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Root => EventData.get_UIElement (unchecked((int)4011903426) /* Root (P_ROOT) */);
            public UIElement Parent => EventData.get_UIElement (unchecked((int)1512946026) /* Parent (P_PARENT) */);
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct ElementRemovedEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.ElementRemoved += ...' instead.")]
             public Subscription SubscribeToElementRemoved (Action<ElementRemovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ElementRemovedEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
            public int ElementX => EventData.get_int (unchecked((int)3977097692) /* ElementX (P_ELEMENTX) */);
            public int ElementY => EventData.get_int (unchecked((int)3977097693) /* ElementY (P_ELEMENTY) */);
        } /* struct HoverBeginEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.HoverBegin += ...' instead.")]
             public Subscription SubscribeToHoverBegin (Action<HoverBeginEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new HoverBeginEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
        } /* struct HoverEndEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.HoverEnd += ...' instead.")]
             public Subscription SubscribeToHoverEnd (Action<HoverEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new HoverEndEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
            public int ElementX => EventData.get_int (unchecked((int)3977097692) /* ElementX (P_ELEMENTX) */);
            public int ElementY => EventData.get_int (unchecked((int)3977097693) /* ElementY (P_ELEMENTY) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int NumButtons => EventData.get_int (unchecked((int)1318335099) /* NumButtons (P_NUMBUTTONS) */);
        } /* struct DragBeginEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.DragBegin += ...' instead.")]
             public Subscription SubscribeToDragBegin (Action<DragBeginEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragBeginEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
            public int DX => EventData.get_int (unchecked((int)6560020) /* DX (P_DX) */);
            public int DY => EventData.get_int (unchecked((int)6560021) /* DY (P_DY) */);
            public int ElementX => EventData.get_int (unchecked((int)3977097692) /* ElementX (P_ELEMENTX) */);
            public int ElementY => EventData.get_int (unchecked((int)3977097693) /* ElementY (P_ELEMENTY) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int NumButtons => EventData.get_int (unchecked((int)1318335099) /* NumButtons (P_NUMBUTTONS) */);
        } /* struct DragMoveEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.DragMove += ...' instead.")]
             public Subscription SubscribeToDragMove (Action<DragMoveEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragMoveEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
            public int ElementX => EventData.get_int (unchecked((int)3977097692) /* ElementX (P_ELEMENTX) */);
            public int ElementY => EventData.get_int (unchecked((int)3977097693) /* ElementY (P_ELEMENTY) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int NumButtons => EventData.get_int (unchecked((int)1318335099) /* NumButtons (P_NUMBUTTONS) */);
        } /* struct DragEndEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.DragEnd += ...' instead.")]
             public Subscription SubscribeToDragEnd (Action<DragEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragEndEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
            public int ElementX => EventData.get_int (unchecked((int)3977097692) /* ElementX (P_ELEMENTX) */);
            public int ElementY => EventData.get_int (unchecked((int)3977097693) /* ElementY (P_ELEMENTY) */);
            public int Buttons => EventData.get_int (unchecked((int)838874785) /* Buttons (P_BUTTONS) */);
            public int NumButtons => EventData.get_int (unchecked((int)1318335099) /* NumButtons (P_NUMBUTTONS) */);
        } /* struct DragCancelEventArgs */

        public partial class UIElement {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.DragCancel += ...' instead.")]
             public Subscription SubscribeToDragCancel (Action<DragCancelEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragCancelEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public String FileName => EventData.get_String (unchecked((int)633459751) /* FileName (P_FILENAME) */);
            public UIElement Element => EventData.get_UIElement (unchecked((int)2809902492) /* Element (P_ELEMENT) */);
            public int X => EventData.get_int (unchecked((int)120) /* X (P_X) */);
            public int Y => EventData.get_int (unchecked((int)121) /* Y (P_Y) */);
            public int ElementX => EventData.get_int (unchecked((int)3977097692) /* ElementX (P_ELEMENTX) */);
            public int ElementY => EventData.get_int (unchecked((int)3977097693) /* ElementY (P_ELEMENTY) */);
        } /* struct UIDropFileEventArgs */

        public partial class UI {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.UIDropFile += ...' instead.")]
             public Subscription SubscribeToUIDropFile (Action<UIDropFileEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UIDropFileEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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

namespace Urho {
        public partial struct PhysicsUpdateContact2DEventArgs {
            public EventDataContainer EventData;
            public PhysicsWorld2D World => EventData.get_PhysicsWorld2D (unchecked((int)4158893746) /* World (P_WORLD) */);
            public RigidBody2D BodyA => EventData.get_RigidBody2D (unchecked((int)1588071871) /* BodyA (P_BODYA) */);
            public RigidBody2D BodyB => EventData.get_RigidBody2D (unchecked((int)1588071872) /* BodyB (P_BODYB) */);
            public Node NodeA => EventData.get_Node (unchecked((int)2376629471) /* NodeA (P_NODEA) */);
            public Node NodeB => EventData.get_Node (unchecked((int)2376629472) /* NodeB (P_NODEB) */);
            public CollisionData [] Contacts => EventData.get_CollisionData (unchecked((int)216739987) /* Contacts (P_CONTACTS) */);
            public CollisionShape2D ShapeA => EventData.get_CollisionShape2D (unchecked((int)1721188800) /* ShapeA (P_SHAPEA) */);
            public CollisionShape2D ShapeB => EventData.get_CollisionShape2D (unchecked((int)1721188801) /* ShapeB (P_SHAPEB) */);
            public bool Enabled => EventData.get_bool (unchecked((int)3351342625) /* Enabled (P_ENABLED) */);
        } /* struct PhysicsUpdateContact2DEventArgs */

} /* namespace */

namespace Urho.Urho2D {
        public partial struct PhysicsBeginContact2DEventArgs {
            public EventDataContainer EventData;
            public PhysicsWorld2D World => EventData.get_PhysicsWorld2D (unchecked((int)4158893746) /* World (P_WORLD) */);
            public RigidBody2D BodyA => EventData.get_RigidBody2D (unchecked((int)1588071871) /* BodyA (P_BODYA) */);
            public RigidBody2D BodyB => EventData.get_RigidBody2D (unchecked((int)1588071872) /* BodyB (P_BODYB) */);
            public Node NodeA => EventData.get_Node (unchecked((int)2376629471) /* NodeA (P_NODEA) */);
            public Node NodeB => EventData.get_Node (unchecked((int)2376629472) /* NodeB (P_NODEB) */);
            public CollisionData [] Contacts => EventData.get_CollisionData (unchecked((int)216739987) /* Contacts (P_CONTACTS) */);
            public CollisionShape2D ShapeA => EventData.get_CollisionShape2D (unchecked((int)1721188800) /* ShapeA (P_SHAPEA) */);
            public CollisionShape2D ShapeB => EventData.get_CollisionShape2D (unchecked((int)1721188801) /* ShapeB (P_SHAPEB) */);
        } /* struct PhysicsBeginContact2DEventArgs */

        public partial class PhysicsWorld2D {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PhysicsBeginContact2D += ...' instead.")]
             public Subscription SubscribeToPhysicsBeginContact2D (Action<PhysicsBeginContact2DEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsBeginContact2DEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
            public EventDataContainer EventData;
            public PhysicsWorld2D World => EventData.get_PhysicsWorld2D (unchecked((int)4158893746) /* World (P_WORLD) */);
            public RigidBody2D BodyA => EventData.get_RigidBody2D (unchecked((int)1588071871) /* BodyA (P_BODYA) */);
            public RigidBody2D BodyB => EventData.get_RigidBody2D (unchecked((int)1588071872) /* BodyB (P_BODYB) */);
            public Node NodeA => EventData.get_Node (unchecked((int)2376629471) /* NodeA (P_NODEA) */);
            public Node NodeB => EventData.get_Node (unchecked((int)2376629472) /* NodeB (P_NODEB) */);
            public CollisionData [] Contacts => EventData.get_CollisionData (unchecked((int)216739987) /* Contacts (P_CONTACTS) */);
            public CollisionShape2D ShapeA => EventData.get_CollisionShape2D (unchecked((int)1721188800) /* ShapeA (P_SHAPEA) */);
            public CollisionShape2D ShapeB => EventData.get_CollisionShape2D (unchecked((int)1721188801) /* ShapeB (P_SHAPEB) */);
        } /* struct PhysicsEndContact2DEventArgs */

        public partial class PhysicsWorld2D {
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.PhysicsEndContact2D += ...' instead.")]
             public Subscription SubscribeToPhysicsEndContact2D (Action<PhysicsEndContact2DEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsEndContact2DEventArgs () { EventData = new EventDataContainer(x) }; handler (d); };
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
        public partial struct NodeUpdateContact2DEventArgs {
            public EventDataContainer EventData;
            public RigidBody2D Body => EventData.get_RigidBody2D (unchecked((int)111721250) /* Body (P_BODY) */);
            public Node OtherNode => EventData.get_Node (unchecked((int)2707292594) /* OtherNode (P_OTHERNODE) */);
            public RigidBody2D OtherBody => EventData.get_RigidBody2D (unchecked((int)1930180818) /* OtherBody (P_OTHERBODY) */);
            public CollisionData [] Contacts => EventData.get_CollisionData (unchecked((int)216739987) /* Contacts (P_CONTACTS) */);
            public CollisionShape2D Shape => EventData.get_CollisionShape2D (unchecked((int)2586277601) /* Shape (P_SHAPE) */);
            public CollisionShape2D OtherShape => EventData.get_CollisionShape2D (unchecked((int)3293799729) /* OtherShape (P_OTHERSHAPE) */);
            public bool Enabled => EventData.get_bool (unchecked((int)3351342625) /* Enabled (P_ENABLED) */);
        } /* struct NodeUpdateContact2DEventArgs */

} /* namespace */

namespace Urho {
        public partial struct NodeBeginContact2DEventArgs {
            public EventDataContainer EventData;
            public RigidBody2D Body => EventData.get_RigidBody2D (unchecked((int)111721250) /* Body (P_BODY) */);
            public Node OtherNode => EventData.get_Node (unchecked((int)2707292594) /* OtherNode (P_OTHERNODE) */);
            public RigidBody2D OtherBody => EventData.get_RigidBody2D (unchecked((int)1930180818) /* OtherBody (P_OTHERBODY) */);
            public CollisionData [] Contacts => EventData.get_CollisionData (unchecked((int)216739987) /* Contacts (P_CONTACTS) */);
            public CollisionShape2D Shape => EventData.get_CollisionShape2D (unchecked((int)2586277601) /* Shape (P_SHAPE) */);
            public CollisionShape2D OtherShape => EventData.get_CollisionShape2D (unchecked((int)3293799729) /* OtherShape (P_OTHERSHAPE) */);
        } /* struct NodeBeginContact2DEventArgs */

} /* namespace */

namespace Urho {
        public partial struct NodeEndContact2DEventArgs {
            public EventDataContainer EventData;
            public RigidBody2D Body => EventData.get_RigidBody2D (unchecked((int)111721250) /* Body (P_BODY) */);
            public Node OtherNode => EventData.get_Node (unchecked((int)2707292594) /* OtherNode (P_OTHERNODE) */);
            public RigidBody2D OtherBody => EventData.get_RigidBody2D (unchecked((int)1930180818) /* OtherBody (P_OTHERBODY) */);
            public CollisionData [] Contacts => EventData.get_CollisionData (unchecked((int)216739987) /* Contacts (P_CONTACTS) */);
            public CollisionShape2D Shape => EventData.get_CollisionShape2D (unchecked((int)2586277601) /* Shape (P_SHAPE) */);
            public CollisionShape2D OtherShape => EventData.get_CollisionShape2D (unchecked((int)3293799729) /* OtherShape (P_OTHERSHAPE) */);
        } /* struct NodeEndContact2DEventArgs */

} /* namespace */

namespace Urho {
        public partial struct ParticlesEndEventArgs {
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public ParticleEffect2D Effect => EventData.get_ParticleEffect2D (unchecked((int)2340854545) /* Effect (P_EFFECT) */);
        } /* struct ParticlesEndEventArgs */

} /* namespace */

namespace Urho {
        public partial struct ParticlesDurationEventArgs {
            public EventDataContainer EventData;
            public Node Node => EventData.get_Node (unchecked((int)888833026) /* Node (P_NODE) */);
            public ParticleEffect2D Effect => EventData.get_ParticleEffect2D (unchecked((int)2340854545) /* Effect (P_EFFECT) */);
        } /* struct ParticlesDurationEventArgs */

} /* namespace */

#pragma warning restore CS0618, CS0649