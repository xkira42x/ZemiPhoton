using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Effect : MonoBehaviour {

	ParticleSystem[] particles = null;
	float time = 0;

	void Start () {
		particles = GetComponentsInChildren<ParticleSystem> ();

		for (int ii = 0; ii < particles.Length; ii++) {
			if (time < particles [ii].duration)
				time = particles [ii].duration;
		}
		StartCoroutine (EndPlayback (time));
	}

	IEnumerator EndPlayback(float interval){
		yield return new WaitForSeconds (interval);
		Destroy (gameObject);
	}
}
