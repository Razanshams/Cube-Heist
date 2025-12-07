using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    public InputAction MoveAction;
    public float speed = 5f;

    private CharacterController controller;
    private Vector2 moveInput;
    private Animator anim;

    private void Awake() {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable() {
        MoveAction.Enable();
    }

    private void OnDisable() {
        MoveAction.Disable();
    }

    private void Update() {
        moveInput = MoveAction.ReadValue<Vector2>();

        Vector3 move = new Vector3(-moveInput.x, 0f, -moveInput.y);

        controller.Move(move * speed * Time.deltaTime);

        anim.speed = move.magnitude;

        RotateTowardsMovement(move);
    }

    private void RotateTowardsMovement(Vector3 move) {
        if (move.sqrMagnitude > 0.001f) {
            Quaternion targetRotation = Quaternion.LookRotation(move);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.15f);
        }
    }
}
