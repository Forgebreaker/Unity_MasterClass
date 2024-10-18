using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 15f;
    [SerializeField] private GameObject BulletEffect;

    private void OnEnable()
    {
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += this.gameObject.transform.up * MoveSpeed * Time.deltaTime;
        Destroy(this.gameObject, 5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(BulletEffect, this.gameObject.transform.position, BulletEffect.transform.rotation);
        if (other.tag == "Player")
        {
            PlayerLogic Player = other.gameObject.GetComponent<PlayerLogic>();
            if (Player) 
            { 
                Player.Die();
            }
        }
        Destroy(this.gameObject);
    }
}
