using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRangeIncrease : MonoBehaviour {

    bool startIncreasing = false;
    public float startingRange, endRange;
    public float startIntensity, endIntensity;

    Light light;

    float timeSinceStart = 0.0f;
    public float duration = 0.0f;

	// Use this for initialization
	void Start () {
        light = gameObject.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        if (startIncreasing)
        {
            timeSinceStart += Time.deltaTime;
            float t = timeSinceStart / duration;
            if (t >= 1.0f)
            {
                t = 1.0f;
                startIncreasing = false;
            }

            light.intensity = Mathf.Lerp(startIntensity, endIntensity, t);
            light.range = Mathf.Lerp(startingRange, endRange, t);
        }
    }

    public void StartSequence()
    {
        startIncreasing = true;
    }
}
