using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject), typeof(AudioSource))]
public class SteamVR_TestThrow1 : MonoBehaviour
{
	public GameObject prefab;
	public Rigidbody attachPoint;
	public GameObject cooldownIndicator;
	public AudioClip igniteSound;
	public Rigidbody projectile;
	public float speed = 20;

	SteamVR_TrackedObject trackedObj;
	FixedJoint joint;

	float throwCooldownTime = 2.0f; // Seconds
	float currentCoolDownTime = 0.0f;
	GameObject cooldownInstance;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		cooldownInstance = GameObject.Instantiate(cooldownIndicator);
        cooldownInstance.transform.localScale = Vector3.one * 0.05f;

    }

	void FixedUpdate()
    {// Update cool down timer
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (currentCoolDownTime > 0.0f)//do scaling
        {
            if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
                currentCoolDownTime -= Time.deltaTime;
            else
                currentCoolDownTime += Time.deltaTime;

            if (currentCoolDownTime > 0.0f)
            {
                cooldownInstance.transform.localScale = Vector3.Lerp(Vector3.one * 0.05f, Vector3.one*0.5f, (throwCooldownTime - currentCoolDownTime) / throwCooldownTime);
            }
            else {
                currentCoolDownTime = 0.0f;
                cooldownInstance.GetComponent<Renderer>().enabled = true;
            }
        }

       
        if (joint == null && device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger))
        {
            if (currentCoolDownTime == 0.0f)
            {
                currentCoolDownTime = throwCooldownTime;

                /*var go = GameObject.Instantiate(prefab);
				go.transform.position = attachPoint.transform.position;

				joint = go.AddComponent<FixedJoint>();
				joint.connectedBody = attachPoint;*/

                Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
                instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
                AudioSource.PlayClipAtPoint(igniteSound, attachPoint.transform.position);

            }
        }
        // Update cool down timer
        /*if (currentCoolDownTime > 0.0f) {
			currentCoolDownTime -= Time.deltaTime;
			if (currentCoolDownTime > 0.0f) {
				cooldownInstance.GetComponent<Renderer> ().enabled = false;
			}
			else {
				currentCoolDownTime = 0.0f;
				cooldownInstance.GetComponent<Renderer> ().enabled = true;
			}
		}

		var device = SteamVR_Controller.Input((int)trackedObj.index);
		if (joint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			if (currentCoolDownTime == 0.0f) {
				currentCoolDownTime = throwCooldownTime;

				/*var go = GameObject.Instantiate(prefab);
				go.transform.position = attachPoint.transform.position;

				joint = go.AddComponent<FixedJoint>();
				joint.connectedBody = attachPoint;

				Rigidbody instantiatedProjectile = Instantiate(projectile,transform.position,transform.rotation)as Rigidbody;
				instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0,speed));
				AudioSource.PlayClipAtPoint(igniteSound, attachPoint.transform.position);

			} 
		}*/
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

		// Position cool down indicator
		cooldownInstance.transform.position = attachPoint.transform.position;
	}
}
