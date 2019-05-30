using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LaserBattle
{
    public class DestructibleObject : MonoBehaviour {

        [SerializeField] GameObject destroyParticles;
        public AudioManager audio;

        private void Start()
        {
            audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }

        public void DestroyObject(GameObject go)
        {
            if (go == gameObject)
            {
                Destroy(gameObject, 2.0f);
                audio.PlayAudio("sizzle");
            }
        }

        private void OnDestroy()
        {
            if (!GameManager.quittingOrChangingScene && destroyParticles != null)
            {
                if(audio != null)
                {
                    audio.PlayAudio("explosion");
                }

                GameObject particleEffect = Instantiate(destroyParticles, transform.position, transform.rotation);
                Destroy(particleEffect, 2.0f);
            }
        }

    }
}