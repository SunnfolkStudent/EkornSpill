using UnityEngine;

public class InputActions : MonoBehaviour
{
    private InputSystem_Actions _inputSystem;

    public float Horizontal;
    
    public float Vertical;

    public bool Jump;
    public bool Attack;
    public bool Interact;

    private void Update()
    {
        Horizontal = _inputSystem.Player.Move.ReadValue<Vector2>().x;
        Vertical = _inputSystem.Player.Move.ReadValue<Vector2>().y;
        Jump = _inputSystem.Player.Jump.WasPressedThisFrame();
    }

    private void Awake()
    {
        _inputSystem = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _inputSystem.Enable();
    }

    private void OnDisable()
    {
        _inputSystem.Disable();
    }
}
