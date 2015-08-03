#
#
# This really should use an installed Urho
# and use `pkg-config --libs Urho3D`
#
URHO_DIR=/cvs/Urho3D
mono64=/mono
OBJSHARPIE=/cvs/ObjectiveSharpie
OBJSHARPIE_BIN=$(OBJSHARPIE)/llvm/_install/bin
URHO_FLAGS=-I$(URHO_DIR)/include -I$(URHO_DIR)/include/kNet
URHO_LIBS=-L$(URHO_DIR)/lib -framework AudioUnit -framework Carbon -framework Cocoa -framework CoreAudio -framework ForceFeedback -framework IOKit -framework OpenGL -framework CoreServices -lUrho3D -ldl -lpthread 
NO_CLS_WARNINGS=-nowarn:3021

GENERATED_SOURCES =	\
	generated/Animatable.cs		\
	generated/AnimatedModel.cs	\
	generated/AnimatedSprite2D.cs	\
	generated/Animation.cs		\
	generated/Animation2D.cs	\
	generated/AnimationController.cs\
	generated/AnimationSet2D.cs	\
	generated/AnimationState.cs	\
	generated/Application.cs	\
	generated/AsyncLoadState.cs	\
	generated/AttributeAccessor.cs	\
	generated/AttributeAnimationInfo.cs\
	generated/Audio.cs		\
	generated/BillboardSet.cs	\
	generated/BlendMode.cs		\
	generated/BodyType2D.cs		\
	generated/BorderImage.cs	\
	generated/BufferedSoundStream.cs\
	generated/Button.cs		\
	generated/Camera.cs		\
	generated/CheckBox.cs		\
	generated/CollisionBox2D.cs	\
	generated/CollisionChain2D.cs	\
	generated/CollisionCircle2D.cs	\
	generated/CollisionEdge2D.cs	\
	generated/CollisionEventMode.cs	\
	generated/CollisionGeometryData.cs\
	generated/CollisionPolygon2D.cs	\
	generated/CollisionShape.cs	\
	generated/CollisionShape2D.cs	\
	generated/CompareMode.cs	\
	generated/Component.cs		\
	generated/CompressedFormat.cs	\
	generated/Connection.cs		\
	generated/Constraint.cs		\
	generated/UrhoConsole.cs	\
	generated/ConstantBuffer.cs	\
	generated/Constraint2D.cs	\
	generated/ConstraintDistance2D.cs\
	generated/ConstraintFriction2D.cs\
	generated/ConstraintGear2D.cs	\
	generated/ConstraintMotor2D.cs	\
	generated/ConstraintMouse2D.cs	\
	generated/ConstraintPrismatic2D.cs\
	generated/ConstraintPulley2D.cs	\
	generated/ConstraintRevolute2D.cs\
	generated/ConstraintRope2D.cs	\
	generated/ConstraintType.cs	\
	generated/ConstraintWeld2D.cs	\
	generated/ConstraintWheel2D.cs	\
	generated/Context.cs		\
	generated/Corner.cs		\
	generated/CreateMode.cs		\
	generated/CrowdAgent.cs		\
	generated/CrowdAgentState.cs	\
	generated/CrowdTargetState.cs	\
	generated/CubeMapFace.cs	\
	generated/CubeMapLayout.cs	\
	generated/CullMode.cs		\
	generated/Cursor.cs		\
	generated/CursorShape.cs	\
	generated/CustomGeometry.cs	\
	generated/DebugHud.cs		\
	generated/DebugRenderer.cs	\
	generated/DecalSet.cs		\
	generated/DeferredLightPSVariation.cs	\
	generated/DeferredLightVSVariation.cs	\
	generated/DetourCrowdManager.cs	\
	generated/Drawable.cs		\
	generated/Drawable2D.cs		\
	generated/DropDownList.cs	\
	generated/DynamicNavigationMesh.cs\
	generated/EmitterType.cs	\
	generated/EmitterType2D.cs	\
	generated/Engine.cs		\
	generated/FONT_TYPE.cs		\
	generated/FaceCameraMode.cs	\
	generated/File.cs		\
	generated/FileMode.cs		\
	generated/FileSelector.cs	\
	generated/FileSystem.cs		\
	generated/FileWatcher.cs	\
	generated/FillMode.cs		\
	generated/FocusMode.cs		\
	generated/Font.cs		\
	generated/FontFace.cs		\
	generated/FontFaceBitmap.cs	\
	generated/FontFaceFreeType.cs	\
	generated/FrustumPlane.cs	\
	generated/Geometry.cs		\
	generated/GeometryType.cs	\
	generated/Graphics.cs		\
	generated/HighlightMode.cs	\
	generated/HorizontalAlignment.cs	\
	generated/Image.cs		\
	generated/Input.cs		\
	generated/InterpMethod.cs	\
	generated/InterpolationMode.cs	\
	generated/Intersection.cs	\
	generated/LayoutMode.cs		\
	generated/Light.cs		\
	generated/LightPSVariation.cs	\
	generated/LightType.cs		\
	generated/LightVSVariation.cs	\
	generated/LineEdit.cs		\
	generated/ListView.cs		\
	generated/LoadMode.cs		\
	generated/LockState.cs		\
	generated/Log.cs		\
	generated/LogicComponent.cs	\
	generated/LoopMode2D.cs	\
	generated/Material.cs	\
	generated/Menu.cs	\
	generated/MessageBox.cs	\
	generated/Model.cs	\
	generated/MouseMode.cs	\
	generated/NavArea.cs	\
	generated/Navigable.cs	\
	generated/NavigationMesh.cs	\
	generated/NavigationPushiness.cs	\
	generated/NavigationQuality.cs	\
	generated/NavmeshPartitionType.cs	\
	generated/Network.cs		\
	generated/Node.cs		\
	generated/UrhoObject.cs		\
	generated/ObjectAnimation.cs	\
	generated/ObjectFactory.cs	\
	generated/Obstacle.cs		\
	generated/OcclusionBuffer.cs	\
	generated/Octree.cs		\
	generated/OffMeshConnection.cs	\
	generated/OggVorbisSoundStream.cs	\
	generated/Orientation.cs	\
	generated/Orientation2D.cs	\
	generated/PListFile.cs		\
	generated/PListValueType.cs	\
	generated/PackageFile.cs	\
	generated/ParticleEffect.cs	\
	generated/ParticleEffect2D.cs	\
	generated/ParticleEmitter.cs	\
	generated/ParticleEmitter2D.cs	\
	generated/Pass.cs		\
	generated/PassLightingMode.cs	\
	generated/PhysicsWorld.cs	\
	generated/PhysicsWorld2D.cs	\
	generated/PrimitiveType.cs	\
	generated/Profiler.cs		\
	generated/PropertySet2D.cs	\
	generated/RayQueryLevel.cs	\
	generated/RefCounted.cs		\
	generated/RenderCommandSortMode.cs	\
	generated/RenderCommandType.cs	\
	generated/RenderPath.cs		\
	generated/RenderSurface.cs	\
	generated/RenderSurfaceUpdateMode.cs	\
	generated/RenderTargetSizeMode.cs	\
	generated/Renderer.cs		\
	generated/Renderer2D.cs		\
	generated/Resource.cs		\
	generated/ResourceCache.cs	\
	generated/ResourceRequest.cs	\
	generated/ResourceRouter.cs	\
	generated/RigidBody.cs		\
	generated/RigidBody2D.cs	\
	generated/Scene.cs		\
	generated/ScrollBar.cs		\
	generated/ScrollView.cs		\
	generated/Serializable.cs	\
	generated/Shader.cs		\
	generated/ShaderParameterAnimationInfo.cs	\
	generated/ShaderParameterGroup.cs	\
	generated/ShaderPrecache.cs	\
	generated/ShaderProgram.cs	\
	generated/ShaderType.cs		\
	generated/ShaderVariation.cs	\
	generated/ShapeType.cs		\
	generated/Skeleton.cs		\
	generated/Skybox.cs		\
	generated/Slider.cs		\
	generated/SmoothedTransform.cs	\
	generated/Sound.cs		\
	generated/SoundListener.cs	\
	generated/SoundSource.cs	\
	generated/SoundSource3D.cs	\
	generated/SoundStream.cs	\
	generated/SplinePath.cs		\
	generated/Sprite.cs		\
	generated/Sprite2D.cs		\
	generated/SpriteSheet2D.cs	\
	generated/State.cs		\
	generated/StaticModel.cs	\
	generated/StaticModelGroup.cs	\
	generated/StaticSprite2D.cs	\
	generated/StencilOp.cs		\
	generated/Technique.cs		\
	generated/Terrain.cs		\
	generated/TerrainPatch.cs	\
	generated/Text.cs		\
	generated/Text3D.cs		\
	generated/TextEffect.cs		\
	generated/Texture.cs		\
	generated/Texture2D.cs		\
	generated/Texture3D.cs		\
	generated/TextureAddressMode.cs	\
	generated/TextureCoordinate.cs	\
	generated/TextureCube.cs	\
	generated/TextureFilterMode.cs	\
	generated/TextureUnit.cs	\
	generated/TextureUsage.cs	\
	generated/Tile2D.cs		\
	generated/TileMap2D.cs		\
	generated/TileMapLayer2D.cs	\
	generated/TileMapLayerType2D.cs	\
	generated/TileMapObject2D.cs	\
	generated/TileMapObjectType2D.cs	\
	generated/Time.cs		\
	generated/TmxFile2D.cs		\
	generated/TmxImageLayer2D.cs	\
	generated/TmxLayer2D.cs		\
	generated/TmxObjectGroup2D.cs	\
	generated/TmxTileLayer2D.cs	\
	generated/ToolTip.cs		\
	generated/TransformSpace.cs	\
	generated/TraversalMode.cs	\
	generated/Type.cs		\
	generated/UI.cs			\
	generated/UIElement.cs		\
	generated/UnknownComponent.cs	\
	generated/UpdateGeometryType.cs	\
	generated/ValueAnimation.cs	\
	generated/ValueAnimationInfo.cs	\
	generated/VariantType.cs	\
	generated/VertexBuffer.cs	\
	generated/IndexBuffer.cs	\
	generated/VertexElement.cs	\
	generated/VertexLightVSVariation.cs	\
	generated/VerticalAlignment.cs	\
	generated/View.cs		\
	generated/View3D.cs		\
	generated/Viewport.cs		\
	generated/Window.cs		\
	generated/WindowDragMode.cs	\
	generated/WorkQueue.cs		\
	generated/WrapMode.cs		\
	generated/XMLFile.cs		\
	generated/Zone.cs

