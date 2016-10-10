using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemySpawnerController : MonoBehaviour {

	public Transform spawnPoint1;
	public Transform spawnPoint2;

	public GameObject enemy;
	public GameObject missileEnemy;
	public GameObject flakEnemy;

	private int followDistance = 30;

	void Start () {
		StartCoroutine("SpawnWave");
	}

	private IEnumerator SpawnWave () {
		switch (UpgradesController.wave) {
			case 1:
				Instantiate(enemy, GetRandomSpawnPoint(), transform.rotation);

				yield return new WaitForSeconds(20);

				Instantiate(enemy, GetRandomSpawnPoint() + Vector3.right * followDistance, transform.rotation);
				Instantiate(enemy, GetRandomSpawnPoint() - Vector3.right * followDistance, transform.rotation);

				yield return new WaitForSeconds(20);

				Instantiate(missileEnemy, GetRandomSpawnPoint(), transform.rotation);

				yield return new WaitForSeconds(15);

				Instantiate(missileEnemy, GetRandomSpawnPoint() - Vector3.right * followDistance, transform.rotation);
				Instantiate(enemy, GetRandomSpawnPoint() + Vector3.right * followDistance, transform.rotation);

				yield return new WaitForSeconds(20);

				break;
			case 2:
				yield return new WaitForSeconds(5);

				Instantiate(enemy, GetRandomSpawnPoint() + Vector3.right * followDistance, transform.rotation);
				Instantiate(enemy, GetRandomSpawnPoint() - Vector3.right * followDistance, transform.rotation);
				Instantiate(missileEnemy, GetRandomSpawnPoint(), transform.rotation);

				yield return new WaitForSeconds(20);

				Instantiate(missileEnemy, GetRandomSpawnPoint() + Vector3.right * followDistance, transform.rotation);
				Instantiate(enemy, GetRandomSpawnPoint() - Vector3.right * followDistance, transform.rotation);
				Instantiate(missileEnemy, GetRandomSpawnPoint(), transform.rotation);

				yield return new WaitForSeconds(25);

				Instantiate(enemy, GetRandomSpawnPoint() - Vector3.right * followDistance, transform.rotation);
				Instantiate(flakEnemy, GetRandomSpawnPoint() + Vector3.right * followDistance, transform.rotation);

				yield return new WaitForSeconds(25);

				Instantiate(flakEnemy, GetRandomSpawnPoint() - Vector3.right * followDistance, transform.rotation);
				yield return new WaitForSeconds(5);
				Instantiate(flakEnemy, GetRandomSpawnPoint() + Vector3.right * followDistance, transform.rotation);
				yield return new WaitForSeconds(5);
				Instantiate(enemy, GetRandomSpawnPoint() + Vector3.right * followDistance, transform.rotation);
				yield return new WaitForSeconds(9);
				Instantiate(missileEnemy, GetRandomSpawnPoint() + Vector3.right * followDistance, transform.rotation);

				yield return new WaitForSeconds(25);

				break;
			case 3:
				yield return new WaitForSeconds(5);

				Instantiate(missileEnemy, GetRandomSpawnPoint() + Vector3.right * followDistance, transform.rotation);
				Instantiate(missileEnemy, GetRandomSpawnPoint(), transform.rotation);

				yield return new WaitForSeconds(20);

				Instantiate(missileEnemy, GetRandomSpawnPoint() + Vector3.right * followDistance, transform.rotation);

				yield return new WaitForSeconds(10);

				Instantiate(enemy, GetRandomSpawnPoint() - Vector3.right * followDistance, transform.rotation);
				Instantiate(missileEnemy, GetRandomSpawnPoint(), transform.rotation);

				yield return new WaitForSeconds(25);

				Instantiate(enemy, GetRandomSpawnPoint() - Vector3.right * followDistance, transform.rotation);
				Instantiate(flakEnemy, GetRandomSpawnPoint() + Vector3.right * followDistance, transform.rotation);
				Instantiate(flakEnemy, GetRandomSpawnPoint() + Vector3.forward * followDistance, transform.rotation);

				yield return new WaitForSeconds(25);

				Instantiate(flakEnemy, GetRandomSpawnPoint() - Vector3.right * followDistance, transform.rotation);
				yield return new WaitForSeconds(5);
				Instantiate(flakEnemy, GetRandomSpawnPoint() + Vector3.right * followDistance, transform.rotation);
				yield return new WaitForSeconds(4);
				Instantiate(enemy, GetRandomSpawnPoint() + Vector3.right * followDistance, transform.rotation);
				yield return new WaitForSeconds(8);
				Instantiate(missileEnemy, GetRandomSpawnPoint() + Vector3.right * followDistance, transform.rotation);

				yield return new WaitForSeconds(25);

				break;
			case 4:
				Instantiate(enemy, GetRandomSpawnPoint() + Vector3.right * followDistance, transform.rotation);
				Instantiate(missileEnemy, GetRandomSpawnPoint() - Vector3.right * followDistance, transform.rotation);
				Instantiate(enemy, GetRandomSpawnPoint() + Vector3.forward * followDistance, transform.rotation);

				yield return new WaitForSeconds(20);

				Instantiate(enemy, GetRandomSpawnPoint() - Vector3.right * followDistance, transform.rotation);
				Instantiate(flakEnemy, GetRandomSpawnPoint() + Vector3.right * followDistance, transform.rotation);
				Instantiate(flakEnemy, GetRandomSpawnPoint() + Vector3.forward * followDistance, transform.rotation);

				yield return new WaitForSeconds(25);

				Instantiate(enemy, spawnPoint1.position - Vector3.right * followDistance, transform.rotation);
				Instantiate(flakEnemy, spawnPoint1.position + Vector3.right * followDistance, transform.rotation);
				Instantiate(missileEnemy, spawnPoint1.position + Vector3.forward * followDistance, transform.rotation);

				yield return new WaitForSeconds(25);

				Instantiate(enemy, spawnPoint2.position - Vector3.right * followDistance, transform.rotation);
				Instantiate(flakEnemy, spawnPoint2.position + Vector3.right * followDistance, transform.rotation);
				Instantiate(missileEnemy, spawnPoint2.position + Vector3.forward * followDistance, transform.rotation);

				yield return new WaitForSeconds(30);

				Instantiate(flakEnemy, GetRandomSpawnPoint() - Vector3.right * followDistance, transform.rotation);
				Instantiate(flakEnemy, GetRandomSpawnPoint() + Vector3.right * followDistance, transform.rotation);
				Instantiate(flakEnemy, GetRandomSpawnPoint() + Vector3.forward * followDistance, transform.rotation);

				yield return new WaitForSeconds(45);

				break;
			default:
				break;
		}

		UpgradesController.wave++;
		SceneManager.LoadScene("Shop");
	}

	private Vector3 GetRandomSpawnPoint () {
		if (Random.value > 0.5) {
			return spawnPoint1.position;
		} else {
			return spawnPoint2.position;
		}
	}
}
