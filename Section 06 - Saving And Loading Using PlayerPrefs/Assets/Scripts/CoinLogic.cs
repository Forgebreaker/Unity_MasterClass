using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoinState 
{ 
    Active,
    Taken
}
public class CoinLogic : MonoBehaviour
{
    [SerializeField] private AudioClip coinClip;
    private AudioSource audioSource;
    private Collider collider;
    private MeshRenderer[] childapperance;
    private MeshRenderer apperance;

    private CoinState CurrentState = CoinState.Active;

    private void Awake()
    {
        childapperance = GetComponentsInChildren<MeshRenderer>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
        apperance = this.gameObject.GetComponent<MeshRenderer>();
        collider = this.gameObject.GetComponent<Collider>();
    }

    void Update()
    {
        if (CurrentState != CoinState.Taken)
        {
            apperance.enabled = true;
            collider.enabled = true;
            for (int counter = 0; counter < childapperance.Length; counter++)
            {
                childapperance[counter].enabled = true;
            }
        }
        else if (CurrentState == CoinState.Taken)
        {
            apperance.enabled = false;
            collider.enabled = false;
            for (int counter = 0; counter < childapperance.Length; counter++)
            {
                childapperance[counter].enabled = false;
            }
        }
    }

    private void FixedUpdate()
    {
        gameObject.transform.Rotate(1, 0, 0); // Rotate base on local space
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            CurrentState = CoinState.Taken;
            audioSource.PlayOneShot(coinClip);    
        }
    }

    public void Save(int index) 
    {
        PlayerPrefs.SetInt($"CurrentCoinState{index}", (int)CurrentState);
    }

    public void Load(int index) 
    {
        CurrentState = (CoinState)PlayerPrefs.GetInt($"CurrentCoinState{index}");
        // this trick prevent overide state since you can't use the state of a current coin for all of them
    }
}
