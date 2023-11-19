using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [Header("Light Sources Variables")]
    public Light pointLight;
    public Light spotLight;

    [Header("Time Variables")]
    public float MinTime;
    public float MaxTime;
    public float Timer;

    [Header("Audio Variables")]
    public AudioSource AS;
    public AudioClip LightAudio;

    [Header("Material Variables")]
    public int materialIndex;
    public Material[] newMaterials;
    private Material[] originalMaterials;
    private Renderer renderer;

    public void Start()
    {
        Timer = Random.Range(MinTime, MaxTime);
        renderer = GetComponent<Renderer>();
        originalMaterials = renderer.materials;
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

            if (Timer <= 0)
            {
                if (pointLight != null)
                {
                    pointLight.enabled = !pointLight.enabled;

                    if (pointLight.enabled)
                    {
                        ChangeToOn();
                    }
                    else if (!pointLight.enabled)
                    {
                        ChangeToOff();
                    }
                }

                if (spotLight != null)
                {
                    spotLight.enabled = !spotLight.enabled;

                    if (spotLight.enabled)
                    {
                        ChangeToOn();
                    }
                    else if (!spotLight.enabled)
                    {
                        ChangeToOff();
                    }
                }

                Timer = Random.Range(MinTime, MaxTime);
            }
        }

        //AS.PlayOneShot(LightAudio);
    }

    public void ChangeToOff()
    {
        renderer.materials = newMaterials;
    }

    public void ChangeToOn()
    {
        renderer.materials = originalMaterials;
    }
}
