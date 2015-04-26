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

}
