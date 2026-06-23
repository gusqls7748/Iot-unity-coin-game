using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class UFOMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float mouseSensitivity = 2.0f;
    public float stabilizationSpeed = 5.0f; // 수평 보정 속도

    private Rigidbody rb;
    private Vector3 moveInput;
    private float yaw;
    private float pitch;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 이동 입력 및 마우스 입력 (기존과 동일)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float height = 0f;
        if (Keyboard.current.spaceKey.isPressed) height = 1f;
        if (Keyboard.current.leftShiftKey.isPressed) height = -1f;
        moveInput = new Vector3(horizontal, height, vertical);

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        yaw += mouseDelta.x * mouseSensitivity * Time.deltaTime;
        pitch -= mouseDelta.y * mouseSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -80f, 80f);
    }

    void FixedUpdate()
    {
        // 1. 이동
        Vector3 force = transform.TransformDirection(moveInput) * moveSpeed;
        rb.linearVelocity = force;

        // 2. 수평 보정 로직 (Auto-Leveling)
        // 현재 회전값에서 X축과 Z축 회전을 0으로 만드는 타겟 회전값을 생성
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);

        // Slerp를 사용하여 현재 회전에서 타겟 회전으로 부드럽게 보정
        Quaternion smoothedRotation = Quaternion.Slerp(rb.rotation, targetRotation, stabilizationSpeed * Time.fixedDeltaTime);

        rb.MoveRotation(smoothedRotation);
    }
}