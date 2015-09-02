using Urho;

class Ragdoll : Component
{
	private readonly RigidBody body;

	public Ragdoll(Context context, RigidBody body) : base(context)
	{
		this.body = body;
		SubscribeToNodeCollision(HandleNodeCollision);
	}

	void HandleNodeCollision(NodeCollisionEventArgs args)
	{
		if (args.Body != body)
			return;

		// Get the other colliding body, make sure it is moving (has nonzero mass)
		RigidBody otherBody = args.OtherBody;

		if (otherBody.Mass > 0.0f)
		{
			// We do not need the physics components in the AnimatedModel's root scene node anymore
			Node.RemoveComponent<RigidBody>();
			Node.RemoveComponent<CollisionShape>();
		
			// Create RigidBody & CollisionShape components to bones
			CreateRagdollBone("Bip01_Pelvis", ShapeType.SHAPE_BOX, new Vector3(0.3f, 0.2f, 0.25f), new Vector3(0.0f, 0.0f, 0.0f),
				new Quaternion(0.0f, 0.0f, 0.0f));
			CreateRagdollBone("Bip01_Spine1", ShapeType.SHAPE_BOX, new Vector3(0.35f, 0.2f, 0.3f), new Vector3(0.15f, 0.0f, 0.0f),
				new Quaternion(0.0f, 0.0f, 0.0f));
			CreateRagdollBone("Bip01_L_Thigh", ShapeType.SHAPE_CAPSULE, new Vector3(0.175f, 0.45f, 0.175f), new Vector3(0.25f, 0.0f, 0.0f),
				new Quaternion(0.0f, 0.0f, 90.0f));
			CreateRagdollBone("Bip01_R_Thigh", ShapeType.SHAPE_CAPSULE, new Vector3(0.175f, 0.45f, 0.175f), new Vector3(0.25f, 0.0f, 0.0f),
				new Quaternion(0.0f, 0.0f, 90.0f));
			CreateRagdollBone("Bip01_L_Calf", ShapeType.SHAPE_CAPSULE, new Vector3(0.15f, 0.55f, 0.15f), new Vector3(0.25f, 0.0f, 0.0f),
				new Quaternion(0.0f, 0.0f, 90.0f));
			CreateRagdollBone("Bip01_R_Calf", ShapeType.SHAPE_CAPSULE, new Vector3(0.15f, 0.55f, 0.15f), new Vector3(0.25f, 0.0f, 0.0f),
				new Quaternion(0.0f, 0.0f, 90.0f));
			CreateRagdollBone("Bip01_Head", ShapeType.SHAPE_BOX, new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.1f, 0.0f, 0.0f),
				new Quaternion(0.0f, 0.0f, 0.0f));
			CreateRagdollBone("Bip01_L_UpperArm", ShapeType.SHAPE_CAPSULE, new Vector3(0.15f, 0.35f, 0.15f), new Vector3(0.1f, 0.0f, 0.0f),
				new Quaternion(0.0f, 0.0f, 90.0f));
			CreateRagdollBone("Bip01_R_UpperArm", ShapeType.SHAPE_CAPSULE, new Vector3(0.15f, 0.35f, 0.15f), new Vector3(0.1f, 0.0f, 0.0f),
				new Quaternion(0.0f, 0.0f, 90.0f));
			CreateRagdollBone("Bip01_L_Forearm", ShapeType.SHAPE_CAPSULE, new Vector3(0.125f, 0.4f, 0.125f), new Vector3(0.2f, 0.0f, 0.0f),
				new Quaternion(0.0f, 0.0f, 90.0f));
			CreateRagdollBone("Bip01_R_Forearm", ShapeType.SHAPE_CAPSULE, new Vector3(0.125f, 0.4f, 0.125f), new Vector3(0.2f, 0.0f, 0.0f),
				new Quaternion(0.0f, 0.0f, 90.0f));
		
			Vector3 back = new Vector3(0f, 0f, -1f);
			Vector3 forward = new Vector3(0f, 0f, 1f);
			Vector3 left = new Vector3(-1f, 0f, 0f);
			Vector3 down = new Vector3(0f, -1f, 0f);
			Vector3 up = new Vector3(0f, 1f, 0f);

			// Create Constraints between bones
			CreateRagdollConstraint("Bip01_L_Thigh", "Bip01_Pelvis", ConstraintType.ConeTwist, back, forward,
				new Vector2(45.0f, 45.0f), Vector2.Zero);
			CreateRagdollConstraint("Bip01_R_Thigh", "Bip01_Pelvis", ConstraintType.ConeTwist, back, forward,
				new Vector2(45.0f, 45.0f), Vector2.Zero);
			CreateRagdollConstraint("Bip01_L_Calf", "Bip01_L_Thigh", ConstraintType.Hinge, back, back,
				new Vector2(90.0f, 0.0f), Vector2.Zero);
			CreateRagdollConstraint("Bip01_R_Calf", "Bip01_R_Thigh", ConstraintType.Hinge, back, back,
				new Vector2(90.0f, 0.0f), Vector2.Zero);
			CreateRagdollConstraint("Bip01_Spine1", "Bip01_Pelvis", ConstraintType.Hinge, forward, forward,
				new Vector2(45.0f, 0.0f), new Vector2(-10.0f, 0.0f));
			CreateRagdollConstraint("Bip01_Head", "Bip01_Spine1", ConstraintType.ConeTwist, left, left,
				new Vector2(0.0f, 30.0f), Vector2.Zero);
			CreateRagdollConstraint("Bip01_L_UpperArm", "Bip01_Spine1", ConstraintType.ConeTwist, down, up,
				new Vector2(45.0f, 45.0f), Vector2.Zero, false);
			CreateRagdollConstraint("Bip01_R_UpperArm", "Bip01_Spine1", ConstraintType.ConeTwist, down, up,
				new Vector2(45.0f, 45.0f), Vector2.Zero, false);
			CreateRagdollConstraint("Bip01_L_Forearm", "Bip01_L_UpperArm", ConstraintType.Hinge, back, back,
				new Vector2(90.0f, 0.0f), Vector2.Zero);
			CreateRagdollConstraint("Bip01_R_Forearm", "Bip01_R_UpperArm", ConstraintType.Hinge, back, back,
				new Vector2(90.0f, 0.0f), Vector2.Zero);
		
			// Disable keyframe animation from all bones so that they will not interfere with the ragdoll
			AnimatedModel model = GetComponent<AnimatedModel>();
			Skeleton skeleton = model.Skeleton;
			for (uint i = 0; i < skeleton.NumBones; ++i)
			{
				//var offset = Marshal.OffsetOf(typeof(Bone), "_animated");
				skeleton.GetBoneSafe(i).Animated = false;
			}
		
			// Finally remove self from the scene node. Note that this must be the last operation performed in the function
			Remove();
		}
	}

	private void CreateRagdollBone(string boneName, ShapeType type, Vector3 size, Vector3 position, Quaternion rotation)
	{
		// Find the correct child scene node recursively
		Node boneNode = Node.GetChild(boneName, true);
		if (boneNode == null)
		{
			Log.Write(LogLevel.Warning, "Could not find bone " + boneName + " for creating ragdoll physics components");
			return;
		}
	
		RigidBody body = boneNode.CreateComponent<RigidBody>();
		// Set mass to make movable
		body.Mass = 1.0f;
		// Set damping parameters to smooth out the motion
		body.LinearDamping = 0.05f;
		body.AngularDamping = 0.85f;
		// Set rest thresholds to ensure the ragdoll rigid bodies come to rest to not consume CPU endlessly
		body.LinearRestThreshold = 1.5f;
		body.AngularRestThreshold = 2.5f;

		CollisionShape shape = boneNode.CreateComponent<CollisionShape>();
		// We use either a box or a capsule shape for all of the bones
		if (type == ShapeType.SHAPE_BOX)
			shape.SetBox(size, position, rotation);
		else
			shape.SetCapsule(size.X, size.Y, position, rotation);
	}

	private void CreateRagdollConstraint(string boneName, string parentName, ConstraintType type,
		Vector3 axis, Vector3 parentAxis, Vector2 highLimit, Vector2 lowLimit, bool disableCollision = true)
	{
		Node boneNode = Node.GetChild(boneName, true);
		Node parentNode = Node.GetChild(parentName, true);
		if (boneNode == null)
		{
			Log.Write(LogLevel.Warning, "Could not find bone " + boneName + " for creating ragdoll constraint");
			return;
		}
		if (parentNode == null)
		{
			Log.Write(LogLevel.Warning, "Could not find bone " + parentName + " for creating ragdoll constraint");
			return;
		}
	
		Constraint constraint = boneNode.CreateComponent<Constraint>();
		constraint.ConstraintType = type;
		// Most of the constraints in the ragdoll will work better when the connected bodies don't collide against each other
		constraint.DisableCollision = disableCollision;
		// The connected body must be specified before setting the world position
		constraint.OtherBody = parentNode.GetComponent<RigidBody>();
		// Position the constraint at the child bone we are connecting
		constraint.SetWorldPosition(boneNode.WorldPosition);
		// Configure axes and limits
		constraint.SetAxis(axis);
		constraint.SetOtherAxis(parentAxis);
		constraint.HighLimit = highLimit;
		constraint.LowLimit = lowLimit;
	}
}