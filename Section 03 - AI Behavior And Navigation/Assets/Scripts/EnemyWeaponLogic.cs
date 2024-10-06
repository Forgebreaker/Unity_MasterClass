using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponLogic : MonoBehaviour
{
    private int RotateSpeed = 1000;
    private int Damage = 1;
    private Collider DamageTrick;
    private float CoolDown = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        DamageTrick = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(new Vector3(0, RotateSpeed, 0) * Time.deltaTime);
        StartCoroutine(Damage2Player());
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

    IEnumerator Damage2Player() 
    {
        DamageTrick.enabled = false;
        yield return new WaitForSeconds(CoolDown);
        DamageTrick.enabled = true;
    }
}
