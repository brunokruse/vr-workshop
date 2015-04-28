using UnityEngine;
using System.Collections;

public class VKBeatListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void BeatHit(float RMS) {

		Debug.Log("Detected a beat: " + RMS);

	}

	void lowHit() {
		Debug.Log("LOW HIT");

	}

	void midHit() {
		Debug.Log("MID HIT");
	}

	void highHit() {
		Debug.Log("HIGH HIT");
	}

}
