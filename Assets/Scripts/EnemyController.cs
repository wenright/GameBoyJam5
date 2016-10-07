using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject explosion;
	public GameObject smoke;

	private int health = 100;
	
	public void DealDamage (int amount) {
		health -= amount;

		if (health <= 0) {
			Destroy(Instantiate(explosion, transform.position, transform.rotation), 2);
			Destroy(Instantiate(smoke, transform.position, transform.rotation), 15);
			Destroy(gameObject);
		}
	}
}
