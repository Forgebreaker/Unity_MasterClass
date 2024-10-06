using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerLogic : MonoBehaviour
{
    [Header("Movement")]
        private CharacterController characterController;
        private float horizontalInput;
        private float verticalInput;
        private Vector3 movementDirection;
        [SerializeField] private float originalMoveSpeed;
        private float moveSpeed;
        [SerializeField] bool FaceDirectionMoving;

    [Header("Jump")]
        private Vector3 velocityDirection;
        [SerializeField] private Transform isGroundedCheckPoint;
        [SerializeField] private float isGroundedCheckPointRadius;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private float gravity = -9.81f * 3f;
        [SerializeField] private float jumpForce;
        private bool isGrounded;
        private bool abled2Jump;
        [SerializeField] private bool ActiveJumpFunction;

    [Header("Weapon")]

        [SerializeField] private GameObject HandHoldWeapon;
        [SerializeField] private GameObject InteractableObject = null;
        public bool IsEquiping;

    [Header("Health")]

        [SerializeField] private int PlayerMaxHealth = 100;
        [SerializeField] private int PlayerCurrentHealth;
        [SerializeField] private TMP_Text PlayerHealthDisplay;
        private MeshRenderer[] PlayerComponents;
        private bool IsAlive;
        string currentSceneName;


    [Header("Teleport")]

        private float PlayerCurrentTeleportCoolDown = 0;
        public float CurrentTeleportCoolDown
        {
            get { return PlayerCurrentTeleportCoolDown;  }
            set { PlayerCurrentTeleportCoolDown = value; }
        }
        [SerializeField] private float PlayerTeleportCoolDown = 0.25f;
        public float TeleportCoolDown
        {
            get { return PlayerTeleportCoolDown; }
        }
    /*[Object Instance]*/ public static PlayerLogic instance;
    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        characterController = this.gameObject.GetComponent<CharacterController>();
        instance = this;
        PlayerCurrentHealth = PlayerMaxHealth;
        IsAlive = true;

    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        isGrounded = Physics.CheckSphere(isGroundedCheckPoint.position, isGroundedCheckPointRadius, whatIsGround);

        // other way to normalize movement direction since the original way has a small issue
        if (horizontalInput != 0 && verticalInput != 0)
        {
            moveSpeed = originalMoveSpeed * 0.75f;
        }
        else
        {
            moveSpeed = originalMoveSpeed;
        }

        // Jump condition
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space) && abled2Jump == false && ActiveJumpFunction == true)
        {
            abled2Jump = true;
        }
        
        EquipedWeapon();
        PlayerHealthSystem();

        if (PlayerCurrentTeleportCoolDown > 0) 
        {
            PlayerCurrentTeleportCoolDown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        if ((horizontalInput + verticalInput) != 0 && FaceDirectionMoving)
        {
            this.gameObject.transform.forward = movementDirection;
            // the forward definition vector of the character is now equal to the move Direction
            // this means that the character will change its face direction base on the direction it's moving on
        }

        RotateCharacterSystem();

        if (IsAlive)
        {
            characterController.Move(movementDirection * Time.deltaTime * moveSpeed);
        }
        // Gravity System

        if (isGrounded == false)
        {
            velocityDirection.y += gravity * Time.deltaTime;
        }
        else if (isGrounded == true)
        {
            velocityDirection.y = -1f;
        }

        // Teleport-like fall prevent

        if (velocityDirection.y < 0)
        {
            velocityDirection.y = Mathf.Max(-10f, velocityDirection.y);
        }

        // Jumping Logic

        if (abled2Jump == true)
        {
            velocityDirection.y = Mathf.Sqrt(jumpForce * Mathf.Abs(gravity * 2));
            // physics equation for motion under uniform acceleration (like gravity).
            // It calculates how much initial upward velocity is needed to overcome gravity and achieve the desired height
            // Now velocityDirection.y have a value big enough to overcome gravity * Time.deltaTime
            // => instead of a negative value when minus gravity * Time.deltaTime, it will have a short time of positive value (create a smooth jump)
            abled2Jump = false;
        }

        characterController.Move(velocityDirection * Time.deltaTime);
    }

    private void RotateCharacterSystem()
    {
        Vector3 mousePosition_ScreenSpace = Input.mousePosition;
            // Gets the position of the mouse on the screen.
            // It is a 2D position (X and Y), but in Unity, this is represented as a 3D vector (X, Y, Z).
        Vector3 playerPosition_WorldSpace = this.gameObject.transform.position;
            // Logic of this is to convert the player position from WorldSpace to ScreenSpace so that you can control it rotation using our mouse
            // in this case is to make it facing toward the mouse cursor
        Vector3 playerPosition_ScreenSpace = Camera.main.WorldToScreenPoint(playerPosition_WorldSpace);
            // With this, I used camera to convert the player position from world space to screen space
        Vector3 Direction = mousePosition_ScreenSpace - playerPosition_ScreenSpace;
            // By minus these 2 Vector, we have the direction that player should facing to
            // But that is just the direction it should look to, and how can we make it do that
        float Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
            // https://drive.google.com/file/d/1_1JOdAHelFIo83YqwQNwRfSJEsNxiGqf/view?usp=sharing
            // https://drive.google.com/file/d/1oaT2GFVrwwZQ8MVc1JQsxlbRTDye6zI7/view?usp=sharing
        this.gameObject.transform.rotation = Quaternion.AngleAxis(90 - Angle, Vector3.up);
            // https://drive.google.com/file/d/12wtM5gphJc2w2_Nn-pAdbtpiN6hai8HU/view?usp=sharing
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gun" && IsEquiping == false)
        {
            InteractableObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Gun" && IsEquiping == false)
        {
            InteractableObject = null;
        }
    }

    private void PlayerHealthSystem()
    {
        PlayerHealthDisplay.text = $"Health: {PlayerCurrentHealth}";
        PlayerCurrentHealth = Mathf.Clamp(PlayerCurrentHealth, 0, PlayerMaxHealth + 1);
        if (PlayerCurrentHealth <= 0)
        {
            IsAlive = false;
            PlayerComponents = this.gameObject.GetComponentsInChildren<MeshRenderer>(); // include itself
            for(int counter = 0; counter < PlayerComponents.Length; counter++)
            {
                PlayerComponents[counter].enabled = false;
            }
            characterController.Move(Vector3.zero);
            StartCoroutine(PlayerWaitAndDie());
        }

    }
    IEnumerator PlayerWaitAndDie()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(currentSceneName);
    }

    public void TakeDamage(int Damage)
    {
        if (IsAlive == true)
        {
            PlayerCurrentHealth -= Damage;
        }
    }

    private void EquipedWeapon()
    {
        if (IsEquiping == false && InteractableObject != null && Input.GetKeyDown(KeyCode.E) && PlayerCurrentHealth > 0)
        {
            GunLogic GunData = InteractableObject.GetComponent<GunLogic>();
            if (GunData)
            {
                if (GunData.EquipedData == false)
                {
                    IsEquiping = true;
                    GunData.Equiped();
                }
            }
        }

        if (IsEquiping == true) 
        {
            GunLogic GunData = InteractableObject.GetComponent<GunLogic>();
            if (GunData)
            {
                GunData.transform.position = HandHoldWeapon.transform.position;
                GunData.transform.rotation = HandHoldWeapon.transform.rotation;
                GunData.transform.parent = this.gameObject.transform;
            }
            if (Input.GetKeyDown(KeyCode.Q) || PlayerCurrentHealth <= 0)
            {
                if (GunData.EquipedData == true)
                {
                    IsEquiping = false;
                    GunData.UnEquiped();
                    GunData.transform.parent = null;
                }
            }
        }


     }
}
