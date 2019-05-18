using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererFix : MonoBehaviour {

    public float lineWidth;

	// Use this for initialization
	void Start () {
		LineRenderer rend = GetComponent<LineRenderer>();
        rend.widthMultiplier = lineWidth;
	}
}
