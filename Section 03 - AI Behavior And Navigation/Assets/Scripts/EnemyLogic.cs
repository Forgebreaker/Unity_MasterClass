using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

enum EnemyState 
{ 
    Idle,
    Patrol,
    ChaseAndAttack
}
public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private EnemyState CurrentEnemyState = EnemyState.Idle;

    private NavMeshAgent EnemyMovementAI;
    [SerializeField] private GameObject Player;
    private float SuitableDistance = 1f;

    [SerializeField] private GameObject PatrolPoint_Start;
    [SerializeField] private GameObject PatrolPoint_End;
    [SerializeField] private GameObject CurrentPatrolPoint;

    [SerializeField] private float HuntingRadius;
    [SerializeField] private GameObject DieEffect;
    [SerializeField] private int CurrentHealth = 100;

    private int EnemyHealth;

    void Awake()
    {
        EnemyMovementAI = this.gameObject.GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
    }
    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        HealthSystem();

        switch (CurrentEnemyState)
        { 
            case EnemyState.Idle:
                SearchForPlayer();
                break;
            case EnemyState.Patrol:
                if (PatrolPoint_Start && PatrolPoint_End)
                {
                    PatrolSystem();
                }
                SearchForPlayer();
                break;
            case EnemyState.ChaseAndAttack:
                if (Player != null)
                {
                    ChaseAndAttackSystem();
                }
                else if (Player == null) 
                {
                    EnemyMovementAI.isStopped = false;
                    CurrentEnemyState = EnemyState.Patrol;
                }
                break;
        }
    }

    public void TakeDamage(int Damage) 
    {
        CurrentHealth -= Damage;
        if (CurrentEnemyState != EnemyState.ChaseAndAttack) 
        {
            CurrentEnemyState = EnemyState.ChaseAndAttack;
        }
    }

    void ChaseAndAttackSystem()
    {
        if (Player && EnemyMovementAI)
        {
            EnemyMovementAI.SetDestination(Player.transform.position);
        }

        float Distance = Vector3.Distance(this.gameObject.transform.position, Player.transform.position);

        if (Distance < SuitableDistance && EnemyMovementAI)
        {
            EnemyMovementAI.isStopped = true;
            EnemyMovementAI.velocity = Vector3.zero;
        }

        else if (Distance > SuitableDistance + 1 && EnemyMovementAI)
        {
            EnemyMovementAI.isStopped = false;
        }
    }
    void PatrolSystem()
    {

        if (EnemyMovementAI && CurrentPatrolPoint == null)
        {
            CurrentPatrolPoint = PatrolPoint_Start;
        }

        float Distance = Vector3.Distance(this.gameObject.transform.position, CurrentPatrolPoint.transform.position);

        if (Distance < SuitableDistance)
        {
            if (CurrentPatrolPoint == PatrolPoint_Start)
            {
                CurrentPatrolPoint = PatrolPoint_End;
            }
            else if (CurrentPatrolPoint == PatrolPoint_End)
            {
                CurrentPatrolPoint = PatrolPoint_Start;
            }
        }

        EnemyMovementAI.SetDestination(CurrentPatrolPoint.transform.position);
    }
    void SearchForPlayer() 
    {
        if (Player)
        {
            float Distance = Vector3.Distance(this.gameObject.transform.position, Player.transform.position);
            
            if (EnemyMovementAI && Player && Distance < HuntingRadius)
            {
                CurrentEnemyState = EnemyState.ChaseAndAttack;
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.gameObject.transform.position, HuntingRadius);
    }

    private void HealthSystem()
    {
        if (CurrentHealth <= 0)
        {
            CurrentEnemyState = EnemyState.Idle;
            Instantiate(DieEffect, this.gameObject.transform.position, this.gameObject.transform.rotation);
            Destroy(this.gameObject);
        }
    }

    
}