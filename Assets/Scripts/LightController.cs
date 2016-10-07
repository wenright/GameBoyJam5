using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {

	private float defaultIntensity = 0.45f;
	private float extraIntensity = 0.0f;

	void Update () {
		GetComponent<Light>().intensity = defaultIntensity + extraIntensity;
		extraIntensity /= 2.0f;
	}

	public void Flash (int amount) {
		extraIntensity += amount / 50.0f;
	}
}
