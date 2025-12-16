using UnityEngine;

public class GuardMovement1 : MonoBehaviour {
    public Transform upPoint;
    public Transform downPoint;
    public float speed = 2f;

    private Transform target;

    void Start() {
        target = upPoint;
    }

    void Update() {
        Vector3 targetPos = new Vector3(
            transform.position.x,
            transform.position.y,
            target.position.z
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

        
        if (Mathf.Abs(transform.position.z - target.position.z) < 0.1f) {
            target = target == upPoint ? downPoint : upPoint;
        }
    }
}
