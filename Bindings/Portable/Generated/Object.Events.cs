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
namespace Urho {

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]	public delegate void ObjectCallbackSignature (IntPtr data, int stringhash, IntPtr variantMap);
}

namespace Urho {
        public partial struct SoundFinishedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public SoundSource SoundSource => UrhoMap.get_SoundSource (handle, UrhoHash.P_SOUNDSOURCE);
            public Sound Sound => UrhoMap.get_Sound (handle, UrhoHash.P_SOUND);
        } /* struct SoundFinishedEventArgs */

} /* namespace */

namespace Urho {
        public partial struct FrameStartedEventArgs {
            internal IntPtr handle;
            public uint FrameNumber => UrhoMap.get_uint (handle, UrhoHash.P_FRAMENUMBER);
            public float TimeStep => UrhoMap.get_float (handle, UrhoHash.P_TIMESTEP);
        } /* struct FrameStartedEventArgs */

        public partial class Time {
             ObjectCallbackSignature callbackFrameStarted;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_FrameStarted (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToFrameStarted (Action<FrameStartedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FrameStartedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackFrameStarted = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_FrameStarted (handle, callbackFrameStarted, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<FrameStartedEventArgs> eventAdapterForFrameStarted;
             public event Action<FrameStartedEventArgs> FrameStarted
             {
                 add
                 {
                      if (eventAdapterForFrameStarted == null)
                          eventAdapterForFrameStarted = new UrhoEventAdapter<FrameStartedEventArgs>();
                      eventAdapterForFrameStarted.AddManagedSubscriber(handle, value, SubscribeToFrameStarted);
                 }
                 remove { eventAdapterForFrameStarted.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Time */ 

} /* namespace */

namespace Urho {
        public partial struct UpdateEventArgs {
            internal IntPtr handle;
            public float TimeStep => UrhoMap.get_float (handle, UrhoHash.P_TIMESTEP);
        } /* struct UpdateEventArgs */

        public partial class Engine {
             ObjectCallbackSignature callbackUpdate;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_Update (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             internal Subscription SubscribeToUpdate (Action<UpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackUpdate = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_Update (handle, callbackUpdate, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<UpdateEventArgs> eventAdapterForUpdate;
             public event Action<UpdateEventArgs> Update
             {
                 add
                 {
                      if (eventAdapterForUpdate == null)
                          eventAdapterForUpdate = new UrhoEventAdapter<UpdateEventArgs>();
                      eventAdapterForUpdate.AddManagedSubscriber(handle, value, SubscribeToUpdate);
                 }
                 remove { eventAdapterForUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Engine */ 

} /* namespace */

namespace Urho {
        public partial struct PostUpdateEventArgs {
            internal IntPtr handle;
            public float TimeStep => UrhoMap.get_float (handle, UrhoHash.P_TIMESTEP);
        } /* struct PostUpdateEventArgs */

        public partial class Engine {
             ObjectCallbackSignature callbackPostUpdate;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_PostUpdate (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToPostUpdate (Action<PostUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PostUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackPostUpdate = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_PostUpdate (handle, callbackPostUpdate, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<PostUpdateEventArgs> eventAdapterForPostUpdate;
             public event Action<PostUpdateEventArgs> PostUpdate
             {
                 add
                 {
                      if (eventAdapterForPostUpdate == null)
                          eventAdapterForPostUpdate = new UrhoEventAdapter<PostUpdateEventArgs>();
                      eventAdapterForPostUpdate.AddManagedSubscriber(handle, value, SubscribeToPostUpdate);
                 }
                 remove { eventAdapterForPostUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Engine */ 

} /* namespace */

namespace Urho {
        public partial struct RenderUpdateEventArgs {
            internal IntPtr handle;
            public float TimeStep => UrhoMap.get_float (handle, UrhoHash.P_TIMESTEP);
        } /* struct RenderUpdateEventArgs */

        public partial class Engine {
             ObjectCallbackSignature callbackRenderUpdate;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_RenderUpdate (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToRenderUpdate (Action<RenderUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new RenderUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackRenderUpdate = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_RenderUpdate (handle, callbackRenderUpdate, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<RenderUpdateEventArgs> eventAdapterForRenderUpdate;
             public event Action<RenderUpdateEventArgs> RenderUpdate
             {
                 add
                 {
                      if (eventAdapterForRenderUpdate == null)
                          eventAdapterForRenderUpdate = new UrhoEventAdapter<RenderUpdateEventArgs>();
                      eventAdapterForRenderUpdate.AddManagedSubscriber(handle, value, SubscribeToRenderUpdate);
                 }
                 remove { eventAdapterForRenderUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Engine */ 

} /* namespace */

namespace Urho {
        public partial struct PostRenderUpdateEventArgs {
            internal IntPtr handle;
            public float TimeStep => UrhoMap.get_float (handle, UrhoHash.P_TIMESTEP);
        } /* struct PostRenderUpdateEventArgs */

        public partial class Engine {
             ObjectCallbackSignature callbackPostRenderUpdate;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_PostRenderUpdate (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToPostRenderUpdate (Action<PostRenderUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PostRenderUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackPostRenderUpdate = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_PostRenderUpdate (handle, callbackPostRenderUpdate, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<PostRenderUpdateEventArgs> eventAdapterForPostRenderUpdate;
             public event Action<PostRenderUpdateEventArgs> PostRenderUpdate
             {
                 add
                 {
                      if (eventAdapterForPostRenderUpdate == null)
                          eventAdapterForPostRenderUpdate = new UrhoEventAdapter<PostRenderUpdateEventArgs>();
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
             ObjectCallbackSignature callbackFrameEnded;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_FrameEnded (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToFrameEnded (Action<FrameEndedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FrameEndedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackFrameEnded = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_FrameEnded (handle, callbackFrameEnded, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<FrameEndedEventArgs> eventAdapterForFrameEnded;
             public event Action<FrameEndedEventArgs> FrameEnded
             {
                 add
                 {
                      if (eventAdapterForFrameEnded == null)
                          eventAdapterForFrameEnded = new UrhoEventAdapter<FrameEndedEventArgs>();
                      eventAdapterForFrameEnded.AddManagedSubscriber(handle, value, SubscribeToFrameEnded);
                 }
                 remove { eventAdapterForFrameEnded.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Time */ 

} /* namespace */

namespace Urho {
        public partial struct WorkItemCompletedEventArgs {
            internal IntPtr handle;
            public WorkItem Item => UrhoMap.get_WorkItem (handle, UrhoHash.P_ITEM);
        } /* struct WorkItemCompletedEventArgs */

        public partial class WorkQueue {
             ObjectCallbackSignature callbackWorkItemCompleted;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_WorkItemCompleted (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToWorkItemCompleted (Action<WorkItemCompletedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new WorkItemCompletedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackWorkItemCompleted = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_WorkItemCompleted (handle, callbackWorkItemCompleted, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<WorkItemCompletedEventArgs> eventAdapterForWorkItemCompleted;
             public event Action<WorkItemCompletedEventArgs> WorkItemCompleted
             {
                 add
                 {
                      if (eventAdapterForWorkItemCompleted == null)
                          eventAdapterForWorkItemCompleted = new UrhoEventAdapter<WorkItemCompletedEventArgs>();
                      eventAdapterForWorkItemCompleted.AddManagedSubscriber(handle, value, SubscribeToWorkItemCompleted);
                 }
                 remove { eventAdapterForWorkItemCompleted.RemoveManagedSubscriber(handle, value); }
             }
        } /* class WorkQueue */ 

} /* namespace */

namespace Urho {
        public partial struct ConsoleCommandEventArgs {
            internal IntPtr handle;
            public String Command => UrhoMap.get_String (handle, UrhoHash.P_COMMAND);
            public String Id => UrhoMap.get_String (handle, UrhoHash.P_ID);
        } /* struct ConsoleCommandEventArgs */

        public partial class UrhoConsole {
             ObjectCallbackSignature callbackConsoleCommand;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ConsoleCommand (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToConsoleCommand (Action<ConsoleCommandEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ConsoleCommandEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackConsoleCommand = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ConsoleCommand (handle, callbackConsoleCommand, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ConsoleCommandEventArgs> eventAdapterForConsoleCommand;
             public event Action<ConsoleCommandEventArgs> ConsoleCommand
             {
                 add
                 {
                      if (eventAdapterForConsoleCommand == null)
                          eventAdapterForConsoleCommand = new UrhoEventAdapter<ConsoleCommandEventArgs>();
                      eventAdapterForConsoleCommand.AddManagedSubscriber(handle, value, SubscribeToConsoleCommand);
                 }
                 remove { eventAdapterForConsoleCommand.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UrhoConsole */ 

} /* namespace */

namespace Urho {
        public partial struct BoneHierarchyCreatedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
        } /* struct BoneHierarchyCreatedEventArgs */

        public partial class Node {
             ObjectCallbackSignature callbackBoneHierarchyCreated;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_BoneHierarchyCreated (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToBoneHierarchyCreated (Action<BoneHierarchyCreatedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new BoneHierarchyCreatedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackBoneHierarchyCreated = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_BoneHierarchyCreated (handle, callbackBoneHierarchyCreated, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<BoneHierarchyCreatedEventArgs> eventAdapterForBoneHierarchyCreated;
             public event Action<BoneHierarchyCreatedEventArgs> BoneHierarchyCreated
             {
                 add
                 {
                      if (eventAdapterForBoneHierarchyCreated == null)
                          eventAdapterForBoneHierarchyCreated = new UrhoEventAdapter<BoneHierarchyCreatedEventArgs>();
                      eventAdapterForBoneHierarchyCreated.AddManagedSubscriber(handle, value, SubscribeToBoneHierarchyCreated);
                 }
                 remove { eventAdapterForBoneHierarchyCreated.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Node */ 

} /* namespace */

namespace Urho {
        public partial struct AnimationTriggerEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public Animation Animation => UrhoMap.get_Animation (handle, UrhoHash.P_ANIMATION);
            public String Name => UrhoMap.get_String (handle, UrhoHash.P_NAME);
            public float Time => UrhoMap.get_float (handle, UrhoHash.P_TIME);
            public IntPtr Data => UrhoMap.get_IntPtr (handle, UrhoHash.P_DATA);
        } /* struct AnimationTriggerEventArgs */

        public partial class Node {
             ObjectCallbackSignature callbackAnimationTrigger;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_AnimationTrigger (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToAnimationTrigger (Action<AnimationTriggerEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AnimationTriggerEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackAnimationTrigger = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_AnimationTrigger (handle, callbackAnimationTrigger, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<AnimationTriggerEventArgs> eventAdapterForAnimationTrigger;
             public event Action<AnimationTriggerEventArgs> AnimationTrigger
             {
                 add
                 {
                      if (eventAdapterForAnimationTrigger == null)
                          eventAdapterForAnimationTrigger = new UrhoEventAdapter<AnimationTriggerEventArgs>();
                      eventAdapterForAnimationTrigger.AddManagedSubscriber(handle, value, SubscribeToAnimationTrigger);
                 }
                 remove { eventAdapterForAnimationTrigger.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Node */ 

} /* namespace */

namespace Urho {
        public partial struct AnimationFinishedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public Animation Animation => UrhoMap.get_Animation (handle, UrhoHash.P_ANIMATION);
            public String Name => UrhoMap.get_String (handle, UrhoHash.P_NAME);
            public bool Looped => UrhoMap.get_bool (handle, UrhoHash.P_LOOPED);
        } /* struct AnimationFinishedEventArgs */

} /* namespace */

namespace Urho {
        public partial struct ParticleEffectFinishedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public ParticleEffect Effect => UrhoMap.get_ParticleEffect (handle, UrhoHash.P_EFFECT);
        } /* struct ParticleEffectFinishedEventArgs */

} /* namespace */

namespace Urho {
        public partial struct TerrainCreatedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
        } /* struct TerrainCreatedEventArgs */

        public partial class Terrain {
             ObjectCallbackSignature callbackTerrainCreated;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_TerrainCreated (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToTerrainCreated (Action<TerrainCreatedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TerrainCreatedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackTerrainCreated = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_TerrainCreated (handle, callbackTerrainCreated, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<TerrainCreatedEventArgs> eventAdapterForTerrainCreated;
             public event Action<TerrainCreatedEventArgs> TerrainCreated
             {
                 add
                 {
                      if (eventAdapterForTerrainCreated == null)
                          eventAdapterForTerrainCreated = new UrhoEventAdapter<TerrainCreatedEventArgs>();
                      eventAdapterForTerrainCreated.AddManagedSubscriber(handle, value, SubscribeToTerrainCreated);
                 }
                 remove { eventAdapterForTerrainCreated.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Terrain */ 

} /* namespace */

namespace Urho {
        public partial struct ScreenModeEventArgs {
            internal IntPtr handle;
            public int Width => UrhoMap.get_int (handle, UrhoHash.P_WIDTH);
            public int Height => UrhoMap.get_int (handle, UrhoHash.P_HEIGHT);
            public bool Fullscreen => UrhoMap.get_bool (handle, UrhoHash.P_FULLSCREEN);
            public bool Borderless => UrhoMap.get_bool (handle, UrhoHash.P_BORDERLESS);
            public bool Resizable => UrhoMap.get_bool (handle, UrhoHash.P_RESIZABLE);
            public bool HighDPI => UrhoMap.get_bool (handle, UrhoHash.P_HIGHDPI);
        } /* struct ScreenModeEventArgs */

} /* namespace */

namespace Urho {
        public partial struct WindowPosEventArgs {
            internal IntPtr handle;
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
        } /* struct WindowPosEventArgs */

} /* namespace */

namespace Urho {
        public partial struct RenderSurfaceUpdateEventArgs {
            internal IntPtr handle;
        } /* struct RenderSurfaceUpdateEventArgs */

        public partial class Renderer {
             ObjectCallbackSignature callbackRenderSurfaceUpdate;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_RenderSurfaceUpdate (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToRenderSurfaceUpdate (Action<RenderSurfaceUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new RenderSurfaceUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackRenderSurfaceUpdate = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_RenderSurfaceUpdate (handle, callbackRenderSurfaceUpdate, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<RenderSurfaceUpdateEventArgs> eventAdapterForRenderSurfaceUpdate;
             public event Action<RenderSurfaceUpdateEventArgs> RenderSurfaceUpdate
             {
                 add
                 {
                      if (eventAdapterForRenderSurfaceUpdate == null)
                          eventAdapterForRenderSurfaceUpdate = new UrhoEventAdapter<RenderSurfaceUpdateEventArgs>();
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
            public View View => UrhoMap.get_View (handle, UrhoHash.P_VIEW);
            public Texture Texture => UrhoMap.get_Texture (handle, UrhoHash.P_TEXTURE);
            public RenderSurface Surface => UrhoMap.get_RenderSurface (handle, UrhoHash.P_SURFACE);
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public Camera Camera => UrhoMap.get_Camera (handle, UrhoHash.P_CAMERA);
        } /* struct BeginViewUpdateEventArgs */

        public partial class View {
             ObjectCallbackSignature callbackBeginViewUpdate;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_BeginViewUpdate (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToBeginViewUpdate (Action<BeginViewUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new BeginViewUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackBeginViewUpdate = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_BeginViewUpdate (handle, callbackBeginViewUpdate, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<BeginViewUpdateEventArgs> eventAdapterForBeginViewUpdate;
             public event Action<BeginViewUpdateEventArgs> BeginViewUpdate
             {
                 add
                 {
                      if (eventAdapterForBeginViewUpdate == null)
                          eventAdapterForBeginViewUpdate = new UrhoEventAdapter<BeginViewUpdateEventArgs>();
                      eventAdapterForBeginViewUpdate.AddManagedSubscriber(handle, value, SubscribeToBeginViewUpdate);
                 }
                 remove { eventAdapterForBeginViewUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class View */ 

} /* namespace */

namespace Urho {
        public partial struct EndViewUpdateEventArgs {
            internal IntPtr handle;
            public View View => UrhoMap.get_View (handle, UrhoHash.P_VIEW);
            public Texture Texture => UrhoMap.get_Texture (handle, UrhoHash.P_TEXTURE);
            public RenderSurface Surface => UrhoMap.get_RenderSurface (handle, UrhoHash.P_SURFACE);
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public Camera Camera => UrhoMap.get_Camera (handle, UrhoHash.P_CAMERA);
        } /* struct EndViewUpdateEventArgs */

        public partial class View {
             ObjectCallbackSignature callbackEndViewUpdate;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_EndViewUpdate (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToEndViewUpdate (Action<EndViewUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new EndViewUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackEndViewUpdate = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_EndViewUpdate (handle, callbackEndViewUpdate, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<EndViewUpdateEventArgs> eventAdapterForEndViewUpdate;
             public event Action<EndViewUpdateEventArgs> EndViewUpdate
             {
                 add
                 {
                      if (eventAdapterForEndViewUpdate == null)
                          eventAdapterForEndViewUpdate = new UrhoEventAdapter<EndViewUpdateEventArgs>();
                      eventAdapterForEndViewUpdate.AddManagedSubscriber(handle, value, SubscribeToEndViewUpdate);
                 }
                 remove { eventAdapterForEndViewUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class View */ 

} /* namespace */

namespace Urho {
        public partial struct BeginViewRenderEventArgs {
            internal IntPtr handle;
            public View View => UrhoMap.get_View (handle, UrhoHash.P_VIEW);
            public Texture Texture => UrhoMap.get_Texture (handle, UrhoHash.P_TEXTURE);
            public RenderSurface Surface => UrhoMap.get_RenderSurface (handle, UrhoHash.P_SURFACE);
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public Camera Camera => UrhoMap.get_Camera (handle, UrhoHash.P_CAMERA);
        } /* struct BeginViewRenderEventArgs */

        public partial class Renderer {
             ObjectCallbackSignature callbackBeginViewRender;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_BeginViewRender (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToBeginViewRender (Action<BeginViewRenderEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new BeginViewRenderEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackBeginViewRender = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_BeginViewRender (handle, callbackBeginViewRender, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<BeginViewRenderEventArgs> eventAdapterForBeginViewRender;
             public event Action<BeginViewRenderEventArgs> BeginViewRender
             {
                 add
                 {
                      if (eventAdapterForBeginViewRender == null)
                          eventAdapterForBeginViewRender = new UrhoEventAdapter<BeginViewRenderEventArgs>();
                      eventAdapterForBeginViewRender.AddManagedSubscriber(handle, value, SubscribeToBeginViewRender);
                 }
                 remove { eventAdapterForBeginViewRender.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Renderer */ 

} /* namespace */

namespace Urho {
        public partial struct ViewBuffersReadyEventArgs {
            internal IntPtr handle;
            public View View => UrhoMap.get_View (handle, UrhoHash.P_VIEW);
            public Texture Texture => UrhoMap.get_Texture (handle, UrhoHash.P_TEXTURE);
            public RenderSurface Surface => UrhoMap.get_RenderSurface (handle, UrhoHash.P_SURFACE);
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public Camera Camera => UrhoMap.get_Camera (handle, UrhoHash.P_CAMERA);
        } /* struct ViewBuffersReadyEventArgs */

} /* namespace */

namespace Urho {
        public partial struct ViewGlobalShaderParametersEventArgs {
            internal IntPtr handle;
            public View View => UrhoMap.get_View (handle, UrhoHash.P_VIEW);
            public Texture Texture => UrhoMap.get_Texture (handle, UrhoHash.P_TEXTURE);
            public RenderSurface Surface => UrhoMap.get_RenderSurface (handle, UrhoHash.P_SURFACE);
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public Camera Camera => UrhoMap.get_Camera (handle, UrhoHash.P_CAMERA);
        } /* struct ViewGlobalShaderParametersEventArgs */

} /* namespace */

namespace Urho {
        public partial struct EndViewRenderEventArgs {
            internal IntPtr handle;
            public View View => UrhoMap.get_View (handle, UrhoHash.P_VIEW);
            public Texture Texture => UrhoMap.get_Texture (handle, UrhoHash.P_TEXTURE);
            public RenderSurface Surface => UrhoMap.get_RenderSurface (handle, UrhoHash.P_SURFACE);
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public Camera Camera => UrhoMap.get_Camera (handle, UrhoHash.P_CAMERA);
        } /* struct EndViewRenderEventArgs */

        public partial class Renderer {
             ObjectCallbackSignature callbackEndViewRender;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_EndViewRender (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToEndViewRender (Action<EndViewRenderEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new EndViewRenderEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackEndViewRender = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_EndViewRender (handle, callbackEndViewRender, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<EndViewRenderEventArgs> eventAdapterForEndViewRender;
             public event Action<EndViewRenderEventArgs> EndViewRender
             {
                 add
                 {
                      if (eventAdapterForEndViewRender == null)
                          eventAdapterForEndViewRender = new UrhoEventAdapter<EndViewRenderEventArgs>();
                      eventAdapterForEndViewRender.AddManagedSubscriber(handle, value, SubscribeToEndViewRender);
                 }
                 remove { eventAdapterForEndViewRender.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Renderer */ 

} /* namespace */

namespace Urho {
        public partial struct RenderPathEventEventArgs {
            internal IntPtr handle;
            public String Name => UrhoMap.get_String (handle, UrhoHash.P_NAME);
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
            public String Message => UrhoMap.get_String (handle, UrhoHash.P_MESSAGE);
            public int Level => UrhoMap.get_int (handle, UrhoHash.P_LEVEL);
        } /* struct LogMessageEventArgs */

        public partial class Log {
             ObjectCallbackSignature callbackLogMessage;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_LogMessage (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToLogMessage (Action<LogMessageEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new LogMessageEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackLogMessage = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_LogMessage (handle, callbackLogMessage, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<LogMessageEventArgs> eventAdapterForLogMessage;
             public event Action<LogMessageEventArgs> LogMessage
             {
                 add
                 {
                      if (eventAdapterForLogMessage == null)
                          eventAdapterForLogMessage = new UrhoEventAdapter<LogMessageEventArgs>();
                      eventAdapterForLogMessage.AddManagedSubscriber(handle, value, SubscribeToLogMessage);
                 }
                 remove { eventAdapterForLogMessage.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Log */ 

} /* namespace */

namespace Urho.IO {
        public partial struct AsyncExecFinishedEventArgs {
            internal IntPtr handle;
            public uint RequestID => UrhoMap.get_uint (handle, UrhoHash.P_REQUESTID);
            public int ExitCode => UrhoMap.get_int (handle, UrhoHash.P_EXITCODE);
        } /* struct AsyncExecFinishedEventArgs */

        public partial class FileSystem {
             ObjectCallbackSignature callbackAsyncExecFinished;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_AsyncExecFinished (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToAsyncExecFinished (Action<AsyncExecFinishedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AsyncExecFinishedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackAsyncExecFinished = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_AsyncExecFinished (handle, callbackAsyncExecFinished, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<AsyncExecFinishedEventArgs> eventAdapterForAsyncExecFinished;
             public event Action<AsyncExecFinishedEventArgs> AsyncExecFinished
             {
                 add
                 {
                      if (eventAdapterForAsyncExecFinished == null)
                          eventAdapterForAsyncExecFinished = new UrhoEventAdapter<AsyncExecFinishedEventArgs>();
                      eventAdapterForAsyncExecFinished.AddManagedSubscriber(handle, value, SubscribeToAsyncExecFinished);
                 }
                 remove { eventAdapterForAsyncExecFinished.RemoveManagedSubscriber(handle, value); }
             }
        } /* class FileSystem */ 

} /* namespace */

namespace Urho {
        public partial struct MouseButtonDownEventArgs {
            internal IntPtr handle;
            public int Button => UrhoMap.get_int (handle, UrhoHash.P_BUTTON);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int Qualifiers => UrhoMap.get_int (handle, UrhoHash.P_QUALIFIERS);
        } /* struct MouseButtonDownEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackMouseButtonDown;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_MouseButtonDown (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToMouseButtonDown (Action<MouseButtonDownEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseButtonDownEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackMouseButtonDown = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_MouseButtonDown (handle, callbackMouseButtonDown, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<MouseButtonDownEventArgs> eventAdapterForMouseButtonDown;
             public event Action<MouseButtonDownEventArgs> MouseButtonDown
             {
                 add
                 {
                      if (eventAdapterForMouseButtonDown == null)
                          eventAdapterForMouseButtonDown = new UrhoEventAdapter<MouseButtonDownEventArgs>();
                      eventAdapterForMouseButtonDown.AddManagedSubscriber(handle, value, SubscribeToMouseButtonDown);
                 }
                 remove { eventAdapterForMouseButtonDown.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct MouseButtonUpEventArgs {
            internal IntPtr handle;
            public int Button => UrhoMap.get_int (handle, UrhoHash.P_BUTTON);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int Qualifiers => UrhoMap.get_int (handle, UrhoHash.P_QUALIFIERS);
        } /* struct MouseButtonUpEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackMouseButtonUp;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_MouseButtonUp (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToMouseButtonUp (Action<MouseButtonUpEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseButtonUpEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackMouseButtonUp = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_MouseButtonUp (handle, callbackMouseButtonUp, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<MouseButtonUpEventArgs> eventAdapterForMouseButtonUp;
             public event Action<MouseButtonUpEventArgs> MouseButtonUp
             {
                 add
                 {
                      if (eventAdapterForMouseButtonUp == null)
                          eventAdapterForMouseButtonUp = new UrhoEventAdapter<MouseButtonUpEventArgs>();
                      eventAdapterForMouseButtonUp.AddManagedSubscriber(handle, value, SubscribeToMouseButtonUp);
                 }
                 remove { eventAdapterForMouseButtonUp.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct MouseMovedEventArgs {
            internal IntPtr handle;
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
            public int DX => UrhoMap.get_int (handle, UrhoHash.P_DX);
            public int DY => UrhoMap.get_int (handle, UrhoHash.P_DY);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int Qualifiers => UrhoMap.get_int (handle, UrhoHash.P_QUALIFIERS);
        } /* struct MouseMovedEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackMouseMoved;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_MouseMoved (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToMouseMoved (Action<MouseMovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseMovedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackMouseMoved = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_MouseMoved (handle, callbackMouseMoved, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<MouseMovedEventArgs> eventAdapterForMouseMoved;
             public event Action<MouseMovedEventArgs> MouseMoved
             {
                 add
                 {
                      if (eventAdapterForMouseMoved == null)
                          eventAdapterForMouseMoved = new UrhoEventAdapter<MouseMovedEventArgs>();
                      eventAdapterForMouseMoved.AddManagedSubscriber(handle, value, SubscribeToMouseMoved);
                 }
                 remove { eventAdapterForMouseMoved.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct MouseWheelEventArgs {
            internal IntPtr handle;
            public int Wheel => UrhoMap.get_int (handle, UrhoHash.P_WHEEL);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int Qualifiers => UrhoMap.get_int (handle, UrhoHash.P_QUALIFIERS);
        } /* struct MouseWheelEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackMouseWheel;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_MouseWheel (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToMouseWheel (Action<MouseWheelEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseWheelEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackMouseWheel = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_MouseWheel (handle, callbackMouseWheel, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<MouseWheelEventArgs> eventAdapterForMouseWheel;
             public event Action<MouseWheelEventArgs> MouseWheel
             {
                 add
                 {
                      if (eventAdapterForMouseWheel == null)
                          eventAdapterForMouseWheel = new UrhoEventAdapter<MouseWheelEventArgs>();
                      eventAdapterForMouseWheel.AddManagedSubscriber(handle, value, SubscribeToMouseWheel);
                 }
                 remove { eventAdapterForMouseWheel.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct KeyDownEventArgs {
            internal IntPtr handle;
            public Key Key =>(Key) UrhoMap.get_int (handle, UrhoHash.P_KEY);
            public int Scancode => UrhoMap.get_int (handle, UrhoHash.P_SCANCODE);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int Qualifiers => UrhoMap.get_int (handle, UrhoHash.P_QUALIFIERS);
            public bool Repeat => UrhoMap.get_bool (handle, UrhoHash.P_REPEAT);
        } /* struct KeyDownEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackKeyDown;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_KeyDown (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToKeyDown (Action<KeyDownEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new KeyDownEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackKeyDown = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_KeyDown (handle, callbackKeyDown, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<KeyDownEventArgs> eventAdapterForKeyDown;
             public event Action<KeyDownEventArgs> KeyDown
             {
                 add
                 {
                      if (eventAdapterForKeyDown == null)
                          eventAdapterForKeyDown = new UrhoEventAdapter<KeyDownEventArgs>();
                      eventAdapterForKeyDown.AddManagedSubscriber(handle, value, SubscribeToKeyDown);
                 }
                 remove { eventAdapterForKeyDown.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct KeyUpEventArgs {
            internal IntPtr handle;
            public Key Key =>(Key) UrhoMap.get_int (handle, UrhoHash.P_KEY);
            public int Scancode => UrhoMap.get_int (handle, UrhoHash.P_SCANCODE);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int Qualifiers => UrhoMap.get_int (handle, UrhoHash.P_QUALIFIERS);
        } /* struct KeyUpEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackKeyUp;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_KeyUp (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToKeyUp (Action<KeyUpEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new KeyUpEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackKeyUp = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_KeyUp (handle, callbackKeyUp, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<KeyUpEventArgs> eventAdapterForKeyUp;
             public event Action<KeyUpEventArgs> KeyUp
             {
                 add
                 {
                      if (eventAdapterForKeyUp == null)
                          eventAdapterForKeyUp = new UrhoEventAdapter<KeyUpEventArgs>();
                      eventAdapterForKeyUp.AddManagedSubscriber(handle, value, SubscribeToKeyUp);
                 }
                 remove { eventAdapterForKeyUp.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct TextInputEventArgs {
            internal IntPtr handle;
            public String Text => UrhoMap.get_String (handle, UrhoHash.P_TEXT);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int Qualifiers => UrhoMap.get_int (handle, UrhoHash.P_QUALIFIERS);
        } /* struct TextInputEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackTextInput;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_TextInput (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToTextInput (Action<TextInputEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TextInputEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackTextInput = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_TextInput (handle, callbackTextInput, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<TextInputEventArgs> eventAdapterForTextInput;
             public event Action<TextInputEventArgs> TextInput
             {
                 add
                 {
                      if (eventAdapterForTextInput == null)
                          eventAdapterForTextInput = new UrhoEventAdapter<TextInputEventArgs>();
                      eventAdapterForTextInput.AddManagedSubscriber(handle, value, SubscribeToTextInput);
                 }
                 remove { eventAdapterForTextInput.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct JoystickConnectedEventArgs {
            internal IntPtr handle;
            public int JoystickID => UrhoMap.get_int (handle, UrhoHash.P_JOYSTICKID);
        } /* struct JoystickConnectedEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackJoystickConnected;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_JoystickConnected (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToJoystickConnected (Action<JoystickConnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickConnectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackJoystickConnected = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_JoystickConnected (handle, callbackJoystickConnected, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<JoystickConnectedEventArgs> eventAdapterForJoystickConnected;
             public event Action<JoystickConnectedEventArgs> JoystickConnected
             {
                 add
                 {
                      if (eventAdapterForJoystickConnected == null)
                          eventAdapterForJoystickConnected = new UrhoEventAdapter<JoystickConnectedEventArgs>();
                      eventAdapterForJoystickConnected.AddManagedSubscriber(handle, value, SubscribeToJoystickConnected);
                 }
                 remove { eventAdapterForJoystickConnected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct JoystickDisconnectedEventArgs {
            internal IntPtr handle;
            public int JoystickID => UrhoMap.get_int (handle, UrhoHash.P_JOYSTICKID);
        } /* struct JoystickDisconnectedEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackJoystickDisconnected;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_JoystickDisconnected (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToJoystickDisconnected (Action<JoystickDisconnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickDisconnectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackJoystickDisconnected = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_JoystickDisconnected (handle, callbackJoystickDisconnected, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<JoystickDisconnectedEventArgs> eventAdapterForJoystickDisconnected;
             public event Action<JoystickDisconnectedEventArgs> JoystickDisconnected
             {
                 add
                 {
                      if (eventAdapterForJoystickDisconnected == null)
                          eventAdapterForJoystickDisconnected = new UrhoEventAdapter<JoystickDisconnectedEventArgs>();
                      eventAdapterForJoystickDisconnected.AddManagedSubscriber(handle, value, SubscribeToJoystickDisconnected);
                 }
                 remove { eventAdapterForJoystickDisconnected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct JoystickButtonDownEventArgs {
            internal IntPtr handle;
            public int JoystickID => UrhoMap.get_int (handle, UrhoHash.P_JOYSTICKID);
            public int Button => UrhoMap.get_int (handle, UrhoHash.P_BUTTON);
        } /* struct JoystickButtonDownEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackJoystickButtonDown;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_JoystickButtonDown (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToJoystickButtonDown (Action<JoystickButtonDownEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickButtonDownEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackJoystickButtonDown = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_JoystickButtonDown (handle, callbackJoystickButtonDown, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<JoystickButtonDownEventArgs> eventAdapterForJoystickButtonDown;
             public event Action<JoystickButtonDownEventArgs> JoystickButtonDown
             {
                 add
                 {
                      if (eventAdapterForJoystickButtonDown == null)
                          eventAdapterForJoystickButtonDown = new UrhoEventAdapter<JoystickButtonDownEventArgs>();
                      eventAdapterForJoystickButtonDown.AddManagedSubscriber(handle, value, SubscribeToJoystickButtonDown);
                 }
                 remove { eventAdapterForJoystickButtonDown.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct JoystickButtonUpEventArgs {
            internal IntPtr handle;
            public int JoystickID => UrhoMap.get_int (handle, UrhoHash.P_JOYSTICKID);
            public int Button => UrhoMap.get_int (handle, UrhoHash.P_BUTTON);
        } /* struct JoystickButtonUpEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackJoystickButtonUp;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_JoystickButtonUp (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToJoystickButtonUp (Action<JoystickButtonUpEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickButtonUpEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackJoystickButtonUp = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_JoystickButtonUp (handle, callbackJoystickButtonUp, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<JoystickButtonUpEventArgs> eventAdapterForJoystickButtonUp;
             public event Action<JoystickButtonUpEventArgs> JoystickButtonUp
             {
                 add
                 {
                      if (eventAdapterForJoystickButtonUp == null)
                          eventAdapterForJoystickButtonUp = new UrhoEventAdapter<JoystickButtonUpEventArgs>();
                      eventAdapterForJoystickButtonUp.AddManagedSubscriber(handle, value, SubscribeToJoystickButtonUp);
                 }
                 remove { eventAdapterForJoystickButtonUp.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct JoystickAxisMoveEventArgs {
            internal IntPtr handle;
            public int JoystickID => UrhoMap.get_int (handle, UrhoHash.P_JOYSTICKID);
            public int Button => UrhoMap.get_int (handle, UrhoHash.P_AXIS);
            public float Position => UrhoMap.get_float (handle, UrhoHash.P_POSITION);
        } /* struct JoystickAxisMoveEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackJoystickAxisMove;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_JoystickAxisMove (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToJoystickAxisMove (Action<JoystickAxisMoveEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickAxisMoveEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackJoystickAxisMove = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_JoystickAxisMove (handle, callbackJoystickAxisMove, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<JoystickAxisMoveEventArgs> eventAdapterForJoystickAxisMove;
             public event Action<JoystickAxisMoveEventArgs> JoystickAxisMove
             {
                 add
                 {
                      if (eventAdapterForJoystickAxisMove == null)
                          eventAdapterForJoystickAxisMove = new UrhoEventAdapter<JoystickAxisMoveEventArgs>();
                      eventAdapterForJoystickAxisMove.AddManagedSubscriber(handle, value, SubscribeToJoystickAxisMove);
                 }
                 remove { eventAdapterForJoystickAxisMove.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct JoystickHatMoveEventArgs {
            internal IntPtr handle;
            public int JoystickID => UrhoMap.get_int (handle, UrhoHash.P_JOYSTICKID);
            public int Button => UrhoMap.get_int (handle, UrhoHash.P_HAT);
            public int Position => UrhoMap.get_int (handle, UrhoHash.P_POSITION);
        } /* struct JoystickHatMoveEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackJoystickHatMove;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_JoystickHatMove (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToJoystickHatMove (Action<JoystickHatMoveEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new JoystickHatMoveEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackJoystickHatMove = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_JoystickHatMove (handle, callbackJoystickHatMove, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<JoystickHatMoveEventArgs> eventAdapterForJoystickHatMove;
             public event Action<JoystickHatMoveEventArgs> JoystickHatMove
             {
                 add
                 {
                      if (eventAdapterForJoystickHatMove == null)
                          eventAdapterForJoystickHatMove = new UrhoEventAdapter<JoystickHatMoveEventArgs>();
                      eventAdapterForJoystickHatMove.AddManagedSubscriber(handle, value, SubscribeToJoystickHatMove);
                 }
                 remove { eventAdapterForJoystickHatMove.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct TouchBeginEventArgs {
            internal IntPtr handle;
            public int TouchID => UrhoMap.get_int (handle, UrhoHash.P_TOUCHID);
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
            public float Pressure => UrhoMap.get_float (handle, UrhoHash.P_PRESSURE);
        } /* struct TouchBeginEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackTouchBegin;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_TouchBegin (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToTouchBegin (Action<TouchBeginEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TouchBeginEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackTouchBegin = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_TouchBegin (handle, callbackTouchBegin, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<TouchBeginEventArgs> eventAdapterForTouchBegin;
             public event Action<TouchBeginEventArgs> TouchBegin
             {
                 add
                 {
                      if (eventAdapterForTouchBegin == null)
                          eventAdapterForTouchBegin = new UrhoEventAdapter<TouchBeginEventArgs>();
                      eventAdapterForTouchBegin.AddManagedSubscriber(handle, value, SubscribeToTouchBegin);
                 }
                 remove { eventAdapterForTouchBegin.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct TouchEndEventArgs {
            internal IntPtr handle;
            public int TouchID => UrhoMap.get_int (handle, UrhoHash.P_TOUCHID);
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
        } /* struct TouchEndEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackTouchEnd;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_TouchEnd (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToTouchEnd (Action<TouchEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TouchEndEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackTouchEnd = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_TouchEnd (handle, callbackTouchEnd, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<TouchEndEventArgs> eventAdapterForTouchEnd;
             public event Action<TouchEndEventArgs> TouchEnd
             {
                 add
                 {
                      if (eventAdapterForTouchEnd == null)
                          eventAdapterForTouchEnd = new UrhoEventAdapter<TouchEndEventArgs>();
                      eventAdapterForTouchEnd.AddManagedSubscriber(handle, value, SubscribeToTouchEnd);
                 }
                 remove { eventAdapterForTouchEnd.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct TouchMoveEventArgs {
            internal IntPtr handle;
            public int TouchID => UrhoMap.get_int (handle, UrhoHash.P_TOUCHID);
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
            public int DX => UrhoMap.get_int (handle, UrhoHash.P_DX);
            public int DY => UrhoMap.get_int (handle, UrhoHash.P_DY);
            public float Pressure => UrhoMap.get_float (handle, UrhoHash.P_PRESSURE);
        } /* struct TouchMoveEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackTouchMove;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_TouchMove (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToTouchMove (Action<TouchMoveEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TouchMoveEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackTouchMove = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_TouchMove (handle, callbackTouchMove, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<TouchMoveEventArgs> eventAdapterForTouchMove;
             public event Action<TouchMoveEventArgs> TouchMove
             {
                 add
                 {
                      if (eventAdapterForTouchMove == null)
                          eventAdapterForTouchMove = new UrhoEventAdapter<TouchMoveEventArgs>();
                      eventAdapterForTouchMove.AddManagedSubscriber(handle, value, SubscribeToTouchMove);
                 }
                 remove { eventAdapterForTouchMove.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct GestureRecordedEventArgs {
            internal IntPtr handle;
            public uint GestureID => UrhoMap.get_uint (handle, UrhoHash.P_GESTUREID);
        } /* struct GestureRecordedEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackGestureRecorded;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_GestureRecorded (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToGestureRecorded (Action<GestureRecordedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new GestureRecordedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackGestureRecorded = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_GestureRecorded (handle, callbackGestureRecorded, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<GestureRecordedEventArgs> eventAdapterForGestureRecorded;
             public event Action<GestureRecordedEventArgs> GestureRecorded
             {
                 add
                 {
                      if (eventAdapterForGestureRecorded == null)
                          eventAdapterForGestureRecorded = new UrhoEventAdapter<GestureRecordedEventArgs>();
                      eventAdapterForGestureRecorded.AddManagedSubscriber(handle, value, SubscribeToGestureRecorded);
                 }
                 remove { eventAdapterForGestureRecorded.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct GestureInputEventArgs {
            internal IntPtr handle;
            public uint GestureID => UrhoMap.get_uint (handle, UrhoHash.P_GESTUREID);
            public int CenterX => UrhoMap.get_int (handle, UrhoHash.P_CENTERX);
            public int CenterY => UrhoMap.get_int (handle, UrhoHash.P_CENTERY);
            public int NumFingers => UrhoMap.get_int (handle, UrhoHash.P_NUMFINGERS);
            public float Error => UrhoMap.get_float (handle, UrhoHash.P_ERROR);
        } /* struct GestureInputEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackGestureInput;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_GestureInput (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToGestureInput (Action<GestureInputEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new GestureInputEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackGestureInput = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_GestureInput (handle, callbackGestureInput, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<GestureInputEventArgs> eventAdapterForGestureInput;
             public event Action<GestureInputEventArgs> GestureInput
             {
                 add
                 {
                      if (eventAdapterForGestureInput == null)
                          eventAdapterForGestureInput = new UrhoEventAdapter<GestureInputEventArgs>();
                      eventAdapterForGestureInput.AddManagedSubscriber(handle, value, SubscribeToGestureInput);
                 }
                 remove { eventAdapterForGestureInput.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct MultiGestureEventArgs {
            internal IntPtr handle;
            public int CenterX => UrhoMap.get_int (handle, UrhoHash.P_CENTERX);
            public int CenterY => UrhoMap.get_int (handle, UrhoHash.P_CENTERY);
            public int NumFingers => UrhoMap.get_int (handle, UrhoHash.P_NUMFINGERS);
            public float DTheta => UrhoMap.get_float (handle, UrhoHash.P_DTHETA);
            public float DDist => UrhoMap.get_float (handle, UrhoHash.P_DDIST);
        } /* struct MultiGestureEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackMultiGesture;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_MultiGesture (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToMultiGesture (Action<MultiGestureEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MultiGestureEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackMultiGesture = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_MultiGesture (handle, callbackMultiGesture, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<MultiGestureEventArgs> eventAdapterForMultiGesture;
             public event Action<MultiGestureEventArgs> MultiGesture
             {
                 add
                 {
                      if (eventAdapterForMultiGesture == null)
                          eventAdapterForMultiGesture = new UrhoEventAdapter<MultiGestureEventArgs>();
                      eventAdapterForMultiGesture.AddManagedSubscriber(handle, value, SubscribeToMultiGesture);
                 }
                 remove { eventAdapterForMultiGesture.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct DropFileEventArgs {
            internal IntPtr handle;
            public String FileName => UrhoMap.get_String (handle, UrhoHash.P_FILENAME);
        } /* struct DropFileEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackDropFile;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_DropFile (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToDropFile (Action<DropFileEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DropFileEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackDropFile = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_DropFile (handle, callbackDropFile, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<DropFileEventArgs> eventAdapterForDropFile;
             public event Action<DropFileEventArgs> DropFile
             {
                 add
                 {
                      if (eventAdapterForDropFile == null)
                          eventAdapterForDropFile = new UrhoEventAdapter<DropFileEventArgs>();
                      eventAdapterForDropFile.AddManagedSubscriber(handle, value, SubscribeToDropFile);
                 }
                 remove { eventAdapterForDropFile.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct InputFocusEventArgs {
            internal IntPtr handle;
            public bool Focus => UrhoMap.get_bool (handle, UrhoHash.P_FOCUS);
            public bool Minimized => UrhoMap.get_bool (handle, UrhoHash.P_MINIMIZED);
        } /* struct InputFocusEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackInputFocus;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_InputFocus (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToInputFocus (Action<InputFocusEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new InputFocusEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackInputFocus = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_InputFocus (handle, callbackInputFocus, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<InputFocusEventArgs> eventAdapterForInputFocus;
             public event Action<InputFocusEventArgs> InputFocus
             {
                 add
                 {
                      if (eventAdapterForInputFocus == null)
                          eventAdapterForInputFocus = new UrhoEventAdapter<InputFocusEventArgs>();
                      eventAdapterForInputFocus.AddManagedSubscriber(handle, value, SubscribeToInputFocus);
                 }
                 remove { eventAdapterForInputFocus.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct MouseVisibleChangedEventArgs {
            internal IntPtr handle;
            public bool Visible => UrhoMap.get_bool (handle, UrhoHash.P_VISIBLE);
        } /* struct MouseVisibleChangedEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackMouseVisibleChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_MouseVisibleChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToMouseVisibleChanged (Action<MouseVisibleChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseVisibleChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackMouseVisibleChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_MouseVisibleChanged (handle, callbackMouseVisibleChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<MouseVisibleChangedEventArgs> eventAdapterForMouseVisibleChanged;
             public event Action<MouseVisibleChangedEventArgs> MouseVisibleChanged
             {
                 add
                 {
                      if (eventAdapterForMouseVisibleChanged == null)
                          eventAdapterForMouseVisibleChanged = new UrhoEventAdapter<MouseVisibleChangedEventArgs>();
                      eventAdapterForMouseVisibleChanged.AddManagedSubscriber(handle, value, SubscribeToMouseVisibleChanged);
                 }
                 remove { eventAdapterForMouseVisibleChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct MouseModeChangedEventArgs {
            internal IntPtr handle;
            public MouseMode Mode => UrhoMap.get_MouseMode (handle, UrhoHash.P_MODE);
            public bool MouseLocked => UrhoMap.get_bool (handle, UrhoHash.P_MOUSELOCKED);
        } /* struct MouseModeChangedEventArgs */

        public partial class Input {
             ObjectCallbackSignature callbackMouseModeChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_MouseModeChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToMouseModeChanged (Action<MouseModeChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MouseModeChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackMouseModeChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_MouseModeChanged (handle, callbackMouseModeChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<MouseModeChangedEventArgs> eventAdapterForMouseModeChanged;
             public event Action<MouseModeChangedEventArgs> MouseModeChanged
             {
                 add
                 {
                      if (eventAdapterForMouseModeChanged == null)
                          eventAdapterForMouseModeChanged = new UrhoEventAdapter<MouseModeChangedEventArgs>();
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
             ObjectCallbackSignature callbackExitRequested;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ExitRequested (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToExitRequested (Action<ExitRequestedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ExitRequestedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackExitRequested = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ExitRequested (handle, callbackExitRequested, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ExitRequestedEventArgs> eventAdapterForExitRequested;
             public event Action<ExitRequestedEventArgs> ExitRequested
             {
                 add
                 {
                      if (eventAdapterForExitRequested == null)
                          eventAdapterForExitRequested = new UrhoEventAdapter<ExitRequestedEventArgs>();
                      eventAdapterForExitRequested.AddManagedSubscriber(handle, value, SubscribeToExitRequested);
                 }
                 remove { eventAdapterForExitRequested.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Input */ 

} /* namespace */

namespace Urho {
        public partial struct SDLRawInputEventArgs {
            internal IntPtr handle;
            public IntPtr SDLEvent => UrhoMap.get_IntPtr (handle, UrhoHash.P_SDLEVENT);
            public bool Consumed => UrhoMap.get_bool (handle, UrhoHash.P_CONSUMED);
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
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public NavigationMesh Mesh => UrhoMap.get_NavigationMesh (handle, UrhoHash.P_MESH);
        } /* struct NavigationMeshRebuiltEventArgs */

        public partial class NavigationMesh {
             ObjectCallbackSignature callbackNavigationMeshRebuilt;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_NavigationMeshRebuilt (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToNavigationMeshRebuilt (Action<NavigationMeshRebuiltEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NavigationMeshRebuiltEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackNavigationMeshRebuilt = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_NavigationMeshRebuilt (handle, callbackNavigationMeshRebuilt, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<NavigationMeshRebuiltEventArgs> eventAdapterForNavigationMeshRebuilt;
             public event Action<NavigationMeshRebuiltEventArgs> NavigationMeshRebuilt
             {
                 add
                 {
                      if (eventAdapterForNavigationMeshRebuilt == null)
                          eventAdapterForNavigationMeshRebuilt = new UrhoEventAdapter<NavigationMeshRebuiltEventArgs>();
                      eventAdapterForNavigationMeshRebuilt.AddManagedSubscriber(handle, value, SubscribeToNavigationMeshRebuilt);
                 }
                 remove { eventAdapterForNavigationMeshRebuilt.RemoveManagedSubscriber(handle, value); }
             }
        } /* class NavigationMesh */ 

} /* namespace */

namespace Urho.Navigation {
        public partial struct NavigationAreaRebuiltEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public NavigationMesh Mesh => UrhoMap.get_NavigationMesh (handle, UrhoHash.P_MESH);
            public Vector3 BoundsMin => UrhoMap.get_Vector3 (handle, UrhoHash.P_BOUNDSMIN);
            public Vector3 BoundsMax => UrhoMap.get_Vector3 (handle, UrhoHash.P_BOUNDSMAX);
        } /* struct NavigationAreaRebuiltEventArgs */

        public partial class NavigationMesh {
             ObjectCallbackSignature callbackNavigationAreaRebuilt;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_NavigationAreaRebuilt (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToNavigationAreaRebuilt (Action<NavigationAreaRebuiltEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NavigationAreaRebuiltEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackNavigationAreaRebuilt = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_NavigationAreaRebuilt (handle, callbackNavigationAreaRebuilt, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<NavigationAreaRebuiltEventArgs> eventAdapterForNavigationAreaRebuilt;
             public event Action<NavigationAreaRebuiltEventArgs> NavigationAreaRebuilt
             {
                 add
                 {
                      if (eventAdapterForNavigationAreaRebuilt == null)
                          eventAdapterForNavigationAreaRebuilt = new UrhoEventAdapter<NavigationAreaRebuiltEventArgs>();
                      eventAdapterForNavigationAreaRebuilt.AddManagedSubscriber(handle, value, SubscribeToNavigationAreaRebuilt);
                 }
                 remove { eventAdapterForNavigationAreaRebuilt.RemoveManagedSubscriber(handle, value); }
             }
        } /* class NavigationMesh */ 

} /* namespace */

namespace Urho.Navigation {
        public partial struct CrowdAgentFormationEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public CrowdAgent CrowdAgent => UrhoMap.get_CrowdAgent (handle, UrhoHash.P_CROWD_AGENT);
            public uint Index => UrhoMap.get_uint (handle, UrhoHash.P_INDEX);
            public uint Size => UrhoMap.get_uint (handle, UrhoHash.P_SIZE);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, UrhoHash.P_POSITION);
        } /* struct CrowdAgentFormationEventArgs */

} /* namespace */

namespace Urho {
        public partial struct CrowdAgentNodeFormationEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public CrowdAgent CrowdAgent => UrhoMap.get_CrowdAgent (handle, UrhoHash.P_CROWD_AGENT);
            public uint Index => UrhoMap.get_uint (handle, UrhoHash.P_INDEX);
            public uint Size => UrhoMap.get_uint (handle, UrhoHash.P_SIZE);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, UrhoHash.P_POSITION);
        } /* struct CrowdAgentNodeFormationEventArgs */

} /* namespace */

namespace Urho.Navigation {
        public partial struct CrowdAgentRepositionEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public CrowdAgent CrowdAgent => UrhoMap.get_CrowdAgent (handle, UrhoHash.P_CROWD_AGENT);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, UrhoHash.P_POSITION);
            public Vector3 Velocity => UrhoMap.get_Vector3 (handle, UrhoHash.P_VELOCITY);
            public bool Arrived => UrhoMap.get_bool (handle, UrhoHash.P_ARRIVED);
            public float TimeStep => UrhoMap.get_float (handle, UrhoHash.P_TIMESTEP);
        } /* struct CrowdAgentRepositionEventArgs */

        public partial class CrowdManager {
             ObjectCallbackSignature callbackCrowdAgentReposition;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_CrowdAgentReposition (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToCrowdAgentReposition (Action<CrowdAgentRepositionEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new CrowdAgentRepositionEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackCrowdAgentReposition = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_CrowdAgentReposition (handle, callbackCrowdAgentReposition, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<CrowdAgentRepositionEventArgs> eventAdapterForCrowdAgentReposition;
             public event Action<CrowdAgentRepositionEventArgs> CrowdAgentReposition
             {
                 add
                 {
                      if (eventAdapterForCrowdAgentReposition == null)
                          eventAdapterForCrowdAgentReposition = new UrhoEventAdapter<CrowdAgentRepositionEventArgs>();
                      eventAdapterForCrowdAgentReposition.AddManagedSubscriber(handle, value, SubscribeToCrowdAgentReposition);
                 }
                 remove { eventAdapterForCrowdAgentReposition.RemoveManagedSubscriber(handle, value); }
             }
        } /* class CrowdManager */ 

} /* namespace */

namespace Urho {
        public partial struct CrowdAgentNodeRepositionEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public CrowdAgent CrowdAgent => UrhoMap.get_CrowdAgent (handle, UrhoHash.P_CROWD_AGENT);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, UrhoHash.P_POSITION);
            public Vector3 Velocity => UrhoMap.get_Vector3 (handle, UrhoHash.P_VELOCITY);
            public bool Arrived => UrhoMap.get_bool (handle, UrhoHash.P_ARRIVED);
            public float TimeStep => UrhoMap.get_float (handle, UrhoHash.P_TIMESTEP);
        } /* struct CrowdAgentNodeRepositionEventArgs */

} /* namespace */

namespace Urho.Navigation {
        public partial struct CrowdAgentFailureEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public CrowdAgent CrowdAgent => UrhoMap.get_CrowdAgent (handle, UrhoHash.P_CROWD_AGENT);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, UrhoHash.P_POSITION);
            public Vector3 Velocity => UrhoMap.get_Vector3 (handle, UrhoHash.P_VELOCITY);
            public int CrowdAgentState => UrhoMap.get_int (handle, UrhoHash.P_CROWD_AGENT_STATE);
            public int CrowdTargetState => UrhoMap.get_int (handle, UrhoHash.P_CROWD_TARGET_STATE);
        } /* struct CrowdAgentFailureEventArgs */

        public partial class CrowdManager {
             ObjectCallbackSignature callbackCrowdAgentFailure;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_CrowdAgentFailure (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToCrowdAgentFailure (Action<CrowdAgentFailureEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new CrowdAgentFailureEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackCrowdAgentFailure = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_CrowdAgentFailure (handle, callbackCrowdAgentFailure, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<CrowdAgentFailureEventArgs> eventAdapterForCrowdAgentFailure;
             public event Action<CrowdAgentFailureEventArgs> CrowdAgentFailure
             {
                 add
                 {
                      if (eventAdapterForCrowdAgentFailure == null)
                          eventAdapterForCrowdAgentFailure = new UrhoEventAdapter<CrowdAgentFailureEventArgs>();
                      eventAdapterForCrowdAgentFailure.AddManagedSubscriber(handle, value, SubscribeToCrowdAgentFailure);
                 }
                 remove { eventAdapterForCrowdAgentFailure.RemoveManagedSubscriber(handle, value); }
             }
        } /* class CrowdManager */ 

} /* namespace */

namespace Urho {
        public partial struct CrowdAgentNodeFailureEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public CrowdAgent CrowdAgent => UrhoMap.get_CrowdAgent (handle, UrhoHash.P_CROWD_AGENT);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, UrhoHash.P_POSITION);
            public Vector3 Velocity => UrhoMap.get_Vector3 (handle, UrhoHash.P_VELOCITY);
            public int CrowdAgentState => UrhoMap.get_int (handle, UrhoHash.P_CROWD_AGENT_STATE);
            public int CrowdTargetState => UrhoMap.get_int (handle, UrhoHash.P_CROWD_TARGET_STATE);
        } /* struct CrowdAgentNodeFailureEventArgs */

} /* namespace */

namespace Urho.Navigation {
        public partial struct CrowdAgentStateChangedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public CrowdAgent CrowdAgent => UrhoMap.get_CrowdAgent (handle, UrhoHash.P_CROWD_AGENT);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, UrhoHash.P_POSITION);
            public Vector3 Velocity => UrhoMap.get_Vector3 (handle, UrhoHash.P_VELOCITY);
            public int CrowdAgentState => UrhoMap.get_int (handle, UrhoHash.P_CROWD_AGENT_STATE);
            public int CrowdTargetState => UrhoMap.get_int (handle, UrhoHash.P_CROWD_TARGET_STATE);
        } /* struct CrowdAgentStateChangedEventArgs */

        public partial class CrowdManager {
             ObjectCallbackSignature callbackCrowdAgentStateChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_CrowdAgentStateChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToCrowdAgentStateChanged (Action<CrowdAgentStateChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new CrowdAgentStateChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackCrowdAgentStateChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_CrowdAgentStateChanged (handle, callbackCrowdAgentStateChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<CrowdAgentStateChangedEventArgs> eventAdapterForCrowdAgentStateChanged;
             public event Action<CrowdAgentStateChangedEventArgs> CrowdAgentStateChanged
             {
                 add
                 {
                      if (eventAdapterForCrowdAgentStateChanged == null)
                          eventAdapterForCrowdAgentStateChanged = new UrhoEventAdapter<CrowdAgentStateChangedEventArgs>();
                      eventAdapterForCrowdAgentStateChanged.AddManagedSubscriber(handle, value, SubscribeToCrowdAgentStateChanged);
                 }
                 remove { eventAdapterForCrowdAgentStateChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class CrowdManager */ 

} /* namespace */

namespace Urho {
        public partial struct CrowdAgentNodeStateChangedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public CrowdAgent CrowdAgent => UrhoMap.get_CrowdAgent (handle, UrhoHash.P_CROWD_AGENT);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, UrhoHash.P_POSITION);
            public Vector3 Velocity => UrhoMap.get_Vector3 (handle, UrhoHash.P_VELOCITY);
            public int CrowdAgentState => UrhoMap.get_int (handle, UrhoHash.P_CROWD_AGENT_STATE);
            public int CrowdTargetState => UrhoMap.get_int (handle, UrhoHash.P_CROWD_TARGET_STATE);
        } /* struct CrowdAgentNodeStateChangedEventArgs */

} /* namespace */

namespace Urho.Navigation {
        public partial struct NavigationObstacleAddedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public Obstacle Obstacle => UrhoMap.get_Obstacle (handle, UrhoHash.P_OBSTACLE);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, UrhoHash.P_POSITION);
            public float Radius => UrhoMap.get_float (handle, UrhoHash.P_RADIUS);
            public float Height => UrhoMap.get_float (handle, UrhoHash.P_HEIGHT);
        } /* struct NavigationObstacleAddedEventArgs */

        public partial class DynamicNavigationMesh {
             ObjectCallbackSignature callbackNavigationObstacleAdded;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_NavigationObstacleAdded (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToNavigationObstacleAdded (Action<NavigationObstacleAddedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NavigationObstacleAddedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackNavigationObstacleAdded = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_NavigationObstacleAdded (handle, callbackNavigationObstacleAdded, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<NavigationObstacleAddedEventArgs> eventAdapterForNavigationObstacleAdded;
             public event Action<NavigationObstacleAddedEventArgs> NavigationObstacleAdded
             {
                 add
                 {
                      if (eventAdapterForNavigationObstacleAdded == null)
                          eventAdapterForNavigationObstacleAdded = new UrhoEventAdapter<NavigationObstacleAddedEventArgs>();
                      eventAdapterForNavigationObstacleAdded.AddManagedSubscriber(handle, value, SubscribeToNavigationObstacleAdded);
                 }
                 remove { eventAdapterForNavigationObstacleAdded.RemoveManagedSubscriber(handle, value); }
             }
        } /* class DynamicNavigationMesh */ 

} /* namespace */

namespace Urho.Navigation {
        public partial struct NavigationObstacleRemovedEventArgs {
            internal IntPtr handle;
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public Obstacle Obstacle => UrhoMap.get_Obstacle (handle, UrhoHash.P_OBSTACLE);
            public Vector3 Position => UrhoMap.get_Vector3 (handle, UrhoHash.P_POSITION);
            public float Radius => UrhoMap.get_float (handle, UrhoHash.P_RADIUS);
            public float Height => UrhoMap.get_float (handle, UrhoHash.P_HEIGHT);
        } /* struct NavigationObstacleRemovedEventArgs */

        public partial class DynamicNavigationMesh {
             ObjectCallbackSignature callbackNavigationObstacleRemoved;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_NavigationObstacleRemoved (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToNavigationObstacleRemoved (Action<NavigationObstacleRemovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NavigationObstacleRemovedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackNavigationObstacleRemoved = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_NavigationObstacleRemoved (handle, callbackNavigationObstacleRemoved, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<NavigationObstacleRemovedEventArgs> eventAdapterForNavigationObstacleRemoved;
             public event Action<NavigationObstacleRemovedEventArgs> NavigationObstacleRemoved
             {
                 add
                 {
                      if (eventAdapterForNavigationObstacleRemoved == null)
                          eventAdapterForNavigationObstacleRemoved = new UrhoEventAdapter<NavigationObstacleRemovedEventArgs>();
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
             ObjectCallbackSignature callbackServerConnected;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ServerConnected (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToServerConnected (Action<ServerConnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ServerConnectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackServerConnected = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ServerConnected (handle, callbackServerConnected, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ServerConnectedEventArgs> eventAdapterForServerConnected;
             public event Action<ServerConnectedEventArgs> ServerConnected
             {
                 add
                 {
                      if (eventAdapterForServerConnected == null)
                          eventAdapterForServerConnected = new UrhoEventAdapter<ServerConnectedEventArgs>();
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
             ObjectCallbackSignature callbackServerDisconnected;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ServerDisconnected (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToServerDisconnected (Action<ServerDisconnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ServerDisconnectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackServerDisconnected = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ServerDisconnected (handle, callbackServerDisconnected, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ServerDisconnectedEventArgs> eventAdapterForServerDisconnected;
             public event Action<ServerDisconnectedEventArgs> ServerDisconnected
             {
                 add
                 {
                      if (eventAdapterForServerDisconnected == null)
                          eventAdapterForServerDisconnected = new UrhoEventAdapter<ServerDisconnectedEventArgs>();
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
             ObjectCallbackSignature callbackConnectFailed;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ConnectFailed (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToConnectFailed (Action<ConnectFailedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ConnectFailedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackConnectFailed = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ConnectFailed (handle, callbackConnectFailed, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ConnectFailedEventArgs> eventAdapterForConnectFailed;
             public event Action<ConnectFailedEventArgs> ConnectFailed
             {
                 add
                 {
                      if (eventAdapterForConnectFailed == null)
                          eventAdapterForConnectFailed = new UrhoEventAdapter<ConnectFailedEventArgs>();
                      eventAdapterForConnectFailed.AddManagedSubscriber(handle, value, SubscribeToConnectFailed);
                 }
                 remove { eventAdapterForConnectFailed.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Network */ 

} /* namespace */

namespace Urho.Network {
        public partial struct ClientConnectedEventArgs {
            internal IntPtr handle;
            public Connection Connection => UrhoMap.get_Connection (handle, UrhoHash.P_CONNECTION);
        } /* struct ClientConnectedEventArgs */

        public partial class Network {
             ObjectCallbackSignature callbackClientConnected;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ClientConnected (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToClientConnected (Action<ClientConnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ClientConnectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackClientConnected = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ClientConnected (handle, callbackClientConnected, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ClientConnectedEventArgs> eventAdapterForClientConnected;
             public event Action<ClientConnectedEventArgs> ClientConnected
             {
                 add
                 {
                      if (eventAdapterForClientConnected == null)
                          eventAdapterForClientConnected = new UrhoEventAdapter<ClientConnectedEventArgs>();
                      eventAdapterForClientConnected.AddManagedSubscriber(handle, value, SubscribeToClientConnected);
                 }
                 remove { eventAdapterForClientConnected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Network */ 

} /* namespace */

namespace Urho.Network {
        public partial struct ClientDisconnectedEventArgs {
            internal IntPtr handle;
            public Connection Connection => UrhoMap.get_Connection (handle, UrhoHash.P_CONNECTION);
        } /* struct ClientDisconnectedEventArgs */

        public partial class Network {
             ObjectCallbackSignature callbackClientDisconnected;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ClientDisconnected (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToClientDisconnected (Action<ClientDisconnectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ClientDisconnectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackClientDisconnected = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ClientDisconnected (handle, callbackClientDisconnected, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ClientDisconnectedEventArgs> eventAdapterForClientDisconnected;
             public event Action<ClientDisconnectedEventArgs> ClientDisconnected
             {
                 add
                 {
                      if (eventAdapterForClientDisconnected == null)
                          eventAdapterForClientDisconnected = new UrhoEventAdapter<ClientDisconnectedEventArgs>();
                      eventAdapterForClientDisconnected.AddManagedSubscriber(handle, value, SubscribeToClientDisconnected);
                 }
                 remove { eventAdapterForClientDisconnected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Network */ 

} /* namespace */

namespace Urho.Network {
        public partial struct ClientIdentityEventArgs {
            internal IntPtr handle;
            public Connection Connection => UrhoMap.get_Connection (handle, UrhoHash.P_CONNECTION);
            public bool Allow => UrhoMap.get_bool (handle, UrhoHash.P_ALLOW);
        } /* struct ClientIdentityEventArgs */

        public partial class Connection {
             ObjectCallbackSignature callbackClientIdentity;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ClientIdentity (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToClientIdentity (Action<ClientIdentityEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ClientIdentityEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackClientIdentity = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ClientIdentity (handle, callbackClientIdentity, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ClientIdentityEventArgs> eventAdapterForClientIdentity;
             public event Action<ClientIdentityEventArgs> ClientIdentity
             {
                 add
                 {
                      if (eventAdapterForClientIdentity == null)
                          eventAdapterForClientIdentity = new UrhoEventAdapter<ClientIdentityEventArgs>();
                      eventAdapterForClientIdentity.AddManagedSubscriber(handle, value, SubscribeToClientIdentity);
                 }
                 remove { eventAdapterForClientIdentity.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Connection */ 

} /* namespace */

namespace Urho.Network {
        public partial struct ClientSceneLoadedEventArgs {
            internal IntPtr handle;
            public Connection Connection => UrhoMap.get_Connection (handle, UrhoHash.P_CONNECTION);
        } /* struct ClientSceneLoadedEventArgs */

        public partial class Connection {
             ObjectCallbackSignature callbackClientSceneLoaded;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ClientSceneLoaded (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToClientSceneLoaded (Action<ClientSceneLoadedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ClientSceneLoadedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackClientSceneLoaded = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ClientSceneLoaded (handle, callbackClientSceneLoaded, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ClientSceneLoadedEventArgs> eventAdapterForClientSceneLoaded;
             public event Action<ClientSceneLoadedEventArgs> ClientSceneLoaded
             {
                 add
                 {
                      if (eventAdapterForClientSceneLoaded == null)
                          eventAdapterForClientSceneLoaded = new UrhoEventAdapter<ClientSceneLoadedEventArgs>();
                      eventAdapterForClientSceneLoaded.AddManagedSubscriber(handle, value, SubscribeToClientSceneLoaded);
                 }
                 remove { eventAdapterForClientSceneLoaded.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Connection */ 

} /* namespace */

namespace Urho.Network {
        public partial struct NetworkMessageEventArgs {
            internal IntPtr handle;
            public Connection Connection => UrhoMap.get_Connection (handle, UrhoHash.P_CONNECTION);
            public int MessageID => UrhoMap.get_int (handle, UrhoHash.P_MESSAGEID);
            public byte [] Data => UrhoMap.get_Buffer (handle, UrhoHash.P_DATA);
        } /* struct NetworkMessageEventArgs */

        public partial class Network {
             ObjectCallbackSignature callbackNetworkMessage;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_NetworkMessage (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToNetworkMessage (Action<NetworkMessageEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NetworkMessageEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackNetworkMessage = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_NetworkMessage (handle, callbackNetworkMessage, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<NetworkMessageEventArgs> eventAdapterForNetworkMessage;
             public event Action<NetworkMessageEventArgs> NetworkMessage
             {
                 add
                 {
                      if (eventAdapterForNetworkMessage == null)
                          eventAdapterForNetworkMessage = new UrhoEventAdapter<NetworkMessageEventArgs>();
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
             ObjectCallbackSignature callbackNetworkUpdate;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_NetworkUpdate (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToNetworkUpdate (Action<NetworkUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NetworkUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackNetworkUpdate = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_NetworkUpdate (handle, callbackNetworkUpdate, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<NetworkUpdateEventArgs> eventAdapterForNetworkUpdate;
             public event Action<NetworkUpdateEventArgs> NetworkUpdate
             {
                 add
                 {
                      if (eventAdapterForNetworkUpdate == null)
                          eventAdapterForNetworkUpdate = new UrhoEventAdapter<NetworkUpdateEventArgs>();
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
             ObjectCallbackSignature callbackNetworkUpdateSent;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_NetworkUpdateSent (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToNetworkUpdateSent (Action<NetworkUpdateSentEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NetworkUpdateSentEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackNetworkUpdateSent = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_NetworkUpdateSent (handle, callbackNetworkUpdateSent, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<NetworkUpdateSentEventArgs> eventAdapterForNetworkUpdateSent;
             public event Action<NetworkUpdateSentEventArgs> NetworkUpdateSent
             {
                 add
                 {
                      if (eventAdapterForNetworkUpdateSent == null)
                          eventAdapterForNetworkUpdateSent = new UrhoEventAdapter<NetworkUpdateSentEventArgs>();
                      eventAdapterForNetworkUpdateSent.AddManagedSubscriber(handle, value, SubscribeToNetworkUpdateSent);
                 }
                 remove { eventAdapterForNetworkUpdateSent.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Network */ 

} /* namespace */

namespace Urho.Network {
        public partial struct NetworkSceneLoadFailedEventArgs {
            internal IntPtr handle;
            public Connection Connection => UrhoMap.get_Connection (handle, UrhoHash.P_CONNECTION);
        } /* struct NetworkSceneLoadFailedEventArgs */

        public partial class Network {
             ObjectCallbackSignature callbackNetworkSceneLoadFailed;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_NetworkSceneLoadFailed (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToNetworkSceneLoadFailed (Action<NetworkSceneLoadFailedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NetworkSceneLoadFailedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackNetworkSceneLoadFailed = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_NetworkSceneLoadFailed (handle, callbackNetworkSceneLoadFailed, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<NetworkSceneLoadFailedEventArgs> eventAdapterForNetworkSceneLoadFailed;
             public event Action<NetworkSceneLoadFailedEventArgs> NetworkSceneLoadFailed
             {
                 add
                 {
                      if (eventAdapterForNetworkSceneLoadFailed == null)
                          eventAdapterForNetworkSceneLoadFailed = new UrhoEventAdapter<NetworkSceneLoadFailedEventArgs>();
                      eventAdapterForNetworkSceneLoadFailed.AddManagedSubscriber(handle, value, SubscribeToNetworkSceneLoadFailed);
                 }
                 remove { eventAdapterForNetworkSceneLoadFailed.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Network */ 

} /* namespace */

namespace Urho.Network {
        public partial struct RemoteEventDataEventArgs {
            internal IntPtr handle;
            public Connection Connection => UrhoMap.get_Connection (handle, UrhoHash.P_CONNECTION);
        } /* struct RemoteEventDataEventArgs */

} /* namespace */

namespace Urho.Physics {
        public partial struct PhysicsPreStepEventArgs {
            internal IntPtr handle;
            public PhysicsWorld World => UrhoMap.get_PhysicsWorld (handle, UrhoHash.P_WORLD);
            public float TimeStep => UrhoMap.get_float (handle, UrhoHash.P_TIMESTEP);
        } /* struct PhysicsPreStepEventArgs */

        public partial class PhysicsWorld {
             ObjectCallbackSignature callbackPhysicsPreStep;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_PhysicsPreStep (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToPhysicsPreStep (Action<PhysicsPreStepEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsPreStepEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackPhysicsPreStep = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_PhysicsPreStep (handle, callbackPhysicsPreStep, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<PhysicsPreStepEventArgs> eventAdapterForPhysicsPreStep;
             public event Action<PhysicsPreStepEventArgs> PhysicsPreStep
             {
                 add
                 {
                      if (eventAdapterForPhysicsPreStep == null)
                          eventAdapterForPhysicsPreStep = new UrhoEventAdapter<PhysicsPreStepEventArgs>();
                      eventAdapterForPhysicsPreStep.AddManagedSubscriber(handle, value, SubscribeToPhysicsPreStep);
                 }
                 remove { eventAdapterForPhysicsPreStep.RemoveManagedSubscriber(handle, value); }
             }
        } /* class PhysicsWorld */ 

} /* namespace */

namespace Urho.Physics {
        public partial struct PhysicsPostStepEventArgs {
            internal IntPtr handle;
            public PhysicsWorld World => UrhoMap.get_PhysicsWorld (handle, UrhoHash.P_WORLD);
            public float TimeStep => UrhoMap.get_float (handle, UrhoHash.P_TIMESTEP);
        } /* struct PhysicsPostStepEventArgs */

        public partial class PhysicsWorld {
             ObjectCallbackSignature callbackPhysicsPostStep;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_PhysicsPostStep (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToPhysicsPostStep (Action<PhysicsPostStepEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsPostStepEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackPhysicsPostStep = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_PhysicsPostStep (handle, callbackPhysicsPostStep, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<PhysicsPostStepEventArgs> eventAdapterForPhysicsPostStep;
             public event Action<PhysicsPostStepEventArgs> PhysicsPostStep
             {
                 add
                 {
                      if (eventAdapterForPhysicsPostStep == null)
                          eventAdapterForPhysicsPostStep = new UrhoEventAdapter<PhysicsPostStepEventArgs>();
                      eventAdapterForPhysicsPostStep.AddManagedSubscriber(handle, value, SubscribeToPhysicsPostStep);
                 }
                 remove { eventAdapterForPhysicsPostStep.RemoveManagedSubscriber(handle, value); }
             }
        } /* class PhysicsWorld */ 

} /* namespace */

namespace Urho.Physics {
        public partial struct PhysicsCollisionStartEventArgs {
            internal IntPtr handle;
            public PhysicsWorld World => UrhoMap.get_PhysicsWorld (handle, UrhoHash.P_WORLD);
            public Node NodeA => UrhoMap.get_Node (handle, UrhoHash.P_NODEA);
            public Node NodeB => UrhoMap.get_Node (handle, UrhoHash.P_NODEB);
            public RigidBody BodyA => UrhoMap.get_RigidBody (handle, UrhoHash.P_BODYA);
            public RigidBody BodyB => UrhoMap.get_RigidBody (handle, UrhoHash.P_BODYB);
            public bool Trigger => UrhoMap.get_bool (handle, UrhoHash.P_TRIGGER);
            public CollisionData [] Contacts => UrhoMap.get_CollisionData (handle, UrhoHash.P_CONTACTS);
        } /* struct PhysicsCollisionStartEventArgs */

        public partial class PhysicsWorld {
             ObjectCallbackSignature callbackPhysicsCollisionStart;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_PhysicsCollisionStart (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToPhysicsCollisionStart (Action<PhysicsCollisionStartEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsCollisionStartEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackPhysicsCollisionStart = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_PhysicsCollisionStart (handle, callbackPhysicsCollisionStart, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<PhysicsCollisionStartEventArgs> eventAdapterForPhysicsCollisionStart;
             public event Action<PhysicsCollisionStartEventArgs> PhysicsCollisionStart
             {
                 add
                 {
                      if (eventAdapterForPhysicsCollisionStart == null)
                          eventAdapterForPhysicsCollisionStart = new UrhoEventAdapter<PhysicsCollisionStartEventArgs>();
                      eventAdapterForPhysicsCollisionStart.AddManagedSubscriber(handle, value, SubscribeToPhysicsCollisionStart);
                 }
                 remove { eventAdapterForPhysicsCollisionStart.RemoveManagedSubscriber(handle, value); }
             }
        } /* class PhysicsWorld */ 

} /* namespace */

namespace Urho.Physics {
        public partial struct PhysicsCollisionEventArgs {
            internal IntPtr handle;
            public PhysicsWorld World => UrhoMap.get_PhysicsWorld (handle, UrhoHash.P_WORLD);
            public Node NodeA => UrhoMap.get_Node (handle, UrhoHash.P_NODEA);
            public Node NodeB => UrhoMap.get_Node (handle, UrhoHash.P_NODEB);
            public RigidBody BodyA => UrhoMap.get_RigidBody (handle, UrhoHash.P_BODYA);
            public RigidBody BodyB => UrhoMap.get_RigidBody (handle, UrhoHash.P_BODYB);
            public bool Trigger => UrhoMap.get_bool (handle, UrhoHash.P_TRIGGER);
            public CollisionData [] Contacts => UrhoMap.get_CollisionData (handle, UrhoHash.P_CONTACTS);
        } /* struct PhysicsCollisionEventArgs */

        public partial class PhysicsWorld {
             ObjectCallbackSignature callbackPhysicsCollision;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_PhysicsCollision (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToPhysicsCollision (Action<PhysicsCollisionEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsCollisionEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackPhysicsCollision = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_PhysicsCollision (handle, callbackPhysicsCollision, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<PhysicsCollisionEventArgs> eventAdapterForPhysicsCollision;
             public event Action<PhysicsCollisionEventArgs> PhysicsCollision
             {
                 add
                 {
                      if (eventAdapterForPhysicsCollision == null)
                          eventAdapterForPhysicsCollision = new UrhoEventAdapter<PhysicsCollisionEventArgs>();
                      eventAdapterForPhysicsCollision.AddManagedSubscriber(handle, value, SubscribeToPhysicsCollision);
                 }
                 remove { eventAdapterForPhysicsCollision.RemoveManagedSubscriber(handle, value); }
             }
        } /* class PhysicsWorld */ 

} /* namespace */

namespace Urho.Physics {
        public partial struct PhysicsCollisionEndEventArgs {
            internal IntPtr handle;
            public PhysicsWorld World => UrhoMap.get_PhysicsWorld (handle, UrhoHash.P_WORLD);
            public Node NodeA => UrhoMap.get_Node (handle, UrhoHash.P_NODEA);
            public Node NodeB => UrhoMap.get_Node (handle, UrhoHash.P_NODEB);
            public RigidBody BodyA => UrhoMap.get_RigidBody (handle, UrhoHash.P_BODYA);
            public RigidBody BodyB => UrhoMap.get_RigidBody (handle, UrhoHash.P_BODYB);
            public bool Trigger => UrhoMap.get_bool (handle, UrhoHash.P_TRIGGER);
        } /* struct PhysicsCollisionEndEventArgs */

        public partial class PhysicsWorld {
             ObjectCallbackSignature callbackPhysicsCollisionEnd;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_PhysicsCollisionEnd (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToPhysicsCollisionEnd (Action<PhysicsCollisionEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsCollisionEndEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackPhysicsCollisionEnd = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_PhysicsCollisionEnd (handle, callbackPhysicsCollisionEnd, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<PhysicsCollisionEndEventArgs> eventAdapterForPhysicsCollisionEnd;
             public event Action<PhysicsCollisionEndEventArgs> PhysicsCollisionEnd
             {
                 add
                 {
                      if (eventAdapterForPhysicsCollisionEnd == null)
                          eventAdapterForPhysicsCollisionEnd = new UrhoEventAdapter<PhysicsCollisionEndEventArgs>();
                      eventAdapterForPhysicsCollisionEnd.AddManagedSubscriber(handle, value, SubscribeToPhysicsCollisionEnd);
                 }
                 remove { eventAdapterForPhysicsCollisionEnd.RemoveManagedSubscriber(handle, value); }
             }
        } /* class PhysicsWorld */ 

} /* namespace */

namespace Urho {
        public partial struct NodeCollisionStartEventArgs {
            internal IntPtr handle;
            public RigidBody Body => UrhoMap.get_RigidBody (handle, UrhoHash.P_BODY);
            public Node OtherNode => UrhoMap.get_Node (handle, UrhoHash.P_OTHERNODE);
            public RigidBody OtherBody => UrhoMap.get_RigidBody (handle, UrhoHash.P_OTHERBODY);
            public bool Trigger => UrhoMap.get_bool (handle, UrhoHash.P_TRIGGER);
            public CollisionData [] Contacts => UrhoMap.get_CollisionData (handle, UrhoHash.P_CONTACTS);
        } /* struct NodeCollisionStartEventArgs */

        public partial class Node {
             ObjectCallbackSignature callbackNodeCollisionStart;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_NodeCollisionStart (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToNodeCollisionStart (Action<NodeCollisionStartEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeCollisionStartEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackNodeCollisionStart = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_NodeCollisionStart (handle, callbackNodeCollisionStart, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<NodeCollisionStartEventArgs> eventAdapterForNodeCollisionStart;
             public event Action<NodeCollisionStartEventArgs> NodeCollisionStart
             {
                 add
                 {
                      if (eventAdapterForNodeCollisionStart == null)
                          eventAdapterForNodeCollisionStart = new UrhoEventAdapter<NodeCollisionStartEventArgs>();
                      eventAdapterForNodeCollisionStart.AddManagedSubscriber(handle, value, SubscribeToNodeCollisionStart);
                 }
                 remove { eventAdapterForNodeCollisionStart.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Node */ 

} /* namespace */

namespace Urho {
        public partial struct NodeCollisionEventArgs {
            internal IntPtr handle;
            public RigidBody Body => UrhoMap.get_RigidBody (handle, UrhoHash.P_BODY);
            public Node OtherNode => UrhoMap.get_Node (handle, UrhoHash.P_OTHERNODE);
            public RigidBody OtherBody => UrhoMap.get_RigidBody (handle, UrhoHash.P_OTHERBODY);
            public bool Trigger => UrhoMap.get_bool (handle, UrhoHash.P_TRIGGER);
            public CollisionData [] Contacts => UrhoMap.get_CollisionData (handle, UrhoHash.P_CONTACTS);
        } /* struct NodeCollisionEventArgs */

        public partial class Node {
             ObjectCallbackSignature callbackNodeCollision;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_NodeCollision (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToNodeCollision (Action<NodeCollisionEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeCollisionEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackNodeCollision = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_NodeCollision (handle, callbackNodeCollision, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<NodeCollisionEventArgs> eventAdapterForNodeCollision;
             public event Action<NodeCollisionEventArgs> NodeCollision
             {
                 add
                 {
                      if (eventAdapterForNodeCollision == null)
                          eventAdapterForNodeCollision = new UrhoEventAdapter<NodeCollisionEventArgs>();
                      eventAdapterForNodeCollision.AddManagedSubscriber(handle, value, SubscribeToNodeCollision);
                 }
                 remove { eventAdapterForNodeCollision.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Node */ 

} /* namespace */

namespace Urho {
        public partial struct NodeCollisionEndEventArgs {
            internal IntPtr handle;
            public RigidBody Body => UrhoMap.get_RigidBody (handle, UrhoHash.P_BODY);
            public Node OtherNode => UrhoMap.get_Node (handle, UrhoHash.P_OTHERNODE);
            public RigidBody OtherBody => UrhoMap.get_RigidBody (handle, UrhoHash.P_OTHERBODY);
            public bool Trigger => UrhoMap.get_bool (handle, UrhoHash.P_TRIGGER);
        } /* struct NodeCollisionEndEventArgs */

        public partial class Node {
             ObjectCallbackSignature callbackNodeCollisionEnd;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_NodeCollisionEnd (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToNodeCollisionEnd (Action<NodeCollisionEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeCollisionEndEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackNodeCollisionEnd = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_NodeCollisionEnd (handle, callbackNodeCollisionEnd, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<NodeCollisionEndEventArgs> eventAdapterForNodeCollisionEnd;
             public event Action<NodeCollisionEndEventArgs> NodeCollisionEnd
             {
                 add
                 {
                      if (eventAdapterForNodeCollisionEnd == null)
                          eventAdapterForNodeCollisionEnd = new UrhoEventAdapter<NodeCollisionEndEventArgs>();
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
             ObjectCallbackSignature callbackReloadStarted;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ReloadStarted (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToReloadStarted (Action<ReloadStartedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ReloadStartedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackReloadStarted = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ReloadStarted (handle, callbackReloadStarted, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ReloadStartedEventArgs> eventAdapterForReloadStarted;
             public event Action<ReloadStartedEventArgs> ReloadStarted
             {
                 add
                 {
                      if (eventAdapterForReloadStarted == null)
                          eventAdapterForReloadStarted = new UrhoEventAdapter<ReloadStartedEventArgs>();
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
             ObjectCallbackSignature callbackReloadFinished;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ReloadFinished (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToReloadFinished (Action<ReloadFinishedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ReloadFinishedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackReloadFinished = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ReloadFinished (handle, callbackReloadFinished, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ReloadFinishedEventArgs> eventAdapterForReloadFinished;
             public event Action<ReloadFinishedEventArgs> ReloadFinished
             {
                 add
                 {
                      if (eventAdapterForReloadFinished == null)
                          eventAdapterForReloadFinished = new UrhoEventAdapter<ReloadFinishedEventArgs>();
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
             ObjectCallbackSignature callbackReloadFailed;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ReloadFailed (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToReloadFailed (Action<ReloadFailedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ReloadFailedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackReloadFailed = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ReloadFailed (handle, callbackReloadFailed, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ReloadFailedEventArgs> eventAdapterForReloadFailed;
             public event Action<ReloadFailedEventArgs> ReloadFailed
             {
                 add
                 {
                      if (eventAdapterForReloadFailed == null)
                          eventAdapterForReloadFailed = new UrhoEventAdapter<ReloadFailedEventArgs>();
                      eventAdapterForReloadFailed.AddManagedSubscriber(handle, value, SubscribeToReloadFailed);
                 }
                 remove { eventAdapterForReloadFailed.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Resource */ 

} /* namespace */

namespace Urho.Resources {
        public partial struct FileChangedEventArgs {
            internal IntPtr handle;
            public String FileName => UrhoMap.get_String (handle, UrhoHash.P_FILENAME);
            public String ResourceName => UrhoMap.get_String (handle, UrhoHash.P_RESOURCENAME);
        } /* struct FileChangedEventArgs */

        public partial class ResourceCache {
             ObjectCallbackSignature callbackFileChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_FileChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToFileChanged (Action<FileChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FileChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackFileChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_FileChanged (handle, callbackFileChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<FileChangedEventArgs> eventAdapterForFileChanged;
             public event Action<FileChangedEventArgs> FileChanged
             {
                 add
                 {
                      if (eventAdapterForFileChanged == null)
                          eventAdapterForFileChanged = new UrhoEventAdapter<FileChangedEventArgs>();
                      eventAdapterForFileChanged.AddManagedSubscriber(handle, value, SubscribeToFileChanged);
                 }
                 remove { eventAdapterForFileChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ResourceCache */ 

} /* namespace */

namespace Urho.Resources {
        public partial struct LoadFailedEventArgs {
            internal IntPtr handle;
            public String ResourceName => UrhoMap.get_String (handle, UrhoHash.P_RESOURCENAME);
        } /* struct LoadFailedEventArgs */

        public partial class ResourceCache {
             ObjectCallbackSignature callbackLoadFailed;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_LoadFailed (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToLoadFailed (Action<LoadFailedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new LoadFailedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackLoadFailed = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_LoadFailed (handle, callbackLoadFailed, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<LoadFailedEventArgs> eventAdapterForLoadFailed;
             public event Action<LoadFailedEventArgs> LoadFailed
             {
                 add
                 {
                      if (eventAdapterForLoadFailed == null)
                          eventAdapterForLoadFailed = new UrhoEventAdapter<LoadFailedEventArgs>();
                      eventAdapterForLoadFailed.AddManagedSubscriber(handle, value, SubscribeToLoadFailed);
                 }
                 remove { eventAdapterForLoadFailed.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ResourceCache */ 

} /* namespace */

namespace Urho.Resources {
        public partial struct ResourceNotFoundEventArgs {
            internal IntPtr handle;
            public String ResourceName => UrhoMap.get_String (handle, UrhoHash.P_RESOURCENAME);
        } /* struct ResourceNotFoundEventArgs */

        public partial class ResourceCache {
             ObjectCallbackSignature callbackResourceNotFound;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ResourceNotFound (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToResourceNotFound (Action<ResourceNotFoundEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ResourceNotFoundEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackResourceNotFound = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ResourceNotFound (handle, callbackResourceNotFound, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ResourceNotFoundEventArgs> eventAdapterForResourceNotFound;
             public event Action<ResourceNotFoundEventArgs> ResourceNotFound
             {
                 add
                 {
                      if (eventAdapterForResourceNotFound == null)
                          eventAdapterForResourceNotFound = new UrhoEventAdapter<ResourceNotFoundEventArgs>();
                      eventAdapterForResourceNotFound.AddManagedSubscriber(handle, value, SubscribeToResourceNotFound);
                 }
                 remove { eventAdapterForResourceNotFound.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ResourceCache */ 

} /* namespace */

namespace Urho.Resources {
        public partial struct UnknownResourceTypeEventArgs {
            internal IntPtr handle;
            public StringHash ResourceType => UrhoMap.get_StringHash (handle, UrhoHash.P_RESOURCETYPE);
        } /* struct UnknownResourceTypeEventArgs */

        public partial class ResourceCache {
             ObjectCallbackSignature callbackUnknownResourceType;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_UnknownResourceType (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToUnknownResourceType (Action<UnknownResourceTypeEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UnknownResourceTypeEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackUnknownResourceType = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_UnknownResourceType (handle, callbackUnknownResourceType, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<UnknownResourceTypeEventArgs> eventAdapterForUnknownResourceType;
             public event Action<UnknownResourceTypeEventArgs> UnknownResourceType
             {
                 add
                 {
                      if (eventAdapterForUnknownResourceType == null)
                          eventAdapterForUnknownResourceType = new UrhoEventAdapter<UnknownResourceTypeEventArgs>();
                      eventAdapterForUnknownResourceType.AddManagedSubscriber(handle, value, SubscribeToUnknownResourceType);
                 }
                 remove { eventAdapterForUnknownResourceType.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ResourceCache */ 

} /* namespace */

namespace Urho.Resources {
        public partial struct ResourceBackgroundLoadedEventArgs {
            internal IntPtr handle;
            public String ResourceName => UrhoMap.get_String (handle, UrhoHash.P_RESOURCENAME);
            public bool Success => UrhoMap.get_bool (handle, UrhoHash.P_SUCCESS);
            public Resource Resource => UrhoMap.get_Resource (handle, UrhoHash.P_RESOURCE);
        } /* struct ResourceBackgroundLoadedEventArgs */

        public partial class ResourceCache {
             ObjectCallbackSignature callbackResourceBackgroundLoaded;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ResourceBackgroundLoaded (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToResourceBackgroundLoaded (Action<ResourceBackgroundLoadedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ResourceBackgroundLoadedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackResourceBackgroundLoaded = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ResourceBackgroundLoaded (handle, callbackResourceBackgroundLoaded, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ResourceBackgroundLoadedEventArgs> eventAdapterForResourceBackgroundLoaded;
             public event Action<ResourceBackgroundLoadedEventArgs> ResourceBackgroundLoaded
             {
                 add
                 {
                      if (eventAdapterForResourceBackgroundLoaded == null)
                          eventAdapterForResourceBackgroundLoaded = new UrhoEventAdapter<ResourceBackgroundLoadedEventArgs>();
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
             ObjectCallbackSignature callbackChangeLanguage;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ChangeLanguage (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToChangeLanguage (Action<ChangeLanguageEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ChangeLanguageEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackChangeLanguage = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ChangeLanguage (handle, callbackChangeLanguage, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ChangeLanguageEventArgs> eventAdapterForChangeLanguage;
             public event Action<ChangeLanguageEventArgs> ChangeLanguage
             {
                 add
                 {
                      if (eventAdapterForChangeLanguage == null)
                          eventAdapterForChangeLanguage = new UrhoEventAdapter<ChangeLanguageEventArgs>();
                      eventAdapterForChangeLanguage.AddManagedSubscriber(handle, value, SubscribeToChangeLanguage);
                 }
                 remove { eventAdapterForChangeLanguage.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Localization */ 

} /* namespace */

namespace Urho {
        public partial struct SceneUpdateEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public float TimeStep => UrhoMap.get_float (handle, UrhoHash.P_TIMESTEP);
        } /* struct SceneUpdateEventArgs */

        public partial class Scene {
             ObjectCallbackSignature callbackSceneUpdate;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_SceneUpdate (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToSceneUpdate (Action<SceneUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SceneUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackSceneUpdate = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_SceneUpdate (handle, callbackSceneUpdate, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<SceneUpdateEventArgs> eventAdapterForSceneUpdate;
             public event Action<SceneUpdateEventArgs> SceneUpdate
             {
                 add
                 {
                      if (eventAdapterForSceneUpdate == null)
                          eventAdapterForSceneUpdate = new UrhoEventAdapter<SceneUpdateEventArgs>();
                      eventAdapterForSceneUpdate.AddManagedSubscriber(handle, value, SubscribeToSceneUpdate);
                 }
                 remove { eventAdapterForSceneUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct SceneSubsystemUpdateEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public float TimeStep => UrhoMap.get_float (handle, UrhoHash.P_TIMESTEP);
        } /* struct SceneSubsystemUpdateEventArgs */

        public partial class Scene {
             ObjectCallbackSignature callbackSceneSubsystemUpdate;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_SceneSubsystemUpdate (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToSceneSubsystemUpdate (Action<SceneSubsystemUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SceneSubsystemUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackSceneSubsystemUpdate = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_SceneSubsystemUpdate (handle, callbackSceneSubsystemUpdate, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<SceneSubsystemUpdateEventArgs> eventAdapterForSceneSubsystemUpdate;
             public event Action<SceneSubsystemUpdateEventArgs> SceneSubsystemUpdate
             {
                 add
                 {
                      if (eventAdapterForSceneSubsystemUpdate == null)
                          eventAdapterForSceneSubsystemUpdate = new UrhoEventAdapter<SceneSubsystemUpdateEventArgs>();
                      eventAdapterForSceneSubsystemUpdate.AddManagedSubscriber(handle, value, SubscribeToSceneSubsystemUpdate);
                 }
                 remove { eventAdapterForSceneSubsystemUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct UpdateSmoothingEventArgs {
            internal IntPtr handle;
            public float Constant => UrhoMap.get_float (handle, UrhoHash.P_CONSTANT);
            public float SquaredSnapThreshold => UrhoMap.get_float (handle, UrhoHash.P_SQUAREDSNAPTHRESHOLD);
        } /* struct UpdateSmoothingEventArgs */

        public partial class Scene {
             ObjectCallbackSignature callbackUpdateSmoothing;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_UpdateSmoothing (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToUpdateSmoothing (Action<UpdateSmoothingEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UpdateSmoothingEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackUpdateSmoothing = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_UpdateSmoothing (handle, callbackUpdateSmoothing, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<UpdateSmoothingEventArgs> eventAdapterForUpdateSmoothing;
             public event Action<UpdateSmoothingEventArgs> UpdateSmoothing
             {
                 add
                 {
                      if (eventAdapterForUpdateSmoothing == null)
                          eventAdapterForUpdateSmoothing = new UrhoEventAdapter<UpdateSmoothingEventArgs>();
                      eventAdapterForUpdateSmoothing.AddManagedSubscriber(handle, value, SubscribeToUpdateSmoothing);
                 }
                 remove { eventAdapterForUpdateSmoothing.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct SceneDrawableUpdateFinishedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public float TimeStep => UrhoMap.get_float (handle, UrhoHash.P_TIMESTEP);
        } /* struct SceneDrawableUpdateFinishedEventArgs */

        public partial class Scene {
             ObjectCallbackSignature callbackSceneDrawableUpdateFinished;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_SceneDrawableUpdateFinished (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToSceneDrawableUpdateFinished (Action<SceneDrawableUpdateFinishedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SceneDrawableUpdateFinishedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackSceneDrawableUpdateFinished = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_SceneDrawableUpdateFinished (handle, callbackSceneDrawableUpdateFinished, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<SceneDrawableUpdateFinishedEventArgs> eventAdapterForSceneDrawableUpdateFinished;
             public event Action<SceneDrawableUpdateFinishedEventArgs> SceneDrawableUpdateFinished
             {
                 add
                 {
                      if (eventAdapterForSceneDrawableUpdateFinished == null)
                          eventAdapterForSceneDrawableUpdateFinished = new UrhoEventAdapter<SceneDrawableUpdateFinishedEventArgs>();
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
             ObjectCallbackSignature callbackTargetPositionChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_TargetPositionChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToTargetPositionChanged (Action<TargetPositionChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TargetPositionChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackTargetPositionChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_TargetPositionChanged (handle, callbackTargetPositionChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<TargetPositionChangedEventArgs> eventAdapterForTargetPositionChanged;
             public event Action<TargetPositionChangedEventArgs> TargetPositionChanged
             {
                 add
                 {
                      if (eventAdapterForTargetPositionChanged == null)
                          eventAdapterForTargetPositionChanged = new UrhoEventAdapter<TargetPositionChangedEventArgs>();
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
             ObjectCallbackSignature callbackTargetRotationChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_TargetRotationChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToTargetRotationChanged (Action<TargetRotationChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TargetRotationChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackTargetRotationChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_TargetRotationChanged (handle, callbackTargetRotationChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<TargetRotationChangedEventArgs> eventAdapterForTargetRotationChanged;
             public event Action<TargetRotationChangedEventArgs> TargetRotationChanged
             {
                 add
                 {
                      if (eventAdapterForTargetRotationChanged == null)
                          eventAdapterForTargetRotationChanged = new UrhoEventAdapter<TargetRotationChangedEventArgs>();
                      eventAdapterForTargetRotationChanged.AddManagedSubscriber(handle, value, SubscribeToTargetRotationChanged);
                 }
                 remove { eventAdapterForTargetRotationChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class SmoothedTransform */ 

} /* namespace */

namespace Urho {
        public partial struct AttributeAnimationUpdateEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public float TimeStep => UrhoMap.get_float (handle, UrhoHash.P_TIMESTEP);
        } /* struct AttributeAnimationUpdateEventArgs */

        public partial class Scene {
             ObjectCallbackSignature callbackAttributeAnimationUpdate;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_AttributeAnimationUpdate (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToAttributeAnimationUpdate (Action<AttributeAnimationUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AttributeAnimationUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackAttributeAnimationUpdate = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_AttributeAnimationUpdate (handle, callbackAttributeAnimationUpdate, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<AttributeAnimationUpdateEventArgs> eventAdapterForAttributeAnimationUpdate;
             public event Action<AttributeAnimationUpdateEventArgs> AttributeAnimationUpdate
             {
                 add
                 {
                      if (eventAdapterForAttributeAnimationUpdate == null)
                          eventAdapterForAttributeAnimationUpdate = new UrhoEventAdapter<AttributeAnimationUpdateEventArgs>();
                      eventAdapterForAttributeAnimationUpdate.AddManagedSubscriber(handle, value, SubscribeToAttributeAnimationUpdate);
                 }
                 remove { eventAdapterForAttributeAnimationUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct AttributeAnimationAddedEventArgs {
            internal IntPtr handle;
            public Object ObjectAnimation => UrhoMap.get_Object (handle, UrhoHash.P_OBJECTANIMATION);
            public String AttributeAnimationName => UrhoMap.get_String (handle, UrhoHash.P_ATTRIBUTEANIMATIONNAME);
        } /* struct AttributeAnimationAddedEventArgs */

        public partial class ObjectAnimation {
             ObjectCallbackSignature callbackAttributeAnimationAdded;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_AttributeAnimationAdded (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToAttributeAnimationAdded (Action<AttributeAnimationAddedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AttributeAnimationAddedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackAttributeAnimationAdded = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_AttributeAnimationAdded (handle, callbackAttributeAnimationAdded, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<AttributeAnimationAddedEventArgs> eventAdapterForAttributeAnimationAdded;
             public event Action<AttributeAnimationAddedEventArgs> AttributeAnimationAdded
             {
                 add
                 {
                      if (eventAdapterForAttributeAnimationAdded == null)
                          eventAdapterForAttributeAnimationAdded = new UrhoEventAdapter<AttributeAnimationAddedEventArgs>();
                      eventAdapterForAttributeAnimationAdded.AddManagedSubscriber(handle, value, SubscribeToAttributeAnimationAdded);
                 }
                 remove { eventAdapterForAttributeAnimationAdded.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ObjectAnimation */ 

} /* namespace */

namespace Urho {
        public partial struct AttributeAnimationRemovedEventArgs {
            internal IntPtr handle;
            public Object ObjectAnimation => UrhoMap.get_Object (handle, UrhoHash.P_OBJECTANIMATION);
            public String AttributeAnimationName => UrhoMap.get_String (handle, UrhoHash.P_ATTRIBUTEANIMATIONNAME);
        } /* struct AttributeAnimationRemovedEventArgs */

        public partial class ObjectAnimation {
             ObjectCallbackSignature callbackAttributeAnimationRemoved;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_AttributeAnimationRemoved (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToAttributeAnimationRemoved (Action<AttributeAnimationRemovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AttributeAnimationRemovedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackAttributeAnimationRemoved = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_AttributeAnimationRemoved (handle, callbackAttributeAnimationRemoved, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<AttributeAnimationRemovedEventArgs> eventAdapterForAttributeAnimationRemoved;
             public event Action<AttributeAnimationRemovedEventArgs> AttributeAnimationRemoved
             {
                 add
                 {
                      if (eventAdapterForAttributeAnimationRemoved == null)
                          eventAdapterForAttributeAnimationRemoved = new UrhoEventAdapter<AttributeAnimationRemovedEventArgs>();
                      eventAdapterForAttributeAnimationRemoved.AddManagedSubscriber(handle, value, SubscribeToAttributeAnimationRemoved);
                 }
                 remove { eventAdapterForAttributeAnimationRemoved.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ObjectAnimation */ 

} /* namespace */

namespace Urho {
        public partial struct ScenePostUpdateEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public float TimeStep => UrhoMap.get_float (handle, UrhoHash.P_TIMESTEP);
        } /* struct ScenePostUpdateEventArgs */

        public partial class Scene {
             ObjectCallbackSignature callbackScenePostUpdate;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ScenePostUpdate (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToScenePostUpdate (Action<ScenePostUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ScenePostUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackScenePostUpdate = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ScenePostUpdate (handle, callbackScenePostUpdate, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ScenePostUpdateEventArgs> eventAdapterForScenePostUpdate;
             public event Action<ScenePostUpdateEventArgs> ScenePostUpdate
             {
                 add
                 {
                      if (eventAdapterForScenePostUpdate == null)
                          eventAdapterForScenePostUpdate = new UrhoEventAdapter<ScenePostUpdateEventArgs>();
                      eventAdapterForScenePostUpdate.AddManagedSubscriber(handle, value, SubscribeToScenePostUpdate);
                 }
                 remove { eventAdapterForScenePostUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct AsyncLoadProgressEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public float Progress => UrhoMap.get_float (handle, UrhoHash.P_PROGRESS);
            public int LoadedNodes => UrhoMap.get_int (handle, UrhoHash.P_LOADEDNODES);
            public int TotalNodes => UrhoMap.get_int (handle, UrhoHash.P_TOTALNODES);
            public int LoadedResources => UrhoMap.get_int (handle, UrhoHash.P_LOADEDRESOURCES);
            public int TotalResources => UrhoMap.get_int (handle, UrhoHash.P_TOTALRESOURCES);
        } /* struct AsyncLoadProgressEventArgs */

        public partial class Scene {
             ObjectCallbackSignature callbackAsyncLoadProgress;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_AsyncLoadProgress (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToAsyncLoadProgress (Action<AsyncLoadProgressEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AsyncLoadProgressEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackAsyncLoadProgress = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_AsyncLoadProgress (handle, callbackAsyncLoadProgress, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<AsyncLoadProgressEventArgs> eventAdapterForAsyncLoadProgress;
             public event Action<AsyncLoadProgressEventArgs> AsyncLoadProgress
             {
                 add
                 {
                      if (eventAdapterForAsyncLoadProgress == null)
                          eventAdapterForAsyncLoadProgress = new UrhoEventAdapter<AsyncLoadProgressEventArgs>();
                      eventAdapterForAsyncLoadProgress.AddManagedSubscriber(handle, value, SubscribeToAsyncLoadProgress);
                 }
                 remove { eventAdapterForAsyncLoadProgress.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct AsyncLoadFinishedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
        } /* struct AsyncLoadFinishedEventArgs */

        public partial class Scene {
             ObjectCallbackSignature callbackAsyncLoadFinished;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_AsyncLoadFinished (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToAsyncLoadFinished (Action<AsyncLoadFinishedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new AsyncLoadFinishedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackAsyncLoadFinished = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_AsyncLoadFinished (handle, callbackAsyncLoadFinished, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<AsyncLoadFinishedEventArgs> eventAdapterForAsyncLoadFinished;
             public event Action<AsyncLoadFinishedEventArgs> AsyncLoadFinished
             {
                 add
                 {
                      if (eventAdapterForAsyncLoadFinished == null)
                          eventAdapterForAsyncLoadFinished = new UrhoEventAdapter<AsyncLoadFinishedEventArgs>();
                      eventAdapterForAsyncLoadFinished.AddManagedSubscriber(handle, value, SubscribeToAsyncLoadFinished);
                 }
                 remove { eventAdapterForAsyncLoadFinished.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct NodeAddedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public Node Parent => UrhoMap.get_Node (handle, UrhoHash.P_PARENT);
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
        } /* struct NodeAddedEventArgs */

        public partial class Scene {
             ObjectCallbackSignature callbackNodeAdded;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_NodeAdded (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToNodeAdded (Action<NodeAddedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeAddedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackNodeAdded = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_NodeAdded (handle, callbackNodeAdded, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<NodeAddedEventArgs> eventAdapterForNodeAdded;
             public event Action<NodeAddedEventArgs> NodeAdded
             {
                 add
                 {
                      if (eventAdapterForNodeAdded == null)
                          eventAdapterForNodeAdded = new UrhoEventAdapter<NodeAddedEventArgs>();
                      eventAdapterForNodeAdded.AddManagedSubscriber(handle, value, SubscribeToNodeAdded);
                 }
                 remove { eventAdapterForNodeAdded.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct NodeRemovedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public Node Parent => UrhoMap.get_Node (handle, UrhoHash.P_PARENT);
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
        } /* struct NodeRemovedEventArgs */

        public partial class Scene {
             ObjectCallbackSignature callbackNodeRemoved;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_NodeRemoved (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToNodeRemoved (Action<NodeRemovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeRemovedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackNodeRemoved = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_NodeRemoved (handle, callbackNodeRemoved, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<NodeRemovedEventArgs> eventAdapterForNodeRemoved;
             public event Action<NodeRemovedEventArgs> NodeRemoved
             {
                 add
                 {
                      if (eventAdapterForNodeRemoved == null)
                          eventAdapterForNodeRemoved = new UrhoEventAdapter<NodeRemovedEventArgs>();
                      eventAdapterForNodeRemoved.AddManagedSubscriber(handle, value, SubscribeToNodeRemoved);
                 }
                 remove { eventAdapterForNodeRemoved.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct ComponentAddedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public Component Component => UrhoMap.get_Component (handle, UrhoHash.P_COMPONENT);
        } /* struct ComponentAddedEventArgs */

        public partial class Scene {
             ObjectCallbackSignature callbackComponentAdded;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ComponentAdded (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToComponentAdded (Action<ComponentAddedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ComponentAddedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackComponentAdded = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ComponentAdded (handle, callbackComponentAdded, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ComponentAddedEventArgs> eventAdapterForComponentAdded;
             public event Action<ComponentAddedEventArgs> ComponentAdded
             {
                 add
                 {
                      if (eventAdapterForComponentAdded == null)
                          eventAdapterForComponentAdded = new UrhoEventAdapter<ComponentAddedEventArgs>();
                      eventAdapterForComponentAdded.AddManagedSubscriber(handle, value, SubscribeToComponentAdded);
                 }
                 remove { eventAdapterForComponentAdded.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct ComponentRemovedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public Component Component => UrhoMap.get_Component (handle, UrhoHash.P_COMPONENT);
        } /* struct ComponentRemovedEventArgs */

        public partial class Scene {
             ObjectCallbackSignature callbackComponentRemoved;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ComponentRemoved (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToComponentRemoved (Action<ComponentRemovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ComponentRemovedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackComponentRemoved = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ComponentRemoved (handle, callbackComponentRemoved, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ComponentRemovedEventArgs> eventAdapterForComponentRemoved;
             public event Action<ComponentRemovedEventArgs> ComponentRemoved
             {
                 add
                 {
                      if (eventAdapterForComponentRemoved == null)
                          eventAdapterForComponentRemoved = new UrhoEventAdapter<ComponentRemovedEventArgs>();
                      eventAdapterForComponentRemoved.AddManagedSubscriber(handle, value, SubscribeToComponentRemoved);
                 }
                 remove { eventAdapterForComponentRemoved.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct NodeNameChangedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
        } /* struct NodeNameChangedEventArgs */

        public partial class Scene {
             ObjectCallbackSignature callbackNodeNameChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_NodeNameChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToNodeNameChanged (Action<NodeNameChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeNameChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackNodeNameChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_NodeNameChanged (handle, callbackNodeNameChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<NodeNameChangedEventArgs> eventAdapterForNodeNameChanged;
             public event Action<NodeNameChangedEventArgs> NodeNameChanged
             {
                 add
                 {
                      if (eventAdapterForNodeNameChanged == null)
                          eventAdapterForNodeNameChanged = new UrhoEventAdapter<NodeNameChangedEventArgs>();
                      eventAdapterForNodeNameChanged.AddManagedSubscriber(handle, value, SubscribeToNodeNameChanged);
                 }
                 remove { eventAdapterForNodeNameChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct NodeEnabledChangedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
        } /* struct NodeEnabledChangedEventArgs */

        public partial class Scene {
             ObjectCallbackSignature callbackNodeEnabledChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_NodeEnabledChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToNodeEnabledChanged (Action<NodeEnabledChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeEnabledChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackNodeEnabledChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_NodeEnabledChanged (handle, callbackNodeEnabledChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<NodeEnabledChangedEventArgs> eventAdapterForNodeEnabledChanged;
             public event Action<NodeEnabledChangedEventArgs> NodeEnabledChanged
             {
                 add
                 {
                      if (eventAdapterForNodeEnabledChanged == null)
                          eventAdapterForNodeEnabledChanged = new UrhoEventAdapter<NodeEnabledChangedEventArgs>();
                      eventAdapterForNodeEnabledChanged.AddManagedSubscriber(handle, value, SubscribeToNodeEnabledChanged);
                 }
                 remove { eventAdapterForNodeEnabledChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct NodeTagAddedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public String Tag => UrhoMap.get_String (handle, UrhoHash.P_TAG);
        } /* struct NodeTagAddedEventArgs */

} /* namespace */

namespace Urho {
        public partial struct NodeTagRemovedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public String Tag => UrhoMap.get_String (handle, UrhoHash.P_TAG);
        } /* struct NodeTagRemovedEventArgs */

} /* namespace */

namespace Urho {
        public partial struct ComponentEnabledChangedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public Component Component => UrhoMap.get_Component (handle, UrhoHash.P_COMPONENT);
        } /* struct ComponentEnabledChangedEventArgs */

        public partial class Scene {
             ObjectCallbackSignature callbackComponentEnabledChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ComponentEnabledChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToComponentEnabledChanged (Action<ComponentEnabledChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ComponentEnabledChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackComponentEnabledChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ComponentEnabledChanged (handle, callbackComponentEnabledChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ComponentEnabledChangedEventArgs> eventAdapterForComponentEnabledChanged;
             public event Action<ComponentEnabledChangedEventArgs> ComponentEnabledChanged
             {
                 add
                 {
                      if (eventAdapterForComponentEnabledChanged == null)
                          eventAdapterForComponentEnabledChanged = new UrhoEventAdapter<ComponentEnabledChangedEventArgs>();
                      eventAdapterForComponentEnabledChanged.AddManagedSubscriber(handle, value, SubscribeToComponentEnabledChanged);
                 }
                 remove { eventAdapterForComponentEnabledChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct TemporaryChangedEventArgs {
            internal IntPtr handle;
            public Serializable Serializable => UrhoMap.get_Serializable (handle, UrhoHash.P_SERIALIZABLE);
        } /* struct TemporaryChangedEventArgs */

        public partial class Serializable {
             ObjectCallbackSignature callbackTemporaryChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_TemporaryChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToTemporaryChanged (Action<TemporaryChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TemporaryChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackTemporaryChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_TemporaryChanged (handle, callbackTemporaryChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<TemporaryChangedEventArgs> eventAdapterForTemporaryChanged;
             public event Action<TemporaryChangedEventArgs> TemporaryChanged
             {
                 add
                 {
                      if (eventAdapterForTemporaryChanged == null)
                          eventAdapterForTemporaryChanged = new UrhoEventAdapter<TemporaryChangedEventArgs>();
                      eventAdapterForTemporaryChanged.AddManagedSubscriber(handle, value, SubscribeToTemporaryChanged);
                 }
                 remove { eventAdapterForTemporaryChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Serializable */ 

} /* namespace */

namespace Urho {
        public partial struct NodeClonedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public Node Node => UrhoMap.get_Node (handle, UrhoHash.P_NODE);
            public Node CloneNode => UrhoMap.get_Node (handle, UrhoHash.P_CLONENODE);
        } /* struct NodeClonedEventArgs */

        public partial class Scene {
             ObjectCallbackSignature callbackNodeCloned;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_NodeCloned (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToNodeCloned (Action<NodeClonedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NodeClonedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackNodeCloned = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_NodeCloned (handle, callbackNodeCloned, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<NodeClonedEventArgs> eventAdapterForNodeCloned;
             public event Action<NodeClonedEventArgs> NodeCloned
             {
                 add
                 {
                      if (eventAdapterForNodeCloned == null)
                          eventAdapterForNodeCloned = new UrhoEventAdapter<NodeClonedEventArgs>();
                      eventAdapterForNodeCloned.AddManagedSubscriber(handle, value, SubscribeToNodeCloned);
                 }
                 remove { eventAdapterForNodeCloned.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct ComponentClonedEventArgs {
            internal IntPtr handle;
            public Scene Scene => UrhoMap.get_Scene (handle, UrhoHash.P_SCENE);
            public Component Component => UrhoMap.get_Component (handle, UrhoHash.P_COMPONENT);
            public Component CloneComponent => UrhoMap.get_Component (handle, UrhoHash.P_CLONECOMPONENT);
        } /* struct ComponentClonedEventArgs */

        public partial class Scene {
             ObjectCallbackSignature callbackComponentCloned;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ComponentCloned (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToComponentCloned (Action<ComponentClonedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ComponentClonedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackComponentCloned = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ComponentCloned (handle, callbackComponentCloned, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ComponentClonedEventArgs> eventAdapterForComponentCloned;
             public event Action<ComponentClonedEventArgs> ComponentCloned
             {
                 add
                 {
                      if (eventAdapterForComponentCloned == null)
                          eventAdapterForComponentCloned = new UrhoEventAdapter<ComponentClonedEventArgs>();
                      eventAdapterForComponentCloned.AddManagedSubscriber(handle, value, SubscribeToComponentCloned);
                 }
                 remove { eventAdapterForComponentCloned.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Scene */ 

} /* namespace */

namespace Urho {
        public partial struct InterceptNetworkUpdateEventArgs {
            internal IntPtr handle;
            public Serializable Serializable => UrhoMap.get_Serializable (handle, UrhoHash.P_SERIALIZABLE);
            public uint TimeStamp => UrhoMap.get_uint (handle, UrhoHash.P_TIMESTAMP);
            public uint Index => UrhoMap.get_uint (handle, UrhoHash.P_INDEX);
            public String Name => UrhoMap.get_String (handle, UrhoHash.P_NAME);
            public Variant Value => UrhoMap.get_Variant (handle, UrhoHash.P_VALUE);
        } /* struct InterceptNetworkUpdateEventArgs */

        public partial class Serializable {
             ObjectCallbackSignature callbackInterceptNetworkUpdate;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_InterceptNetworkUpdate (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToInterceptNetworkUpdate (Action<InterceptNetworkUpdateEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new InterceptNetworkUpdateEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackInterceptNetworkUpdate = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_InterceptNetworkUpdate (handle, callbackInterceptNetworkUpdate, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<InterceptNetworkUpdateEventArgs> eventAdapterForInterceptNetworkUpdate;
             public event Action<InterceptNetworkUpdateEventArgs> InterceptNetworkUpdate
             {
                 add
                 {
                      if (eventAdapterForInterceptNetworkUpdate == null)
                          eventAdapterForInterceptNetworkUpdate = new UrhoEventAdapter<InterceptNetworkUpdateEventArgs>();
                      eventAdapterForInterceptNetworkUpdate.AddManagedSubscriber(handle, value, SubscribeToInterceptNetworkUpdate);
                 }
                 remove { eventAdapterForInterceptNetworkUpdate.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Serializable */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct UIMouseClickEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
            public int Button => UrhoMap.get_int (handle, UrhoHash.P_BUTTON);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int Qualifiers => UrhoMap.get_int (handle, UrhoHash.P_QUALIFIERS);
        } /* struct UIMouseClickEventArgs */

        public partial class UI {
             ObjectCallbackSignature callbackUIMouseClick;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_UIMouseClick (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToUIMouseClick (Action<UIMouseClickEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UIMouseClickEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackUIMouseClick = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_UIMouseClick (handle, callbackUIMouseClick, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<UIMouseClickEventArgs> eventAdapterForUIMouseClick;
             public event Action<UIMouseClickEventArgs> UIMouseClick
             {
                 add
                 {
                      if (eventAdapterForUIMouseClick == null)
                          eventAdapterForUIMouseClick = new UrhoEventAdapter<UIMouseClickEventArgs>();
                      eventAdapterForUIMouseClick.AddManagedSubscriber(handle, value, SubscribeToUIMouseClick);
                 }
                 remove { eventAdapterForUIMouseClick.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UI */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct UIMouseClickEndEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public UIElement BeginElement => UrhoMap.get_UIElement (handle, UrhoHash.P_BEGINELEMENT);
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
            public int Button => UrhoMap.get_int (handle, UrhoHash.P_BUTTON);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int Qualifiers => UrhoMap.get_int (handle, UrhoHash.P_QUALIFIERS);
        } /* struct UIMouseClickEndEventArgs */

        public partial class UI {
             ObjectCallbackSignature callbackUIMouseClickEnd;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_UIMouseClickEnd (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToUIMouseClickEnd (Action<UIMouseClickEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UIMouseClickEndEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackUIMouseClickEnd = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_UIMouseClickEnd (handle, callbackUIMouseClickEnd, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<UIMouseClickEndEventArgs> eventAdapterForUIMouseClickEnd;
             public event Action<UIMouseClickEndEventArgs> UIMouseClickEnd
             {
                 add
                 {
                      if (eventAdapterForUIMouseClickEnd == null)
                          eventAdapterForUIMouseClickEnd = new UrhoEventAdapter<UIMouseClickEndEventArgs>();
                      eventAdapterForUIMouseClickEnd.AddManagedSubscriber(handle, value, SubscribeToUIMouseClickEnd);
                 }
                 remove { eventAdapterForUIMouseClickEnd.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UI */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct UIMouseDoubleClickEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
            public int Button => UrhoMap.get_int (handle, UrhoHash.P_BUTTON);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int Qualifiers => UrhoMap.get_int (handle, UrhoHash.P_QUALIFIERS);
        } /* struct UIMouseDoubleClickEventArgs */

} /* namespace */

namespace Urho {
        public partial struct ClickEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
            public int Button => UrhoMap.get_int (handle, UrhoHash.P_BUTTON);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int Qualifiers => UrhoMap.get_int (handle, UrhoHash.P_QUALIFIERS);
        } /* struct ClickEventArgs */

} /* namespace */

namespace Urho {
        public partial struct ClickEndEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public UIElement BeginElement => UrhoMap.get_UIElement (handle, UrhoHash.P_BEGINELEMENT);
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
            public int Button => UrhoMap.get_int (handle, UrhoHash.P_BUTTON);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int Qualifiers => UrhoMap.get_int (handle, UrhoHash.P_QUALIFIERS);
        } /* struct ClickEndEventArgs */

} /* namespace */

namespace Urho {
        public partial struct DoubleClickEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
            public int Button => UrhoMap.get_int (handle, UrhoHash.P_BUTTON);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int Qualifiers => UrhoMap.get_int (handle, UrhoHash.P_QUALIFIERS);
        } /* struct DoubleClickEventArgs */

} /* namespace */

namespace Urho.Gui {
        public partial struct DragDropTestEventArgs {
            internal IntPtr handle;
            public UIElement Source => UrhoMap.get_UIElement (handle, UrhoHash.P_SOURCE);
            public UIElement Target => UrhoMap.get_UIElement (handle, UrhoHash.P_TARGET);
            public bool Accept => UrhoMap.get_bool (handle, UrhoHash.P_ACCEPT);
        } /* struct DragDropTestEventArgs */

        public partial class UIElement {
             ObjectCallbackSignature callbackDragDropTest;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_DragDropTest (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToDragDropTest (Action<DragDropTestEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragDropTestEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackDragDropTest = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_DragDropTest (handle, callbackDragDropTest, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<DragDropTestEventArgs> eventAdapterForDragDropTest;
             public event Action<DragDropTestEventArgs> DragDropTest
             {
                 add
                 {
                      if (eventAdapterForDragDropTest == null)
                          eventAdapterForDragDropTest = new UrhoEventAdapter<DragDropTestEventArgs>();
                      eventAdapterForDragDropTest.AddManagedSubscriber(handle, value, SubscribeToDragDropTest);
                 }
                 remove { eventAdapterForDragDropTest.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct DragDropFinishEventArgs {
            internal IntPtr handle;
            public UIElement Source => UrhoMap.get_UIElement (handle, UrhoHash.P_SOURCE);
            public UIElement Target => UrhoMap.get_UIElement (handle, UrhoHash.P_TARGET);
            public bool Accept => UrhoMap.get_bool (handle, UrhoHash.P_ACCEPT);
        } /* struct DragDropFinishEventArgs */

        public partial class UIElement {
             ObjectCallbackSignature callbackDragDropFinish;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_DragDropFinish (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToDragDropFinish (Action<DragDropFinishEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragDropFinishEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackDragDropFinish = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_DragDropFinish (handle, callbackDragDropFinish, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<DragDropFinishEventArgs> eventAdapterForDragDropFinish;
             public event Action<DragDropFinishEventArgs> DragDropFinish
             {
                 add
                 {
                      if (eventAdapterForDragDropFinish == null)
                          eventAdapterForDragDropFinish = new UrhoEventAdapter<DragDropFinishEventArgs>();
                      eventAdapterForDragDropFinish.AddManagedSubscriber(handle, value, SubscribeToDragDropFinish);
                 }
                 remove { eventAdapterForDragDropFinish.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct FocusChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public UIElement ClickedElement => UrhoMap.get_UIElement (handle, UrhoHash.P_CLICKEDELEMENT);
        } /* struct FocusChangedEventArgs */

        public partial class UI {
             ObjectCallbackSignature callbackFocusChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_FocusChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToFocusChanged (Action<FocusChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FocusChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackFocusChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_FocusChanged (handle, callbackFocusChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<FocusChangedEventArgs> eventAdapterForFocusChanged;
             public event Action<FocusChangedEventArgs> FocusChanged
             {
                 add
                 {
                      if (eventAdapterForFocusChanged == null)
                          eventAdapterForFocusChanged = new UrhoEventAdapter<FocusChangedEventArgs>();
                      eventAdapterForFocusChanged.AddManagedSubscriber(handle, value, SubscribeToFocusChanged);
                 }
                 remove { eventAdapterForFocusChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UI */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct NameChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
        } /* struct NameChangedEventArgs */

        public partial class UIElement {
             ObjectCallbackSignature callbackNameChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_NameChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToNameChanged (Action<NameChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new NameChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackNameChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_NameChanged (handle, callbackNameChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<NameChangedEventArgs> eventAdapterForNameChanged;
             public event Action<NameChangedEventArgs> NameChanged
             {
                 add
                 {
                      if (eventAdapterForNameChanged == null)
                          eventAdapterForNameChanged = new UrhoEventAdapter<NameChangedEventArgs>();
                      eventAdapterForNameChanged.AddManagedSubscriber(handle, value, SubscribeToNameChanged);
                 }
                 remove { eventAdapterForNameChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ResizedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public int Width => UrhoMap.get_int (handle, UrhoHash.P_WIDTH);
            public int Height => UrhoMap.get_int (handle, UrhoHash.P_HEIGHT);
            public int DX => UrhoMap.get_int (handle, UrhoHash.P_DX);
            public int DY => UrhoMap.get_int (handle, UrhoHash.P_DY);
        } /* struct ResizedEventArgs */

        public partial class UIElement {
             ObjectCallbackSignature callbackResized;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_Resized (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToResized (Action<ResizedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ResizedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackResized = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_Resized (handle, callbackResized, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ResizedEventArgs> eventAdapterForResized;
             public event Action<ResizedEventArgs> Resized
             {
                 add
                 {
                      if (eventAdapterForResized == null)
                          eventAdapterForResized = new UrhoEventAdapter<ResizedEventArgs>();
                      eventAdapterForResized.AddManagedSubscriber(handle, value, SubscribeToResized);
                 }
                 remove { eventAdapterForResized.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct PositionedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
        } /* struct PositionedEventArgs */

        public partial class UIElement {
             ObjectCallbackSignature callbackPositioned;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_Positioned (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToPositioned (Action<PositionedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PositionedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackPositioned = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_Positioned (handle, callbackPositioned, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<PositionedEventArgs> eventAdapterForPositioned;
             public event Action<PositionedEventArgs> Positioned
             {
                 add
                 {
                      if (eventAdapterForPositioned == null)
                          eventAdapterForPositioned = new UrhoEventAdapter<PositionedEventArgs>();
                      eventAdapterForPositioned.AddManagedSubscriber(handle, value, SubscribeToPositioned);
                 }
                 remove { eventAdapterForPositioned.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct VisibleChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public bool Visible => UrhoMap.get_bool (handle, UrhoHash.P_VISIBLE);
        } /* struct VisibleChangedEventArgs */

        public partial class UIElement {
             ObjectCallbackSignature callbackVisibleChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_VisibleChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToVisibleChanged (Action<VisibleChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new VisibleChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackVisibleChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_VisibleChanged (handle, callbackVisibleChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<VisibleChangedEventArgs> eventAdapterForVisibleChanged;
             public event Action<VisibleChangedEventArgs> VisibleChanged
             {
                 add
                 {
                      if (eventAdapterForVisibleChanged == null)
                          eventAdapterForVisibleChanged = new UrhoEventAdapter<VisibleChangedEventArgs>();
                      eventAdapterForVisibleChanged.AddManagedSubscriber(handle, value, SubscribeToVisibleChanged);
                 }
                 remove { eventAdapterForVisibleChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct FocusedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public bool ByKey => UrhoMap.get_bool (handle, UrhoHash.P_BYKEY);
        } /* struct FocusedEventArgs */

        public partial class UIElement {
             ObjectCallbackSignature callbackFocused;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_Focused (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToFocused (Action<FocusedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FocusedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackFocused = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_Focused (handle, callbackFocused, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<FocusedEventArgs> eventAdapterForFocused;
             public event Action<FocusedEventArgs> Focused
             {
                 add
                 {
                      if (eventAdapterForFocused == null)
                          eventAdapterForFocused = new UrhoEventAdapter<FocusedEventArgs>();
                      eventAdapterForFocused.AddManagedSubscriber(handle, value, SubscribeToFocused);
                 }
                 remove { eventAdapterForFocused.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct DefocusedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
        } /* struct DefocusedEventArgs */

        public partial class UIElement {
             ObjectCallbackSignature callbackDefocused;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_Defocused (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToDefocused (Action<DefocusedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DefocusedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackDefocused = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_Defocused (handle, callbackDefocused, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<DefocusedEventArgs> eventAdapterForDefocused;
             public event Action<DefocusedEventArgs> Defocused
             {
                 add
                 {
                      if (eventAdapterForDefocused == null)
                          eventAdapterForDefocused = new UrhoEventAdapter<DefocusedEventArgs>();
                      eventAdapterForDefocused.AddManagedSubscriber(handle, value, SubscribeToDefocused);
                 }
                 remove { eventAdapterForDefocused.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct LayoutUpdatedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
        } /* struct LayoutUpdatedEventArgs */

        public partial class UIElement {
             ObjectCallbackSignature callbackLayoutUpdated;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_LayoutUpdated (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToLayoutUpdated (Action<LayoutUpdatedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new LayoutUpdatedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackLayoutUpdated = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_LayoutUpdated (handle, callbackLayoutUpdated, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<LayoutUpdatedEventArgs> eventAdapterForLayoutUpdated;
             public event Action<LayoutUpdatedEventArgs> LayoutUpdated
             {
                 add
                 {
                      if (eventAdapterForLayoutUpdated == null)
                          eventAdapterForLayoutUpdated = new UrhoEventAdapter<LayoutUpdatedEventArgs>();
                      eventAdapterForLayoutUpdated.AddManagedSubscriber(handle, value, SubscribeToLayoutUpdated);
                 }
                 remove { eventAdapterForLayoutUpdated.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct PressedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
        } /* struct PressedEventArgs */

        public partial class Button {
             ObjectCallbackSignature callbackPressed;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_Pressed (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToPressed (Action<PressedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PressedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackPressed = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_Pressed (handle, callbackPressed, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<PressedEventArgs> eventAdapterForPressed;
             public event Action<PressedEventArgs> Pressed
             {
                 add
                 {
                      if (eventAdapterForPressed == null)
                          eventAdapterForPressed = new UrhoEventAdapter<PressedEventArgs>();
                      eventAdapterForPressed.AddManagedSubscriber(handle, value, SubscribeToPressed);
                 }
                 remove { eventAdapterForPressed.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Button */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ReleasedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
        } /* struct ReleasedEventArgs */

        public partial class Button {
             ObjectCallbackSignature callbackReleased;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_Released (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToReleased (Action<ReleasedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ReleasedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackReleased = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_Released (handle, callbackReleased, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ReleasedEventArgs> eventAdapterForReleased;
             public event Action<ReleasedEventArgs> Released
             {
                 add
                 {
                      if (eventAdapterForReleased == null)
                          eventAdapterForReleased = new UrhoEventAdapter<ReleasedEventArgs>();
                      eventAdapterForReleased.AddManagedSubscriber(handle, value, SubscribeToReleased);
                 }
                 remove { eventAdapterForReleased.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Button */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ToggledEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public bool State => UrhoMap.get_bool (handle, UrhoHash.P_STATE);
        } /* struct ToggledEventArgs */

        public partial class CheckBox {
             ObjectCallbackSignature callbackToggled;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_Toggled (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToToggled (Action<ToggledEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ToggledEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackToggled = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_Toggled (handle, callbackToggled, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ToggledEventArgs> eventAdapterForToggled;
             public event Action<ToggledEventArgs> Toggled
             {
                 add
                 {
                      if (eventAdapterForToggled == null)
                          eventAdapterForToggled = new UrhoEventAdapter<ToggledEventArgs>();
                      eventAdapterForToggled.AddManagedSubscriber(handle, value, SubscribeToToggled);
                 }
                 remove { eventAdapterForToggled.RemoveManagedSubscriber(handle, value); }
             }
        } /* class CheckBox */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct SliderChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public float Value => UrhoMap.get_float (handle, UrhoHash.P_VALUE);
        } /* struct SliderChangedEventArgs */

        public partial class Slider {
             ObjectCallbackSignature callbackSliderChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_SliderChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToSliderChanged (Action<SliderChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SliderChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackSliderChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_SliderChanged (handle, callbackSliderChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<SliderChangedEventArgs> eventAdapterForSliderChanged;
             public event Action<SliderChangedEventArgs> SliderChanged
             {
                 add
                 {
                      if (eventAdapterForSliderChanged == null)
                          eventAdapterForSliderChanged = new UrhoEventAdapter<SliderChangedEventArgs>();
                      eventAdapterForSliderChanged.AddManagedSubscriber(handle, value, SubscribeToSliderChanged);
                 }
                 remove { eventAdapterForSliderChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Slider */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct SliderPagedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public int Offset => UrhoMap.get_int (handle, UrhoHash.P_OFFSET);
            public bool Pressed => UrhoMap.get_bool (handle, UrhoHash.P_PRESSED);
        } /* struct SliderPagedEventArgs */

        public partial class Slider {
             ObjectCallbackSignature callbackSliderPaged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_SliderPaged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToSliderPaged (Action<SliderPagedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SliderPagedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackSliderPaged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_SliderPaged (handle, callbackSliderPaged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<SliderPagedEventArgs> eventAdapterForSliderPaged;
             public event Action<SliderPagedEventArgs> SliderPaged
             {
                 add
                 {
                      if (eventAdapterForSliderPaged == null)
                          eventAdapterForSliderPaged = new UrhoEventAdapter<SliderPagedEventArgs>();
                      eventAdapterForSliderPaged.AddManagedSubscriber(handle, value, SubscribeToSliderPaged);
                 }
                 remove { eventAdapterForSliderPaged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Slider */ 

} /* namespace */

namespace Urho {
        public partial struct ProgressBarChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public float Value => UrhoMap.get_float (handle, UrhoHash.P_VALUE);
        } /* struct ProgressBarChangedEventArgs */

} /* namespace */

namespace Urho.Gui {
        public partial struct ScrollBarChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public float Value => UrhoMap.get_float (handle, UrhoHash.P_VALUE);
        } /* struct ScrollBarChangedEventArgs */

        public partial class ScrollBar {
             ObjectCallbackSignature callbackScrollBarChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ScrollBarChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToScrollBarChanged (Action<ScrollBarChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ScrollBarChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackScrollBarChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ScrollBarChanged (handle, callbackScrollBarChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ScrollBarChangedEventArgs> eventAdapterForScrollBarChanged;
             public event Action<ScrollBarChangedEventArgs> ScrollBarChanged
             {
                 add
                 {
                      if (eventAdapterForScrollBarChanged == null)
                          eventAdapterForScrollBarChanged = new UrhoEventAdapter<ScrollBarChangedEventArgs>();
                      eventAdapterForScrollBarChanged.AddManagedSubscriber(handle, value, SubscribeToScrollBarChanged);
                 }
                 remove { eventAdapterForScrollBarChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ScrollBar */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ViewChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
        } /* struct ViewChangedEventArgs */

        public partial class ScrollView {
             ObjectCallbackSignature callbackViewChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ViewChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToViewChanged (Action<ViewChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ViewChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackViewChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ViewChanged (handle, callbackViewChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ViewChangedEventArgs> eventAdapterForViewChanged;
             public event Action<ViewChangedEventArgs> ViewChanged
             {
                 add
                 {
                      if (eventAdapterForViewChanged == null)
                          eventAdapterForViewChanged = new UrhoEventAdapter<ViewChangedEventArgs>();
                      eventAdapterForViewChanged.AddManagedSubscriber(handle, value, SubscribeToViewChanged);
                 }
                 remove { eventAdapterForViewChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ScrollView */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ModalChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public bool Modal => UrhoMap.get_bool (handle, UrhoHash.P_MODAL);
        } /* struct ModalChangedEventArgs */

        public partial class Window {
             ObjectCallbackSignature callbackModalChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ModalChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToModalChanged (Action<ModalChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ModalChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackModalChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ModalChanged (handle, callbackModalChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ModalChangedEventArgs> eventAdapterForModalChanged;
             public event Action<ModalChangedEventArgs> ModalChanged
             {
                 add
                 {
                      if (eventAdapterForModalChanged == null)
                          eventAdapterForModalChanged = new UrhoEventAdapter<ModalChangedEventArgs>();
                      eventAdapterForModalChanged.AddManagedSubscriber(handle, value, SubscribeToModalChanged);
                 }
                 remove { eventAdapterForModalChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Window */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct CharEntryEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public String Text => UrhoMap.get_String (handle, UrhoHash.P_TEXT);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int Qualifiers => UrhoMap.get_int (handle, UrhoHash.P_QUALIFIERS);
        } /* struct CharEntryEventArgs */

        public partial class LineEdit {
             ObjectCallbackSignature callbackCharEntry;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_CharEntry (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToCharEntry (Action<CharEntryEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new CharEntryEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackCharEntry = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_CharEntry (handle, callbackCharEntry, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<CharEntryEventArgs> eventAdapterForCharEntry;
             public event Action<CharEntryEventArgs> CharEntry
             {
                 add
                 {
                      if (eventAdapterForCharEntry == null)
                          eventAdapterForCharEntry = new UrhoEventAdapter<CharEntryEventArgs>();
                      eventAdapterForCharEntry.AddManagedSubscriber(handle, value, SubscribeToCharEntry);
                 }
                 remove { eventAdapterForCharEntry.RemoveManagedSubscriber(handle, value); }
             }
        } /* class LineEdit */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct TextChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public String Text => UrhoMap.get_String (handle, UrhoHash.P_TEXT);
        } /* struct TextChangedEventArgs */

        public partial class LineEdit {
             ObjectCallbackSignature callbackTextChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_TextChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToTextChanged (Action<TextChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TextChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackTextChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_TextChanged (handle, callbackTextChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<TextChangedEventArgs> eventAdapterForTextChanged;
             public event Action<TextChangedEventArgs> TextChanged
             {
                 add
                 {
                      if (eventAdapterForTextChanged == null)
                          eventAdapterForTextChanged = new UrhoEventAdapter<TextChangedEventArgs>();
                      eventAdapterForTextChanged.AddManagedSubscriber(handle, value, SubscribeToTextChanged);
                 }
                 remove { eventAdapterForTextChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class LineEdit */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct TextFinishedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public String Text => UrhoMap.get_String (handle, UrhoHash.P_TEXT);
            public float Value => UrhoMap.get_float (handle, UrhoHash.P_VALUE);
        } /* struct TextFinishedEventArgs */

        public partial class LineEdit {
             ObjectCallbackSignature callbackTextFinished;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_TextFinished (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToTextFinished (Action<TextFinishedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new TextFinishedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackTextFinished = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_TextFinished (handle, callbackTextFinished, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<TextFinishedEventArgs> eventAdapterForTextFinished;
             public event Action<TextFinishedEventArgs> TextFinished
             {
                 add
                 {
                      if (eventAdapterForTextFinished == null)
                          eventAdapterForTextFinished = new UrhoEventAdapter<TextFinishedEventArgs>();
                      eventAdapterForTextFinished.AddManagedSubscriber(handle, value, SubscribeToTextFinished);
                 }
                 remove { eventAdapterForTextFinished.RemoveManagedSubscriber(handle, value); }
             }
        } /* class LineEdit */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct MenuSelectedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
        } /* struct MenuSelectedEventArgs */

        public partial class Menu {
             ObjectCallbackSignature callbackMenuSelected;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_MenuSelected (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToMenuSelected (Action<MenuSelectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MenuSelectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackMenuSelected = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_MenuSelected (handle, callbackMenuSelected, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<MenuSelectedEventArgs> eventAdapterForMenuSelected;
             public event Action<MenuSelectedEventArgs> MenuSelected
             {
                 add
                 {
                      if (eventAdapterForMenuSelected == null)
                          eventAdapterForMenuSelected = new UrhoEventAdapter<MenuSelectedEventArgs>();
                      eventAdapterForMenuSelected.AddManagedSubscriber(handle, value, SubscribeToMenuSelected);
                 }
                 remove { eventAdapterForMenuSelected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class Menu */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ItemSelectedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public int Selection => UrhoMap.get_int (handle, UrhoHash.P_SELECTION);
        } /* struct ItemSelectedEventArgs */

        public partial class DropDownList {
             ObjectCallbackSignature callbackItemSelected;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ItemSelected (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToItemSelected (Action<ItemSelectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ItemSelectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackItemSelected = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ItemSelected (handle, callbackItemSelected, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ItemSelectedEventArgs> eventAdapterForItemSelected;
             public event Action<ItemSelectedEventArgs> ItemSelected
             {
                 add
                 {
                      if (eventAdapterForItemSelected == null)
                          eventAdapterForItemSelected = new UrhoEventAdapter<ItemSelectedEventArgs>();
                      eventAdapterForItemSelected.AddManagedSubscriber(handle, value, SubscribeToItemSelected);
                 }
                 remove { eventAdapterForItemSelected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class DropDownList */ 

        public partial class ListView {
             ObjectCallbackSignature callbackItemSelected;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ItemSelected (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToItemSelected (Action<ItemSelectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ItemSelectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackItemSelected = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ItemSelected (handle, callbackItemSelected, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ItemSelectedEventArgs> eventAdapterForItemSelected;
             public event Action<ItemSelectedEventArgs> ItemSelected
             {
                 add
                 {
                      if (eventAdapterForItemSelected == null)
                          eventAdapterForItemSelected = new UrhoEventAdapter<ItemSelectedEventArgs>();
                      eventAdapterForItemSelected.AddManagedSubscriber(handle, value, SubscribeToItemSelected);
                 }
                 remove { eventAdapterForItemSelected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ListView */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ItemDeselectedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public int Selection => UrhoMap.get_int (handle, UrhoHash.P_SELECTION);
        } /* struct ItemDeselectedEventArgs */

        public partial class ListView {
             ObjectCallbackSignature callbackItemDeselected;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ItemDeselected (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToItemDeselected (Action<ItemDeselectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ItemDeselectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackItemDeselected = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ItemDeselected (handle, callbackItemDeselected, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ItemDeselectedEventArgs> eventAdapterForItemDeselected;
             public event Action<ItemDeselectedEventArgs> ItemDeselected
             {
                 add
                 {
                      if (eventAdapterForItemDeselected == null)
                          eventAdapterForItemDeselected = new UrhoEventAdapter<ItemDeselectedEventArgs>();
                      eventAdapterForItemDeselected.AddManagedSubscriber(handle, value, SubscribeToItemDeselected);
                 }
                 remove { eventAdapterForItemDeselected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ListView */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct SelectionChangedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
        } /* struct SelectionChangedEventArgs */

        public partial class ListView {
             ObjectCallbackSignature callbackSelectionChanged;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_SelectionChanged (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToSelectionChanged (Action<SelectionChangedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new SelectionChangedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackSelectionChanged = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_SelectionChanged (handle, callbackSelectionChanged, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<SelectionChangedEventArgs> eventAdapterForSelectionChanged;
             public event Action<SelectionChangedEventArgs> SelectionChanged
             {
                 add
                 {
                      if (eventAdapterForSelectionChanged == null)
                          eventAdapterForSelectionChanged = new UrhoEventAdapter<SelectionChangedEventArgs>();
                      eventAdapterForSelectionChanged.AddManagedSubscriber(handle, value, SubscribeToSelectionChanged);
                 }
                 remove { eventAdapterForSelectionChanged.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ListView */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ItemClickedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public UIElement Item => UrhoMap.get_UIElement (handle, UrhoHash.P_ITEM);
            public int Selection => UrhoMap.get_int (handle, UrhoHash.P_SELECTION);
            public int Button => UrhoMap.get_int (handle, UrhoHash.P_BUTTON);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int Qualifiers => UrhoMap.get_int (handle, UrhoHash.P_QUALIFIERS);
        } /* struct ItemClickedEventArgs */

        public partial class ListView {
             ObjectCallbackSignature callbackItemClicked;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ItemClicked (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToItemClicked (Action<ItemClickedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ItemClickedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackItemClicked = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ItemClicked (handle, callbackItemClicked, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ItemClickedEventArgs> eventAdapterForItemClicked;
             public event Action<ItemClickedEventArgs> ItemClicked
             {
                 add
                 {
                      if (eventAdapterForItemClicked == null)
                          eventAdapterForItemClicked = new UrhoEventAdapter<ItemClickedEventArgs>();
                      eventAdapterForItemClicked.AddManagedSubscriber(handle, value, SubscribeToItemClicked);
                 }
                 remove { eventAdapterForItemClicked.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ListView */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ItemDoubleClickedEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public UIElement Item => UrhoMap.get_UIElement (handle, UrhoHash.P_ITEM);
            public int Selection => UrhoMap.get_int (handle, UrhoHash.P_SELECTION);
            public int Button => UrhoMap.get_int (handle, UrhoHash.P_BUTTON);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int Qualifiers => UrhoMap.get_int (handle, UrhoHash.P_QUALIFIERS);
        } /* struct ItemDoubleClickedEventArgs */

        public partial class ListView {
             ObjectCallbackSignature callbackItemDoubleClicked;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ItemDoubleClicked (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToItemDoubleClicked (Action<ItemDoubleClickedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ItemDoubleClickedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackItemDoubleClicked = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ItemDoubleClicked (handle, callbackItemDoubleClicked, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ItemDoubleClickedEventArgs> eventAdapterForItemDoubleClicked;
             public event Action<ItemDoubleClickedEventArgs> ItemDoubleClicked
             {
                 add
                 {
                      if (eventAdapterForItemDoubleClicked == null)
                          eventAdapterForItemDoubleClicked = new UrhoEventAdapter<ItemDoubleClickedEventArgs>();
                      eventAdapterForItemDoubleClicked.AddManagedSubscriber(handle, value, SubscribeToItemDoubleClicked);
                 }
                 remove { eventAdapterForItemDoubleClicked.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ListView */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct UnhandledKeyEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public Key Key =>(Key) UrhoMap.get_int (handle, UrhoHash.P_KEY);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int Qualifiers => UrhoMap.get_int (handle, UrhoHash.P_QUALIFIERS);
        } /* struct UnhandledKeyEventArgs */

        public partial class LineEdit {
             ObjectCallbackSignature callbackUnhandledKey;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_UnhandledKey (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToUnhandledKey (Action<UnhandledKeyEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UnhandledKeyEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackUnhandledKey = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_UnhandledKey (handle, callbackUnhandledKey, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<UnhandledKeyEventArgs> eventAdapterForUnhandledKey;
             public event Action<UnhandledKeyEventArgs> UnhandledKey
             {
                 add
                 {
                      if (eventAdapterForUnhandledKey == null)
                          eventAdapterForUnhandledKey = new UrhoEventAdapter<UnhandledKeyEventArgs>();
                      eventAdapterForUnhandledKey.AddManagedSubscriber(handle, value, SubscribeToUnhandledKey);
                 }
                 remove { eventAdapterForUnhandledKey.RemoveManagedSubscriber(handle, value); }
             }
        } /* class LineEdit */ 

        public partial class ListView {
             ObjectCallbackSignature callbackUnhandledKey;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_UnhandledKey (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToUnhandledKey (Action<UnhandledKeyEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UnhandledKeyEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackUnhandledKey = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_UnhandledKey (handle, callbackUnhandledKey, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<UnhandledKeyEventArgs> eventAdapterForUnhandledKey;
             public event Action<UnhandledKeyEventArgs> UnhandledKey
             {
                 add
                 {
                      if (eventAdapterForUnhandledKey == null)
                          eventAdapterForUnhandledKey = new UrhoEventAdapter<UnhandledKeyEventArgs>();
                      eventAdapterForUnhandledKey.AddManagedSubscriber(handle, value, SubscribeToUnhandledKey);
                 }
                 remove { eventAdapterForUnhandledKey.RemoveManagedSubscriber(handle, value); }
             }
        } /* class ListView */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct FileSelectedEventArgs {
            internal IntPtr handle;
            public String FileName => UrhoMap.get_String (handle, UrhoHash.P_FILENAME);
            public String Filter => UrhoMap.get_String (handle, UrhoHash.P_FILTER);
            public bool Ok => UrhoMap.get_bool (handle, UrhoHash.P_OK);
        } /* struct FileSelectedEventArgs */

        public partial class FileSelector {
             ObjectCallbackSignature callbackFileSelected;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_FileSelected (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToFileSelected (Action<FileSelectedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new FileSelectedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackFileSelected = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_FileSelected (handle, callbackFileSelected, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<FileSelectedEventArgs> eventAdapterForFileSelected;
             public event Action<FileSelectedEventArgs> FileSelected
             {
                 add
                 {
                      if (eventAdapterForFileSelected == null)
                          eventAdapterForFileSelected = new UrhoEventAdapter<FileSelectedEventArgs>();
                      eventAdapterForFileSelected.AddManagedSubscriber(handle, value, SubscribeToFileSelected);
                 }
                 remove { eventAdapterForFileSelected.RemoveManagedSubscriber(handle, value); }
             }
        } /* class FileSelector */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct MessageACKEventArgs {
            internal IntPtr handle;
            public bool Ok => UrhoMap.get_bool (handle, UrhoHash.P_OK);
        } /* struct MessageACKEventArgs */

        public partial class MessageBox {
             ObjectCallbackSignature callbackMessageACK;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_MessageACK (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToMessageACK (Action<MessageACKEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new MessageACKEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackMessageACK = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_MessageACK (handle, callbackMessageACK, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<MessageACKEventArgs> eventAdapterForMessageACK;
             public event Action<MessageACKEventArgs> MessageACK
             {
                 add
                 {
                      if (eventAdapterForMessageACK == null)
                          eventAdapterForMessageACK = new UrhoEventAdapter<MessageACKEventArgs>();
                      eventAdapterForMessageACK.AddManagedSubscriber(handle, value, SubscribeToMessageACK);
                 }
                 remove { eventAdapterForMessageACK.RemoveManagedSubscriber(handle, value); }
             }
        } /* class MessageBox */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ElementAddedEventArgs {
            internal IntPtr handle;
            public UIElement Root => UrhoMap.get_UIElement (handle, UrhoHash.P_ROOT);
            public UIElement Parent => UrhoMap.get_UIElement (handle, UrhoHash.P_PARENT);
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
        } /* struct ElementAddedEventArgs */

        public partial class UIElement {
             ObjectCallbackSignature callbackElementAdded;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ElementAdded (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToElementAdded (Action<ElementAddedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ElementAddedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackElementAdded = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ElementAdded (handle, callbackElementAdded, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ElementAddedEventArgs> eventAdapterForElementAdded;
             public event Action<ElementAddedEventArgs> ElementAdded
             {
                 add
                 {
                      if (eventAdapterForElementAdded == null)
                          eventAdapterForElementAdded = new UrhoEventAdapter<ElementAddedEventArgs>();
                      eventAdapterForElementAdded.AddManagedSubscriber(handle, value, SubscribeToElementAdded);
                 }
                 remove { eventAdapterForElementAdded.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct ElementRemovedEventArgs {
            internal IntPtr handle;
            public UIElement Root => UrhoMap.get_UIElement (handle, UrhoHash.P_ROOT);
            public UIElement Parent => UrhoMap.get_UIElement (handle, UrhoHash.P_PARENT);
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
        } /* struct ElementRemovedEventArgs */

        public partial class UIElement {
             ObjectCallbackSignature callbackElementRemoved;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_ElementRemoved (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToElementRemoved (Action<ElementRemovedEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new ElementRemovedEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackElementRemoved = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_ElementRemoved (handle, callbackElementRemoved, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<ElementRemovedEventArgs> eventAdapterForElementRemoved;
             public event Action<ElementRemovedEventArgs> ElementRemoved
             {
                 add
                 {
                      if (eventAdapterForElementRemoved == null)
                          eventAdapterForElementRemoved = new UrhoEventAdapter<ElementRemovedEventArgs>();
                      eventAdapterForElementRemoved.AddManagedSubscriber(handle, value, SubscribeToElementRemoved);
                 }
                 remove { eventAdapterForElementRemoved.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct HoverBeginEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
            public int ElementX => UrhoMap.get_int (handle, UrhoHash.P_ELEMENTX);
            public int ElementY => UrhoMap.get_int (handle, UrhoHash.P_ELEMENTY);
        } /* struct HoverBeginEventArgs */

        public partial class UIElement {
             ObjectCallbackSignature callbackHoverBegin;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_HoverBegin (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToHoverBegin (Action<HoverBeginEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new HoverBeginEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackHoverBegin = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_HoverBegin (handle, callbackHoverBegin, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<HoverBeginEventArgs> eventAdapterForHoverBegin;
             public event Action<HoverBeginEventArgs> HoverBegin
             {
                 add
                 {
                      if (eventAdapterForHoverBegin == null)
                          eventAdapterForHoverBegin = new UrhoEventAdapter<HoverBeginEventArgs>();
                      eventAdapterForHoverBegin.AddManagedSubscriber(handle, value, SubscribeToHoverBegin);
                 }
                 remove { eventAdapterForHoverBegin.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct HoverEndEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
        } /* struct HoverEndEventArgs */

        public partial class UIElement {
             ObjectCallbackSignature callbackHoverEnd;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_HoverEnd (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToHoverEnd (Action<HoverEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new HoverEndEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackHoverEnd = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_HoverEnd (handle, callbackHoverEnd, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<HoverEndEventArgs> eventAdapterForHoverEnd;
             public event Action<HoverEndEventArgs> HoverEnd
             {
                 add
                 {
                      if (eventAdapterForHoverEnd == null)
                          eventAdapterForHoverEnd = new UrhoEventAdapter<HoverEndEventArgs>();
                      eventAdapterForHoverEnd.AddManagedSubscriber(handle, value, SubscribeToHoverEnd);
                 }
                 remove { eventAdapterForHoverEnd.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct DragBeginEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
            public int ElementX => UrhoMap.get_int (handle, UrhoHash.P_ELEMENTX);
            public int ElementY => UrhoMap.get_int (handle, UrhoHash.P_ELEMENTY);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int NumButtons => UrhoMap.get_int (handle, UrhoHash.P_NUMBUTTONS);
        } /* struct DragBeginEventArgs */

        public partial class UIElement {
             ObjectCallbackSignature callbackDragBegin;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_DragBegin (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToDragBegin (Action<DragBeginEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragBeginEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackDragBegin = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_DragBegin (handle, callbackDragBegin, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<DragBeginEventArgs> eventAdapterForDragBegin;
             public event Action<DragBeginEventArgs> DragBegin
             {
                 add
                 {
                      if (eventAdapterForDragBegin == null)
                          eventAdapterForDragBegin = new UrhoEventAdapter<DragBeginEventArgs>();
                      eventAdapterForDragBegin.AddManagedSubscriber(handle, value, SubscribeToDragBegin);
                 }
                 remove { eventAdapterForDragBegin.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct DragMoveEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
            public int DX => UrhoMap.get_int (handle, UrhoHash.P_DX);
            public int DY => UrhoMap.get_int (handle, UrhoHash.P_DY);
            public int ElementX => UrhoMap.get_int (handle, UrhoHash.P_ELEMENTX);
            public int ElementY => UrhoMap.get_int (handle, UrhoHash.P_ELEMENTY);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int NumButtons => UrhoMap.get_int (handle, UrhoHash.P_NUMBUTTONS);
        } /* struct DragMoveEventArgs */

        public partial class UIElement {
             ObjectCallbackSignature callbackDragMove;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_DragMove (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToDragMove (Action<DragMoveEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragMoveEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackDragMove = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_DragMove (handle, callbackDragMove, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<DragMoveEventArgs> eventAdapterForDragMove;
             public event Action<DragMoveEventArgs> DragMove
             {
                 add
                 {
                      if (eventAdapterForDragMove == null)
                          eventAdapterForDragMove = new UrhoEventAdapter<DragMoveEventArgs>();
                      eventAdapterForDragMove.AddManagedSubscriber(handle, value, SubscribeToDragMove);
                 }
                 remove { eventAdapterForDragMove.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct DragEndEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
            public int ElementX => UrhoMap.get_int (handle, UrhoHash.P_ELEMENTX);
            public int ElementY => UrhoMap.get_int (handle, UrhoHash.P_ELEMENTY);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int NumButtons => UrhoMap.get_int (handle, UrhoHash.P_NUMBUTTONS);
        } /* struct DragEndEventArgs */

        public partial class UIElement {
             ObjectCallbackSignature callbackDragEnd;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_DragEnd (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToDragEnd (Action<DragEndEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragEndEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackDragEnd = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_DragEnd (handle, callbackDragEnd, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<DragEndEventArgs> eventAdapterForDragEnd;
             public event Action<DragEndEventArgs> DragEnd
             {
                 add
                 {
                      if (eventAdapterForDragEnd == null)
                          eventAdapterForDragEnd = new UrhoEventAdapter<DragEndEventArgs>();
                      eventAdapterForDragEnd.AddManagedSubscriber(handle, value, SubscribeToDragEnd);
                 }
                 remove { eventAdapterForDragEnd.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct DragCancelEventArgs {
            internal IntPtr handle;
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
            public int ElementX => UrhoMap.get_int (handle, UrhoHash.P_ELEMENTX);
            public int ElementY => UrhoMap.get_int (handle, UrhoHash.P_ELEMENTY);
            public int Buttons => UrhoMap.get_int (handle, UrhoHash.P_BUTTONS);
            public int NumButtons => UrhoMap.get_int (handle, UrhoHash.P_NUMBUTTONS);
        } /* struct DragCancelEventArgs */

        public partial class UIElement {
             ObjectCallbackSignature callbackDragCancel;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_DragCancel (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToDragCancel (Action<DragCancelEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new DragCancelEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackDragCancel = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_DragCancel (handle, callbackDragCancel, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<DragCancelEventArgs> eventAdapterForDragCancel;
             public event Action<DragCancelEventArgs> DragCancel
             {
                 add
                 {
                      if (eventAdapterForDragCancel == null)
                          eventAdapterForDragCancel = new UrhoEventAdapter<DragCancelEventArgs>();
                      eventAdapterForDragCancel.AddManagedSubscriber(handle, value, SubscribeToDragCancel);
                 }
                 remove { eventAdapterForDragCancel.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UIElement */ 

} /* namespace */

namespace Urho.Gui {
        public partial struct UIDropFileEventArgs {
            internal IntPtr handle;
            public String FileName => UrhoMap.get_String (handle, UrhoHash.P_FILENAME);
            public UIElement Element => UrhoMap.get_UIElement (handle, UrhoHash.P_ELEMENT);
            public int X => UrhoMap.get_int (handle, UrhoHash.P_X);
            public int Y => UrhoMap.get_int (handle, UrhoHash.P_Y);
            public int ElementX => UrhoMap.get_int (handle, UrhoHash.P_ELEMENTX);
            public int ElementY => UrhoMap.get_int (handle, UrhoHash.P_ELEMENTY);
        } /* struct UIDropFileEventArgs */

        public partial class UI {
             ObjectCallbackSignature callbackUIDropFile;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_UIDropFile (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToUIDropFile (Action<UIDropFileEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new UIDropFileEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackUIDropFile = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_UIDropFile (handle, callbackUIDropFile, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<UIDropFileEventArgs> eventAdapterForUIDropFile;
             public event Action<UIDropFileEventArgs> UIDropFile
             {
                 add
                 {
                      if (eventAdapterForUIDropFile == null)
                          eventAdapterForUIDropFile = new UrhoEventAdapter<UIDropFileEventArgs>();
                      eventAdapterForUIDropFile.AddManagedSubscriber(handle, value, SubscribeToUIDropFile);
                 }
                 remove { eventAdapterForUIDropFile.RemoveManagedSubscriber(handle, value); }
             }
        } /* class UI */ 

} /* namespace */

namespace Urho.Urho2D {
        public partial struct PhysicsBeginContact2DEventArgs {
            internal IntPtr handle;
            public PhysicsWorld2D World => UrhoMap.get_PhysicsWorld2D (handle, UrhoHash.P_WORLD);
            public RigidBody2D BodyA => UrhoMap.get_RigidBody2D (handle, UrhoHash.P_BODYA);
            public RigidBody2D BodyB => UrhoMap.get_RigidBody2D (handle, UrhoHash.P_BODYB);
            public Node NodeA => UrhoMap.get_Node (handle, UrhoHash.P_NODEA);
            public Node NodeB => UrhoMap.get_Node (handle, UrhoHash.P_NODEB);
            public IntPtr Contact => UrhoMap.get_IntPtr (handle, UrhoHash.P_CONTACT);
        } /* struct PhysicsBeginContact2DEventArgs */

        public partial class PhysicsWorld2D {
             ObjectCallbackSignature callbackPhysicsBeginContact2D;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_PhysicsBeginContact2D (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToPhysicsBeginContact2D (Action<PhysicsBeginContact2DEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsBeginContact2DEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackPhysicsBeginContact2D = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_PhysicsBeginContact2D (handle, callbackPhysicsBeginContact2D, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<PhysicsBeginContact2DEventArgs> eventAdapterForPhysicsBeginContact2D;
             public event Action<PhysicsBeginContact2DEventArgs> PhysicsBeginContact2D
             {
                 add
                 {
                      if (eventAdapterForPhysicsBeginContact2D == null)
                          eventAdapterForPhysicsBeginContact2D = new UrhoEventAdapter<PhysicsBeginContact2DEventArgs>();
                      eventAdapterForPhysicsBeginContact2D.AddManagedSubscriber(handle, value, SubscribeToPhysicsBeginContact2D);
                 }
                 remove { eventAdapterForPhysicsBeginContact2D.RemoveManagedSubscriber(handle, value); }
             }
        } /* class PhysicsWorld2D */ 

} /* namespace */

namespace Urho.Urho2D {
        public partial struct PhysicsEndContact2DEventArgs {
            internal IntPtr handle;
            public PhysicsWorld2D World => UrhoMap.get_PhysicsWorld2D (handle, UrhoHash.P_WORLD);
            public RigidBody2D BodyA => UrhoMap.get_RigidBody2D (handle, UrhoHash.P_BODYA);
            public RigidBody2D BodyB => UrhoMap.get_RigidBody2D (handle, UrhoHash.P_BODYB);
            public Node NodeA => UrhoMap.get_Node (handle, UrhoHash.P_NODEA);
            public Node NodeB => UrhoMap.get_Node (handle, UrhoHash.P_NODEB);
            public IntPtr Contact => UrhoMap.get_IntPtr (handle, UrhoHash.P_CONTACT);
        } /* struct PhysicsEndContact2DEventArgs */

        public partial class PhysicsWorld2D {
             ObjectCallbackSignature callbackPhysicsEndContact2D;
             [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
             extern static IntPtr urho_subscribe_PhysicsEndContact2D (IntPtr target, ObjectCallbackSignature act, IntPtr data);
             [Obsolete("SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# events instead (SubscribeToKeyDown -> KeyDown += ...).")]
             public Subscription SubscribeToPhysicsEndContact2D (Action<PhysicsEndContact2DEventArgs> handler)
             {
                  Action<IntPtr> proxy = (x)=> { var d = new PhysicsEndContact2DEventArgs () { handle = x }; handler (d); };
                  var s = new Subscription (proxy);
                  callbackPhysicsEndContact2D = ObjectCallback;
                  s.UnmanagedProxy = urho_subscribe_PhysicsEndContact2D (handle, callbackPhysicsEndContact2D, GCHandle.ToIntPtr (s.gch));
                  return s;
             }

             static UrhoEventAdapter<PhysicsEndContact2DEventArgs> eventAdapterForPhysicsEndContact2D;
             public event Action<PhysicsEndContact2DEventArgs> PhysicsEndContact2D
             {
                 add
                 {
                      if (eventAdapterForPhysicsEndContact2D == null)
                          eventAdapterForPhysicsEndContact2D = new UrhoEventAdapter<PhysicsEndContact2DEventArgs>();
                      eventAdapterForPhysicsEndContact2D.AddManagedSubscriber(handle, value, SubscribeToPhysicsEndContact2D);
                 }
                 remove { eventAdapterForPhysicsEndContact2D.RemoveManagedSubscriber(handle, value); }
             }
        } /* class PhysicsWorld2D */ 

} /* namespace */

namespace Urho {
        public partial struct NodeBeginContact2DEventArgs {
            internal IntPtr handle;
            public RigidBody2D Body => UrhoMap.get_RigidBody2D (handle, UrhoHash.P_BODY);
            public Node OtherNode => UrhoMap.get_Node (handle, UrhoHash.P_OTHERNODE);
            public RigidBody2D OtherBody => UrhoMap.get_RigidBody2D (handle, UrhoHash.P_OTHERBODY);
            public IntPtr Contact => UrhoMap.get_IntPtr (handle, UrhoHash.P_CONTACT);
        } /* struct NodeBeginContact2DEventArgs */

} /* namespace */

namespace Urho {
        public partial struct NodeEndContact2DEventArgs {
            internal IntPtr handle;
            public RigidBody2D Body => UrhoMap.get_RigidBody2D (handle, UrhoHash.P_BODY);
            public Node OtherNode => UrhoMap.get_Node (handle, UrhoHash.P_OTHERNODE);
            public RigidBody2D OtherBody => UrhoMap.get_RigidBody2D (handle, UrhoHash.P_OTHERBODY);
            public IntPtr Contact => UrhoMap.get_IntPtr (handle, UrhoHash.P_CONTACT);
        } /* struct NodeEndContact2DEventArgs */

} /* namespace */

// Hash Getters
namespace Urho {    internal class UrhoHash {
            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_CLONENODE ();
            static int _P_CLONENODE;
            internal static int P_CLONENODE { get { if (_P_CLONENODE == 0){ _P_CLONENODE = urho_hash_get_P_CLONENODE (); } return _P_CLONENODE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_POSITION ();
            static int _P_POSITION;
            internal static int P_POSITION { get { if (_P_POSITION == 0){ _P_POSITION = urho_hash_get_P_POSITION (); } return _P_POSITION; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_SUCCESS ();
            static int _P_SUCCESS;
            internal static int P_SUCCESS { get { if (_P_SUCCESS == 0){ _P_SUCCESS = urho_hash_get_P_SUCCESS (); } return _P_SUCCESS; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_TIME ();
            static int _P_TIME;
            internal static int P_TIME { get { if (_P_TIME == 0){ _P_TIME = urho_hash_get_P_TIME (); } return _P_TIME; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_REQUESTID ();
            static int _P_REQUESTID;
            internal static int P_REQUESTID { get { if (_P_REQUESTID == 0){ _P_REQUESTID = urho_hash_get_P_REQUESTID (); } return _P_REQUESTID; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_OK ();
            static int _P_OK;
            internal static int P_OK { get { if (_P_OK == 0){ _P_OK = urho_hash_get_P_OK (); } return _P_OK; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_DTHETA ();
            static int _P_DTHETA;
            internal static int P_DTHETA { get { if (_P_DTHETA == 0){ _P_DTHETA = urho_hash_get_P_DTHETA (); } return _P_DTHETA; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_BUTTON ();
            static int _P_BUTTON;
            internal static int P_BUTTON { get { if (_P_BUTTON == 0){ _P_BUTTON = urho_hash_get_P_BUTTON (); } return _P_BUTTON; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_BODYB ();
            static int _P_BODYB;
            internal static int P_BODYB { get { if (_P_BODYB == 0){ _P_BODYB = urho_hash_get_P_BODYB (); } return _P_BODYB; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_PROGRESS ();
            static int _P_PROGRESS;
            internal static int P_PROGRESS { get { if (_P_PROGRESS == 0){ _P_PROGRESS = urho_hash_get_P_PROGRESS (); } return _P_PROGRESS; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_SOUNDSOURCE ();
            static int _P_SOUNDSOURCE;
            internal static int P_SOUNDSOURCE { get { if (_P_SOUNDSOURCE == 0){ _P_SOUNDSOURCE = urho_hash_get_P_SOUNDSOURCE (); } return _P_SOUNDSOURCE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_CONSUMED ();
            static int _P_CONSUMED;
            internal static int P_CONSUMED { get { if (_P_CONSUMED == 0){ _P_CONSUMED = urho_hash_get_P_CONSUMED (); } return _P_CONSUMED; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_FILTER ();
            static int _P_FILTER;
            internal static int P_FILTER { get { if (_P_FILTER == 0){ _P_FILTER = urho_hash_get_P_FILTER (); } return _P_FILTER; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_Y ();
            static int _P_Y;
            internal static int P_Y { get { if (_P_Y == 0){ _P_Y = urho_hash_get_P_Y (); } return _P_Y; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_REPEAT ();
            static int _P_REPEAT;
            internal static int P_REPEAT { get { if (_P_REPEAT == 0){ _P_REPEAT = urho_hash_get_P_REPEAT (); } return _P_REPEAT; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_RESOURCE ();
            static int _P_RESOURCE;
            internal static int P_RESOURCE { get { if (_P_RESOURCE == 0){ _P_RESOURCE = urho_hash_get_P_RESOURCE (); } return _P_RESOURCE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_ELEMENTY ();
            static int _P_ELEMENTY;
            internal static int P_ELEMENTY { get { if (_P_ELEMENTY == 0){ _P_ELEMENTY = urho_hash_get_P_ELEMENTY (); } return _P_ELEMENTY; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_SDLEVENT ();
            static int _P_SDLEVENT;
            internal static int P_SDLEVENT { get { if (_P_SDLEVENT == 0){ _P_SDLEVENT = urho_hash_get_P_SDLEVENT (); } return _P_SDLEVENT; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_BORDERLESS ();
            static int _P_BORDERLESS;
            internal static int P_BORDERLESS { get { if (_P_BORDERLESS == 0){ _P_BORDERLESS = urho_hash_get_P_BORDERLESS (); } return _P_BORDERLESS; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_MODAL ();
            static int _P_MODAL;
            internal static int P_MODAL { get { if (_P_MODAL == 0){ _P_MODAL = urho_hash_get_P_MODAL (); } return _P_MODAL; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_CONTACT ();
            static int _P_CONTACT;
            internal static int P_CONTACT { get { if (_P_CONTACT == 0){ _P_CONTACT = urho_hash_get_P_CONTACT (); } return _P_CONTACT; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_BOUNDSMAX ();
            static int _P_BOUNDSMAX;
            internal static int P_BOUNDSMAX { get { if (_P_BOUNDSMAX == 0){ _P_BOUNDSMAX = urho_hash_get_P_BOUNDSMAX (); } return _P_BOUNDSMAX; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_FOCUS ();
            static int _P_FOCUS;
            internal static int P_FOCUS { get { if (_P_FOCUS == 0){ _P_FOCUS = urho_hash_get_P_FOCUS (); } return _P_FOCUS; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_LOOPED ();
            static int _P_LOOPED;
            internal static int P_LOOPED { get { if (_P_LOOPED == 0){ _P_LOOPED = urho_hash_get_P_LOOPED (); } return _P_LOOPED; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_ITEM ();
            static int _P_ITEM;
            internal static int P_ITEM { get { if (_P_ITEM == 0){ _P_ITEM = urho_hash_get_P_ITEM (); } return _P_ITEM; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_BODY ();
            static int _P_BODY;
            internal static int P_BODY { get { if (_P_BODY == 0){ _P_BODY = urho_hash_get_P_BODY (); } return _P_BODY; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_TIMESTEP ();
            static int _P_TIMESTEP;
            internal static int P_TIMESTEP { get { if (_P_TIMESTEP == 0){ _P_TIMESTEP = urho_hash_get_P_TIMESTEP (); } return _P_TIMESTEP; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_ACCEPT ();
            static int _P_ACCEPT;
            internal static int P_ACCEPT { get { if (_P_ACCEPT == 0){ _P_ACCEPT = urho_hash_get_P_ACCEPT (); } return _P_ACCEPT; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_ELEMENT ();
            static int _P_ELEMENT;
            internal static int P_ELEMENT { get { if (_P_ELEMENT == 0){ _P_ELEMENT = urho_hash_get_P_ELEMENT (); } return _P_ELEMENT; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_BEGINELEMENT ();
            static int _P_BEGINELEMENT;
            internal static int P_BEGINELEMENT { get { if (_P_BEGINELEMENT == 0){ _P_BEGINELEMENT = urho_hash_get_P_BEGINELEMENT (); } return _P_BEGINELEMENT; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_DDIST ();
            static int _P_DDIST;
            internal static int P_DDIST { get { if (_P_DDIST == 0){ _P_DDIST = urho_hash_get_P_DDIST (); } return _P_DDIST; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_BODYA ();
            static int _P_BODYA;
            internal static int P_BODYA { get { if (_P_BODYA == 0){ _P_BODYA = urho_hash_get_P_BODYA (); } return _P_BODYA; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_TEXTURE ();
            static int _P_TEXTURE;
            internal static int P_TEXTURE { get { if (_P_TEXTURE == 0){ _P_TEXTURE = urho_hash_get_P_TEXTURE (); } return _P_TEXTURE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_OBJECTANIMATION ();
            static int _P_OBJECTANIMATION;
            internal static int P_OBJECTANIMATION { get { if (_P_OBJECTANIMATION == 0){ _P_OBJECTANIMATION = urho_hash_get_P_OBJECTANIMATION (); } return _P_OBJECTANIMATION; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_CONTACTS ();
            static int _P_CONTACTS;
            internal static int P_CONTACTS { get { if (_P_CONTACTS == 0){ _P_CONTACTS = urho_hash_get_P_CONTACTS (); } return _P_CONTACTS; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_SIZE ();
            static int _P_SIZE;
            internal static int P_SIZE { get { if (_P_SIZE == 0){ _P_SIZE = urho_hash_get_P_SIZE (); } return _P_SIZE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_SCENE ();
            static int _P_SCENE;
            internal static int P_SCENE { get { if (_P_SCENE == 0){ _P_SCENE = urho_hash_get_P_SCENE (); } return _P_SCENE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_BYKEY ();
            static int _P_BYKEY;
            internal static int P_BYKEY { get { if (_P_BYKEY == 0){ _P_BYKEY = urho_hash_get_P_BYKEY (); } return _P_BYKEY; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_VALUE ();
            static int _P_VALUE;
            internal static int P_VALUE { get { if (_P_VALUE == 0){ _P_VALUE = urho_hash_get_P_VALUE (); } return _P_VALUE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_COMPONENT ();
            static int _P_COMPONENT;
            internal static int P_COMPONENT { get { if (_P_COMPONENT == 0){ _P_COMPONENT = urho_hash_get_P_COMPONENT (); } return _P_COMPONENT; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_TIMESTAMP ();
            static int _P_TIMESTAMP;
            internal static int P_TIMESTAMP { get { if (_P_TIMESTAMP == 0){ _P_TIMESTAMP = urho_hash_get_P_TIMESTAMP (); } return _P_TIMESTAMP; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_LOADEDNODES ();
            static int _P_LOADEDNODES;
            internal static int P_LOADEDNODES { get { if (_P_LOADEDNODES == 0){ _P_LOADEDNODES = urho_hash_get_P_LOADEDNODES (); } return _P_LOADEDNODES; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_PRESSED ();
            static int _P_PRESSED;
            internal static int P_PRESSED { get { if (_P_PRESSED == 0){ _P_PRESSED = urho_hash_get_P_PRESSED (); } return _P_PRESSED; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_CONNECTION ();
            static int _P_CONNECTION;
            internal static int P_CONNECTION { get { if (_P_CONNECTION == 0){ _P_CONNECTION = urho_hash_get_P_CONNECTION (); } return _P_CONNECTION; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_FULLSCREEN ();
            static int _P_FULLSCREEN;
            internal static int P_FULLSCREEN { get { if (_P_FULLSCREEN == 0){ _P_FULLSCREEN = urho_hash_get_P_FULLSCREEN (); } return _P_FULLSCREEN; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_SQUAREDSNAPTHRESHOLD ();
            static int _P_SQUAREDSNAPTHRESHOLD;
            internal static int P_SQUAREDSNAPTHRESHOLD { get { if (_P_SQUAREDSNAPTHRESHOLD == 0){ _P_SQUAREDSNAPTHRESHOLD = urho_hash_get_P_SQUAREDSNAPTHRESHOLD (); } return _P_SQUAREDSNAPTHRESHOLD; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_WIDTH ();
            static int _P_WIDTH;
            internal static int P_WIDTH { get { if (_P_WIDTH == 0){ _P_WIDTH = urho_hash_get_P_WIDTH (); } return _P_WIDTH; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_RESOURCENAME ();
            static int _P_RESOURCENAME;
            internal static int P_RESOURCENAME { get { if (_P_RESOURCENAME == 0){ _P_RESOURCENAME = urho_hash_get_P_RESOURCENAME (); } return _P_RESOURCENAME; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_TOUCHID ();
            static int _P_TOUCHID;
            internal static int P_TOUCHID { get { if (_P_TOUCHID == 0){ _P_TOUCHID = urho_hash_get_P_TOUCHID (); } return _P_TOUCHID; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_CROWD_AGENT_STATE ();
            static int _P_CROWD_AGENT_STATE;
            internal static int P_CROWD_AGENT_STATE { get { if (_P_CROWD_AGENT_STATE == 0){ _P_CROWD_AGENT_STATE = urho_hash_get_P_CROWD_AGENT_STATE (); } return _P_CROWD_AGENT_STATE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_HIGHDPI ();
            static int _P_HIGHDPI;
            internal static int P_HIGHDPI { get { if (_P_HIGHDPI == 0){ _P_HIGHDPI = urho_hash_get_P_HIGHDPI (); } return _P_HIGHDPI; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_TAG ();
            static int _P_TAG;
            internal static int P_TAG { get { if (_P_TAG == 0){ _P_TAG = urho_hash_get_P_TAG (); } return _P_TAG; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_RESOURCETYPE ();
            static int _P_RESOURCETYPE;
            internal static int P_RESOURCETYPE { get { if (_P_RESOURCETYPE == 0){ _P_RESOURCETYPE = urho_hash_get_P_RESOURCETYPE (); } return _P_RESOURCETYPE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_OFFSET ();
            static int _P_OFFSET;
            internal static int P_OFFSET { get { if (_P_OFFSET == 0){ _P_OFFSET = urho_hash_get_P_OFFSET (); } return _P_OFFSET; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_SOURCE ();
            static int _P_SOURCE;
            internal static int P_SOURCE { get { if (_P_SOURCE == 0){ _P_SOURCE = urho_hash_get_P_SOURCE (); } return _P_SOURCE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_HAT ();
            static int _P_HAT;
            internal static int P_HAT { get { if (_P_HAT == 0){ _P_HAT = urho_hash_get_P_HAT (); } return _P_HAT; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_BOUNDSMIN ();
            static int _P_BOUNDSMIN;
            internal static int P_BOUNDSMIN { get { if (_P_BOUNDSMIN == 0){ _P_BOUNDSMIN = urho_hash_get_P_BOUNDSMIN (); } return _P_BOUNDSMIN; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_NODE ();
            static int _P_NODE;
            internal static int P_NODE { get { if (_P_NODE == 0){ _P_NODE = urho_hash_get_P_NODE (); } return _P_NODE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_FRAMENUMBER ();
            static int _P_FRAMENUMBER;
            internal static int P_FRAMENUMBER { get { if (_P_FRAMENUMBER == 0){ _P_FRAMENUMBER = urho_hash_get_P_FRAMENUMBER (); } return _P_FRAMENUMBER; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_EFFECT ();
            static int _P_EFFECT;
            internal static int P_EFFECT { get { if (_P_EFFECT == 0){ _P_EFFECT = urho_hash_get_P_EFFECT (); } return _P_EFFECT; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_TOTALNODES ();
            static int _P_TOTALNODES;
            internal static int P_TOTALNODES { get { if (_P_TOTALNODES == 0){ _P_TOTALNODES = urho_hash_get_P_TOTALNODES (); } return _P_TOTALNODES; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_DX ();
            static int _P_DX;
            internal static int P_DX { get { if (_P_DX == 0){ _P_DX = urho_hash_get_P_DX (); } return _P_DX; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_ERROR ();
            static int _P_ERROR;
            internal static int P_ERROR { get { if (_P_ERROR == 0){ _P_ERROR = urho_hash_get_P_ERROR (); } return _P_ERROR; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_RESIZABLE ();
            static int _P_RESIZABLE;
            internal static int P_RESIZABLE { get { if (_P_RESIZABLE == 0){ _P_RESIZABLE = urho_hash_get_P_RESIZABLE (); } return _P_RESIZABLE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_CROWD_TARGET_STATE ();
            static int _P_CROWD_TARGET_STATE;
            internal static int P_CROWD_TARGET_STATE { get { if (_P_CROWD_TARGET_STATE == 0){ _P_CROWD_TARGET_STATE = urho_hash_get_P_CROWD_TARGET_STATE (); } return _P_CROWD_TARGET_STATE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_CLICKEDELEMENT ();
            static int _P_CLICKEDELEMENT;
            internal static int P_CLICKEDELEMENT { get { if (_P_CLICKEDELEMENT == 0){ _P_CLICKEDELEMENT = urho_hash_get_P_CLICKEDELEMENT (); } return _P_CLICKEDELEMENT; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_SERIALIZABLE ();
            static int _P_SERIALIZABLE;
            internal static int P_SERIALIZABLE { get { if (_P_SERIALIZABLE == 0){ _P_SERIALIZABLE = urho_hash_get_P_SERIALIZABLE (); } return _P_SERIALIZABLE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_PARENT ();
            static int _P_PARENT;
            internal static int P_PARENT { get { if (_P_PARENT == 0){ _P_PARENT = urho_hash_get_P_PARENT (); } return _P_PARENT; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_ALLOW ();
            static int _P_ALLOW;
            internal static int P_ALLOW { get { if (_P_ALLOW == 0){ _P_ALLOW = urho_hash_get_P_ALLOW (); } return _P_ALLOW; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_HEIGHT ();
            static int _P_HEIGHT;
            internal static int P_HEIGHT { get { if (_P_HEIGHT == 0){ _P_HEIGHT = urho_hash_get_P_HEIGHT (); } return _P_HEIGHT; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_QUALIFIERS ();
            static int _P_QUALIFIERS;
            internal static int P_QUALIFIERS { get { if (_P_QUALIFIERS == 0){ _P_QUALIFIERS = urho_hash_get_P_QUALIFIERS (); } return _P_QUALIFIERS; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_TOTALRESOURCES ();
            static int _P_TOTALRESOURCES;
            internal static int P_TOTALRESOURCES { get { if (_P_TOTALRESOURCES == 0){ _P_TOTALRESOURCES = urho_hash_get_P_TOTALRESOURCES (); } return _P_TOTALRESOURCES; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_NODEB ();
            static int _P_NODEB;
            internal static int P_NODEB { get { if (_P_NODEB == 0){ _P_NODEB = urho_hash_get_P_NODEB (); } return _P_NODEB; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_MESSAGEID ();
            static int _P_MESSAGEID;
            internal static int P_MESSAGEID { get { if (_P_MESSAGEID == 0){ _P_MESSAGEID = urho_hash_get_P_MESSAGEID (); } return _P_MESSAGEID; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_MODE ();
            static int _P_MODE;
            internal static int P_MODE { get { if (_P_MODE == 0){ _P_MODE = urho_hash_get_P_MODE (); } return _P_MODE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_SELECTION ();
            static int _P_SELECTION;
            internal static int P_SELECTION { get { if (_P_SELECTION == 0){ _P_SELECTION = urho_hash_get_P_SELECTION (); } return _P_SELECTION; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_SCANCODE ();
            static int _P_SCANCODE;
            internal static int P_SCANCODE { get { if (_P_SCANCODE == 0){ _P_SCANCODE = urho_hash_get_P_SCANCODE (); } return _P_SCANCODE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_LOADEDRESOURCES ();
            static int _P_LOADEDRESOURCES;
            internal static int P_LOADEDRESOURCES { get { if (_P_LOADEDRESOURCES == 0){ _P_LOADEDRESOURCES = urho_hash_get_P_LOADEDRESOURCES (); } return _P_LOADEDRESOURCES; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_GESTUREID ();
            static int _P_GESTUREID;
            internal static int P_GESTUREID { get { if (_P_GESTUREID == 0){ _P_GESTUREID = urho_hash_get_P_GESTUREID (); } return _P_GESTUREID; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_OBSTACLE ();
            static int _P_OBSTACLE;
            internal static int P_OBSTACLE { get { if (_P_OBSTACLE == 0){ _P_OBSTACLE = urho_hash_get_P_OBSTACLE (); } return _P_OBSTACLE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_OTHERNODE ();
            static int _P_OTHERNODE;
            internal static int P_OTHERNODE { get { if (_P_OTHERNODE == 0){ _P_OTHERNODE = urho_hash_get_P_OTHERNODE (); } return _P_OTHERNODE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_CROWD_AGENT ();
            static int _P_CROWD_AGENT;
            internal static int P_CROWD_AGENT { get { if (_P_CROWD_AGENT == 0){ _P_CROWD_AGENT = urho_hash_get_P_CROWD_AGENT (); } return _P_CROWD_AGENT; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_JOYSTICKID ();
            static int _P_JOYSTICKID;
            internal static int P_JOYSTICKID { get { if (_P_JOYSTICKID == 0){ _P_JOYSTICKID = urho_hash_get_P_JOYSTICKID (); } return _P_JOYSTICKID; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_MINIMIZED ();
            static int _P_MINIMIZED;
            internal static int P_MINIMIZED { get { if (_P_MINIMIZED == 0){ _P_MINIMIZED = urho_hash_get_P_MINIMIZED (); } return _P_MINIMIZED; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_KEY ();
            static int _P_KEY;
            internal static int P_KEY { get { if (_P_KEY == 0){ _P_KEY = urho_hash_get_P_KEY (); } return _P_KEY; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_NUMFINGERS ();
            static int _P_NUMFINGERS;
            internal static int P_NUMFINGERS { get { if (_P_NUMFINGERS == 0){ _P_NUMFINGERS = urho_hash_get_P_NUMFINGERS (); } return _P_NUMFINGERS; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_NODEA ();
            static int _P_NODEA;
            internal static int P_NODEA { get { if (_P_NODEA == 0){ _P_NODEA = urho_hash_get_P_NODEA (); } return _P_NODEA; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_ARRIVED ();
            static int _P_ARRIVED;
            internal static int P_ARRIVED { get { if (_P_ARRIVED == 0){ _P_ARRIVED = urho_hash_get_P_ARRIVED (); } return _P_ARRIVED; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_COMMAND ();
            static int _P_COMMAND;
            internal static int P_COMMAND { get { if (_P_COMMAND == 0){ _P_COMMAND = urho_hash_get_P_COMMAND (); } return _P_COMMAND; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_X ();
            static int _P_X;
            internal static int P_X { get { if (_P_X == 0){ _P_X = urho_hash_get_P_X (); } return _P_X; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_CONSTANT ();
            static int _P_CONSTANT;
            internal static int P_CONSTANT { get { if (_P_CONSTANT == 0){ _P_CONSTANT = urho_hash_get_P_CONSTANT (); } return _P_CONSTANT; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_FILENAME ();
            static int _P_FILENAME;
            internal static int P_FILENAME { get { if (_P_FILENAME == 0){ _P_FILENAME = urho_hash_get_P_FILENAME (); } return _P_FILENAME; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_CAMERA ();
            static int _P_CAMERA;
            internal static int P_CAMERA { get { if (_P_CAMERA == 0){ _P_CAMERA = urho_hash_get_P_CAMERA (); } return _P_CAMERA; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_RADIUS ();
            static int _P_RADIUS;
            internal static int P_RADIUS { get { if (_P_RADIUS == 0){ _P_RADIUS = urho_hash_get_P_RADIUS (); } return _P_RADIUS; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_WORLD ();
            static int _P_WORLD;
            internal static int P_WORLD { get { if (_P_WORLD == 0){ _P_WORLD = urho_hash_get_P_WORLD (); } return _P_WORLD; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_EXITCODE ();
            static int _P_EXITCODE;
            internal static int P_EXITCODE { get { if (_P_EXITCODE == 0){ _P_EXITCODE = urho_hash_get_P_EXITCODE (); } return _P_EXITCODE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_MESH ();
            static int _P_MESH;
            internal static int P_MESH { get { if (_P_MESH == 0){ _P_MESH = urho_hash_get_P_MESH (); } return _P_MESH; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_STATE ();
            static int _P_STATE;
            internal static int P_STATE { get { if (_P_STATE == 0){ _P_STATE = urho_hash_get_P_STATE (); } return _P_STATE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_TRIGGER ();
            static int _P_TRIGGER;
            internal static int P_TRIGGER { get { if (_P_TRIGGER == 0){ _P_TRIGGER = urho_hash_get_P_TRIGGER (); } return _P_TRIGGER; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_MOUSELOCKED ();
            static int _P_MOUSELOCKED;
            internal static int P_MOUSELOCKED { get { if (_P_MOUSELOCKED == 0){ _P_MOUSELOCKED = urho_hash_get_P_MOUSELOCKED (); } return _P_MOUSELOCKED; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_CENTERX ();
            static int _P_CENTERX;
            internal static int P_CENTERX { get { if (_P_CENTERX == 0){ _P_CENTERX = urho_hash_get_P_CENTERX (); } return _P_CENTERX; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_NUMBUTTONS ();
            static int _P_NUMBUTTONS;
            internal static int P_NUMBUTTONS { get { if (_P_NUMBUTTONS == 0){ _P_NUMBUTTONS = urho_hash_get_P_NUMBUTTONS (); } return _P_NUMBUTTONS; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_OTHERBODY ();
            static int _P_OTHERBODY;
            internal static int P_OTHERBODY { get { if (_P_OTHERBODY == 0){ _P_OTHERBODY = urho_hash_get_P_OTHERBODY (); } return _P_OTHERBODY; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_TEXT ();
            static int _P_TEXT;
            internal static int P_TEXT { get { if (_P_TEXT == 0){ _P_TEXT = urho_hash_get_P_TEXT (); } return _P_TEXT; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_MESSAGE ();
            static int _P_MESSAGE;
            internal static int P_MESSAGE { get { if (_P_MESSAGE == 0){ _P_MESSAGE = urho_hash_get_P_MESSAGE (); } return _P_MESSAGE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_CLONECOMPONENT ();
            static int _P_CLONECOMPONENT;
            internal static int P_CLONECOMPONENT { get { if (_P_CLONECOMPONENT == 0){ _P_CLONECOMPONENT = urho_hash_get_P_CLONECOMPONENT (); } return _P_CLONECOMPONENT; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_CENTERY ();
            static int _P_CENTERY;
            internal static int P_CENTERY { get { if (_P_CENTERY == 0){ _P_CENTERY = urho_hash_get_P_CENTERY (); } return _P_CENTERY; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_VELOCITY ();
            static int _P_VELOCITY;
            internal static int P_VELOCITY { get { if (_P_VELOCITY == 0){ _P_VELOCITY = urho_hash_get_P_VELOCITY (); } return _P_VELOCITY; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_ELEMENTX ();
            static int _P_ELEMENTX;
            internal static int P_ELEMENTX { get { if (_P_ELEMENTX == 0){ _P_ELEMENTX = urho_hash_get_P_ELEMENTX (); } return _P_ELEMENTX; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_WHEEL ();
            static int _P_WHEEL;
            internal static int P_WHEEL { get { if (_P_WHEEL == 0){ _P_WHEEL = urho_hash_get_P_WHEEL (); } return _P_WHEEL; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_INDEX ();
            static int _P_INDEX;
            internal static int P_INDEX { get { if (_P_INDEX == 0){ _P_INDEX = urho_hash_get_P_INDEX (); } return _P_INDEX; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_ATTRIBUTEANIMATIONNAME ();
            static int _P_ATTRIBUTEANIMATIONNAME;
            internal static int P_ATTRIBUTEANIMATIONNAME { get { if (_P_ATTRIBUTEANIMATIONNAME == 0){ _P_ATTRIBUTEANIMATIONNAME = urho_hash_get_P_ATTRIBUTEANIMATIONNAME (); } return _P_ATTRIBUTEANIMATIONNAME; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_TARGET ();
            static int _P_TARGET;
            internal static int P_TARGET { get { if (_P_TARGET == 0){ _P_TARGET = urho_hash_get_P_TARGET (); } return _P_TARGET; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_ROOT ();
            static int _P_ROOT;
            internal static int P_ROOT { get { if (_P_ROOT == 0){ _P_ROOT = urho_hash_get_P_ROOT (); } return _P_ROOT; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_AXIS ();
            static int _P_AXIS;
            internal static int P_AXIS { get { if (_P_AXIS == 0){ _P_AXIS = urho_hash_get_P_AXIS (); } return _P_AXIS; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_BUTTONS ();
            static int _P_BUTTONS;
            internal static int P_BUTTONS { get { if (_P_BUTTONS == 0){ _P_BUTTONS = urho_hash_get_P_BUTTONS (); } return _P_BUTTONS; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_LEVEL ();
            static int _P_LEVEL;
            internal static int P_LEVEL { get { if (_P_LEVEL == 0){ _P_LEVEL = urho_hash_get_P_LEVEL (); } return _P_LEVEL; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_VIEW ();
            static int _P_VIEW;
            internal static int P_VIEW { get { if (_P_VIEW == 0){ _P_VIEW = urho_hash_get_P_VIEW (); } return _P_VIEW; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_SOUND ();
            static int _P_SOUND;
            internal static int P_SOUND { get { if (_P_SOUND == 0){ _P_SOUND = urho_hash_get_P_SOUND (); } return _P_SOUND; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_VISIBLE ();
            static int _P_VISIBLE;
            internal static int P_VISIBLE { get { if (_P_VISIBLE == 0){ _P_VISIBLE = urho_hash_get_P_VISIBLE (); } return _P_VISIBLE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_ID ();
            static int _P_ID;
            internal static int P_ID { get { if (_P_ID == 0){ _P_ID = urho_hash_get_P_ID (); } return _P_ID; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_NAME ();
            static int _P_NAME;
            internal static int P_NAME { get { if (_P_NAME == 0){ _P_NAME = urho_hash_get_P_NAME (); } return _P_NAME; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_DATA ();
            static int _P_DATA;
            internal static int P_DATA { get { if (_P_DATA == 0){ _P_DATA = urho_hash_get_P_DATA (); } return _P_DATA; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_DY ();
            static int _P_DY;
            internal static int P_DY { get { if (_P_DY == 0){ _P_DY = urho_hash_get_P_DY (); } return _P_DY; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_ANIMATION ();
            static int _P_ANIMATION;
            internal static int P_ANIMATION { get { if (_P_ANIMATION == 0){ _P_ANIMATION = urho_hash_get_P_ANIMATION (); } return _P_ANIMATION; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_SURFACE ();
            static int _P_SURFACE;
            internal static int P_SURFACE { get { if (_P_SURFACE == 0){ _P_SURFACE = urho_hash_get_P_SURFACE (); } return _P_SURFACE; }}

            [DllImport(Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
            extern static int urho_hash_get_P_PRESSURE ();
            static int _P_PRESSURE;
            internal static int P_PRESSURE { get { if (_P_PRESSURE == 0){ _P_PRESSURE = urho_hash_get_P_PRESSURE (); } return _P_PRESSURE; }}

        }
    }