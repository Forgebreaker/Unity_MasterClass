using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLogic : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 10f;
    [SerializeField] private GameObject TakeDamageEffect;
    private GameObject TakeDamageEffectTarget;
    void Start()
    {
        
    }

    void Update()
    {
        this.gameObject.transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        Destroy(gameObject, 5);
        if (TakeDamageEffectTarget != null) 
        {
            Destroy(TakeDamageEffectTarget, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") 
        { 
            MonsterLogic Monster = other.GetComponent<MonsterLogic>();
            if (Monster) 
            {
                Monster.TakeDamage(20);
                TakeDamageEffectTarget = Instantiate(TakeDamageEffect, Monster.transform.position, TakeDamageEffect.transform.rotation);
            }
        }
    }
}
