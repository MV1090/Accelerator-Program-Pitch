using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum CameraMode { FirstPerson, ThirdPerson }
    public CameraMode currentMode = CameraMode.FirstPerson;

    public Transform firstPersonTarget;
    public Transform thirdPersonTarget;
    public float mouseSensitivity = 100f;
    public float thirdPersonSmooth = 5f;
    public Transform playerBody;

    float xRotation = 0f;
    float orbitYaw = 0f;
    float orbitPitch = 20f; // Default pitch for third person
    public float orbitDistance = 4f;

    // Reference to the Player script to check movement
    public Player playerScript;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SetCameraMode(currentMode);
    }

    public void SetCameraMode(CameraMode mode)
    {
        currentMode = mode;
        if (mode == CameraMode.FirstPerson)
        {
            transform.SetPositionAndRotation(firstPersonTarget.position, firstPersonTarget.rotation);
        }
        else
        {
            // Snap to third-person position
            UpdateThirdPersonCamera(true);
        }
    }

    public void ToggleCameraMode()
    {
        if (currentMode == CameraMode.FirstPerson)
            SetCameraMode(CameraMode.ThirdPerson);
        else
            SetCameraMode(CameraMode.FirstPerson);
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (currentMode == CameraMode.FirstPerson)
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            if (playerBody != null)
                playerBody.Rotate(Vector3.up * mouseX);
        }
        else if (currentMode == CameraMode.ThirdPerson)
        {
            bool isMoving = playerScript != null && playerScript.moveInput.sqrMagnitude > 0.05f;

            // Always update pitch and yaw with mouse input
            orbitYaw += mouseX;
            orbitPitch -= mouseY;
            orbitPitch = Mathf.Clamp(orbitPitch, 5f, 80f);

            if (isMoving)
            {
                // When moving, rotate player to match camera yaw
                if (playerBody != null)
                {
                    Quaternion targetRotation = Quaternion.Euler(0f, orbitYaw, 0f);
                    playerBody.rotation = Quaternion.Slerp(playerBody.rotation, targetRotation, Time.deltaTime * 10f);
                }
            }
        }
    }

    void LateUpdate()
    {
        if (currentMode == CameraMode.ThirdPerson)
        {
            UpdateThirdPersonCamera(false);
        }
    }

    void UpdateThirdPersonCamera(bool instant)
    {
        // Calculate orbit position
        Vector3 offset = Quaternion.Euler(orbitPitch, orbitYaw, 0) * new Vector3(0, 0, -orbitDistance);
        Vector3 targetPos = playerBody.position + offset;
        Quaternion targetRot = Quaternion.LookRotation(playerBody.position - targetPos, Vector3.up);

        if (instant)
        {
            transform.position = targetPos;
            transform.rotation = targetRot;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * thirdPersonSmooth);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * thirdPersonSmooth);
        }
    }
} 