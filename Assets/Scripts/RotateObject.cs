using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

    [SerializeField] float rotationRate;
    [SerializeField] Vector3 rotationAxis;

	// Update is called once per frame
	void Update () {
        transform.Rotate(rotationAxis * rotationRate * Time.deltaTime);
	}
}
