using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SamuraiHealthController : MonoBehaviour {

	public GameObject explosionPrefab;
	public AudioClip explosionSound;
	public AudioClip deathSound;

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
			AudioSource.PlayClipAtPoint(explosionSound, explosion.transform.position);

      // Remove fireball
      Destroy(collision.gameObject);

			// Subtract health
			int healthLost = getHealthCost(collision.gameObject.name);
			currentHealth -= healthLost;
            Debug.Log(currentHealth);

			// Check for death
			if (currentHealth <= 0) {
				var samuraiExplosion = GameObject.Instantiate(explosionPrefab);
				samuraiExplosion.transform.position = gameObject.transform.position;
				AudioSource.PlayClipAtPoint(deathSound, samuraiExplosion.transform.position);
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
