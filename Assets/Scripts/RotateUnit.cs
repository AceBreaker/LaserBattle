using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUnit : MonoBehaviour {

    public float moveSpeed;

    Vector3 startPosition;
    Vector3 endPosition;
    bool beginMoving = false;

    public Vector3[] rotations;

    public Vector3[] testRot;

    Vector3 catchPosition;

    float timeSinceStart;
    public float lerpTime;

    public int index = 2;

    int selectedindex = 2;

    private void Start()
    {
        for (int i = 0; i < testRot.Length; i++)
        {
            if (transform.rotation.eulerAngles.y == testRot[i].y)
            {
                index = i;
            }
        }

        selectedindex = index;
    }

    private void Update()
    {
        if(beginMoving)
        {
            timeSinceStart += Time.deltaTime;
                    float t = timeSinceStart / lerpTime;
                    if (timeSinceStart >= 1.0f)
                    {
                        t = 1.0f;
                        beginMoving = false;
                    }
            transform.eulerAngles = new Vector3(0.0f, Mathf.LerpAngle(startPosition.y, endPosition.y, t), 0.0f);
        }
    }

    public void SetNewRotation(Vector3 start, float direction)
    {
        startPosition = transform.rotation.eulerAngles;
        index += (int)direction;
        if (index > 3)
            index = 0;
        else if (index < 0)
            index = 3;
        if(rotations.Length > index)
            endPosition = rotations[index];
        beginMoving = true;
        timeSinceStart = 0.0f;
    }

    public void ObjectSelected()
    {
        selectedindex = index;
    }

    public void ResetRotation()
    {
        index = selectedindex;

        if (rotations.Length > index)
            endPosition = rotations[index];
        beginMoving = true;
        timeSinceStart = 0.0f;
    }
}
