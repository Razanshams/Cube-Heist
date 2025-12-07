using UnityEngine;

public class GuardDetection : MonoBehaviour
{
    public float detectionRange = 5f;
    public float detectionAngle = 45f;
    public bool showVisualCone = true; // Toggle this on/off
    
    private Transform player;
    private PlayerDeath playerDeath;
    private GameObject visualCone;

    void Start()
    {
        // Find the player
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerDeath = playerObj.GetComponent<PlayerDeath>();
        }
        
        // Create visible cone
        if (showVisualCone)
        {
            CreateVisualCone();
        }
    }

    void Update()
    {
        if (player == null || playerDeath == null) return;
        if (playerDeath.IsDead()) return; // Don't detect if already dead
        
        // Check if player is in detection zone
        Vector3 directionToPlayer = player.position - transform.position;
        float distance = directionToPlayer.magnitude;
        
        // Is player close enough?
        if (distance <= detectionRange)
        {
            // Calculate angle to player
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            
            // Is player in the cone?
            if (angle <= detectionAngle)
            {
                // CAUGHT!
                playerDeath.Die();
            }
        }
    }

    // Shows the detection cone in the editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Vector3 forward = transform.forward * detectionRange;
        Vector3 leftEdge = Quaternion.Euler(0, -detectionAngle, 0) * forward;
        Vector3 rightEdge = Quaternion.Euler(0, detectionAngle, 0) * forward;
        
        Gizmos.DrawLine(transform.position, transform.position + leftEdge);
        Gizmos.DrawLine(transform.position, transform.position + rightEdge);
        Gizmos.DrawLine(transform.position + leftEdge, transform.position + rightEdge);
    }
    
    void CreateVisualCone()
    {
        // Create a simple cube to represent the detection zone
        visualCone = GameObject.CreatePrimitive(PrimitiveType.Cube);
        visualCone.transform.SetParent(transform);
        visualCone.transform.localPosition = new Vector3(0, 0, detectionRange / 2);
        
        // Calculate width based on angle at the far end of the cone
        float widthAtEnd = 2f * detectionRange * Mathf.Tan(detectionAngle * Mathf.Deg2Rad);
        
        visualCone.transform.localScale = new Vector3(
            widthAtEnd,
            0.1f,
            detectionRange
        );
        
        // Remove collider so it doesn't block anything
        Destroy(visualCone.GetComponent<Collider>());
        
        // Make it red - using Unlit shader which always works
        Renderer renderer = visualCone.GetComponent<Renderer>();
        Material mat = new Material(Shader.Find("Unlit/Color"));
        mat.color = new Color(1f, 0f, 0f, 1f); // Bright red
        renderer.material = mat;
    }
}