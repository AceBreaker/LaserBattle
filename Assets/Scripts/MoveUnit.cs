using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnit : MonoBehaviour {

    public float moveSpeed;

    Vector3 startPosition;
    Vector3 endPosition;
    bool beginMoving = false;

    float timeSinceStart;
    public float lerpTime;

    private void Update()
    {
        if (beginMoving)
        {
            timeSinceStart += Time.deltaTime;
            float t = timeSinceStart / lerpTime;
            if (timeSinceStart >= 1.0f)
                t = 1.0f;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
        }
    }

    public void MoveToPosition(Vector3 start, Vector3 end)
    {
        startPosition = start;
        endPosition = end;
        beginMoving = true;
        timeSinceStart = 0.0f;
    }
}
