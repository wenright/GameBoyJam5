using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject explosion;

	private int speed = 30000;
	private int lifetime = 2;

	private Vector3 lastPosition;

	void Start () {
		lastPosition = transform.position;
		GetComponent<Rigidbody>().AddForce(transform.forward * speed);
		Destroy(gameObject, lifetime);
	}

	void FixedUpdate () {
		RaycastHit hit;

		if (Physics.Linecast(lastPosition, transform.position, out hit)) {
			Destroy(Instantiate(explosion, lastPosition, transform.rotation), 2);
			// TODO do damage here
		}

		lastPosition = transform.position;
	}
}
