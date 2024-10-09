using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickVisualization : MonoBehaviour
{
    [SerializeField] private float ChangeSizeSpeed = 1;
    void Start()
    {
        
    }

    void Update()
    {
        ChangeSizeSpeed -= Time.deltaTime;
        this.gameObject.transform.localScale = Vector3.one * ChangeSizeSpeed;
        Destroy(gameObject, 1f);
    }
}
