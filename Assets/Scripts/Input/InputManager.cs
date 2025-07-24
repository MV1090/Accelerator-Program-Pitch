using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;
    
    [SerializeField] Player player;
    
    void Awake()
    {
        playerInput = new PlayerInput();
    }

    void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.Move.performed += player.Move;
        playerInput.Player.Move.canceled += player.StopMove;     
        playerInput.Player.FirstAction.performed += player.FirstAction;
        playerInput.Player.SecondAction.performed += player.SecondAction;
        playerInput.Player.ThirdAction.performed += player.ThirdAction;
        playerInput.Player.Jump.performed += player.Jump;
    }
    void OnDisable()
    {
        playerInput.Disable();
        playerInput.Player.Move.performed -= player.Move;
        playerInput.Player.Move.canceled -= player.StopMove;     
        playerInput.Player.FirstAction.performed -= player.FirstAction;
        playerInput.Player.SecondAction.performed -= player.SecondAction;
        playerInput.Player.ThirdAction.performed -= player.ThirdAction;
        playerInput.Player.Jump.performed -= player.Jump;
    }

}
