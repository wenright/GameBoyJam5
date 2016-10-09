using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public GameObject bullet;
	public GameObject flare;

	public AudioClip shootSound;
	public AudioClip shipExplosionSound;

	public Text ammoText;
	public Text flareText;
	public Text moneyText;
	public Image reticle;

	private AudioSource audioSource;

	private int orbitalRadius = 1000;
	private float orbitalSpeed = 0.1f;
	private float currentOrbitalTime = 0.0f;
	private int height = 1000;

	private float lookSpeed = 100.0f;

	private Vector3 lookPosition = Vector3.zero;
	private Vector3 lookOffset = Vector3.zero;
	private int maxOffset = 27;
	private Vector3 kickOffset = Vector3.zero;

	private float lastShot = 0.0f;
	private int reloadTime = 5;
	private float ammo = UpgradesController.clipSize;

	private int health = 100;
	private int flares = UpgradesController.maxFlares;

	private Queue<int> zoomLevels = new Queue<int>(new int[] {12, 7, 4});

	private List<GameObject> lockedMissiles = new List<GameObject>();

	void Awake () {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		audioSource = GetComponent<AudioSource>();

		ToggleZoom();
	}

	void Update () {
		ammoText.text = ammo + "/" + UpgradesController.clipSize;
		flareText.text = flares + "/" + UpgradesController.maxFlares;
		moneyText.text = "$" + UpgradesController.money;

		currentOrbitalTime += Time.deltaTime * orbitalSpeed;
		transform.position = new Vector3(Mathf.Sin(currentOrbitalTime) * orbitalRadius, height, Mathf.Cos(currentOrbitalTime) * orbitalRadius);

		transform.LookAt(lookPosition);

		lookOffset.x = Mathf.Clamp(Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime + lookOffset.x, -maxOffset, maxOffset);
		lookOffset.y = Mathf.Clamp(-Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime + lookOffset.y, -maxOffset, maxOffset);

		transform.Rotate(lookOffset.y + kickOffset.x, lookOffset.x + kickOffset.y, 0);

		kickOffset = kickOffset/1.2f;

		if (ammo == 0) {
			reticle.color = Color.grey;
		} else {
			reticle.color = Color.white;
		}

		if (Input.GetButton("Fire1") && Time.time > lastShot + UpgradesController.fireRate && ammo > 0) {
			GameObject b = Instantiate(bullet, transform.position + new Vector3(0, -5, 0), transform.rotation) as GameObject;
			b.GetComponent<BulletController>().explosionRadius = UpgradesController.explosionRadius;
			b.GetComponent<BulletController>().damage = UpgradesController.damage;

			lastShot = Time.time;
			kickOffset += Random.insideUnitSphere;
			audioSource.PlayOneShot(shootSound);

			ammo--;
			if (ammo <= 0) {
				StartCoroutine("Reload");
			}
		}

		if (lockedMissiles.Count > 0) {
			if (!audioSource.isPlaying) {
				audioSource.Play();
			}
		}

		if (Input.GetButtonDown("Flares") && flares > 0) {
			flares--;
			GameObject f = Instantiate(flare, transform.position + new Vector3(0, -5, 0), transform.rotation) as GameObject;
			Destroy(f, 15);

			if (lockedMissiles.Count > 0 && Random.value > 0.6) {
				int i = Mathf.Min((int) (Random.value * lockedMissiles.Count), lockedMissiles.Count - 1);
				lockedMissiles[i].GetComponent<MissileController>().SetTarget(f);
				lockedMissiles.RemoveAt(i);
			}
		}

		if (Input.GetButtonDown("Zoom")) {
			ToggleZoom();
		}

		if (Input.GetButtonDown("Restart")) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	public void Damage (int amount) {
		health -= amount;

		if (health <= 0) {
			print("Game Over!");
			audioSource.PlayOneShot(shipExplosionSound);
			Destroy(GetComponent<PlayerController>());
		}
	}

	public void LockOn (GameObject missile) {
		lockedMissiles.Add(missile);
	}

	public void LoseLock (GameObject missile) {
		lockedMissiles.Remove(missile);
	}

	private Vector3 GetLookPosition () {
		RaycastHit hit;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit)) {
            return hit.point;
        }

        return Vector3.zero;
	}

	private void ToggleZoom () {
		int newLevel = zoomLevels.Dequeue();
		Camera.main.fieldOfView = newLevel;
		lookSpeed = newLevel * 5;
		zoomLevels.Enqueue(newLevel);
	}

	private IEnumerator Reload () {
		yield return new WaitForSeconds(reloadTime);
		ammo = UpgradesController.clipSize;
	}
}
