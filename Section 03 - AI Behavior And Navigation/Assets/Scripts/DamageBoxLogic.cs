using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoxLogic : MonoBehaviour
{
    [SerializeField] private int Damage = 100;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerLogic playerhealth = other.GetComponent<PlayerLogic>();
            if (playerhealth != null)
            {
                playerhealth.TakeDamage(Damage);
            }
        }
    }
}
