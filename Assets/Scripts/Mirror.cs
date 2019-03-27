using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class Mirror : MonoBehaviour
    {
        private RenderTexture rt;
        [SerializeField] Camera camera;

        private void Start()
        {
            rt = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
            rt.Create();

            camera.targetTexture = rt;

            GetComponent<Renderer>().material.mainTexture = rt;
        }
    }
}