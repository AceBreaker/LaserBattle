using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour {

    public Vector3 movementAxis;
    public float speed;
    public float magnitude;
    public float frequency;

    Vector3 startPosition;

    public bool waveOn = true;

    float time;
    float startTime;
    private void Start()
    {
        startTime = Time.time;
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update () {
        //transform.position += movementAxis * Mathf.Sin((time +=Time.deltaTime*speed)) * magnitude;
        time += Time.time - startTime;
        transform.localPosition = startPosition + (movementAxis * speed * magnitude * Mathf.Sin(Time.time * frequency));
        //transform.localPosition = startPosition + (movementAxis * speed * magnitude * Mathf.Sin(frequency * time));
    }

    public void ToggleWave()
    {
        ToggleWave(!waveOn);
    }

    public void ToggleWave(bool on)
    {
        waveOn = on;
    }
}
