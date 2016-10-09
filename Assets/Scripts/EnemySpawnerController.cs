using UnityEngine;
using System.Collections;

public class EnemySpawnerController : MonoBehaviour {

	public GameObject[] enemies;

	void Start () {
		if (enemies.Length == 0) {
			print("Assign enemies to enemy spawner");
		} else {
			InvokeRepeating("SpawnEnemy", 5.0f, 15.0f);
		}
	}

	private void SpawnEnemy () {
		Instantiate(enemies[(int) (Random.value * enemies.Length)], transform.position, transform.rotation);
	}
}
