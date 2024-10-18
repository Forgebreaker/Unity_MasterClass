using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerID 
{ 
    Player1,
    Player2
}

public class PlayerLogic : MonoBehaviour
{
    [Header("Movement")]

    private CharacterController _characterController;
    public float HorizontalInput;
    public float VerticalInput;
    [SerializeField] private float OriginalMoveSpeed = 5f;
    private float MoveSpeed;
    private Vector3 MovementDirection;
    public bool AbleToMove = true;


    [Header("Jump And Gravity")]

    [SerializeField] private float Gravity = -9.81f * 2;
    private bool IsGrounded;
    private bool AbleToJump;
    private Vector3 JumpDirection;
    [SerializeField] private float JumpForce = 2f;
    [SerializeField] private GameObject IsGrounded_CheckPoint;
    [SerializeField] private float IsGrounded_Radius;
    [SerializeField] private LayerMask WhatIsGround;

    [Header("Audio")]

    [SerializeField] private AudioClip ShootingSound;
    [SerializeField] private List<AudioClip> WalkingSoundList;
    private Animator _animator;
    private AudioSource _audioSource;

    [Header("Shoot")]
    public bool IsShooting;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject ShootingPoint;
    [SerializeField] private float ShootingCoolDown = 1.5f;
    private bool AbleToShoot = true;

    [Header("Other Stuffs")]

    [SerializeField] private PlayerID _playerID;
    [SerializeField] private bool IsAlive;
    private float RespawnCoolDown = 5f;
    private float CurrentRespawnCoolDown = 0;
    [SerializeField] private GameObject RespawnPoint;

    // Event
    public delegate void PlayerDeath(int Player_ID);
    public static event PlayerDeath OnPlayerDeath;


    private void Start()
    {
        MoveSpeed = OriginalMoveSpeed;
        _animator = this.gameObject.GetComponent<Animator>();
        _characterController = this.gameObject.GetComponent<CharacterController>();
        _audioSource = this.gameObject.GetComponent<AudioSource>();
        IsAlive = true;
        Debug.Log($"{_playerID}"); // output: Player1 or Player2
    }

    private void Update()
    {
        if (IsAlive == false) 
        {
            CurrentRespawnCoolDown -= Time.deltaTime;
            return;
        }

        HorizontalInput = Input.GetAxis($"Horizontal_{_playerID}");
        VerticalInput = Input.GetAxis($"Vertical_{_playerID}");

        if (Mathf.Abs(HorizontalInput) > 0.1 && Mathf.Abs(VerticalInput) > 0.1)
        {
            MoveSpeed = OriginalMoveSpeed * 0.75f;
        }
        else
        {
            MoveSpeed = OriginalMoveSpeed;
        }

        _animator.SetFloat("Moving", Mathf.Abs(HorizontalInput) + Mathf.Abs(VerticalInput));

        IsGrounded = Physics.CheckSphere(IsGrounded_CheckPoint.transform.position, IsGrounded_Radius, WhatIsGround);

        if (Input.GetButtonDown($"Jump_{_playerID}") && IsGrounded == true && AbleToJump == false)
        {
            AbleToJump = true;
        }

        _animator.SetFloat("Velocity", JumpDirection.y);

        if (Input.GetButton($"Fire_{_playerID}") && AbleToShoot == true) 
        {  
            StartCoroutine(Shoot());
        }
      
    }

    IEnumerator Shoot() 
    {
        AbleToShoot = false;
        _animator.SetTrigger("Shoot");
        yield return new WaitForSeconds(ShootingCoolDown);
        AbleToShoot = true;
    }

    private void FixedUpdate()
    {
        MovementDirection = new Vector3 (HorizontalInput, 0 ,VerticalInput) * MoveSpeed * Time.deltaTime;
        Vector3 LastDirection = Vector3.zero;

        if (Mathf.Abs(HorizontalInput) > 0.35f || Mathf.Abs(VerticalInput) > 0.35f) 
        {
            this.gameObject.transform.forward = new Vector3(HorizontalInput, 0, VerticalInput);
        }

        // Gravity System 

        if (IsGrounded == false)
        {
            JumpDirection.y += Gravity * Time.deltaTime;
        }
        else 
        {
            JumpDirection.y = 0f;
        }

        if (JumpDirection.y < 0) 
        {
            JumpDirection.y = Mathf.Max(JumpDirection.y, Gravity * Time.deltaTime * 90);
        }

        // Jumping

        if (AbleToJump == true) 
        {
            JumpDirection.y = Mathf.Sqrt(JumpForce * Mathf.Abs(Gravity * 2));
            AbleToJump = false;
        }

        if (AbleToMove == true)
        {
            _characterController.Move(MovementDirection);            
        }

        _characterController.Move(JumpDirection * Time.deltaTime);

        // Respawn System

        if (CurrentRespawnCoolDown > 0)
        {
            CurrentRespawnCoolDown -= Time.deltaTime;
        }

        if (CurrentRespawnCoolDown <= 0 && IsAlive == false)
        {
            Respawn();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(IsGrounded_CheckPoint.transform.position, IsGrounded_Radius);
    }

    public void ShootSystem()
    {
        Instantiate(Bullet, ShootingPoint.transform.position, ShootingPoint.transform.rotation);
        _audioSource.PlayOneShot(ShootingSound);
    }

    public void WalkingSoundSystem() 
    {
        _audioSource.PlayOneShot(WalkingSoundList[Random.Range(0, WalkingSoundList.Count - 1)]);
    }

    public void Die() 
    {
        if (IsAlive == true)
        {
            _animator.SetTrigger("Die");
            IsAlive = false;
            _characterController.enabled = false;
            CurrentRespawnCoolDown = RespawnCoolDown;
        }

        if (OnPlayerDeath != null) 
        {
            OnPlayerDeath(GetPlayerID());
        }
    }

    private int GetPlayerID() 
    {
        if (_playerID == PlayerID.Player1) 
        {
            return 1;
        }

        if (_playerID == PlayerID.Player2)
        {
            return 2;
        }

        return 0;
    }

    private void Respawn()
    {
        if (IsAlive == false)
        {
            IsAlive = true;
            this.gameObject.transform.position = RespawnPoint.transform.position;
            _characterController.enabled = true;
            _animator.SetTrigger("Respawn");
        }
    }
}
