using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereLogic : MonoBehaviour
{
    private Rigidbody ObjectRB;
    void Start()
    {
        ObjectRB = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            ObjectRB.AddForce(Vector3.up * 300, ForceMode.Force);
        }
    }
}
