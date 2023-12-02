using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class VSKiller : MonoBehaviour
{
    NavMeshAgent agent;
    private Animator anim;

    public bool bloodLusted;
    public LayerMask targetMask;

    private AudioSource soundSource;
    public AudioClip grabVoice;

    public GameObject Player;
    public Transform player;

    public float _proximityRadius;
    [Range(0, 360)] public float _proximityAngle;

    private VoiceClipRando clipRando;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        soundSource = GetComponent<AudioSource>();
        clipRando = GetComponent<VoiceClipRando>();

        //agent.destination = player.transform.position;
    }

    private void ProximityCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _proximityRadius, targetMask);

        bloodLusted = rangeChecks.Length > 0;

        if (bloodLusted)
        {
            Transform target = rangeChecks[0].transform;
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // Assuming no obstruction check and no consideration of FOV
            bloodLusted = distanceToTarget <= _proximityRadius;
        }
    }

    public void Grab()
    {
        if (bloodLusted)
        {
            //bool isCaught = anim.GetBool("Catch");
            anim.SetBool("Grab", true);
        }
        else
        {
            anim.SetBool("Grab", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the player tag is detected
            if (grabVoice != null)
            {
                clipRando.enabled = false;
                soundSource.PlayOneShot(grabVoice);
            }

            if (agent != null)
            {
                agent.speed = 0f;
                anim.SetBool("Catch", true);
            }
        }
    }

    void Update()
    {
        agent.destination = player.transform.position;
        ProximityCheck();
        Grab();
    }
}
