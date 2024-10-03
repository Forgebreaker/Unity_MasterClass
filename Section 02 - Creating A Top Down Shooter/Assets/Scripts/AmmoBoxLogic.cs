using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GunLogic playergun = other.GetComponentInChildren<GunLogic>();
            if (playergun != null) 
            { 
                playergun.Reload();
                Destroy(this.gameObject);
            }
        }
    }
}
