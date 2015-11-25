using namespace Urho3D;

void check_size(int actual, int expected, const char * typeName)
{
	if (actual != expected)
		printf("sizeof(%s) is %d but %d is expected", typeName, actual, expected);
}

void check_offset(int actual, int expected, const char* typeName, const char* fieldName)
{
	if (actual != expected)
		printf("offset(%s, %s) is %d but %d is expected", typeName, fieldName, actual, expected);
}

// TESTS ARE GENERATED FOR 64bit. MAKE SURE YOU USE THE SAME ARCHITECTURE WHILE RUNNING THESE TESTS!
void check_bindings_offsets()
{

	// IntVector2:
	check_size(sizeof(IntVector2), 8, "IntVector2");
	check_offset(offsetof(IntVector2, x_), 0, "IntVector2", "X");
	check_offset(offsetof(IntVector2, y_), 4, "IntVector2", "Y");

	// Plane:
	check_size(sizeof(Plane), 28, "Plane");
	check_offset(offsetof(Plane, normal_), 0, "Plane", "Normal");
	check_offset(offsetof(Plane, absNormal_), 12, "Plane", "AbsNormal");
	check_offset(offsetof(Plane, d_), 24, "Plane", "D");

	// Ray:
	check_size(sizeof(Ray), 24, "Ray");
	check_offset(offsetof(Ray, origin_), 0, "Ray", "Origin");
	check_offset(offsetof(Ray, direction_), 12, "Ray", "Direction");

	// IntRect:
	check_size(sizeof(IntRect), 16, "IntRect");
	check_offset(offsetof(IntRect, left_), 0, "IntRect", "Left");
	check_offset(offsetof(IntRect, top_), 4, "IntRect", "Top");
	check_offset(offsetof(IntRect, right_), 8, "IntRect", "Right");
	check_offset(offsetof(IntRect, bottom_), 12, "IntRect", "Bottom");

	// Rect:
	check_size(sizeof(Rect), 20, "Rect");
	check_offset(offsetof(Rect, min_), 0, "Rect", "Min");
	check_offset(offsetof(Rect, max_), 8, "Rect", "Max");
	check_offset(offsetof(Rect, defined_), 16, "Rect", "defined");

	// ResourceRef:
	check_size(sizeof(ResourceRef), 24, "ResourceRef");
	check_offset(offsetof(ResourceRef, type_), 0, "ResourceRef", "Type");
	check_offset(offsetof(ResourceRef, name_), 8, "ResourceRef", "Name");

	// BoundingBox:
	check_size(sizeof(BoundingBox), 28, "BoundingBox");
	check_offset(offsetof(BoundingBox, min_), 0, "BoundingBox", "Min");
	check_offset(offsetof(BoundingBox, max_), 12, "BoundingBox", "Max");
	check_offset(offsetof(BoundingBox, defined_), 24, "BoundingBox", "defined");

	// AnimationTriggerPoint:
	check_size(sizeof(AnimationTriggerPoint), 16, "AnimationTriggerPoint");
	check_offset(offsetof(AnimationTriggerPoint, time_), 0, "AnimationTriggerPoint", "Time");
	check_offset(offsetof(AnimationTriggerPoint, data_), 8, "AnimationTriggerPoint", "Variant");

	// Matrix3x4:
	check_size(sizeof(Matrix3x4), 48, "Matrix3x4");
	check_offset(offsetof(Matrix3x4, m00_), 0, "Matrix3x4", "m00");
	check_offset(offsetof(Matrix3x4, m01_), 4, "Matrix3x4", "m01");
	check_offset(offsetof(Matrix3x4, m02_), 8, "Matrix3x4", "m02");
	check_offset(offsetof(Matrix3x4, m03_), 12, "Matrix3x4", "m03");
	check_offset(offsetof(Matrix3x4, m10_), 16, "Matrix3x4", "m10");
	check_offset(offsetof(Matrix3x4, m11_), 20, "Matrix3x4", "m11");
	check_offset(offsetof(Matrix3x4, m12_), 24, "Matrix3x4", "m12");
	check_offset(offsetof(Matrix3x4, m13_), 28, "Matrix3x4", "m13");
	check_offset(offsetof(Matrix3x4, m20_), 32, "Matrix3x4", "m20");
	check_offset(offsetof(Matrix3x4, m21_), 36, "Matrix3x4", "m21");
	check_offset(offsetof(Matrix3x4, m22_), 40, "Matrix3x4", "m22");
	check_offset(offsetof(Matrix3x4, m23_), 44, "Matrix3x4", "m23");

	// Color:
	check_size(sizeof(Color), 16, "Color");
	check_offset(offsetof(Color, r_), 0, "Color", "R");
	check_offset(offsetof(Color, g_), 4, "Color", "G");
	check_offset(offsetof(Color, b_), 8, "Color", "B");
	check_offset(offsetof(Color, a_), 12, "Color", "A");

	// TouchState:
	check_size(sizeof(TouchState), 48, "TouchState");
	check_offset(offsetof(TouchState, touchID_), 0, "TouchState", "TouchID");
	check_offset(offsetof(TouchState, position_), 4, "TouchState", "Position");
	check_offset(offsetof(TouchState, lastPosition_), 12, "TouchState", "LastPosition");
	check_offset(offsetof(TouchState, delta_), 20, "TouchState", "Delta");
	check_offset(offsetof(TouchState, pressure_), 28, "TouchState", "Pressure");
	check_offset(offsetof(TouchState, touchedElement_), 32, "TouchState", "_TouchedElement");

	// JoystickState:
	check_size(sizeof(JoystickState), 112, "JoystickState");
	check_offset(offsetof(JoystickState, joystick_), 0, "JoystickState", "JoystickPtr");
	check_offset(offsetof(JoystickState, joystickID_), 8, "JoystickState", "JoystickIdPtr");
	check_offset(offsetof(JoystickState, controller_), 16, "JoystickState", "ControllerPtr");
	check_offset(offsetof(JoystickState, screenJoystick_), 24, "JoystickState", "ScreenJoystickPtr");
	check_offset(offsetof(JoystickState, name_), 32, "JoystickState", "Name");
	check_offset(offsetof(JoystickState, buttons_), 48, "JoystickState", "Buttons");
	check_offset(offsetof(JoystickState, buttonPress_), 64, "JoystickState", "ButtonPress");
	check_offset(offsetof(JoystickState, axes_), 80, "JoystickState", "Axes");
	check_offset(offsetof(JoystickState, hats_), 96, "JoystickState", "Hats");

	// Bone:
	check_size(sizeof(Bone), 168, "Bone");
	check_offset(offsetof(Bone, name_), 0, "Bone", "Name");
	check_offset(offsetof(Bone, nameHash_), 16, "Bone", "NameHash");
	check_offset(offsetof(Bone, parentIndex_), 20, "Bone", "ParentIndex");
	check_offset(offsetof(Bone, initialPosition_), 24, "Bone", "InitialPosition");
	check_offset(offsetof(Bone, initialRotation_), 36, "Bone", "InitialRotation");
	check_offset(offsetof(Bone, initialScale_), 52, "Bone", "InitialScale");
	check_offset(offsetof(Bone, offsetMatrix_), 64, "Bone", "OffsetMatrix");
	check_offset(offsetof(Bone, animated_), 112, "Bone", "animated");
	check_offset(offsetof(Bone, collisionMask_), 113, "Bone", "CollisionMask");
	check_offset(offsetof(Bone, radius_), 116, "Bone", "Radius");
	check_offset(offsetof(Bone, boundingBox_), 120, "Bone", "BoundingBox");
	check_offset(offsetof(Bone, node_), 152, "Bone", "Node");

	// RayQueryResult:
	check_size(sizeof(RayQueryResult), 64, "RayQueryResult");
	check_offset(offsetof(RayQueryResult, position_), 0, "RayQueryResult", "Position");
	check_offset(offsetof(RayQueryResult, normal_), 12, "RayQueryResult", "Normal");
	check_offset(offsetof(RayQueryResult, textureUV_), 24, "RayQueryResult", "TextureUV");
	check_offset(offsetof(RayQueryResult, distance_), 32, "RayQueryResult", "Distance");
	check_offset(offsetof(RayQueryResult, drawable_), 40, "RayQueryResult", "drawablePtr");
	check_offset(offsetof(RayQueryResult, node_), 48, "RayQueryResult", "nodePtr");
	check_offset(offsetof(RayQueryResult, subObject_), 56, "RayQueryResult", "SubObject");

	// Billboard:
	check_size(sizeof(Billboard), 68, "Billboard");
	check_offset(offsetof(Billboard, position_), 0, "Billboard", "Position");
	check_offset(offsetof(Billboard, size_), 12, "Billboard", "Size");
	check_offset(offsetof(Billboard, uv_), 20, "Billboard", "Uv");
	check_offset(offsetof(Billboard, color_), 40, "Billboard", "Color");
	check_offset(offsetof(Billboard, rotation_), 56, "Billboard", "Rotation");
	check_offset(offsetof(Billboard, enabled_), 60, "Billboard", "enabled");
	check_offset(offsetof(Billboard, sortDistance_), 64, "Billboard", "SortDistance");

	// BiasParameters:
	check_size(sizeof(BiasParameters), 8, "BiasParameters");
	check_offset(offsetof(BiasParameters, constantBias_), 0, "BiasParameters", "ConstantBias");
	check_offset(offsetof(BiasParameters, slopeScaledBias_), 4, "BiasParameters", "SlopeScaleBias");

	// FocusParameters:
	check_size(sizeof(FocusParameters), 12, "FocusParameters");
	check_offset(offsetof(FocusParameters, focus_), 0, "FocusParameters", "Focus");
	check_offset(offsetof(FocusParameters, nonUniform_), 1, "FocusParameters", "NonUniform");
	check_offset(offsetof(FocusParameters, autoSize_), 2, "FocusParameters", "AutoSize");
	check_offset(offsetof(FocusParameters, quantize_), 4, "FocusParameters", "Quantize");
	check_offset(offsetof(FocusParameters, minView_), 8, "FocusParameters", "MinView");

	// Vector3:
	check_size(sizeof(Vector3), 12, "Vector3");
	check_offset(offsetof(Vector3, x_), 0, "Vector3", "X");
	check_offset(offsetof(Vector3, y_), 4, "Vector3", "Y");
	check_offset(offsetof(Vector3, z_), 8, "Vector3", "Z");

	// Vector2:
	check_size(sizeof(Vector2), 8, "Vector2");
	check_offset(offsetof(Vector2, x_), 0, "Vector2", "X");
	check_offset(offsetof(Vector2, y_), 4, "Vector2", "Y");

	// Vector4:
	check_size(sizeof(Vector4), 16, "Vector4");
	check_offset(offsetof(Vector4, x_), 0, "Vector4", "X");
	check_offset(offsetof(Vector4, y_), 4, "Vector4", "Y");
	check_offset(offsetof(Vector4, z_), 8, "Vector4", "Z");
	check_offset(offsetof(Vector4, w_), 12, "Vector4", "W");

	// TileMapInfo2D:
	check_size(sizeof(TileMapInfo2D), 20, "TileMapInfo2D");
	check_offset(offsetof(TileMapInfo2D, orientation_), 0, "TileMapInfo2D", "Orientation");
	check_offset(offsetof(TileMapInfo2D, width_), 4, "TileMapInfo2D", "Width");
	check_offset(offsetof(TileMapInfo2D, height_), 8, "TileMapInfo2D", "Height");
	check_offset(offsetof(TileMapInfo2D, tileWidth_), 12, "TileMapInfo2D", "TileWidth");
	check_offset(offsetof(TileMapInfo2D, tileHeight_), 16, "TileMapInfo2D", "TileHeight");

	// CrowdObstacleAvoidanceParams:
	check_size(sizeof(CrowdObstacleAvoidanceParams), 28, "CrowdObstacleAvoidanceParams");
	check_offset(offsetof(CrowdObstacleAvoidanceParams, velBias), 0, "CrowdObstacleAvoidanceParams", "VelBias");
	check_offset(offsetof(CrowdObstacleAvoidanceParams, weightDesVel), 4, "CrowdObstacleAvoidanceParams", "WeightDesVel");
	check_offset(offsetof(CrowdObstacleAvoidanceParams, weightCurVel), 8, "CrowdObstacleAvoidanceParams", "WeightCurVel");
	check_offset(offsetof(CrowdObstacleAvoidanceParams, weightSide), 12, "CrowdObstacleAvoidanceParams", "WeightSide");
	check_offset(offsetof(CrowdObstacleAvoidanceParams, weightToi), 16, "CrowdObstacleAvoidanceParams", "WeightToi");
	check_offset(offsetof(CrowdObstacleAvoidanceParams, horizTime), 20, "CrowdObstacleAvoidanceParams", "HorizTime");
	check_offset(offsetof(CrowdObstacleAvoidanceParams, gridSize), 24, "CrowdObstacleAvoidanceParams", "GridSize");
	check_offset(offsetof(CrowdObstacleAvoidanceParams, adaptiveDivs), 25, "CrowdObstacleAvoidanceParams", "AdaptiveDivs");
	check_offset(offsetof(CrowdObstacleAvoidanceParams, adaptiveRings), 26, "CrowdObstacleAvoidanceParams", "AdaptiveRings");
	check_offset(offsetof(CrowdObstacleAvoidanceParams, adaptiveDepth), 27, "CrowdObstacleAvoidanceParams", "AdaptiveDepth");

	// PhysicsRaycastResult:
	check_size(sizeof(PhysicsRaycastResult), 40, "PhysicsRaycastResult");
	check_offset(offsetof(PhysicsRaycastResult, position_), 0, "PhysicsRaycastResult", "Position");
	check_offset(offsetof(PhysicsRaycastResult, normal_), 12, "PhysicsRaycastResult", "Normal");
	check_offset(offsetof(PhysicsRaycastResult, distance_), 24, "PhysicsRaycastResult", "Distance");
	check_offset(offsetof(PhysicsRaycastResult, body_), 32, "PhysicsRaycastResult", "bodyPtr");
}

/* Empty structs (stubs?):

  CollisionGeometryData
  WorkItem
  RefCount
  HashIteratorBase
  Iterator
  ResourceRefList
  Frustum
  Variant
  ColorFrame
  TextureFrame
  LightBatchQueue
  ReplicationState
  NodeReplicationState
  RenderPathCommand
  GPUObject
  GraphicsImpl
  FontGlyph
  RandomAccessIterator
  ModelMorph
  Octant
  CompressedLevel
  AnimationTrack
  CustomGeometryVertex
  NetworkState
  ComponentReplicationState
  ShaderParameter
  PackageEntry
  dtQueryFilter
  XPathResultSet

*/
