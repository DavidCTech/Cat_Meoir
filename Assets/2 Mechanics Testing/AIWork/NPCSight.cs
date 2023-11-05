using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSight : MonoBehaviour
{
    [Header("NPC Sight Settings")]
    [Tooltip("The layers it looks for + angle of sight")]
    public float fovRadius;
    public float fovAngle;
    public LayerMask obstructionMask;
    public LayerMask playerMask;
    private float currentWeight;
    public float weightChangeSpeed;


    private void Update()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(this.gameObject.transform.position, fovRadius, playerMask);

        if (rangeChecks.Length != 0)
        {
            for (int i = 0; i < rangeChecks.Length; i++)
            {
                Transform target = rangeChecks[i].transform;
                Vector3 directionToTarget = (target.position - this.gameObject.transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < fovAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(this.gameObject.transform.position, target.position);
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        // Gradually increase the weight
                        currentWeight = Mathf.Lerp(currentWeight, 1.0f, Time.deltaTime * weightChangeSpeed);
                    }
                }
            }
        }
        else
        {
            // Gradually decrease the weight when player is not in line of sight
            currentWeight = Mathf.Lerp(currentWeight, 0.0f, Time.deltaTime * weightChangeSpeed);
        }

        // Set the weight to the HeadLookAt component
        this.gameObject.GetComponent<HeadLookAt>().weight = currentWeight;
    }
}
