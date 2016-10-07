using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject explosion;

	private int speed = 50000;
	private int randomSpeed = 500;
	private int lifetime = 2;
	private int explosionRadius = 3;
	private int damage = 25;

	private Vector3 lastPosition;

	void Start () {
		lastPosition = transform.position;
		GetComponent<Rigidbody>().AddForce(transform.forward * speed + Random.insideUnitSphere * randomSpeed);
		Destroy(gameObject, lifetime);
	}

	void FixedUpdate () {
		RaycastHit hit;

		if (Physics.Linecast(lastPosition, transform.position, out hit)) {
			Destroy(Instantiate(explosion, hit.point, transform.rotation), 2);
			foreach (Collider c in Physics.OverlapSphere(hit.point, explosionRadius)) {
				EnemyController e = c.GetComponent<EnemyController>();

				if (e != null) {
					e.DealDamage(damage);
				}
			}
		}

		lastPosition = transform.position;
	}
}
