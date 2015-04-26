using UnityEngine;
using System.Collections;

public class VKLightIntensity : MonoBehaviour {

	public GameObject light;
	public float lightPower;

	// Use this for initialization
	void Start () {
		light = GameObject.Find ("Light");

	}
	
	// Update is called once per frame
	void Update () {
	
		light.GetComponent<Light> ().intensity = lightPower;
	}

	void BeatHit(float RMS) {
		// Debug.Log ("Pow! " + RMS * scalePower);
		lightPower = 1.0f + (0.5f * RMS * 10.0f);

	}
}
