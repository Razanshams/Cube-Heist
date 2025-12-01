using UnityEngine;

public class GuardMovement : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public float speed = 2f;

    private Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = rightPoint;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);


        if (Mathf.Abs(transform.position.x - target.position.x) < 0.1f)
        {

            target = target == rightPoint ? leftPoint : rightPoint;

            /*if (target == rightPoint)
                transform.rotation = Quaternion.Euler(0, 0, 0);   // face right
            else
                transform.rotation = Quaternion.Euler(0, 180, 0); // face left*/
        }
    }
}