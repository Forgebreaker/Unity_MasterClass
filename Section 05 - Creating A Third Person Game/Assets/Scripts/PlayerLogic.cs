using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [Header("Simple Movement")]
        [SerializeField] private float MoveSpeed = 2.5f;
        private float OriginalMoveSpeed;
        private CharacterController _characterController;
        private Vector3 MovementDirection;
        public float HorizontalInput;
        public float VerticalInput;
        [SerializeField] private List<AudioClip> FootStepOnMetalSound;
        private AudioSource _audioSource;
        [SerializeField] private AudioClip MonsterRoar;
        private float RoarCoolDown = 0f;

    [Header("Jump & Gravity")]
        [SerializeField] private GameObject GroundCheckPoint;
        [SerializeField] private float GroundCheckRadius;
        [SerializeField] private LayerMask WhatIsGround;
        [SerializeField] private bool IsGrounded;
        [SerializeField] private float Gravity = -9.81f * 2;
        private Vector3 JumpDirection;
        private float JumpForce = 2f;
        private bool AbleToJump;

    [Header("Animator")]

        private Animator _animator;

    [Header("Rotate Character")]
        private Camera _mainCamera;
        private CameraLogic CameraController;

    /*Player's Instance*/
    public static PlayerLogic Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        OriginalMoveSpeed = MoveSpeed;
        _characterController = this.gameObject.GetComponent<CharacterController>();
        _animator = this.gameObject.GetComponent<Animator>();
        _audioSource = this.gameObject.GetComponent<AudioSource>();
        _mainCamera = Camera.main;
        if (_mainCamera) 
        {
            CameraController = _mainCamera.GetComponent<CameraLogic>();
        }
    }

    void Update()
    {
        if (RoarCoolDown <= 0)
        {
            HorizontalInput = Input.GetAxis("Horizontal");
            VerticalInput = Input.GetAxis("Vertical");
        }
        if (Mathf.Abs(HorizontalInput) > 0 && Mathf.Abs(VerticalInput) > 0)
        {
            MoveSpeed = OriginalMoveSpeed * 0.75f;
        }
        else 
        {
            MoveSpeed = OriginalMoveSpeed;
        }

        if (Mathf.Abs(HorizontalInput + VerticalInput) != 0 && (Input.GetAxis("Mouse X") != 0))
        {
            this.gameObject.transform.forward = CameraController.GetForwardVector();
        }

        _animator.SetFloat("HorizontalInput", HorizontalInput);
        _animator.SetFloat("VerticalInput", VerticalInput);

        IsGrounded = Physics.CheckSphere(GroundCheckPoint.transform.position, GroundCheckRadius, WhatIsGround);
        _animator.SetBool("IsGrounded", IsGrounded);
        _animator.SetFloat("Velocity", JumpDirection.y);

        if (IsGrounded == true && Input.GetKeyDown(KeyCode.Space) && AbleToJump == false) 
        { 
            AbleToJump = true;
        }

        // Monster Roar

        if (RoarCoolDown > 0) 
        { 
            RoarCoolDown -= Time.deltaTime;
            HorizontalInput = 0;
            VerticalInput = 0;
        }

        if (Input.GetKeyDown(KeyCode.R) && RoarCoolDown <= 0 && IsGrounded)
        {
            _animator.SetTrigger("Roar");
            RoarCoolDown = 5.5f;
        }

    }

    private void FixedUpdate()
    {
        // Simple Movement

        MovementDirection = this.gameObject.transform.forward * VerticalInput + this.gameObject.transform.right * HorizontalInput;
        _characterController.Move(MovementDirection * Time.deltaTime * MoveSpeed);

        // Gravity System

        if (IsGrounded == false)
        {
            JumpDirection.y += Gravity * Time.deltaTime;
        }
        else if (IsGrounded == true) 
        {
            JumpDirection.y = -1f;
        }

        if (JumpDirection.y < 0) 
        { 
            JumpDirection.y = Mathf.Max(JumpDirection.y, Gravity * Time.deltaTime * 120);
        }

        // Jump System

        if (AbleToJump == true) 
        {
            JumpDirection.y = Mathf.Sqrt(JumpForce * -2 * Gravity);
            AbleToJump = false;
        }

        /*Gravity Affect*/ _characterController.Move(JumpDirection * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GroundCheckPoint.transform.position, GroundCheckRadius);
    }

    public void PlayFootStepSound() 
    {
        if (FootStepOnMetalSound.Count > 0 && _audioSource) 
        {
            _audioSource.PlayOneShot(FootStepOnMetalSound[Random.Range(0, FootStepOnMetalSound.Count - 1)]);
        }
    }

    public void PlayRoarSound() 
    {
        if (MonsterRoar && _audioSource) 
        {
            _audioSource.PlayOneShot(MonsterRoar);
        }
    }


}
