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

    [Header("Teleport")]
        [SerializeField] public float TeleportCoolDown = 2f;
        public float CurrentTeleportCoolDown = 0;

    public static PlayerLogic instance;

    [SerializeField] private
    void Awake()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        instance = this;
    }
    void Start()
    {
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics.CheckSphere(isGroundedCheckPoint.position, isGroundedCheckPointRadius, whatIsGround);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space) && abled2Jump == false) 
        { 
            abled2Jump = true;
        }

        CurrentTeleportCoolDown -= Time.deltaTime;

        if (CurrentTeleportCoolDown <= 0) 
        { 
            CurrentTeleportCoolDown = 0;
        }

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

            // [Only Fall]

                //Frame 1 <=> 0 + (-29.43) * 0.02 = -0.5886
                //Frame 2 <=> 0 - 0.5886 * 2 = -1.1772
                //....
                //Frame 100 <=> 0 - 0.5886 * 100 = -58.86 => See! still too fast

            // [Jump and Fall]

                //Frame 2 <=> 13.4 + (-0.5886) = 12.8114
                //Frame 3 <=> 12.8114 + (-0.5886) = 12.2228
                //....
                //Frame 100 <=> 12.8114 + (-0.5886) * 100 = -46.0486 => See! still too fast
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
            velocityDirection.y = Mathf.Sqrt(jumpForce * -2f * gravity); // this formula calculates how fast an object needs to start moving upwards so that, under the influence of gravity, it will reach a desired height
            // [Jump and Fall]
                // Frame 1 <=> velocityDirection.y = 13.4
            abled2Jump = false;
        }

        characterController.Move(velocityDirection * Time.deltaTime);
        // => that's why we have this line

        // [When Fall]
            //Frame 1 <=> -29.43 * 0.02 * 0.02 = 0.5886 * 0.02 = -0.01172
            //Frame 2 <=> -0.5886 * 2 * 0.02 = 1.1772 * 0.02 = -0.023
            //....
            //Frame 100 <=> -0.5886 * 100 * 0.02 = 58.86 * 0.02 = -1.172 => Much more better now!

        // [When Jump and fall]
            //Frame 1 <=> 12.8114 * 0.02 = 0.256228
            //Frame 2 <=> 12.2228 * 0.02 = 0.244456
            //....
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(isGroundedCheckPoint.position, isGroundedCheckPointRadius);
    }
}