SOURCES = $(GENERATED_SOURCES) 		\
	generated/Object.Events.cs	\
	src/Application.cs		\
	src/AttributeInfo.cs		\
	src/BufferedSoundStream.cs	\
	src/Collision.cs		\
	src/Component.cs		\
	src/Controls.cs			\
	src/Connection.cs		\
	src/Enums.cs			\
	src/Input.cs			\
	src/Log.cs			\
	src/Model.cs			\
	src/Network.cs			\
	src/Node.cs			\
	src/Object.cs			\
	src/PodVector.cs		\
	src/UrhoMap.cs			\
	src/RefCounted.cs		\
	src/RenderPath.cs		\
	src/ResourceCache.cs		\
	src/Runtime.cs			\
	src/Skeleton.cs			\
	src/StringHash.cs		\
	src/Stubs.cs			\
	src/Structs.cs			\
	src/UIElement.cs		\
	src/VertexBuffer.cs		\
	external/MathHelper.cs		\
	external/Matrix3.cs		\
	external/Matrix4.cs		\
	external/Matrix4d.cs		\
	external/Quaternion.cs		\
	external/Quaterniond.cs		\
	external/Vector2d.cs		\
	external/Vector3d.cs		\
	external/Vector4d.cs		\
	external/Vector2.cs		\
	external/IntVector2.cs		\
	external/Vector3.cs		\
	external/Vector4.cs		\
	external/Plane.cs

