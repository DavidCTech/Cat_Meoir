using UnityEditor;
using UnityEngine;

 [CustomEditor(typeof(ClimbRaycast))]
public class ClimbRaycastEditor : Editor
{
    private void OnSceneGUI()
    {
        ClimbRaycast climbRaycast = (ClimbRaycast)target;

        // Ensure that the ClimbRaycast component is not null
        if (climbRaycast != null)
        {
            // Set the handle color
            Handles.color = Color.red;

            // Get the player's position
            Vector3 playerPosition = climbRaycast.transform.position;

            // Calculate the position for the maximum distance along the forward axis
            Vector3 maxDistancePosition = playerPosition + climbRaycast.transform.forward * climbRaycast.maxDistance;

            // Draw a line from the player to the calculated position
            Handles.DrawLine(playerPosition, maxDistancePosition);
        }
    }
}
