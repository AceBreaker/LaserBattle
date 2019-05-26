using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour {

    [SerializeField] GameObject destroyParticles;

    public void DestroyObject(GameObject go)
    {
        if(go == gameObject)
        {
            //if (destroyParticles != null)
            //{
            //    GameObject particleEffect = Instantiate(destroyParticles, transform.position, transform.rotation);
            //    Destroy(particleEffect, 2.0f);
            //}
            Destroy(gameObject, 2.0f);
        }
    }

    private void OnDestroy()
    {
        if (destroyParticles != null)
        {
            GameObject particleEffect = Instantiate(destroyParticles, transform.position, transform.rotation);
            Destroy(particleEffect, 2.0f);
        }
    }
}
