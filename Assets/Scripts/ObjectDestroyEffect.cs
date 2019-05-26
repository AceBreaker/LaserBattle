using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class ObjectDestroyEffect : MonoBehaviour
    {

        MaterialColorChanger changer;
        LightRangeIncrease lightIncreaser;

        public GameObject owningUnit;

        // Use this for initialization
        void Start()
        {
            changer = GetComponent<MaterialColorChanger>();
            lightIncreaser = GetComponent<LightRangeIncrease>();
            owningUnit = gameObject.GetComponentInParent<MoveableUnit>().gameObject;

        }

        public void StartDeathSequence(GameObject dyingObject)
        {
            if(owningUnit == dyingObject)
            {
                changer.BeginColorChanging();
                lightIncreaser.StartSequence();
            }
        }


    }
}