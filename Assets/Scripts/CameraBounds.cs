using UnityEngine;

public class CameraBounds : Singleton<CameraBounds>
{
    public float left = -10f;
    public float right = 10f;
    public float up = 10f;
    public float down = -10f;
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        
        Gizmos.DrawLine(new Vector3(left, down), new Vector3(left, up)); // LEFT LINE
        Gizmos.DrawLine(new Vector3(right, down), new Vector3(right, up)); // RIGHT LINE
        Gizmos.DrawLine(new Vector3(left, up), new Vector3(right, up)); // UP LINE
        Gizmos.DrawLine(new Vector3(left, down), new Vector3(right, down)); // DOWN LINE
    }
}
