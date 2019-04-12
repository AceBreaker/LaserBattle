using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class Base : MonoBehaviour {
        public PlayerNumbers playerNumber;

        public GameEvent baseDestroyedEvent;

        [SerializeField] GameObject destroyParticle;

        public void ObjectDestroyed()
        {
            GameObject particleEffect = Instantiate(destroyParticle, transform.position, transform.rotation);
            Destroy(particleEffect, 2.0f);
            Destroy(gameObject);
        }
    }
}