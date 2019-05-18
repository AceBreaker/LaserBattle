using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLights : MonoBehaviour {

    public Light light;

    public float minFlicker;
    public float maxFlicker;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        float flickerAmount1 = Random.Range(minFlicker, maxFlicker);
        float flickerAmount2 = Random.Range(minFlicker, maxFlicker);
        light.intensity = flickerAmount1 > flickerAmount2 ? flickerAmount1 : flickerAmount2;
	}
}
