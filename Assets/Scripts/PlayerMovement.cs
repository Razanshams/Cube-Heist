using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction MoveAction;
    public float speed = 5f;

    private CharacterController controller;
    private Vector2 moveInput;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        MoveAction.Enable();
    }

    private void OnDisable()
    {
        MoveAction.Disable();
    }

    private void Update()
    {
        // Read input
        moveInput = MoveAction.ReadValue<Vector2>();

        // Convert 2D input to 3D movement
        Vector3 move = new Vector3(-moveInput.x, 0f, -moveInput.y);

        // Move the player
        controller.Move(move * speed * Time.deltaTime);
    }
}


