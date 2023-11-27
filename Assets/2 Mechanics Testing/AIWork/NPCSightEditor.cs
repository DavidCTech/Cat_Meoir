#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NPCSight))]
public class NPCSightEditor : Editor
{
    //This script just shows AI for the Camera. Do not mess with it, it does not appear in a build or play mode. 
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        //calculate a circle 
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    private void OnSceneGUI()
    {

        NPCSight fov = (NPCSight)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.fovRadius);
        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.fovAngle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.fovAngle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.fovRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.fovRadius);

    }
}
#endif
