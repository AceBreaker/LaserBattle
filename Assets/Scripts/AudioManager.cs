using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class AudioManager : MonoBehaviour
    {

        public List<AudioClipData> audioClips;
        public List<string> audioNames;
        public List<AudioSource> audioSources;

        private void Start()
        {
            for (int i = 0; i < audioClips.Count; ++i)
            {
                audioSources.Add(gameObject.AddComponent<AudioSource>());
                audioSources[i].clip = audioClips[i].clip;
                audioSources[i].volume = audioClips[i].volume;
                audioSources[i].loop = audioClips[i].looping;
            }
        }

        public void PlayAudio(string name)
        {
            for (int i = 0; i < audioNames.Count; ++i)
            {
                if (audioNames[i] == name)
                {
                    audioSources[i].Play();
                }
            }
        }

        public void StopAudio(string name)
        {
            for (int i = 0; i < audioNames.Count; ++i)
            {
                if (audioNames[i] == name)
                {
                    audioSources[i].Stop();
                }
            }
        }

        [System.Serializable]
        public struct AudioClipData
        {
            public AudioClip clip;
            public float volume;

            public bool looping;
        }
    }
}