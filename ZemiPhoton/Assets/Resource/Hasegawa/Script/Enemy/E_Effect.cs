using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class E_Effect : MonoBehaviour
{

    AudioSource audioSource;
    [SerializeField]AudioClip[] audioClip;

    ParticleSystem[] particles = null;
    float time = 0;

    void Start()
    {           

        particles = GetComponentsInChildren<ParticleSystem>();

        for (int ii = 0; ii < particles.Length; ii++)
        {
            if (time < particles[ii].duration)
                time = particles[ii].duration;
        }

		audioSource = GetComponent<AudioSource> ();
        
        if (audioSource != null)
        {
			int ii = Random.Range (0, audioClip.Length);
            audioSource.PlayOneShot(audioClip[ii]);
            if (time < audioClip[ii].length) time = audioClip[ii].length;
        }

        StartCoroutine(EndPlayback(time));

    }

    IEnumerator EndPlayback(float interval)
    {
        yield return new WaitForSeconds(interval);
        //Destroy(gameObject);
    }
}
