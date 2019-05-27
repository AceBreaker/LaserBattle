using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class Base : MonoBehaviour {
        public PlayerNumbers playerNumber;

        public GameEvent baseDestroyedEvent;

        [SerializeField] GameObject destroyParticle;

        public GameEventGameObject1x coreDestroyedEvent;

        public void ObjectDestroyed()
        {
            Debug.Log("IS ANYTHING HAPPENING HERER");
            if(coreDestroyedEvent != null)
                coreDestroyedEvent.Raise(gameObject);
        }

        public void OnCoreDestroyed(GameObject go)
        {
            if(gameObject == go)
                Destroy(gameObject, 4.0f);
        }

        private void OnDestroy()
        {
            if (!GameManager.quittingOrChangingScene && destroyParticle != null)
            {
                GameObject particleEffect = Instantiate(destroyParticle, transform.position, transform.rotation);
                Destroy(particleEffect, 1.8f);
            }
        }
    }
}