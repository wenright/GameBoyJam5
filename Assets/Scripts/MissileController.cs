using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {

	public GameObject explosion;

	private GameObject target;
	private int speed = 175;
	private int explosionRadius = 5;
	private float startTime;

	void Start () {
		startTime = Time.time;

		target = GameObject.FindGameObjectWithTag("Player");

		if (target == null || target.GetComponent<PlayerController>() == null) {
			print("Cannot find player");
		} else {
			target.GetComponent<PlayerController>().LockOn(gameObject);
		}
	}

	void Update () {
		if (target == null) {
			Kill();
			return;
		}

		transform.LookAt(target.transform.position);
		transform.Translate(Vector3.forward * speed * Time.deltaTime);

		Debug.DrawLine(transform.position, target.transform.position);

		foreach (Collider c in Physics.OverlapSphere(transform.position, explosionRadius)) {
			if (c.gameObject.tag == "Player") {
				if (target.GetComponent<PlayerController>() != null) {
					target.GetComponent<PlayerController>().Damage(50);
					Kill();
				}
			} else if (c.gameObject.tag == "Flare") {
				Kill();
			} else if (c.gameObject.tag == "Terrain" && Time.time > startTime + 2.0f) {
				Kill();
			} else if (c.gameObject.tag == "Bullet") {
				Kill();
			}
		}
	}

	public void SetTarget (GameObject target) {
		this.target = target;
	}

	public void Kill () {
		if (target != null && target.tag == "Player" && target.GetComponent<PlayerController>() != null) {
			target.GetComponent<PlayerController>().LoseLock(gameObject);
		}

		Destroy(Instantiate(explosion, transform.position, transform.rotation), 2);

		Destroy(gameObject);
	}
}