CXXFLAGS=-g -Wno-return-type-c-linkage -Wno-c++11-extensions $(URHO_FLAGS)

all: check-links Urho.pch Urho.dll libmono-urho.dylib sample.exe

run: gen-bind Urho.dll

$(GENERATED_SOURCES): Urho.pch ../SharpieBinder/bin/Debug/SharpieBinder.exe libclang-mono.dylib
	DYLD_LIBRARY_PATH=. $(mono64)/bin/mono --debug ../SharpieBinder/bin/Debug/SharpieBinder.exe

libclang-mono.dylib: 
	if test $(OBJSHARPIE)/bin/x64/Internal/libclang-mono.dylib -nt libclang-mono.dylib; then cp $(OBJSHARPIE)/bin/x64/Internal/libclang-mono.dylib .; fi

Urho.dll: $(SOURCES) Makefile 
	mcs $(NO_CLS_WARNINGS) -unsafe -out:Urho.dll -target:library -debug $(SOURCES)


Urho.pch: AllUrho.h Makefile
	$(OBJSHARPIE_BIN)/clang -cc1 -emit-pch -o Urho.pch all-urho.cpp  -I$(URHO_DIR)/include -I$(URHO_DIR)/include/kNet

libmono-urho.dylib: binding.o glue.o events.o ApplicationProxy.o vector.o Makefile 
	c++ -dynamiclib -g -o libmono-urho.dylib -g $(URHO_LIBS) binding.o glue.o vector.o events.o ApplicationProxy.o

