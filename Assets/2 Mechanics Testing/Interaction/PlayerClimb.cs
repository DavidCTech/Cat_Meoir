using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    // this script goes on the player
    //chat gpt helped write the update and calculation method
    [Header("The TimeCurve messes with the speed of the jump through time")]
    public AnimationCurve timeCurve; 
    [Header("The duration is how long the climb takes.")]
    public float duration;

    // We will need the vector3 positions of these transforms 
    private Vector3 startingVector;
    private Vector3 middleVector;
    private Vector3 endVector;
    
    
    //We will need to keep track of the elapsed time 
    private float elaspedTime;
    //a bool for stopping the animation
    private bool isClimbing = false; 



    //this method will restart the elapsed time and will set the climbing to false and will set motion to true 
    public void Restart()
    {
        isClimbing = false;
        elaspedTime = 0;
        //set motion to true 
        this.gameObject.GetComponent<PlayerMovement>().isFrozen = false; 
    }

    // this method will set climbing to true and will set the motion to false - it gets passed in the middle and end vector 
    public void Climb(Transform middleTransform, Transform endTransform)
    {
        
        startingVector = this.gameObject.transform.position;
        middleVector = middleTransform.position;
        endVector = endTransform.position;
        isClimbing = true;
        this.gameObject.GetComponent<PlayerMovement>().isFrozen = true; 
        //set motion to false 
    }

    void Update()
    {
        if(isClimbing)
        {
            
            if (elaspedTime < duration)
            {
                elaspedTime += Time.deltaTime;

                //the current percentage of the animation clamped between 1 and 0 
                float t = elaspedTime / duration;
                t = Mathf.Clamp01(t);

                // Use AnimationCurve to adjust the time parameter
                float scaledT = timeCurve.Evaluate(t);

                Vector3 parabolicPosition = CalculateParabola(startingVector, middleVector, endVector, t);

                transform.position = parabolicPosition;
            }
        }
        
        if(elaspedTime >= duration)
        {
            Restart(); 
        }
    }

    private Vector3 CalculateParabola(Vector3 start, Vector3 middle, Vector3 end, float t)
    {
        //P(t) = (1-t)^2 * S + 2(1-t) *1 *M + t^2 * E
        //The above equation is for having 3 points controlling a bezier curve in 3D space
        // S is starting transform.position in vector3 
        // M is middle transform ( like top of parabola ) 
        // E is the end transform 

        float u = 1 - t; // = (1-t)
        float uu = u * u;//^2 the (1-t) 
        float tt = t * t;
        

        Vector3 p = uu * start; // (1-t)^2 * S
        p += 2 * u * t * middle; // 2(1-t) * t * M
        p += tt * end; // t^2 * E

        return p;
    }
}
