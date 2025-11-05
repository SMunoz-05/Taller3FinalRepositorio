using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController characterController;
    public float speed = 10f;

    private float gravity = -9.81f;
    Vector3 velocity;

    public Transform groundCheck; 
    public float sphereRadius = 0.3f;
    public LayerMask groundMask;

    bool isGrounded;

    public float jumpHeight = 3f;


    public bool isSprinting;
    public float sprintSpeedMultiplier = 1.5f;
    private float sprintSpeed = 1;

    public float staminaUseAmount = 5;
    private StaminaBar staminaSlider;

    public Animator animator;

    private void Start()
    {
        staminaSlider = FindFirstObjectByType<StaminaBar>();
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position,sphereRadius,groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        animator.SetFloat("VelX", x);
        animator.SetFloat("VelZ", z);
        animator.SetBool("isSprinting", isSprinting);


        Vector3 move = transform.right * x + transform.forward * z;

        CheckJump();

        RunCheck();

        characterController.Move(move * speed * Time.deltaTime*sprintSpeed);


        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);



    }

    public void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

            animator.SetBool("isJumping", true);
        }
        if (!isGrounded)
        {
            animator.SetBool("isJumping", false);
        }

    }

    public void RunCheck()
    {

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = !isSprinting;

            if (isSprinting == true)
            {
                staminaSlider.UseSTamina(staminaUseAmount);
            }
            else
            {
                staminaSlider.UseSTamina(0);
            }
        }

        if (isSprinting == true)
        {
            sprintSpeed = sprintSpeedMultiplier;


        }
        else
        {
            sprintSpeed = 1;


        }

    }

}
