using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [Header("Movement")]
        private CharacterController characterController;
        private float horizontalInput;
        private Vector3 movementDirection;
        [SerializeField] private float moveSpeed;

    [Header("Jump")]
        private Vector3 velocityDirection;
        [SerializeField] private Transform isGroundedCheckPoint;
        [SerializeField] private float isGroundedCheckPointRadius;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private float gravity = -9.81f * 3f;
        [SerializeField] private float jumpForce;
        private bool isGrounded;
        private bool abled2Jump;


    private Animator animator;
    public static PlayerLogic instance;

    [SerializeField] private
    void Awake()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        instance = this;
    }
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics.CheckSphere(isGroundedCheckPoint.position, isGroundedCheckPointRadius, whatIsGround);

        this.gameObject.transform.position = new Vector3(0,transform.position.y,transform.position.z);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space) && abled2Jump == false) 
        { 
            abled2Jump = true;
        }

        animator.SetFloat("Horizontal_Input", Mathf.Abs(horizontalInput));
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("Velocity", velocityDirection.y);
    }

    void FixedUpdate()
    {

        // Movement
        movementDirection = new Vector3(0, 0, horizontalInput);
        
        characterController.Move(movementDirection * moveSpeed * Time.deltaTime);

        if (horizontalInput < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (horizontalInput > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // Gravity

        if (isGrounded == false)
        {
            velocityDirection.y += gravity * Time.deltaTime;

        }
        else 
        { 
            velocityDirection.y = 0;
        }

        if (velocityDirection.y < 0)
        {
            velocityDirection.y = Mathf.Max(velocityDirection.y, gravity * Time.deltaTime * 30);
        }

        // Jump

        if (abled2Jump == true) 
        {
            velocityDirection.y = Mathf.Sqrt(jumpForce * -2f * gravity); 

            abled2Jump = false;
        }

        characterController.Move(velocityDirection * Time.deltaTime);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(isGroundedCheckPoint.position, isGroundedCheckPointRadius);
    }

    public void Save() 
    {
        // Store Player position
        PlayerPrefs.SetFloat("PositionX", this.gameObject.transform.position.x);
        PlayerPrefs.SetFloat("PositionY", this.gameObject.transform.position.y);
        PlayerPrefs.SetFloat("PositionZ", this.gameObject.transform.position.z);

        // PlayerPrefs can only save 3 types of data (int, float and string) and store them locally => just good for single player offline game

        PlayerPrefs.SetFloat("RotationX", this.gameObject.transform.eulerAngles.x); // https://en.wikipedia.org/wiki/Euler_angles
        PlayerPrefs.SetFloat("RotationY", this.gameObject.transform.eulerAngles.y);
        PlayerPrefs.SetFloat("RotationZ", this.gameObject.transform.eulerAngles.z);
        /*
        Debug.Log("X: " + this.gameObject.transform.eulerAngles.x);
        Debug.Log("Y: " + this.gameObject.transform.eulerAngles.y);
        Debug.Log("Z: " + this.gameObject.transform.eulerAngles.z);
        */
    }
    public void Load() 
    {
        float Player_PositionX = PlayerPrefs.GetFloat("PositionX");
        float Player_PositionY = PlayerPrefs.GetFloat("PositionY");
        float Player_PositionZ = PlayerPrefs.GetFloat("PositionZ");

        float Player_RotationX = PlayerPrefs.GetFloat("RotationX");
        float Player_RotationY = PlayerPrefs.GetFloat("RotationY");
        float Player_RotationZ = PlayerPrefs.GetFloat("RotationZ");

        // Teleport to the saved position
        characterController.enabled = false; // prevent teleport error
        this.gameObject.transform.position = new Vector3(Player_PositionX, Player_PositionY, Player_PositionZ);
        this.gameObject.transform.rotation = Quaternion.Euler(Player_RotationX, Player_RotationY, Player_RotationZ);
        characterController.enabled = true;
    }
}
