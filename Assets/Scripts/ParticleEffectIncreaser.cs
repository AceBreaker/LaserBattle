using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectIncreaser : MonoBehaviour {

    bool startIncreasing = false;

    
    public float startEmission, endEmission;
    public float startSpeed, endSpeed;

    ParticleSystem particles;

    float timeSinceStart = 0.0f;
    public float duration = 0.0f;

	// Use this for initialization
	void Start () {
        if (particles == null)
            particles = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
		if(startIncreasing)
        {
            timeSinceStart += Time.deltaTime;
            float t = timeSinceStart / duration;
            if( t >= 1.0f)
            {
                t = 1.0f;
                startIncreasing = false;
            }

            ParticleSystem.EmissionModule e = particles.emission;

            e.rateOverTime = Mathf.Lerp(startEmission, endEmission, t);

            ParticleSystem.MainModule main = particles.main;
            main.startSpeed = Mathf.Lerp(startSpeed, endSpeed, t);
        }
	}

    public void StartSequence()
    {
        startIncreasing = true;
    }
}