binding.o: AllUrho.h generated/binding.cpp Makefile
	c++ -g -c $(X) $(CXXFLAGS) generated/binding.cpp 

glue.o: src/glue.cpp src/glue.h Makefile 
	c++ -c $(CXXFLAGS) src/glue.cpp 

vector.o: src/vector.cpp src/glue.h Makefile 
	c++ -c $(CXXFLAGS) src/vector.cpp 

events.o: generated/events.cpp Makefile parse.pl
	c++ -c generated/events.cpp $(URHO_FLAGS) 

ApplicationProxy.o: src/ApplicationProxy.cpp src/ApplicationProxy.h Makefile
	c++ -c src/ApplicationProxy.cpp $(URHO_FLAGS)

check-links:
	@if test ! -e $(URHO_DIR)/include/SDL; then (cd $(URHO_DIR)/include; ln -s ../Source/ThirdParty/SDL/include SDL); fi
	@if test ! -e $(URHO_DIR)/include/Box2D; then (cd $(URHO_DIR)/include; ln -s ../Source/ThirdParty/Box2D/Box2D Box2D); fi
	@if test ! -e $(URHO_DIR)/include/Bullet; then (cd $(URHO_DIR)/include; ln -s ../Source/ThirdParty/Bullet/src Bullet); fi
	@if test ! -e $(URHO_DIR)/include/GLEW; then (cd $(URHO_DIR)/include; ln -s ../Source/ThirdParty/GLEW GLEW); fi
	@if test ! -e $(URHO_DIR)/include/kNet; then (cd $(URHO_DIR)/include; ln -s ../Source/ThirdParty/kNet/include kNet); fi

../SharpieBinder/bin/Debug/SharpieBinder.exe: ../SharpieBinder/CxxBinder.cs ../SharpieBinder/Program.cs
	(cd ../SharpieBinder; xbuild)

d: d.cpp Makefile
	c++ -c d.cpp $(URHO_FLAGS) 
	c++ -o d d.cpp $(URHO_FLAGS) $(URHO_LIBS)

p generated/events.cpp generated/Object.Events.cs: parse.pl
	perl parse.pl $(URHO_DIR)/include/Urho3d/*/*h

sample.exe: sample.cs sample-lib.dll Urho.dll
	mcs -unsafe -debug sample.cs -r:sample-lib.dll -r:Urho.dll 

sample-lib.dll: sample-lib.cs Urho.dll
	mcs -unsafe -debug -target:library sample-lib.cs -r:Urho.dll 

rs: sample.exe libmono-urho.dylib
	DYLD_LIBRARY_PATH=. $(mono64)/bin/mono --debug sample.exe

deb: sample.exe libmono-urho.dylib
	DYLD_LIBRARY_PATH=. gdb --args $(mono64)/bin/mono --debug sample.exe

clean:
	-rm sample.exe Urho.dll Urho.dll libmono-urho.dylib *.o d *pch

