using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {

	public GameObject explosion;

	private GameObject target;
	private int speed = 125;
	private int explosionRadius = 1;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectsWithTag("Player")[0];

		if (target == null) {
			print("Cannot find player");
		} else {
			target.GetComponent<PlayerController>().LockOn(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			Kill();
			return;
		}

		transform.LookAt(target.transform.position);
		transform.Translate(transform.forward * speed * Time.deltaTime);

		foreach (Collider c in Physics.OverlapSphere(transform.position, explosionRadius)) {
			if (c.gameObject.tag == "Player") {
				target.GetComponent<PlayerController>().Damage(50);
				Kill();
			} else if (c.gameObject.tag == "Flare") {
				Kill();
			} else if (c.gameObject.tag == "Terrain") {
				Kill();
			}
		}
	}

	public void SetTarget (GameObject target) {
		this.target = target;
	}

	public void Kill () {
		if (target != null && target.tag == "Player") {
			target.GetComponent<PlayerController>().LoseLock(gameObject);
		}

		Destroy(Instantiate(explosion, transform.position, transform.rotation), 2);

		Destroy(gameObject);
	}
}
