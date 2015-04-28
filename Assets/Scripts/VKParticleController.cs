using UnityEngine;
using System.Collections;

public class VKParticleController : MonoBehaviour {

	public GameObject particleSystem;
	public string name;
	public float baseEmit;
	public float exciteThreshold;
	public string beatLevel = "none";

	// Use this for initialization
	void Start () {
	
		baseEmit = 100;
		exciteThreshold = 0.1f;
		name = name;
		particleSystem = GameObject.Find (name);
		particleSystem.GetComponent<ParticleSystem> ().emissionRate = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void BeatHitPitch(float inPitch) {

		//Debug.Log ("Particle Blast! " + inPitch);

		if (inPitch > exciteThreshold) {

			float hZ = inPitch;
			
			
			if (hZ > 0f && hZ < 100) {
				baseEmit = 10;
				beatLevel = "low";
				
			} else if (hZ > 100 && hZ < 300) {
				baseEmit = 60;
				beatLevel = "med";
				
			} else if (hZ > 600) {
				baseEmit = 720;
				beatLevel = "high";
				
			}

			particleSystem.GetComponent<ParticleSystem> ().Emit ((int) (baseEmit * Time.deltaTime));

		}


		
	}
}
