using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    /*
        public Light _Light;
        public Light spotLight;

        public float MinTime;
        public float MaxTime;
        public float Timer;

        public AudioSource AS;
        public AudioClip LightAudio;
        public Material lightOff, lightOn;
        public MeshRenderer meshRenderer;
        Renderer renderer;
        public int materialIndex;
        bool wasCalled;
        bool wasCalled2;

        public void Start()
        {
            Timer = Random.Range(MinTime, MaxTime);
            meshRenderer = GetComponent<MeshRenderer>();
        }


        public void Update()
        {
            FlickerLight();
        }

        void FlickerLight()
        {
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
                wasCalled2 = false;

                if (!wasCalled)
                {
                    wasCalled = true;
                    ChangeToOn();
                }
            }

            if (Timer <= 0)
            {
                if (_Light != null)
                {
                    _Light.enabled = !_Light.enabled;
                    spotLight.enabled = !spotLight.enabled;
                    wasCalled = false;

                    if (!wasCalled2)
                    {
                        wasCalled2 = true;
                        ChangeToOff();
                        Timer = Random.Range(MinTime, MaxTime);
                    }


                }
                //AS.PlayOneShot(LightAudio);
            }
        }

        void ChangeToOff()
        {
            var materialsCopy = meshRenderer.materials;
            materialsCopy[materialIndex] = lightOff;
            meshRenderer.materials = materialsCopy;
        }

        void ChangeToOn()
        {
            var materialsCopy = meshRenderer.materials;
            materialsCopy[materialIndex] = lightOn;
            meshRenderer.materials = materialsCopy;
        }*/
}
