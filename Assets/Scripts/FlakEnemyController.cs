using UnityEngine;
using System.Collections;

public class FlakEnemyController : MonoBehaviour {

	public GameObject bullet;

	private GameObject target;
	private int maxBullets = 20;
	private int bullets;
	private bool reloading = false;

	void Start () {
		bullets = maxBullets;

		target = GameObject.FindGameObjectWithTag("Player");

		if (target == null || target.GetComponent<PlayerController>() == null) {
			print("Cannot find player");
		} else {
			InvokeRepeating("FireBullet", Random.value * 10.0f, 0.2f);
		}
	}

	private void FireBullet () {
		if (target == null || target.GetComponent<PlayerController>() == null) {
			Destroy(GetComponent<FlakEnemyController>());
			return;
		}

		if (bullets == 0) {
			if (!reloading) {
				StartCoroutine("Reload");
			}

			return;
		}

		GameObject b = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
		b.GetComponent<BulletController>().explosionRadius = 10;

		float s = target.GetComponent<PlayerController>().GetOrbitalSpeed();
		float leadTime = 1.0f * s;
		float t = target.GetComponent<PlayerController>().GetCurrentOrbitalTime() + leadTime;
		int r = target.GetComponent<PlayerController>().GetOrbitalRadius();

		Vector3 leadTarget = new Vector3(Mathf.Sin(t) * r, target.transform.position.y, Mathf.Cos(t) * r);

		b.transform.LookAt(leadTarget);

		bullets--;
	}

	private IEnumerator Reload () {
		reloading = true;
		yield return new WaitForSeconds(7);
		bullets = maxBullets;
		reloading = false;
	}
}
