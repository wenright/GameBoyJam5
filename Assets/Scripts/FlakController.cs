using UnityEngine;
using System.Collections;

public class FlakController : MonoBehaviour {

	public GameObject explosion;

	private int speed = 75000;
	private int randomSpeed = 600;
	private int lifetime = 10;

	private int explosionRadius = 10;
	private int damage = 8;

	void Start () {
		GetComponent<Rigidbody>().AddForce(transform.forward * speed + Random.insideUnitSphere * randomSpeed);
		Destroy(gameObject, lifetime);
	}

	void Update () {
		// Check for collisions based on proximity
		foreach (Collider c in Physics.OverlapSphere(transform.position, explosionRadius)) {
			if (c.tag == "Player") {
				PlayerController p = c.GetComponent<PlayerController>();

				if (p != null) {
					p.Damage(CalculateDamage(transform.position, c));
				}

				Explode();
			}
		}
	}

	private int CalculateDamage (Vector3 hitPoint, Collider c) {
		float d = Vector3.Distance(hitPoint, c.gameObject.transform.position) / explosionRadius;
		return (int) Mathf.Round((d * 5) * damage);
	}

	private void Explode () {
		GameObject expl = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
		expl.transform.GetChild(2).GetComponent<ParticleSystem>().startLifetime = 0.3f;
		expl.transform.GetChild(2).GetComponent<ParticleSystem>().startSize = 0.7f;
		Destroy(expl, 2);

		GameObject.FindGameObjectWithTag("Sun").GetComponent<LightController>().Flash(damage);

		Destroy(gameObject);
	}
}
