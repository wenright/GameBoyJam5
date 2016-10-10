using UnityEngine;
using System.Collections;

public class EnemySpawnerController : MonoBehaviour {

	public GameObject[] enemies;

	public float delay = 15.0f;

	void Start () {
		if (enemies.Length == 0) {
			print("Assign enemies to enemy spawner");
		} else {
			InvokeRepeating("SpawnEnemy", 5.0f, delay);
		}
	}

	private void SpawnEnemy () {
		Instantiate(enemies[(int) (Random.value * enemies.Length)], transform.position, transform.rotation);
	}
}
