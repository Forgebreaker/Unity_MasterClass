using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [SerializeField] private float BulletSpeed = 20f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.tag != "Gun")
        {
            Destroy(this.gameObject);
        }
    }
    private void FixedUpdate()
    {
        this.gameObject.transform.Translate(Vector3.up * BulletSpeed * Time.deltaTime, Space.Self);
    }
}
