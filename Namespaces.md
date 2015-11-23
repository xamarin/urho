Some ideas for namespacing stuff:

The idea is to remove some stuff that is now always used, but keep Urho namespace as 
useful by default.   That means not to do a strict mapping of the Urho3d directory
structure to namespace, but take out things that might not be commonly used outside.

* Move actions to Urho.Actions
* Move 2D APIs to Urho.Urho2D
  * AnimatedSprite2D
  * AnimationSet2D
  * CollisionBox2D
  * CollisionChain2D
  * CollisionCircle2D
  * CollisionEdge2D
  * CollisionPolygon2D
  * CollisionShape2D
  * Constraint2D
  * ConstraintDistance2D
  * ConstraintFriction2D
  * ConstraintGear2D
  * ConstraintMotor2D
  * ConstraintMouse2D
  * ConstraintPrismatic2D
  * ConstraintPulley2D
  * ConstraintRevolute2D
  * ConstraintRope2D
  * ConstraintWeld2D
  * ConstraintWheel2D
  * Drawable2D
  * ParticleEffect2D
  * ParticleEmitter2D
  * PhysicsEvents2D
  * PhysicsUtils2D
  * PhysicsWorld2D
  * Renderer2D
  * RigidBody2D
  * Sprite2D
  * SpriteSheet2D
  * SpriterData2D
  * SpriterInstance2D
  * StaticSprite2D
  * TileMap2D
  * TileMapDefs2D
  * TileMapLayer2D
  * TmxFile2D
  * Urho2D
* UI Elements:
  * BorderImage
  * Button
  * CheckBox
  * Cursor
  * DropDownList
  * FileSelector
  * Font
  * FontFace
  * FontFaceBitmap
  * FontFaceFreeType
  * LineEdit
  * ListView
  * Menu
  * MessageBox
  * ScrollBar
  * ScrollView
  * Slider
  * Sprite
  * Text
  * Text3D
  * ToolTip
  * UI
  * UIBat
  * UIBatch
  * UIElement
  * UIEvents
  * View3D
  * Window

