using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {

    public GameObject cameraBoom;
    Vector3 prevMousePosition;

    private float x;
    private float y;
    private Vector3 rotateValue;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            y = Input.GetAxis("Mouse X");
            
            Debug.Log(x + ":" + y);
            rotateValue = new Vector3(x, y * -1, 0);
            transform.eulerAngles = transform.eulerAngles - rotateValue;
        }
    }
}
