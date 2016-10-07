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
	
	void Update () {
		currentOrbitalTime += Time.deltaTime * orbitalSpeed;
		transform.position = new Vector3(Mathf.Sin(currentOrbitalTime) * orbitalRadius, height, Mathf.Cos(currentOrbitalTime) * orbitalRadius);

		transform.LookAt(lookPosition);

		lookOffset.x += Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;
		lookOffset.y -= Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;

		transform.Rotate(lookOffset.y, lookOffset.x, 0);

		if (Input.GetButtonDown("Fire1")) {
			Instantiate(bullet, transform.position + new Vector3(0, -5, 0), transform.rotation);
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
