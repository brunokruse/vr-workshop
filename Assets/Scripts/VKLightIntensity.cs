using UnityEngine;
using System.Collections;

public class VKLightIntensity : MonoBehaviour {

	public GameObject light;

	private float lightPower;
	public float scalePower = 2.0f;
	public float speed = 5.0f;

	// Use this for initialization
	void Start () {
		light = GameObject.Find ("Light");

	}
	
	// Update is called once per frame
	void Update () {
	
		// lerp the light intensity
		light.GetComponent<Light> ().intensity = Mathf.Lerp(light.GetComponent<Light> ().intensity, lightPower, Time.deltaTime * speed); 
	}

	void BeatHit(float RMS) {

		// adjust the light power
		lightPower = 1.0f + RMS * scalePower;

	}
}
