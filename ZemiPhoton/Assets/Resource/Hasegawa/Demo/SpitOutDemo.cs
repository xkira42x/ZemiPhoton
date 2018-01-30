using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitOutDemo : MonoBehaviour {

    [SerializeField]
    GameObject geroPuddle;
    [SerializeField]
    Transform target;
    Transform myTrns;
    [SerializeField]
    float speed = 1f;

    public ParticleSystem particleSystem;
    ParticleCollisionEvent[] collisionEvents = null;

	void Start () {
        myTrns = transform;
        //StartCoroutine(GERO());
	}

    IEnumerator GERO() {
        while (true) {
            float ss = Vector3.Distance(myTrns.position, target.position);
            particleSystem.startSpeed = ss * speed;
            particleSystem.Play();
            yield return new WaitForSeconds(10f);
        }
    }

    void OnParticleCollision(GameObject other)
    {

        int safeLength = particleSystem.GetSafeCollisionEventSize();
        if (collisionEvents == null || collisionEvents.Length < safeLength)
        {
            collisionEvents = new ParticleCollisionEvent[safeLength];
        }
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);
        int ii = 0;
        while (ii < safeLength)
        {
            Instantiate(geroPuddle, collisionEvents[ii].intersection, Quaternion.Euler(new Vector3(0, 0, -90)));
            Debug.Log("Position => " + collisionEvents[ii].intersection);
            ii++;
        }
    }

}
