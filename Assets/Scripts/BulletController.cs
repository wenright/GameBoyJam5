using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public GameObject explosion;

	private int speed = 50000;
	private int randomSpeed = 600;
	private int lifetime = 10;
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
			GameObject expl = Instantiate(explosion, hit.point, transform.rotation) as GameObject;
			Destroy(expl, 2);

			// Change Shower size depending on what is hit
			if (hit.transform.tag == "Enemy") {
				expl.transform.GetChild(2).GetComponent<ParticleSystem>().startLifetime = 0.2f;
				expl.transform.GetChild(2).GetComponent<ParticleSystem>().startSize = 0.7f;
			}

			foreach (Collider c in Physics.OverlapSphere(hit.point, explosionRadius)) {
				EnemyController e = c.GetComponent<EnemyController>();

				if (e != null) {
					e.DealDamage(damage);
				}
			}

			if (hit.transform.tag == "Missile") {
				// On the off chance that a player actually shoots down a missile...
				Destroy(hit.transform.gameObject);
			}

			GameObject.FindGameObjectsWithTag("Sun")[0].GetComponent<LightController>().Flash(damage);


			Destroy(gameObject);
		}

		lastPosition = transform.position;
	}
}
