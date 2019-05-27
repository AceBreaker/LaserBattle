using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class ObjectDestroyEffect : MonoBehaviour
    {

        public MaterialColorChanger changer;
        public LightRangeIncrease lightIncreaser;
        public ParticleEffectIncreaser particleIncreaser;

        public GameObject owningUnit;

        // Use this for initialization
        void Start()
        {
            if(changer == null)
                changer = GetComponent<MaterialColorChanger>();
            if(lightIncreaser == null)
                lightIncreaser = GetComponent<LightRangeIncrease>();
            if(particleIncreaser == null)
                particleIncreaser = GetComponent<ParticleEffectIncreaser>();
            if(owningUnit == null)
                owningUnit = gameObject.GetComponentInParent<MoveableUnit>().gameObject;

        }

        public void StartDeathSequence(GameObject dyingObject)
        {
            if(owningUnit == dyingObject)
            {
                if(changer != null)
                    changer.BeginColorChanging();

                if(lightIncreaser != null)
                    lightIncreaser.StartSequence();

                if (particleIncreaser != null)
                    particleIncreaser.StartSequence();
            }
        }
    }
}