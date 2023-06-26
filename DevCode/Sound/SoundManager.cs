using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jsh
{
    public class SoundManager : MonoBehaviour
    {

        public static SoundManager instance = null;

        [SerializeField]
        SFXData SFX;


        [SerializeField]
        AudioSource SFXManager;

        [SerializeField]
        AudioSource themeManager;

        public void Start()
        {
            if(instance == null)
            {
                instance = this;
            }
            else if(instance != this)
            {
                Destroy(this.gameObject);
            }


        }


        public void PlaySFXOnLoop(string category, string name, float pitch = 1)
        {
            var sounds = SFX.GetCategorySFXs(category);

            if (sounds == null)
            {
                Debug.LogError("There is no such category named: " + category + ",Did you missed the capital letter?");
            }

            foreach (var sound in sounds)
            {


                if (sound.Name == name)
                {
                    SFXManager.clip = sound.Clip;
                    SFXManager.pitch = pitch;
                    SFXManager.Play();
                    SFXManager.loop = true;
                    return;
                }


            }

            Debug.LogError("There is no such clip named: " + name + ",Check out the SFX Data");

        }


        public void PlaySFX(string category, string name, float pitch = 1) {

            var sounds = SFX.GetCategorySFXs(category);

            if(sounds == null)
            {
                Debug.LogError("There is no such category named: " + category + ",Did you missed the capital letter?");
            }

            foreach(var sound in sounds)
            {
              
                
                if (sound.Name == name)
                {
                    SFXManager.clip = sound.Clip;
                    SFXManager.pitch = pitch;
                    SFXManager.Play();
                    SFXManager.loop = false;
                    return;
                }


            }

            Debug.LogError("There is no such clip named: " + name + ",Check out the SFX Data");

        
        }

        



        



       

    }
}

