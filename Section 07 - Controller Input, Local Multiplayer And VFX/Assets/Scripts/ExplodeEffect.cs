using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEffect : MonoBehaviour
{
    [SerializeField] private AudioClip ExplodeSound;
    private AudioSource _audioSource;
    private bool Played = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        _audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Played == false) 
        { 
            _audioSource.PlayOneShot(ExplodeSound);
            Played = true;
        }
        Destroy(this.gameObject, 1f);
    }
}
