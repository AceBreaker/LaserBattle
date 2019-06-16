using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveGhost : MonoBehaviour {
    
    public void DestroyGhost()
    {
        Destroy(gameObject);
    }
}
