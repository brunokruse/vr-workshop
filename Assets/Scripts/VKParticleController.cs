using UnityEngine;
using System.Collections;

public class VKParticleController : MonoBehaviour {

	public GameObject particleSystem;
	public string name;
	public float baseEmit;
	public float exciteThreshold;
	public string beatLevel = "none";
	
	public int lowBlast = 10;
	public int midBlast = 60;
	public int highBlast = 720;

	// Use this for initialization
	void Start () {
	
		baseEmit = 100;
		exciteThreshold = 0.1f;

		// if the name ie empty assign it a default
		if (name.Length == 0) {
			name = "Particle System";
		}

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
				baseEmit = lowBlast;
				beatLevel = "low";
				
			} else if (hZ > 100 && hZ < 300) {
				baseEmit = midBlast;
				beatLevel = "med";
				
			} else if (hZ > 600) {
				baseEmit = highBlast;
				beatLevel = "high";
				
			}

			// set the emmision power based ont he incoming pitch
			particleSystem.GetComponent<ParticleSystem> ().Emit ((int) (baseEmit * Time.deltaTime));
		}
		
	}

	void BeatHit(float RMS) {

	}
}
