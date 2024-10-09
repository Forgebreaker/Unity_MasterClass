using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordLogic : MonoBehaviour
{
    [SerializeField] private GameObject Skill;
    private GameObject Player;
    private Animator _animator;
    [SerializeField] private AudioClip Hasagi;
    [SerializeField] private AudioClip Swordslash;
    private AudioSource _audioSource;
    private bool AutoAttack;
    public bool IsAttacking 
    { 
        get { return AutoAttack; }
        set { AutoAttack = value; }
    }
    public static SwordLogic instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        _animator = this.gameObject.GetComponent<Animator>();
        _audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (AutoAttack != true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _animator.SetTrigger("Attack");
                
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                _animator.SetTrigger("Skill");
            }
        }

        _animator.SetBool("Auto Attack", AutoAttack);
    }

    public void WeaponHit() 
    {
        _audioSource.PlayOneShot(Hasagi);
        _audioSource.PlayOneShot(Swordslash);
        Instantiate(Skill, Player.transform.position + Player.transform.forward * 3, Player.transform.rotation);

        if (RaycastLogic.Instance.Able2HitEnemy != null) 
        {
            MonsterLogic Monster = RaycastLogic.Instance.Able2HitEnemy.GetComponent<MonsterLogic>();
            Monster.TakeDamage(10);

        }
    }

    public void NormalAttack() 
    {
        if (RaycastLogic.Instance.Able2HitEnemy != null)
        {
            MonsterLogic Monster = RaycastLogic.Instance.Able2HitEnemy.GetComponent<MonsterLogic>();
            if (Monster)
            {
                Monster.TakeDamage(10);
            }
        }
        _audioSource.PlayOneShot(Swordslash);
    }
}
