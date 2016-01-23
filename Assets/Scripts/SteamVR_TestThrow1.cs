﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class SteamVR_TestThrow1 : MonoBehaviour
{
	public GameObject prefab;
	public Rigidbody attachPoint;

	SteamVR_TrackedObject trackedObj;
	FixedJoint joint;

	float throwCooldownTime = 2.0f; // Seconds
	float currentCoolDownTime = 0.0f;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void FixedUpdate()
	{
		// Update cool down timer
		if (currentCoolDownTime > 0.0f) {
			currentCoolDownTime -= Time.deltaTime;
			if (currentCoolDownTime < 0.0f) {
				currentCoolDownTime = 0.0f;
			}
		}

		var device = SteamVR_Controller.Input((int)trackedObj.index);
		if (joint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			if (currentCoolDownTime == 0.0f) {
				currentCoolDownTime = throwCooldownTime;

				var go = GameObject.Instantiate(prefab);
				go.transform.position = attachPoint.transform.position;

				joint = go.AddComponent<FixedJoint>();
				joint.connectedBody = attachPoint;
			} 
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
		}
	}
}
