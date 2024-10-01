using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoxLogic : MonoBehaviour
{
    [SerializeField] private bool m_OnTriggerEnter;
    [SerializeField] private bool m_OnTriggerExit;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_OnTriggerEnter)
        {
            Destroy(other.gameObject);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_OnTriggerExit)
        { 
            Destroy(other.gameObject);
        }
    }
}
