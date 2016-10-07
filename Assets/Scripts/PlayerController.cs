using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject bullet;

	private int orbitalRadius = 200;
	private float orbitalSpeed = 0.1f;
	private float currentOrbitalTime = 0.0f;
	private int height = 500;

	private float lookSpeed = 100.0f;

	private Vector3 lookPosition = Vector3.zero;
	private Vector3 lookOffset = Vector3.zero;
	private int maxOffset = 15;
	private Vector3 kickOffset = Vector3.zero;

	private float fireRate = 0.2f;
	private float lastShot = 0.0f;
	
	void Update () {
		currentOrbitalTime += Time.deltaTime * orbitalSpeed;
		transform.position = new Vector3(Mathf.Sin(currentOrbitalTime) * orbitalRadius, height, Mathf.Cos(currentOrbitalTime) * orbitalRadius);

		transform.LookAt(lookPosition);

		lookOffset.x = Mathf.Clamp(Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime + lookOffset.x, -maxOffset, maxOffset);
		lookOffset.y = Mathf.Clamp(-Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime + lookOffset.y, -maxOffset, maxOffset);

		transform.Rotate(lookOffset.y + kickOffset.x, lookOffset.x + kickOffset.y, 0);

		kickOffset = kickOffset/2;

		if (Input.GetButton("Fire1") && Time.time > lastShot + fireRate) {
			Instantiate(bullet, transform.position + new Vector3(0, -5, 0), transform.rotation);
			lastShot = Time.time;
			kickOffset += Random.insideUnitSphere/2;
		}
	}

	private Vector3 GetLookPosition () {
		RaycastHit hit;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit)) {
            return hit.point;
        }

        return Vector3.zero;
	}
}
