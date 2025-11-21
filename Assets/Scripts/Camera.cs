using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;      // Drag your player here
    public Vector3 offset;        // Distance between camera and player
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
        //transform.LookAt(player.position + Vector3.up * 1.5f); 
    }
}

