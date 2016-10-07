using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject destination;

	public GameObject explosion;
	public GameObject smoke;

	private int health = 100;

	void Awake () {
		GetComponent<NavMeshAgent>().SetDestination(destination.transform.position);
	}
	
	public void DealDamage (int amount) {
		health -= amount;

		if (health <= 0) {
			Destroy(Instantiate(explosion, transform.position, transform.rotation), 2);
			Destroy(Instantiate(smoke, transform.position, transform.rotation), 15);
			Destroy(gameObject);
		}
	}
}
