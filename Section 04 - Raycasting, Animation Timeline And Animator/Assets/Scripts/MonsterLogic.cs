using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterLogic : MonoBehaviour
{
    [SerializeField] private GameObject PatrolPoint_Start;
    [SerializeField] private GameObject PatrolPoint_End;
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private GameObject CurrentPatrolPoint;
    [SerializeField] private float CurrentHealth = 100f;
    private Animator _animator;
    [SerializeField] private GameObject DieEffect;
    private GameObject FollowDieEffect;
    void Start()
    {
        _navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        _animator = this.gameObject.GetComponent<Animator>();
    }

    public void TakeDamage(int Damage) 
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth -= Damage;
        }
    }

    private void HealthSystem() 
    {
        if (CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
        
    }

    void Update()
    {
        if (CurrentHealth < 0) 
        {
            return;
        }

        HealthSystem();

        if (CurrentPatrolPoint == null) 
        {
            CurrentPatrolPoint = PatrolPoint_Start;
        }

        float Distance = Vector3.Distance(this.gameObject.transform.position, CurrentPatrolPoint.transform.position);

        if (CurrentHealth > 0)
        {
            _navMeshAgent.SetDestination(CurrentPatrolPoint.transform.position);
        }

        if (PatrolPoint_Start && PatrolPoint_End) 
        {
            if (CurrentPatrolPoint == PatrolPoint_Start && Distance < 1.5f) 
            {
                CurrentPatrolPoint = PatrolPoint_End;
            }
            else if (CurrentPatrolPoint == PatrolPoint_End && Distance < 1.5f) 
            {
                CurrentPatrolPoint = PatrolPoint_Start;
            }
        }
    }
}
