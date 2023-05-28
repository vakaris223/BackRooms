using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //----------main-------------
    private PlayerInput playerInput;
        public PlayerInput.OnFootActions onFoot;
    //----------------------------

    //--------other input scritps--------
    private PlayerMotor motor;
    private PlayerLook look;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        onFoot.Jump.performed += ctx => motor.Jump();
        
    }
    void Update()
    {
         //use movement action values (1,-1...) to move player
         motor.ProccessMove(onFoot.Movement.ReadValue<Vector2>());
         look.ProcessLook(onFoot.Look.ReadValue<Vector2>(), true);
         motor.Run(onFoot.Run.ReadValue<float>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
