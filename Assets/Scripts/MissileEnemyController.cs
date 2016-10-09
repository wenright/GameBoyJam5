using UnityEngine;
using System.Collections;

public class MissileEnemyController : MonoBehaviour {

	public GameObject missile;

	void Start () {
		InvokeRepeating("LaunchMissile", Random.value * 10.0f + 10.0f, 20.0f);
	}

	private void LaunchMissile () {
		Instantiate(missile, transform.position, transform.rotation);
	}
}
