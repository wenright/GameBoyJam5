using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public GameObject bullet;
	public GameObject flare;

	public AudioClip shootSound;
	public AudioClip shipExplosionSound;

	private AudioSource audioSource;

	private int orbitalRadius = 1000;
	private float orbitalSpeed = 0.1f;
	private float currentOrbitalTime = 0.0f;
	private int height = 1000;

	private float lookSpeed = 100.0f;

	private Vector3 lookPosition = Vector3.zero;
	private Vector3 lookOffset = Vector3.zero;
	private int maxOffset = 25;
	private Vector3 kickOffset = Vector3.zero;

	private float fireRate = 0.15f;
	private float lastShot = 0.0f;

	private int health = 100;
	private int flares = 50;

	private List<GameObject> lockedMissiles = new List<GameObject>();

	void Awake () {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {
		currentOrbitalTime += Time.deltaTime * orbitalSpeed;
		transform.position = new Vector3(Mathf.Sin(currentOrbitalTime) * orbitalRadius, height, Mathf.Cos(currentOrbitalTime) * orbitalRadius);

		transform.LookAt(lookPosition);

		lookOffset.x = Mathf.Clamp(Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime + lookOffset.x, -maxOffset, maxOffset);
		lookOffset.y = Mathf.Clamp(-Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime + lookOffset.y, -maxOffset, maxOffset);

		transform.Rotate(lookOffset.y + kickOffset.x, lookOffset.x + kickOffset.y, 0);

		kickOffset = kickOffset/1.2f;

		if (Input.GetButton("Fire1") && Time.time > lastShot + fireRate) {
			Instantiate(bullet, transform.position + new Vector3(0, -5, 0), transform.rotation);
			lastShot = Time.time;
			kickOffset += Random.insideUnitSphere;
			audioSource.PlayOneShot(shootSound);
		}

		if (lockedMissiles.Count > 0) {
			print("Missile incoming!");
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

				print("Missile evaded!");
			}
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
}
