using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public enum PlayerState 
{ 
    Idle,
    Attack
}
public class RaycastLogic : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private GameObject ClickVisualization;
    [SerializeField] private float AttackRange = 2f;
    [SerializeField] private PlayerState CurrentState = PlayerState.Idle;
    [SerializeField] private GameObject Enemy;
    public static RaycastLogic Instance;
    [SerializeField] public GameObject Able2HitEnemy;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            RayCastCameraToMouse();
        }
        AutoAttack();

        switch (CurrentState) 
        { 
            case PlayerState.Idle:
                _navMeshAgent.isStopped = false;
                break;
            case PlayerState.Attack:
                if (Enemy != null)
                {
                    float Distance = Vector3.Distance(Enemy.transform.position, this.gameObject.transform.position);
                    
                    _navMeshAgent.SetDestination(Enemy.transform.position);

                    if (Distance < 3)
                    {
                        _navMeshAgent.isStopped = true;
                        _navMeshAgent.velocity = Vector3.zero;
                    }
                    else if (Distance > 4) 
                    { 
                        _navMeshAgent.isStopped = false;
                    }
                }
                else if (Enemy == null) 
                { 
                    CurrentState = PlayerState.Idle;
                }
                break;
            default:
                CurrentState = PlayerState.Idle;
                break;
        }
        
    }

    void RayCastCameraToMouse() 
    {
        RaycastHit hitinfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitinfo, 500f)) 
        {
            /*
            print($"[+] Hit Object's Information: " +
                  $"\n[-] Location: {hitinfo.point}" +
                  $"\n[-] Name: {hitinfo.collider.gameObject.name}");
            */
            // Move the player to the position you right click
            _navMeshAgent.SetDestination(hitinfo.point);
            
            Instantiate(ClickVisualization, hitinfo.point, ClickVisualization.transform.rotation);

            if (hitinfo.collider.tag == "Enemy")
            {
                Enemy = hitinfo.collider.gameObject;
                CurrentState = PlayerState.Attack;
            }
            if (hitinfo.collider.tag != "Enemy" && CurrentState == PlayerState.Attack)
            {
                Enemy = null;
                _navMeshAgent.SetDestination(hitinfo.point);
            }
        }
    }

    void AutoAttack() 
    {
        Debug.DrawRay(this.gameObject.transform.position, this.gameObject.transform.forward * AttackRange, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, AttackRange))
        {
            if (hit.collider.tag == "Enemy")
            {
                SwordLogic.instance.IsAttacking = true;
                Able2HitEnemy = hit.collider.gameObject;
            }
        }
        else 
        {
            SwordLogic.instance.IsAttacking = false;
            Able2HitEnemy = null;
        }
    }
}
