using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{
    [SerializeField] private AudioClip coinClip;
    private AudioSource audioSource;
    private Collider collider;
    private MeshRenderer[] childapperance;
    private MeshRenderer apperance;

    private void Awake()
    {
        childapperance = GetComponentsInChildren<MeshRenderer>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
        apperance = this.gameObject.GetComponent<MeshRenderer>();
        collider = this.gameObject.GetComponent<Collider>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        gameObject.transform.Rotate(1, 0, 0); // Rotate base on local space
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            apperance.enabled = false;
            collider.enabled = false;
            for (int counter = 0; counter < childapperance.Length; counter++) 
            { 
                childapperance[counter].enabled = false;
            }
            audioSource.PlayOneShot(coinClip);
            Destroy(this.gameObject, 2f);
        }
    }
}
