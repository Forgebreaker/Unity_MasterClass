using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState 
{ 
    Sleep,
    StandUp,
    Hunting,
    Attack,
    Die
}
public class EnemyLogic : MonoBehaviour
{
    private Animator _animator;
    private GameObject Player;
    private NavMeshAgent _navMeshAgent;
    private Collider _collider;
    [SerializeField] private bool IsAlive = true;
    private bool PlayerDetected;
    [SerializeField] private float DistanceCheckRadius;
    [SerializeField] public EnemyState CurrentEnemyState = EnemyState.Sleep;
    void Start()
    {
        _animator = this.gameObject.GetComponent<Animator>();
        _navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        _collider = this.gameObject.GetComponent<Collider>();    
    }

    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        
        switch (CurrentEnemyState)
        {
            case EnemyState.Sleep:
                IsAlive = true;
                SearchForPlayer();
                break;
            case EnemyState.StandUp:
                IsAlive = true;
                SearchForPlayer();
                _animator.SetBool("PlayerDetected", PlayerDetected);
                break;
            case EnemyState.Hunting:
                IsAlive = true;
                SearchForPlayer();
                HuntingMode();
                break;
            case EnemyState.Attack:
                IsAlive= true;
                SearchForPlayer();
                HuntingMode();
                _animator.SetTrigger("Attack");
                break;
            case EnemyState.Die:
                if (IsAlive == true)
                {
                    _animator.SetTrigger("Die");
                    IsAlive = false;
                }
                break;
        }
        _animator.SetBool("IsAlive", IsAlive);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.gameObject.transform.position, DistanceCheckRadius);
    }

    private void SearchForPlayer()
    {
        if (Player != null)
        {
            float Distance = Vector3.Distance(this.gameObject.transform.position, Player.transform.position);

            if (Distance < DistanceCheckRadius)
            {
                if (CurrentEnemyState == EnemyState.Sleep)
                {
                    PlayerDetected = true;
                    CurrentEnemyState = EnemyState.StandUp;
                }
            }
            else 
            {
                PlayerDetected = false;
            }
        }
    }
    private void HuntingMode()
    {
        _animator.SetBool("Attack", false);
        
        if (Player != null)
        {
            float Distance = Vector3.Distance(this.gameObject.transform.position, Player.transform.position);
            
            _navMeshAgent.SetDestination(Player.transform.position);
            
            if (Distance < 1.5)
            {
                CurrentEnemyState = EnemyState.Attack;     
            }
            else if (Distance > 2) 
            {
                CurrentEnemyState = EnemyState.Hunting;
            }
        }
    }
    public void SetEnemyState(EnemyState State)
    {
        CurrentEnemyState = State;
    }

    public void Save(int index) 
    {
        PlayerPrefs.SetInt($"Enemy{index}_State", (int)CurrentEnemyState);

        Debug.Log("(Save) Enemy" + index + ": " + (int)CurrentEnemyState);

        PlayerPrefs.SetFloat($"Enemy{index}_PositionX", this.gameObject.transform.position.x);
        PlayerPrefs.SetFloat($"Enemy{index}_PositionY", this.gameObject.transform.position.y);
        PlayerPrefs.SetFloat($"Enemy{index}_PositionZ", this.gameObject.transform.position.z);

        // PlayerPrefs can only save 3 types of data (int, float and string) and store them locally => just good for single player offline game

        PlayerPrefs.SetFloat($"Enemy{index}_RotationX", this.gameObject.transform.eulerAngles.x); // https://en.wikipedia.org/wiki/Euler_angles
        PlayerPrefs.SetFloat($"Enemy{index}_RotationY", this.gameObject.transform.eulerAngles.y);
        PlayerPrefs.SetFloat($"Enemy{index}_RotationZ", this.gameObject.transform.eulerAngles.z);

        // [!] remember to look up 
        AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
        // AnimatorStateInfo is a struct provided by Unity that holds information about the current state of an animation layer.
        // _animator.GetCurrentAnimatorStateInfo(0) refer to the "Base Layer" open the Animator > Layers to see
        int animationHash = info.fullPathHash;
        PlayerPrefs.SetInt($"Enemy{index}_CurrentAnimationStateLayer", animationHash);
        Debug.Log("Animation hash " + animationHash); // -1695557676
        float animationTime = info.normalizedTime;
        Debug.Log("Animation Time " + animationTime); // store the time after the current animation is activate, reset if you move to other animation
        PlayerPrefs.SetFloat($"Enemy{index}_CurrentAnimationStateTime", animationTime);
    }
    public void Load(int index) 
    {
        CurrentEnemyState = (EnemyState)PlayerPrefs.GetInt($"Enemy{index}_State");

        float Enemy_PositionX = PlayerPrefs.GetFloat($"Enemy{index}_PositionX");
        float Enemy_PositionY = PlayerPrefs.GetFloat($"Enemy{index}_PositionY");
        float Enemy_PositionZ = PlayerPrefs.GetFloat($"Enemy{index}_PositionZ");

        float Enemy_RotationX = PlayerPrefs.GetFloat($"Enemy{index}_RotationX");
        float Enemy_RotationY = PlayerPrefs.GetFloat($"Enemy{index}_RotationY");
        float Enemy_RotationZ = PlayerPrefs.GetFloat($"Enemy{index}_RotationZ");

        // Teleport to the saved position
        this.gameObject.transform.position = new Vector3(Enemy_PositionX, Enemy_PositionY, Enemy_PositionZ);
        this.gameObject.transform.rotation = Quaternion.Euler(Enemy_RotationX, Enemy_RotationY, Enemy_RotationZ);

        // [!] remember to look up 
        int animationHash = PlayerPrefs.GetInt($"Enemy{index}_CurrentAnimationStateLayer");
        float animationTime = PlayerPrefs.GetInt($"Enemy{index}_CurrentAnimationStateTime");
        _animator.Play(animationHash, 0, animationTime);
    }
}

