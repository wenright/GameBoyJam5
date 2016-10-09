using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public GameObject explosion;

	public int speed = 50000;

	private int randomSpeed = 600;
	private int lifetime = 10;

	public int explosionRadius = 3;
	public int damage = 25;

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
				expl.transform.GetChild(2).GetComponent<ParticleSystem>().startLifetime = 0.2f * (damage / 200.0f);
				expl.transform.GetChild(2).GetComponent<ParticleSystem>().startSize = 0.7f;
			}

			foreach (Collider c in Physics.OverlapSphere(hit.point, explosionRadius)) {
				if (c.tag == "Enemy") {
					EnemyController e = c.GetComponent<EnemyController>();

					if (e != null) {
						e.DealDamage(CalculateDamage(hit.point, c));
					}
				} else if (c.tag == "Player") {
					int d = CalculateDamage(hit.point, c);
					print(d);
					c.GetComponent<PlayerController>().Damage(d);
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

	private int CalculateDamage (Vector3 hitPoint, Collider c) {
		float d = Vector3.Distance(hitPoint, c.gameObject.transform.position) / explosionRadius;
		return (int) Mathf.Round((d * 5) * damage);
	}
}
