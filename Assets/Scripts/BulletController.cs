using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public GameObject explosion;

	private int speed = 50000;
	private int randomSpeed = 600;
	private int lifetime = 10;

	private int explosionRadius = UpgradesController.explosionRadius;
	private int damage = UpgradesController.damage;

	private Vector3 lastPosition;

	void Start () {
		lastPosition = transform.position;
		GetComponent<Rigidbody>().AddForce(transform.forward * speed + Random.insideUnitSphere * randomSpeed * UpgradesController.accuracy);
		Destroy(gameObject, lifetime);
	}

	void FixedUpdate () {
		// Then check for collisions based on impact
		RaycastHit hit;

		if (Physics.Linecast(lastPosition, transform.position, out hit)) {
			GameObject expl = Instantiate(explosion, hit.point, transform.rotation) as GameObject;
			Destroy(expl, 2);

			// Change Shower size depending on what is hit
			if (hit.transform.tag == "Enemy") {
				expl.transform.GetChild(2).GetComponent<ParticleSystem>().startLifetime = 0.2f * (damage / 200.0f);
				expl.transform.GetChild(2).GetComponent<ParticleSystem>().startSize = 0.7f;
			}

			// Check for collisions based on proximity
			foreach (Collider c in Physics.OverlapSphere(hit.point, explosionRadius)) {
				if (c.tag == "Enemy") {
					EnemyController e = c.GetComponent<EnemyController>();

					if (e != null) {
						e.Damage(CalculateDamage(hit.point, c));
					}
				}
			}

			GameObject.FindGameObjectWithTag("Sun").GetComponent<LightController>().Flash(damage);

			Destroy(gameObject);
		}

		lastPosition = transform.position;
	}

	private int CalculateDamage (Vector3 hitPoint, Collider c) {
		float d = Vector3.Distance(hitPoint, c.gameObject.transform.position) / explosionRadius;
		return (int) Mathf.Round((d * 5) * damage);
	}
}
