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
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerDeath = playerObj.GetComponent<PlayerDeath>();
        }
        
        
        if (showVisualCone)
        {
            CreateVisualCone();
        }
    }

    void Update()
    {
        if (player == null || playerDeath == null) return;
        if (playerDeath.IsDead()) return; 
        
        
        Vector3 directionToPlayer = player.position - transform.position;
        float distance = directionToPlayer.magnitude;
        
        
        if (distance <= detectionRange)
        {
            
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            
            
            if (angle <= detectionAngle)
            {
                
                playerDeath.Die();
            }
        }
    }

    
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
        
        visualCone = new GameObject("DetectionCone");
        visualCone.transform.SetParent(transform);
        visualCone.transform.localPosition = Vector3.zero;
        visualCone.transform.localRotation = Quaternion.identity;
        
        MeshFilter meshFilter = visualCone.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = visualCone.AddComponent<MeshRenderer>();
        
      
        Mesh mesh = new Mesh();
        
        // Calculate the triangle points
        float widthAtEnd = detectionRange * Mathf.Tan(detectionAngle * Mathf.Deg2Rad);
        
        Vector3[] vertices = new Vector3[3]
        {
            new Vector3(0, 0.05f, 0),                           
            new Vector3(-widthAtEnd, 0.05f, detectionRange),   
            new Vector3(widthAtEnd, 0.05f, detectionRange)     
        };
        
        int[] triangles = new int[6]
        {
            0, 1, 2,  
            0, 2, 1   
        };
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
        meshFilter.mesh = mesh;
        
    
        Material mat = new Material(Shader.Find("Unlit/Color"));
        mat.color = new Color(1f, 0f, 0f, 1f);
        meshRenderer.material = mat;
    }
}