using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class Player : MonoBehaviour
    {
        [SerializeField] PlayerController controller;
        
        // Update is called once per frame
        void Update()
        {
            controller.Update();
        }
    }
}