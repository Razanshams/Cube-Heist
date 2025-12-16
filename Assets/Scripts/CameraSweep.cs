using UnityEngine;

public class CameraSweep : MonoBehaviour {
    public float maxAngle = 45f;   // how far left/right
    public float speed = 1f;       // how fast it moves

    private float startY;

    void Start() {
        startY = transform.eulerAngles.y;
    }

    void Update() {
        float offset = Mathf.Sin(Time.time * speed) * maxAngle;
        transform.rotation = Quaternion.Euler(
            0f,
            startY + offset,
            0f
        );
    }
}
