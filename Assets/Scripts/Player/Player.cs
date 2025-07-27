using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{   
    public Role currentRole;
    public Vector2 moveInput;
    public float jumpForce = 5f;
    public float speed = 5f;
    private Rigidbody rb;    
    public Transform weaponSocket;
    public GameObject weaponInstance;
        
    public void SetRole<T>() where T : Role
    {
        // Remove old role if it exists
        if (currentRole != null)
        {
            Destroy(currentRole);
        }
        // Add new role as a component
        currentRole = gameObject.AddComponent<T>();
    }

    void Awake()
    {
        SetRole<Prop>();
        rb = GetComponent<Rigidbody>();
    }    

    public void Move(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }    
    public void StopMove(InputAction.CallbackContext ctx)
    {
        moveInput = Vector2.zero;
    }

    public void FirstAction(InputAction.CallbackContext ctx)
    {
        currentRole.FirstAction();
    }
    public void SecondAction(InputAction.CallbackContext ctx)
    {
        currentRole.SecondAction();
    }
    public void ThirdAction(InputAction.CallbackContext ctx)
    {
        currentRole.ThirdAction();
    }
    public void Jump(InputAction.CallbackContext ctx)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    void FixedUpdate()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 move = (right * moveInput.x + forward * moveInput.y).normalized;
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime * speed);
    }
}
