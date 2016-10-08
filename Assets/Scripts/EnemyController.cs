using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject destination;

	public GameObject missile;
	public GameObject explosion;
	public GameObject smoke;

	private int health = 100;

	void Awake () {
		GetComponent<NavMeshAgent>().SetDestination(destination.transform.position);
	}

	void Update () {
		if (Random.value > 0.995) {
			Instantiate(missile, transform.position, transform.rotation);
		}
	}
	
	public void DealDamage (int amount) {
		health -= amount;

		if (health <= 0) {
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
}
