using UnityEngine;

public class GuardMovement : MonoBehaviour {
    public Transform leftPoint;
    public Transform rightPoint;
    public float speed = 2f;

    private Transform target;

    void Start() {
        target = rightPoint;
    }

    void Update() {
        Vector3 targetPos = new Vector3(
            target.position.x,
            transform.position.y,
            transform.position.z
        );

        Vector3 moveDir = (targetPos - transform.position).normalized;

        // move
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            speed * Time.deltaTime
        );

        
        if (moveDir.sqrMagnitude > 0.0001f) {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                0.15f
            );
        }

        
        if (Mathf.Abs(transform.position.x - target.position.x) < 0.1f) {
            target = target == rightPoint ? leftPoint : rightPoint;
        }
    }
}
