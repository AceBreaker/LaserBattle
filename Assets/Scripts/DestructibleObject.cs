﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour {

    [SerializeField] GameObject destroyParticles;

    public void DestroyObject(GameObject go)
    {
        if(go == gameObject)
        {
            if(destroyParticles != null)
                Instantiate(destroyParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
