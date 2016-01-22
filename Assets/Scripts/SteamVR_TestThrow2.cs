using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class SteamVR_TestThrow2 : MonoBehaviour
{
	public AudioClip pickUpSound;
	public GameObject prefab;
	public GameObject prefab1;
	public Rigidbody attachPoint;
	public GameObject grabBox;
	public GameObject go;
	//objects inside this trigger box can be picked up by the player (think of this as your reach)
	public float gap = 0.5f;
	public float checkRadius = 0.5f;
	public float weightChange = 0.3f;
	[HideInInspector]
	public GameObject heldObj;
	private Vector3 holdPos;
	//private FixedJoint joint1;
	private float timeOfPickup, timeOfThrow, defRotateSpeed;
	private Color gizmoColor;
	private RigidbodyInterpolation objectDefInterpolation;

	SteamVR_TrackedObject trackedObj;
	FixedJoint joint;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void FixedUpdate()
	{/*
		var device = SteamVR_Controller.Input((int)trackedObj.index);
		if (joint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			var go = GameObject.Instantiate(prefab);
			go.transform.position = attachPoint.transform.position;

			joint = go.AddComponent<FixedJoint>();
			joint.connectedBody = attachPoint;
		}
		else if (joint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
		{
			var go = joint.gameObject;
			var rigidbody = go.GetComponent<Rigidbody>();
			Object.DestroyImmediate(joint);
			joint = null;
			Object.Destroy(go, 15.0f);

			// We should probably apply the offset between trackedObj.transform.position
			// and device.transform.pos to insert into the physics sim at the correct
			// location, however, we would then want to predict ahead the visual representation
			// by the same amount we are predicting our render poses.

			var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
			if (origin != null)
			{
				rigidbody.velocity = origin.TransformVector(device.velocity);
				rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
			}
			else
			{
				rigidbody.velocity = device.velocity;
				rigidbody.angularVelocity = device.angularVelocity;
			}

			rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
		}*/
	}
	void OnTriggerStay(Collider other)
	{		
			var device = SteamVR_Controller.Input((int)trackedObj.index);
			if (joint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
			{
			if (other.tag == "Pickup" || other.tag == "yellowbox") 
			{

				//var go = GameObject.Instantiate(prefab1);
				go = other.gameObject;
				go.transform.position = attachPoint.transform.position;
				go.transform.rotation = attachPoint.transform.rotation;
				
				joint = go.AddComponent<FixedJoint>();
				joint.connectedBody = attachPoint;
				//Destroy(other.gameObject);
				//LiftPickup (other);
			}
			}

			else if (joint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
			{
				var go = joint.gameObject;
				var rigidbody = go.GetComponent<Rigidbody>();
				Object.Destroy(joint);
				joint = null;
				//Object.Destroy(go, 15.0f);
				
				// We should probably apply the offset between trackedObj.transform.position
				// and device.transform.pos to insert into the physics sim at the correct
				// location, however, we would then want to predict ahead the visual representation
				// by the same amount we are predicting our render poses.
				
				var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
				if (origin != null)
				{
					rigidbody.velocity = origin.TransformVector(device.velocity);
					rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
				}
				else
				{
					rigidbody.velocity = device.velocity;
					rigidbody.angularVelocity = device.angularVelocity;
				}
				
				rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
			}
		}
	
	private void LiftPickup(Collider other)
	{
		//get where to move item once its picked up
		Mesh otherMesh = other.GetComponent<MeshFilter>().mesh;
		holdPos = transform.position;
		holdPos.y += (GetComponent<Collider>().bounds.extents.y) + (otherMesh.bounds.extents.y) + gap;
		
		//if there is space above our head, pick up item (layermask index 2: "Ignore Raycast", anything on this layer will be ignored)
		if(!Physics.CheckSphere(holdPos, checkRadius, 2))
		{
			//gizmoColor = Color.green;
			heldObj = other.gameObject;
			objectDefInterpolation = heldObj.GetComponent<Rigidbody>().interpolation;
			heldObj.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
			heldObj.transform.position = holdPos;
			heldObj.transform.rotation = transform.rotation;
			AddJoint();
			//here we adjust the mass of the object, so it can seem heavy, but not effect player movement whilst were holding it
			//heldObj.GetComponent<Rigidbody>().mass *= weightChange;
			//make sure we don't immediately throw object after picking it up
			timeOfPickup = Time.time;
		}
		//if not print to console (look in scene view for sphere gizmo to see whats stopping the pickup)
		else
		{
			//gizmoColor = Color.red;
			//print ("Can't lift object here. If nothing is above the player, make sure triggers are set to layer index 2 (ignore raycast by default)");
		}
	}
	private void AddJoint()
	{
		if (heldObj)
		{
			if(pickUpSound)
			{
				GetComponent<AudioSource>().volume = 1;
				GetComponent<AudioSource>().clip = pickUpSound;
				GetComponent<AudioSource>().Play ();
			}
			joint = heldObj.AddComponent<FixedJoint>();
			joint.connectedBody = GetComponent<Rigidbody>();
		}
	}
}

