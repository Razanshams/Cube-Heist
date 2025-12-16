using UnityEngine;

public class CameraDetection : MonoBehaviour
{
    public float spotRadius = 3f;
    public GameObject cameraTarget;
    public GameObject cameraModel;

    private Transform player;
    private PlayerDeath playerDeath;
    private Vector3 targetScale;
    private float previousSpotRadius;

    void Start()
    {
        ApplyScaleToTarget();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerDeath = playerObj.GetComponent<PlayerDeath>();
        }
    }

    void Update()
    {
        if (player == null || playerDeath == null) return;
        if (playerDeath.IsDead()) return;

        if (spotRadius != previousSpotRadius) // Only update target's scaling if a change has been made
        {
            ApplyScaleToTarget();
        }

        Vector3 directionToPlayer = player.position - cameraTarget.transform.position;
        float distance = directionToPlayer.magnitude;
        cameraModel.transform.LookAt(cameraTarget.transform);

        if (distance <= spotRadius)
        {
            playerDeath.Die();
        }
    }

    void ApplyScaleToTarget()
    {
        targetScale = new Vector3(2 * spotRadius, 0.01f, 2 * spotRadius);
        previousSpotRadius = spotRadius;
        cameraTarget.transform.localScale = targetScale;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(cameraTarget.transform.position, spotRadius);
    }
}
