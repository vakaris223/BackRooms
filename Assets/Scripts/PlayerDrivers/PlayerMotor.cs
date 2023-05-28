using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5;
    public float runSpeed = 7;
    private bool isRun;
    private bool isGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    //pulls inputs from InputManager.cs and apply them to character controller
    public void ProccessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        //from 2d to 3d  (y -> z)
        moveDirection.z = input.y;

        if(isRun)
        {
            controller.Move(transform.TransformDirection(moveDirection) * runSpeed * Time.deltaTime);
        }
        else if(!isRun)
        {
            controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        

    }

    public void Jump()
    {
        if(isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Run(float run)
    {
        if(run > 0)
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }
    }
}
