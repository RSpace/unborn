using UnityEngine;
using System.Collections;

public class SamuraiHealthController : MonoBehaviour {

	public GameObject explosionPrefab;

	int currentHealth = 100;

	void Start () {
	
	}
	
	void FixedUpdate () {

	}

	void OnCollisionEnter (Collision collision)
	{
		if(collision.gameObject.tag == "FriendlyFire")
		{
			// Add explosion
			var explosion = GameObject.Instantiate(explosionPrefab);
			explosion.transform.position = collision.gameObject.transform.position;
            Debug.Log(collision.gameObject.name);

            // Remove fireball
            Destroy(collision.gameObject);

			// Subtract health
			int healthLost = getHealthCost(collision.gameObject.name);
			currentHealth -= healthLost;
            Debug.Log(currentHealth);

			// Check for death
			if (currentHealth <= 0) {
				var samuraiExplosion = GameObject.Instantiate(explosionPrefab);
				samuraiExplosion.transform.position = collision.gameObject.transform.position;
				Destroy(gameObject);
			}
		}
	}

	int getHealthCost(string name) {
		switch (name) {
		case "MobileSun(Clone)":
			return 50;
		default:
			return 0;
		}
	}
}
