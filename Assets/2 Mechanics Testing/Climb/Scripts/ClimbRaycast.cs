using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbRaycast : MonoBehaviour
{

    //public float offset;
    public float up;
    public float maxDistance;
    public float directionMagnitude;
    private PlayerMovement playerMovement;
  


    public void Jump(GameObject target)
    {
        
        JumpMethod(target);
       
    }

    private void JumpMethod(GameObject target)
    {
     
        Collider targetCollider = target.GetComponent<Collider>();
        Vector3 closestPoint = targetCollider.ClosestPoint(this.gameObject.transform.position);
        Vector3 targetUpperPoint = targetCollider.bounds.center + Vector3.up * targetCollider.bounds.extents.y;

        // Calculate the direction vector from the player to the target
        Vector3 directionToTarget = (closestPoint - transform.position).normalized;

        // You can adjust the magnitude of the direction vector based on your preferences
        
        Vector3 adjustedDirection = directionToTarget * directionMagnitude;

        // Modify the targetUpperPoint based on the direction vector
        targetUpperPoint = new Vector3(closestPoint.x , targetUpperPoint.y,closestPoint.z);
        targetUpperPoint += adjustedDirection;
        
        
        float distance = Vector3.Distance(transform.position, targetUpperPoint);
        

        //using the distance control if you should jump or not 
        if (distance <= maxDistance)
        {
            Vector3 jumpDirection = (targetUpperPoint - transform.position).normalized;
            

            //trget upper point is the end point.. then 
            // equation for 'mid point' ill use that is not really mid point of the points but still: 
            //(x*.75),y+(y*.25)
            // to alter this it can also include variable for the offset which can be a: 
            //(x*(1-a)),y+(y*a)
            //where a is some value less than 1 , x is the end x and y is the end y 
            //now for it to be in xyz 
            // z is depth so maybe the forward direction? that can be the midpoint between the original z and the new z so (z1+z2) / 2

            //get player start location 
            Vector3 playerVector = this.gameObject.transform.position;

            //use the equation made to get mid point 
            Vector3 targetMidPoint = new Vector3((targetUpperPoint.x + this.gameObject.transform.position.x) /2, (targetUpperPoint.y + this.gameObject.transform.position.y) / 2, (playerVector.z + targetUpperPoint.z) / 2);

            //change the mid and upper point to include half the players height in order to make it not glitch through walls
            float playerHalfHeight = this.gameObject.GetComponent<Collider>().bounds.extents.y;
            targetMidPoint.y = targetMidPoint.y + playerHalfHeight + up;
            targetUpperPoint.y = targetUpperPoint.y + playerHalfHeight;




            //now put it into the original playerclimb 
            this.gameObject.GetComponent<PlayerClimb>().Climb(targetMidPoint, targetUpperPoint);

        }

    }

}