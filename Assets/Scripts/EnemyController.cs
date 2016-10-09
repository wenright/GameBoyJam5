using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject missile;
	public GameObject explosion;
	public GameObject smoke;

	private int health = 100;
	private int worth = 250;

	private GameObject destination;

	void Awake () {
		destination = GameObject.FindGameObjectWithTag("Destination");

		if (destination == null) {
			print("Unable to find a destination for enemy");
		}

		GetComponent<NavMeshAgent>().SetDestination(destination.transform.position);

		InvokeRepeating("LaunchMissile", 2.0f, 10.0f);
	}

	public void DealDamage (int amount) {
		health -= amount;

		if (health <= 0) {
			UpgradesController.money += worth;

			GameObject e = Instantiate(explosion, transform.position, transform.rotation) as GameObject;

			// Make a bigger explosion to let player know this vehicle has been destroyed
			e.transform.GetChild(2).GetComponent<ParticleSystem>().startSize = 0.5f;
			e.transform.GetChild(2).GetComponent<ParticleSystem>().startLifetime = 0.5f;
			e.transform.GetChild(3).GetComponent<ParticleSystem>().startSize = 100;
			e.transform.GetChild(4).GetComponent<ParticleSystem>().startSize = 750;
			Destroy(e, 2);

			Destroy(Instantiate(smoke, transform.position, transform.rotation), 15);
			Destroy(gameObject);
		}
	}

	private void LaunchMissile () {
		Instantiate(missile, transform.position, transform.rotation);
	}
}
